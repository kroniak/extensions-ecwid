using System.Collections.Generic;
using Newtonsoft.Json;
// ReSharper disable UnusedMember.Global

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// This object represents full information about the category and its products and subcategories.
    /// </summary>
    public class LegacyCategory : LegacyCategoryEntry
    {
        /// <summary>
        /// Gets or sets the a list of the child subcategories, without nested products and subcategories.
        /// </summary>
        /// <value>
        /// The subcategories.
        /// </value>
        [JsonProperty("subcategories")]
        public IList<LegacyCategoryEntry> Subcategories { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        [JsonProperty("products")]
        public IList<LegacyProductEntry> Products { get; set; }
    }
}