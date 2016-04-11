// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// The root object that is returned by the LegacyOrder API.
    /// </summary>
    /// <typeparam name="TOrder">The type of the order.</typeparam>
    internal class LegacyOrderResponse<TOrder>
        where TOrder : LegacyBaseOrder
    {
        /// <summary>
        /// Gets or sets the number of orders returned in the 'orders' field.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the number of orders satisfying the conditions specified in the request URL.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the URL of the API request for the next page or null if there if not next page.
        /// </summary>
        /// <value>
        /// The next URL.
        /// </value>
        [JsonProperty("nextUrl")]
        public string NextUrl { get; set; }

        /// <summary>
        /// Gets or sets the no more then limit orders starting from the given offset ordered by time descending. See
        /// <see cref="LegacyOrder" />.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        [JsonProperty("orders")]
        public IList<TOrder> Orders { get; set; }
    }
}