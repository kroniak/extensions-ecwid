using Newtonsoft.Json;
// ReSharper disable ClassNeverInstantiated.Global

namespace Ecwid.Models.Legacy
{
    public class LegacySettings
    {
        /// <summary>
        /// Gets or sets the discount granted to the customer based on the volume ordered either in percents or in currency, based on the <seealso cref="DiscountType"/>.
        /// <value>
        /// The value.
        /// </value>
        /// </summary>
        [JsonProperty("value")]
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the specifies the type of discount 'ABS' or 'PERCENT'.
        /// </summary>
        /// <value>
        /// The type of the discount: 'ABS' or 'PERCENT'.
        /// </value>
        [JsonProperty("discountType")]
        public string DiscountType { get; set; }

        /// <summary>
        /// Gets or sets the name of the coupon.
        /// </summary>
        /// <value>
        /// The name of the coupon.
        /// </value>
        [JsonProperty("couponName")]
        public string CouponName { get; set; }
    }
}