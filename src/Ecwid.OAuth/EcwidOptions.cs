// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Ecwid.OAuth
{
    /// <summary>
    /// Configuration Ecwid options for <see cref="!:OAuthMiddleware" />. Based on
    /// <seealso cref="OAuthOptions" />.
    /// This class sets default option to OAuth2.
    /// </summary>
    public class EcwidOptions : OAuthOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidOptions" /> class with <see cref="EcwidDefaults" /> options.
        /// </summary>
        public EcwidOptions()
        {
            AuthenticationScheme = EcwidDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            CallbackPath = new PathString("/signin-ecwid");
            AuthorizationEndpoint = EcwidDefaults.AuthorizationEndpoint;
            TokenEndpoint = EcwidDefaults.TokenEndpoint;
            UserInformationEndpoint = EcwidDefaults.UserInformationEndpoint;
            ClaimsIssuer = "OAuth2-Ecwid";

            SetEvents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidOptions" /> class with <see cref="EcwidDefaults" /> options.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <exception cref="ArgumentException"><paramref name="clientId" /> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="clientSecret" /> is null or empty.</exception>
        public EcwidOptions(string clientId, string clientSecret) : this()
        {
            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentException("Argument is null or empty", nameof(clientId));

            if (string.IsNullOrEmpty(clientSecret))
                throw new ArgumentException("Argument is null or empty", nameof(clientSecret));

            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidOptions" /> class with <see cref="EcwidDefaults" /> options.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="scopes">A list of permissions to request.</param>
        /// <exception cref="ArgumentException"><paramref name="clientId" /> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="clientSecret" /> is null or empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="scopes" /> is null.</exception>
        public EcwidOptions(string clientId, string clientSecret, IEnumerable<string> scopes) : this()
        {
            if (scopes == null) throw new ArgumentNullException(nameof(scopes));

            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentException("Argument is null or empty", nameof(clientId));

            if (string.IsNullOrEmpty(clientSecret))
                throw new ArgumentException("Argument is null or empty", nameof(clientSecret));

            ClientId = clientId;
            ClientSecret = clientSecret;

            foreach (var scope in scopes)
                // ReSharper disable once ExceptionNotDocumentedOptional
                Scope.Add(scope);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [requests email from profile and adds email to claim].
        /// </summary>
        /// <value>
        /// <c>true</c> if [requests email from profile and adds email to claim]; otherwise, <c>false</c>.
        /// </value>
        public bool AddEmailToClaim { get; set; } = true;

        /// <summary>
        /// Generates the request URL.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="storeId">The store identifier.</param>
        /// <exception cref="ArgumentException"><paramref name="storeId" /> is null or empty</exception>
        /// <exception cref="ArgumentNullException"><paramref name="context" /> is null or empty</exception>
        private string GenerateRequestUrl(OAuthCreatingTicketContext context, string storeId)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(storeId))
                throw new ArgumentException("Argument is null or empty", nameof(storeId));

            var requestUrl = context.Options.UserInformationEndpoint;

            if (!requestUrl.EndsWith("/"))
                requestUrl = $"{requestUrl}/";

            requestUrl = $"{requestUrl}{storeId}/";
            requestUrl = $"{requestUrl}profile?";
            requestUrl = $"{requestUrl}token={context.AccessToken}";

            return requestUrl;
        }

        /// <summary>
        /// Requests the account email.
        /// </summary>
        /// <param name="requestUrl">The request URL.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentException"><paramref name="requestUrl" /> is null or empty</exception>
        /// <exception cref="ArgumentNullException"><paramref name="context" /> is null or empty</exception>
        private static async Task<string> RequestAccountEmail(string requestUrl, OAuthCreatingTicketContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(requestUrl))
                throw new ArgumentException("Argument is null or empty", nameof(requestUrl));

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
            response.EnsureSuccessStatusCode();

            var profile = JObject.Parse(await response.Content.ReadAsStringAsync());
            var account = profile.Value<JObject>("account");

            return account.Value<string>("accountEmail");
        }

        /// <summary>
        /// Sets the events.
        /// </summary>
        private void SetEvents()
        {
            Events = new OAuthEvents
            {
                OnCreatingTicket = async context =>
                {
                    // return store_id from response
                    var storeId = context.TokenResponse.Response.Value<string>("store_id");

                    if (AddEmailToClaim)
                    {
                        // generate link to Ecwid profile
                        var requestUrl = GenerateRequestUrl(context, storeId);

                        // request account email
                        var accountEmail = await RequestAccountEmail(requestUrl, context);

                        // save email to the claim
                        if (!string.IsNullOrEmpty(accountEmail))
                            context.Identity.AddClaim(new Claim(
                                ClaimTypes.Email, accountEmail,
                                ClaimValueTypes.String, context.Options.ClaimsIssuer));
                    }

                    // save token to the claim
                    if (!string.IsNullOrEmpty(context.AccessToken))
                        context.Identity.AddClaim(new Claim(
                            EcwidClaimTypes.Token, context.AccessToken,
                            ClaimValueTypes.String, context.Options.ClaimsIssuer));

                    // save store-id to the claim
                    if (!string.IsNullOrEmpty(storeId))
                        context.Identity.AddClaim(new Claim(
                            ClaimTypes.NameIdentifier, storeId,
                            ClaimValueTypes.String, context.Options.ClaimsIssuer));

                    var scope = context.TokenResponse.Response.Value<string>("scope");

                    // save scope to the claim
                    if (!string.IsNullOrEmpty(scope))
                        context.Identity.AddClaim(new Claim(
                            EcwidClaimTypes.Scope, scope,
                            ClaimValueTypes.String, context.Options.ClaimsIssuer));
                }
            };
        }
    }
}