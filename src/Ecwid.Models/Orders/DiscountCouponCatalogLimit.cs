using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models
{
    public class DiscountCouponCatalogLimit
    {
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        [JsonProperty("products")]
        public IList<int> Products { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        [JsonProperty("categories")]
        public IList<int> Categories { get; set; }
    }
}