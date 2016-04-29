// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

namespace Ecwid
{
    /// <summary>
    /// Setting for Ecwid Client.
    /// </summary>
    public class EcwidSettings
    {
        // TODO Validation in settings
        /// <summary>
        /// Gets or sets the API URL. Default is https://app.ecwid.com/api/v3/.
        /// </summary>
        /// <value>
        /// The API URL.
        /// </value>
        public virtual string ApiUrl { get; set; } = "https://app.ecwid.com/api/v3/";
    }
}