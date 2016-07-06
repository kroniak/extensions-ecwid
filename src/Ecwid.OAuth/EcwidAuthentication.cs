// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using Microsoft.AspNetCore.Builder;

namespace Ecwid.OAuth
{
    /// <summary>
    /// This is extension class to add Ecwid OAuth2 to ASP.NET Core pipeline
    /// </summary>
    public static class EcwidAuthentication
    {
        /// <summary>
        /// Adds the Ecwid <see cref="T:Microsoft.AspNet.Authentication.OAuth.OAuthMiddleware`1" /> middleware to the specified
        /// <see cref="T:Microsoft.AspNet.Builder.IApplicationBuilder" />, which enables OAuth 2.0 authentication capabilities.
        /// </summary>
        /// <param name="app">The <see cref="T:Microsoft.AspNet.Builder.IApplicationBuilder" /> to add the middleware to.</param>
        /// <param name="options">
        /// A <see cref="T:EcwidOptions" /> that specifies options for
        /// the middleware.
        /// </param>
        /// <returns>
        /// A reference to this instance after the operation has completed.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="app" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="options" /> is <see langword="null" />.</exception>
        public static IApplicationBuilder UseEcwidAuthentication(this IApplicationBuilder app, EcwidOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return app.UseOAuthAuthentication(options);
        }
    }
}