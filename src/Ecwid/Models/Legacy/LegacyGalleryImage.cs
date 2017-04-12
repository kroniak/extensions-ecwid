// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// The image class.
    /// </summary>
    public class LegacyGalleryImage
    {
        /// <summary>
        /// Gets or sets the image description, displayed in 'alt' image attribute.
        /// </summary>
        /// <value>
        /// The alt.
        /// </value>
        [JsonProperty("alt")]
        public string Alt { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets an URL of the image thumbnail with dimensions 46x42.
        /// </summary>
        /// <value>
        /// The thumbnail URL.
        /// </value>
        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
    }
}