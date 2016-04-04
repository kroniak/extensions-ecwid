using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    public class LegacyFavorites
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the displayed count.
        /// </summary>
        /// <value>
        /// The displayed count.
        /// </value>
        [JsonProperty("displayedCount")]
        public string DisplayedCount { get; set; }
    }
}