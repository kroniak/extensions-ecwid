// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// Legacy product combination.
    /// </summary>
    public class LegacyProductCombination : BaseEntity
    {
        /// <summary>
        /// If present, combination SKU, unique code.
        /// </summary>
        /// <value>
        /// The sku.
        /// </value>
        [JsonProperty("sku")]
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets the amount of the combination in stock. Absent for unlimited combinations.
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
        /// If present, specifies the combination weight, in the store units. Absent for intangible combinations.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        [JsonProperty("weight")]
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the set of options which identifies this combination.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        [JsonProperty("options")]
        public Dictionary<string, LegacyProductOptionChoice> Options { get; set; }

        /// <summary>
        /// If present, URL of a small combination thumbnail (usually 80x80).
        /// </summary>
        /// <value>
        /// The small thumbnail URL.
        /// </value>
        [JsonProperty("smallThumbnailUrl")]
        public string SmallThumbnailUrl { get; set; }

        /// <summary>
        /// If present, URL of a combination thumbnail (usually 160x160).
        /// </summary>
        /// <value>
        /// The thumbnail URL.
        /// </value>
        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// If present, URL of a combination image, usually no more than 500x500.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the original image URL.
        /// </summary>
        /// <value>
        /// The original image URL.
        /// </value>
        [JsonProperty("originalImageUrl")]
        public string OriginalImageUrl { get; set; }
    }
}