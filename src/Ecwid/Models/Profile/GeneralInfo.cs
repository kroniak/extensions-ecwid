// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// General shop info.
    /// </summary>
    public class GeneralInfo
    {
        /// <summary>
        /// Gets or sets the starter site.
        /// </summary>
        /// <value>
        /// The starter site.
        /// </value>
        [JsonProperty("starterSite")]
        public StarterSite StarterSite { get; set; }

        /// <summary>
        /// Gets or sets the store identifier.
        /// </summary>
        /// <value>
        /// The store identifier.
        /// </value>
        [JsonProperty("storeId")]
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the store URL.
        /// </summary>
        /// <value>
        /// The store URL.
        /// </value>
        [JsonProperty("storeUrl")]
        public string StoreUrl { get; set; }
    }
}