using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Ecwid.Models
{
    /// <summary>
    /// This object represents order item
    /// </summary>
    public class LegacyOrderItem
    {
        /// <summary>
        /// Gets or sets the sku
        /// </summary>
        /// <value>
        /// The sku
        /// </value>
        [JsonProperty("sku")]
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        /// <value>
        /// The name
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Count of purchased products
        /// </summary>
        /// <value>
        /// The quantity
        /// </value>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price
        /// </summary>
        /// <value>
        /// The price
        /// </value>
        [JsonProperty("price")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the weight
        /// </summary>
        /// <value>
        /// The weight
        /// </value>
        [JsonProperty("weight")]
        public double Weight { get; set; }

        /// <summary>
        /// LegacyProduct internal ID. You can use it with our LegacyProduct API
        /// </summary>
        /// <value>
        /// The product identifier
        /// </value>
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        /// <summary>
        /// Files, attached to this order item
        /// </summary>
        /// <value>
        /// The options
        /// </value>
        [JsonProperty("options")]
        public IList<LegacyOrderItemOption> Options { get; set; }

        /// <summary>
        /// Default category ID for this product (0, if no default category has been set for the product)
        /// </summary>
        /// <value>
        /// The category identifier
        /// </value>
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        /// <summary>
        /// List of taxes, applied to this order item
        /// </summary>
        /// <value>
        /// The taxes
        /// </value>
        [JsonProperty("taxes")]
        public IList<LegacyOrderItemTax> Taxes { get; set; }

        /// <summary>
        /// Egoods
        /// </summary>
        /// <value>
        /// The files
        /// </value>
        [JsonProperty("files")]
        public IList<LegacyOrderItemProductFile> Files { get; set; }
    }

    /// <summary>
    /// This object represents options of purchased product
    /// </summary>
    public class LegacyOrderItemOption
    {
        /// <summary>
        /// Option name
        /// </summary>
        /// <value>
        /// The name
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Option type. Can contain one of these values: SELECT, TEXT, DATE, FILE
        /// </summary>
        /// <value>
        /// The type
        /// </value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Contain text value for SELECT, DATE and TEXT option types. Contain List of <seealso cref="LegacyOrderItemOrderFile"/> in case option type is FILE
        /// </summary>
        /// <value>
        /// The value
        /// </value>
        [JsonProperty("value")]
        public object Value { get; set; }
    }

    /// <summary>
    /// This object represents tax of purchased product
    /// </summary>
    public class LegacyOrderItemTax
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        /// <value>
        /// The name
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Tax value in percents. May depends on shipping or billing person
        /// </summary>
        /// <value>
        /// The value
        /// </value>
        [JsonProperty("value")]
        public double Value { get; set; }

        /// <summary>
        /// Tax cost for order item. May depends on shipping or billing person and tax settings
        /// </summary>
        /// <value>
        /// The total
        /// </value>
        [JsonProperty("total")]
        public double Total { get; set; }
    }

    /// <summary>
    /// This object represents file attached by customer to purchased product on checkout
    /// </summary>
    public class LegacyOrderItemOrderFile
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        /// <value>
        /// The name
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL
        /// </summary>
        /// <value>
        /// The URL
        /// </value>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        /// <value>
        /// The size
        /// </value>
        [JsonProperty("size")]
        public int Size { get; set; }
    }

    /// <summary>
    /// This object represents purchased egood
    /// </summary>
    /// <seealso cref="LegacyOrderItemOrderFile" />
    public class LegacyOrderItemProductFile : LegacyOrderItemOrderFile
    {
        /// <summary>
        /// Gets or sets the description
        /// </summary>
        /// <value>
        /// The description
        /// </value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Max number of allowed downloads
        /// </summary>
        /// <value>
        /// The maximum downloads
        /// </value>
        [JsonProperty("maxDownloads")]
        public int MaxDownloads { get; set; }

        /// <summary>
        /// Remaining number of allowed downloads
        /// </summary>
        /// <value>
        /// The remaining downloads
        /// </value>
        [JsonProperty("remainingDownloads")]
        public int RemainingDownloads { get; set; }

        /// <summary>
        /// File link expiration date (EST timezone) 
        /// </summary>
        /// <value>
        /// The expires
        /// </value>
        [JsonProperty("expires")]
        public DateTime Expires { get; set; }
    }
}
