using Newtonsoft.Json;

namespace Ecwid.Models
{
    public class DiscountInfo
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets discount type: ABS or PERCENT.
        /// </summary>
        /// <value>
        /// The type: ABS or PERCENT.
        /// </value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the discount base, one of ON_TOTAL, ON_MEMBERSHIP, ON_TOTAL_AND_MEMBERSHIP.
        /// </summary>
        /// <value>
        /// The base: ON_TOTAL, ON_MEMBERSHIP, ON_TOTAL_AND_MEMBERSHIP.
        /// </value>
        [JsonProperty("base")]
        public string Base { get; set; }

        /// <summary>
        /// Gets or sets the minimum order subtotal the discount applies to.
        /// </summary>
        /// <value>
        /// The order total.
        /// </value>
        [JsonProperty("orderTotal")]
        public int OrderTotal { get; set; }
    }
}