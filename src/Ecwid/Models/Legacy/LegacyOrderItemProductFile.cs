// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <inheritdoc />
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