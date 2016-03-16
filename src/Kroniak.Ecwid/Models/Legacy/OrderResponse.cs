using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// The root object that is returned by the Order API
    /// </summary>
    internal class OrderResponse
    {
        /// <summary>
        /// The number of orders returned in the 'orders' field
        /// </summary>
        /// <value>
        /// The count
        /// </value>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// The number of orders satisfying the conditions specified in the request URL
        /// </summary>
        /// <value>
        /// The total
        /// </value>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// URL of the API request for the next page or null if there if not next page
        /// </summary>
        /// <value>
        /// The next URL
        /// </value>
        [JsonProperty("nextUrl")]
        public string NextUrl { get; set; }

        /// <summary>
        /// No more then limit orders starting from the given offset ordered by time descending. See <see cref="Order"/>
        /// </summary>
        /// <value>
        /// The orders
        /// </value>
        [JsonProperty("orders")]
        public IList<Order> Orders { get; set; }
    }
}
