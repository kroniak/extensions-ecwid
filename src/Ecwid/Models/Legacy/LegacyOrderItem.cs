// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// This object represents order item.
    /// </summary>
    public class LegacyOrderItem
    {
        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        /// <value>
        /// The sku.
        /// </value>
        [JsonProperty("sku")]
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the count of purchased products.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty("price")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        [JsonProperty("weight")]
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the internal ID. You can use it with our LegacyProduct API.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the files, attached to this order item.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        [JsonProperty("options")]
        public List<LegacyOrderItemOption> Options { get; set; }

        /// <summary>
        /// Gets or sets the default category ID for this product (0, if no default category has been set for the product).
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets a list of the taxes, applied to this order item.
        /// </summary>
        /// <value>
        /// The taxes.
        /// </value>
        [JsonProperty("taxes")]
        public List<LegacyOrderItemTax> Taxes { get; set; }

        /// <summary>
        /// Gets or sets the Egoods.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        [JsonProperty("files")]
        public List<LegacyOrderItemProductFile> Files { get; set; }
    }
}