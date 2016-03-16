using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// This object represents full information on a product: all fields of the ProductEntry plus some additional fields
    /// </summary>
    public class Product : ProductEntry
    {
        /// <summary>
        /// If present, product wholesale prices in the form of Quantity, Price
        /// </summary>
        /// <value>
        /// The wholesale prices
        /// </value>
        [JsonProperty("wholesalePrices")]
        public Dictionary<int, double> WholesalePrices { get; set; }

        /// <summary>
        /// A list of the product options. Empty if no options are specified for the product
        /// </summary>
        /// <value>
        /// The options
        /// </value>
        [JsonProperty("options")]
        public IList<ProductOption> Options { get; set; }

        /// <summary>
        /// A list of included taxes
        /// </summary>
        /// <value>
        /// The taxes
        /// </value>
        [JsonProperty("taxes")]
        public IList<ProductTax> Taxes { get; set; }

        /// <summary>
        /// A list of gallery images
        /// </summary>
        /// <value>
        /// The gallery images
        /// </value>
        [JsonProperty("galleryImages")]
        public IList<GalleryImage> GalleryImages { get; set; }

        /// <summary>
        /// A list of categories which this product belongs to. 
        /// In addition to category properties described above, each object has an additional "defaultCategory" boolean property. 
        /// The property is true for the default category of the product.
        /// </summary>
        /// <value>
        /// The categories
        /// </value>
        [JsonProperty("categories")]
        public IList<CategoryEntry> Categories { get; set; }

        /// <summary>
        /// If present, unique ID for default product combination
        /// </summary>
        /// <value>
        /// The default combination identifier
        /// </value>
        [JsonProperty("defaultCombinationId")]
        public int DefaultCombinationId { get; set; }

        /// <summary>
        /// If present, list of product combinations
        /// </summary>
        /// <value>
        /// The combinations
        /// </value>
        [JsonProperty("combinations")]
        public IList<ProductCombination> Combinations { get; set; }

        /// <summary>
        /// Product creation date (UNIX timestamp). 
        /// Note: this field is obsolete and will be removed in a future version of API.Use "created" field instead.
        /// </summary>
        /// <value>
        /// The date added
        /// </value>
        [JsonProperty("dateAdded")]
        public int DateAdded { get; set; }

        /// <summary>
        /// Gets or sets the updated
        /// </summary>
        /// <value>
        /// The updated
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
        /// If present, contains product's attributes values (see the description of object Attribute below). 
        /// For non-authorized calls, hidden attributes are not returned. 
        /// Since Ecwid version 13.2, you can use Authentication as described at the start of this document to show hidden attributes.
        /// You can edit the attribute values on the 'Attributes' tab in the product editor.
        /// </summary>
        /// <value>
        /// The attributes
        /// </value>
        [JsonProperty("attributes")]
        public IList<Attribute> Attributes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [in stock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [in stock]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("inStock")]
        public bool InStock { get; set; }
    }

    public class ProductOption
    {
        /// <summary>
        /// Option name, like 'Color'
        /// </summary>
        /// <value>
        /// The name
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// One of 'SELECT', 'RADIO', 'TEXTFIELD', 'TEXTAREA', 'DATE', 'FILES', 'CHECKBOX'
        /// </summary>
        /// <value>
        /// The type
        /// </value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// All possible option choices, if the type is 'SELECT' or 'RADIO'. Absent otherwise
        /// </summary>
        /// <value>
        /// The choices
        /// </value>
        [JsonProperty("choices")]
        public IList<ProductOptionChoice> Choices { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ProductOption"/> is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if required; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("required")]
        public bool Required { get; set; }

        /// <summary>
        /// The number, starting from 0, of the default choice. Only present if the type is 'SELECT' or 'RADIO'
        /// </summary>
        /// <value>
        /// The default choice
        /// </value>
        [JsonProperty("defaultChoice")]
        public int? DefaultChoice { get; set; }
    }

    public class ProductOptionChoice
    {
        /// <summary>
        /// A text displayed as a choice in a drop-down or a radio box, e.g. 'Green'
        /// </summary>
        /// <value>
        /// The text
        /// </value>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Specifies the way the product price is modified. 
        /// One of 'PERCENT' or 'ABSOLUTE'. If 'PERCENT', then priceModifier is a number of percents to add to the price. 
        /// If 'ABSOLUTE', then priceModifier is a number of currency units to add to the price.
        /// </summary>
        /// <value>
        /// The type of the price modifier.
        /// </value>
        [JsonProperty("priceModifierType")]
        public string PriceModifierType { get; set; }

        /// <summary>
        /// Number of percents or currency units to add to the product price when this choice is selected. May be negative or zero
        /// </summary>
        /// <value>
        /// The price modifier
        /// </value>
        [JsonProperty("priceModifier")]
        public double PriceModifier { get; set; }

        /// <summary>
        /// SKU prefix to add to the product's SKU to obtain the full SKU of the option combination. This is optional
        /// </summary>
        /// <value>
        /// The sku
        /// </value>
        [JsonProperty("sku")]
        public string Sku { get; set; }
    }

    public class ProductTax
    {
        /// <summary>
        /// The tax display name, e.g. 'VAT'
        /// </summary>
        /// <value>
        /// The name
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The default tax value (the one calculated from "Other zones" rate), in store currency. Deprecated
        /// </summary>
        /// <value>
        /// The value
        /// </value>
        [JsonProperty("value")]
        public double Value { get; set; }
    }

    public class GalleryImage
    {
        /// <summary>
        /// The image description, displayed in 'alt' image attribute
        /// </summary>
        /// <value>
        /// The alt
        /// </value>
        [JsonProperty("alt")]
        public string Alt { get; set; }

        /// <summary>
        /// Gets or sets the URL
        /// </summary>
        /// <value>
        /// The URL
        /// </value>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the width
        /// </summary>
        /// <value>
        /// The width
        /// </value>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height
        /// </summary>
        /// <value>
        /// The height
        /// </value>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// An URL of the image thumbnail with dimensions 46x42
        /// </summary>
        /// <value>
        /// The thumbnail URL
        /// </value>
        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
    }

    public class Attribute : BaseEntity
    {
        /// <summary>
        /// Gets or sets the value
        /// </summary>
        /// <value>
        /// The value
        /// </value>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        /// <value>
        /// The name
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// For built-in attributes, contains their internal name like 'Brand' or 'UPC'
        /// </summary>
        /// <value>
        /// The name of the internal.
        /// </value>
        [JsonProperty("internalName")]
        public string InternalName { get; set; }
    }
}
