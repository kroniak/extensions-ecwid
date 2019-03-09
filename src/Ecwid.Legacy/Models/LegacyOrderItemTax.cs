// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Legacy.Models
{
    /// <summary>
    /// This object represents tax of purchased product.
    /// </summary>
    public class LegacyOrderItemTax
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tax value in percents. May depends on shipping or billing person.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the tax cost for order item. May depends on shipping or billing person and tax settings.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty("total")]
        public double Total { get; set; }
    }
}