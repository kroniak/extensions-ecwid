using Newtonsoft.Json;
// ReSharper disable UnusedMember.Global

namespace Ecwid.Models
{
    /// <summary>
    /// This object represents full information on a category
    /// </summary>
    public class LegacyCategoryEntry : BaseEntity
    {
        /// <summary>
        /// If present, an URL of a category thumbnail (usually 160x160).
        /// </summary>
        /// <value>
        /// The thumbnail URL
        /// </value>
        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets the original image URL
        /// </summary>
        /// <value>
        /// The original image URL
        /// </value>
        [JsonProperty("originalImageUrl")]
        public string OriginalImageUrl { get; set; }

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
        /// If present, a category description
        /// </summary>
        /// <value>
        /// The description
        /// </value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the product count
        /// </summary>
        /// <value>
        /// The product count
        /// </value>
        [JsonProperty("productCount")]
        public int ProductCount { get; set; }

        /// <summary>
        /// An ID of the parent category, if any. This key is absent for root categories
        /// </summary>
        /// <value>
        /// The parent identifier
        /// </value>
        [JsonProperty("parentId")]
        public int? ParentId { get; set; }
    }
}
