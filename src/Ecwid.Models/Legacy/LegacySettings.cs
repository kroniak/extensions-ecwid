// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// The shop's legacy settings.
    /// </summary>
    public class LegacySettings
    {
        /// <summary>
        /// Gets or sets the discount granted to the customer based on the volume ordered either in percents or in currency, based
        /// on the <seealso cref="DiscountType" />.
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