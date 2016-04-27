// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

namespace Ecwid.OAuth
{
    /// <summary>
    /// Default setting for Ecwid OAuth2 protocol.
    /// </summary>
    public static class EcwidDefaults
    {
        /// <summary>
        /// The URI where the client will be redirected to authenticate.
        /// </summary>
        public const string AuthorizationEndpoint = "https://my.ecwid.com/api/oauth/authorize";

        /// <summary>
        /// The URI the middleware will access to exchange the OAuth token.
        /// </summary>
        public const string TokenEndpoint = "https://my.ecwid.com/api/oauth/token";

        /// <summary>
        /// Gets or sets the URI the middleware will access to obtain the user information.
        /// This value is not used in the default implementation, it is for use in custom implementations of
        /// IOAuthAuthenticationEvents.Authenticated or OAuthAuthenticationHandler.CreateTicketAsync.
        /// </summary>
        public const string UserInformationEndpoint = "https://app.ecwid.com/api/v3/";

        /// <summary>
        /// The AuthenticationScheme in the options corresponds to the logical name for a particular authentication scheme. A
        /// different
        /// value may be assigned in order to use the same authentication middleware type more than once in a pipeline.
        /// </summary>
        public const string AuthenticationScheme = "Ecwid";
    }
}