// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Legacy.Models
{
    /// <summary>
    /// LegacyOrder discounts
    /// </summary>
    public class LegacyOrderDiscount
    {
        /// <summary>
        /// Gets or sets the specifies the type of discount
        /// </summary>
        /// <value>
        /// The discount base
        /// </value>
        [JsonProperty("discountBase")]
        public string DiscountBase { get; set; }

        /// <summary>
        /// Gets or sets the discount in currency granted to the customer based on the volume ordered
        /// </summary>
        /// <value>
        /// The discount cost
        /// </value>
        [JsonProperty("discountCost")]
        public double DiscountCost { get; set; }

        /// <summary>
        /// Gets or sets the settings
        /// </summary>
        /// <value>
        /// The settings
        /// </value>
        [JsonProperty("settings")]
        public LegacySettings LegacySettings { get; set; }
    }
}