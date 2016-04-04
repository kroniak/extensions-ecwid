using System;
using Newtonsoft.Json;
// ReSharper disable ClassNeverInstantiated.Global

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// This object represents purchased egood.
    /// </summary>
    /// <seealso cref="LegacyOrderItemOrderFile" />
    public class LegacyOrderItemProductFile : LegacyOrderItemOrderFile
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the max number of allowed downloads.
        /// </summary>
        /// <value>
        /// The maximum downloads.
        /// </value>
        [JsonProperty("maxDownloads")]
        public int MaxDownloads { get; set; }

        /// <summary>
        /// Gets or sets the remaining number of allowed downloads.
        /// </summary>
        /// <value>
        /// The remaining downloads.
        /// </value>
        [JsonProperty("remainingDownloads")]
        public int RemainingDownloads { get; set; }

        /// <summary>
        /// Gets or sets the file link expiration date (EST timezone).
        /// </summary>
        /// <value>
        /// The expires.
        /// </value>
        [JsonProperty("expires")]
        public DateTime Expires { get; set; }
    }
}