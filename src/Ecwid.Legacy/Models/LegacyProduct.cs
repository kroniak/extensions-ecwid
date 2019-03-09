// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Legacy.Models
{
    /// <inheritdoc />
    public class LegacyProduct : LegacyProductEntry
    {
        /// <summary>
        /// If present, product wholesale prices in the form of Quantity, Price.
        /// </summary>
        /// <value>
        /// The wholesale prices.
        /// </value>
        [JsonProperty("wholesalePrices")]
        public Dictionary<int, double> WholesalePrices { get; set; }

        /// <summary>
        /// Gets or sets a list of the product options. Empty if no options are specified for the product.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        [JsonProperty("options")]
        public List<LegacyProductOption> Options { get; set; }

        /// <summary>
        /// Gets or sets a list of included taxes.
        /// </summary>
        /// <value>
        /// The taxes.
        /// </value>
        [JsonProperty("taxes")]
        public List<LegacyProductTax> Taxes { get; set; }

        /// <summary>
        /// Gets or sets a list of gallery images.
        /// </summary>
        /// <value>
        /// The gallery images.
        /// </value>
        [JsonProperty("galleryImages")]
        public List<LegacyGalleryImage> GalleryImages { get; set; }

        /// <summary>
        /// Gets or sets a list of categories which this product belongs to.
        /// In addition to category properties described above, each object has an additional "defaultCategory" boolean property.
        /// The property is true for the default category of the product.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        [JsonProperty("categories")]
        public List<LegacyCategoryEntry> Categories { get; set; }

        /// <summary>
        /// If present, unique ID for default product combination.
        /// </summary>
        /// <value>
        /// The default combination identifier.
        /// </value>
        [JsonProperty("defaultCombinationId")]
        public int DefaultCombinationId { get; set; }

        /// <summary>
        /// If present, list of product combinations.
        /// </summary>
        /// <value>
        /// The combinations.
        /// </value>
        [JsonProperty("combinations")]
        public List<LegacyProductCombination> Combinations { get; set; }

        /// <summary>
        /// Gets or sets the creation date (UNIX timestamp).
        /// Note: this field is obsolete and will be removed in a future version of API.Use "created" field instead.
        /// </summary>
        /// <value>
        /// The date added.
        /// </value>
        [JsonProperty("dateAdded")]
        public int DateAdded { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>
        /// The updated.
        /// </value>
        [JsonProperty("updated")]
        public string Updated { get; set; }

        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        /// <value>
        /// The type of the product.
        /// </value>
        [JsonProperty("productType")]
        public string ProductType { get; set; }

        /// <summary>
        /// If present, contains product's attributes values (see the description of object LegacyAttribute below).
        /// For non-authorized calls, hidden attributes are not returned.
        /// Since Ecwid version 13.2, you can use Authentication as described at the start of this document to show hidden
        /// attributes.
        /// You can edit the attribute values on the 'Attributes' tab in the product editor.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        [JsonProperty("attributes")]
        public List<LegacyAttribute> Attributes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [in stock].
        /// </summary>
        /// <value>
        /// <c>true</c> if [in stock]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("inStock")]
        public bool InStock { get; set; }
    }
}