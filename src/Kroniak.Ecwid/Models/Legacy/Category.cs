using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// This object represents full information about the category and its products and subcategories
    /// </summary>
    public class Category : CategoryEntry
    {
        /// <summary>
        /// A list of child subcategories, without nested products and subcategories
        /// </summary>
        /// <value>
        /// The subcategories
        /// </value>
        [JsonProperty("subcategories")]
        public IList<CategoryEntry> Subcategories { get; set; }

        /// <summary>
        /// Gets or sets the products
        /// </summary>
        /// <value>
        /// The products
        /// </value>
        [JsonProperty("products")]
        public IList<ProductEntry> Products { get; set; }
    }
}
