// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// This object represents tax of purchased product.
    /// </summary>
    public class OrderItemTax
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
        /// Gets or sets the tax value in percent.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the tax amount for the item.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty("total")]
        public double Total { get; set; }

        /// <summary>
        /// Gets or sets the tax on item subtotal (after applying discounts).
        /// </summary>
        /// <value>
        /// The tax on discounted subtotal.
        /// </value>
        [JsonProperty("taxOnDiscountedSubtotal")]
        public double TaxOnDiscountedSubtotal { get; set; }

        /// <summary>
        /// Gets or sets the tax on shipping.
        /// </summary>
        /// <value>
        /// The tax on shipping.
        /// </value>
        [JsonProperty("taxOnShipping")]
        public int TaxOnShipping { get; set; }
    }
}