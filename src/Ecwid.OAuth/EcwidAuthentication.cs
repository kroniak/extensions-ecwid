// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace Ecwid.OAuth
{
    /// <summary>
    /// This is extension class to add Ecwid OAuth2 to ASP.NET Core pipeline
    /// </summary>
    public static class EcwidAuthentication
    {
        /// <summary>
        /// Adds the Ecwid auth middleware to the specified
        /// <see cref="T:AuthenticationBuilder" />, which enables OAuth 2.0 authentication capabilities.
        /// </summary>
        /// <param name="builder">The <see cref="T:AuthenticationBuilder" /> to add the auth middleware to.</param>
        /// <param name="clientId">Your client id from Ecwid</param>
        /// <param name="clientSecret">Your client secret from Ecwid</param>
        /// <param name="addEmailToClaim">Add or not user email to Claims</param>
        /// <returns>
        /// A reference to <see cref="T:AuthenticationBuilder" /> instance after the operation has completed.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="clientId" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="clientSecret" /> is <see langword="null" />.</exception>
        public static AuthenticationBuilder AddEcwidAuthentication(this AuthenticationBuilder builder, string clientId,
            string clientSecret, bool addEmailToClaim = false)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException("Argument is null or empty", nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException("Argument is null or empty", nameof(clientSecret));


            return builder.AddOAuth(
                "Ecwid",
                authOptions => SetOAuth2Options(authOptions, clientId, clientSecret, addEmailToClaim));
        }

        private static void SetOAuth2Options(OAuthOptions options, string clientId, string clientSecret,
            bool addEmailToClaim)
        {
            options.CallbackPath = new PathString("/signin-ecwid");
            options.AuthorizationEndpoint = EcwidDefaults.AuthorizationEndpoint;
            options.TokenEndpoint = EcwidDefaults.TokenEndpoint;
            options.UserInformationEndpoint = EcwidDefaults.UserInformationEndpoint;
            options.ClaimsIssuer = "OAuth2-Ecwid";
            options.ClientId = clientId;
            options.ClientSecret = clientSecret;

            SetEvents(options, addEmailToClaim);
        }

        /// <summary>
        /// Sets the events.
        /// </summary>
        private static void SetEvents(OAuthOptions options, bool addEmailToClaim)
        {
            options.Events = new OAuthEvents
            {
                OnCreatingTicket = async context =>
                {
                    // return store_id from response
                    var storeId = context.TokenResponse.Response.Value<string>("store_id");

                    if (addEmailToClaim)
                    {
                        // generate link to Ecwid profile
                        var requestUrl = GenerateRequestUrl(context, storeId);

                        // request account email
                        var accountEmail = await RequestAccountEmail(requestUrl, context);

                        // save email to the claim
                        if (!string.IsNullOrWhiteSpace(accountEmail))
                            context.Identity.AddClaim(new Claim(
                                ClaimTypes.Email, accountEmail,
                                ClaimValueTypes.String, context.Options.ClaimsIssuer));
                    }

                    // save token to the claim
                    if (!string.IsNullOrWhiteSpace(context.AccessToken))
                        context.Identity.AddClaim(new Claim(
                            EcwidClaimTypes.Token, context.AccessToken,
                            ClaimValueTypes.String, context.Options.ClaimsIssuer));

                    // save store-id to the claim
                    if (!string.IsNullOrWhiteSpace(storeId))
                        context.Identity.AddClaim(new Claim(
                            ClaimTypes.NameIdentifier, storeId,
                            ClaimValueTypes.String, context.Options.ClaimsIssuer));

                    var scope = context.TokenResponse.Response.Value<string>("scope");

                    // save scope to the claim
                    if (!string.IsNullOrWhiteSpace(scope))
                        context.Identity.AddClaim(new Claim(
                            EcwidClaimTypes.Scope, scope,
                            ClaimValueTypes.String, context.Options.ClaimsIssuer));
                }
            };
        }

        /// <summary>
        /// Generates the request URL.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="storeId">The store identifier.</param>
        /// <exception cref="ArgumentException"><paramref name="storeId" /> is null or empty</exception>
        /// <exception cref="ArgumentNullException"><paramref name="context" /> is null or empty</exception>
        private static string GenerateRequestUrl(OAuthCreatingTicketContext context, string storeId)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(storeId))
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

            if (string.IsNullOrWhiteSpace(requestUrl))
                throw new ArgumentException("Argument is null or empty", nameof(requestUrl));

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
            response.EnsureSuccessStatusCode();

            var profile = JObject.Parse(await response.Content.ReadAsStringAsync());
            var account = profile.Value<JObject>("account");

            return account.Value<string>("accountEmail");
        }
    }
}