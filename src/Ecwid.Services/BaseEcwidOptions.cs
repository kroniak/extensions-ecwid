// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

namespace Ecwid.Services
{
    /// <summary>
    /// Base setting for Ecwid Client.
    /// </summary>
    public abstract class BaseEcwidOptions
    {
        /// <summary>
        /// Gets or sets the API URL. Default is https://app.ecwid.com/api/v3/
        /// </summary>
        /// <value>
        /// The API URL
        /// </value>
        public virtual string ApiUrl { get; set; } = "https://app.ecwid.com/api/v3/";

        /// <summary>
        /// Gets or sets the shop identifier
        /// </summary>
        /// <value>
        /// The shop identifier
        /// </value>
        public int? ShopId { get; set; }
    }
}