using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// The root object that is returned by the Discount Coupon search API.
    /// </summary>
    public class DiscountCouponSearchResults
    {
        /// <summary>
        /// Gets or sets the total number of found items (might be more than the number of returned items).
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the total number of the items returned in this batch.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the offset from the beginning of the returned items list (for paging).
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        [JsonProperty("offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of returned items. Maximum allowed value: 100. Default value: 100.
        /// </summary>
        /// <value>
        /// The limit.
        /// </value>
        [JsonProperty("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets the items list.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty("items")]
        public IList<DiscountCouponInfo> DiscountCoupons { get; set; }
    }
}