// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

namespace Ecwid.Services
{
    /// <summary>
    /// Base setting for Ecwid Client v3.
    /// </summary>
    public class EcwidOptions : BaseEcwidOptions
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }
    }
}