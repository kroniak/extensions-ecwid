// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// Represent discount's coupon information. 
    /// </summary>
    public class DiscountCouponInfo
    {
        /// <summary>
        /// Gets or sets the coupon title in store control panel.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the type of the discount type: ABS, PERCENT, SHIPPING, ABS_AND_SHIPPING, PERCENT_AND_SHIPPING.
        /// </summary>
        /// <value>
        /// The type of the discount: ABS, PERCENT, SHIPPING, ABS_AND_SHIPPING, PERCENT_AND_SHIPPING
        /// </value>
        [JsonProperty("discountType")]
        public string DiscountType { get; set; }

        /// <summary>
        /// Gets or sets the discount coupon state: ACTIVE, PAUSED, EXPIRED or USEDUP.
        /// </summary>
        /// <value>
        /// The status: ACTIVE, PAUSED, EXPIRED or USEDUP
        /// </value>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        [JsonProperty("discount")]
        public double Discount { get; set; }

        /// <summary>
        /// Gets or sets the date of coupon launch, e.g. 2014-06-06 08:00:00 +0000.
        /// </summary>
        /// <value>
        /// The launch date: 2014-06-06 08:00:00 +0000.
        /// </value>
        [JsonProperty("launchDate")]
        public string LaunchDate { get; set; }

        /// <summary>
        /// Gets or sets the date of coupon expiration, e.g. 2014-06-06 08:00:00 +0000.
        /// </summary>
        /// <value>
        /// The launch date: 2014-06-06 08:00:00 +0000.
        /// </value>
        [JsonProperty("expirationDate")]
        public string ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the uses limitation: UNLIMITED, ONCEPERCUSTOMER, SINGLE.
        /// </summary>
        /// <value>
        /// The uses limit: UNLIMITED, ONCEPERCUSTOMER, SINGLE
        /// </value>
        [JsonProperty("usesLimit")]
        public string UsesLimit { get; set; }

        /// <summary>
        /// Gets or sets the minimum order subtotal the coupon applies to.
        /// </summary>
        /// <value>
        /// The uses limit.
        /// </value>
        [JsonProperty("totalLimit")]
        public double TotalLimit { get; set; }

        /// <summary>
        /// Gets or sets the coupon usage limitation flag identifying whether the coupon works for all customers or only repeat
        /// customers.
        /// </summary>
        /// <value>
        /// <c>true</c> if [repeat customer only]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("repeatCustomerOnly")]
        public bool RepeatCustomerOnly { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        [JsonProperty("creationDate")]
        public string CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the number of uses.
        /// </summary>
        /// <value>
        /// The order count.
        /// </value>
        [JsonProperty("orderCount")]
        public int OrderCount { get; set; }

        /// <summary>
        /// Gets or sets the products and categories the coupon can be applied to.
        /// </summary>
        /// <value>
        /// The catalog limit.
        /// </value>
        [JsonProperty("catalogLimit")]
        public DiscountCouponCatalogLimit CatalogLimit { get; set; }
    }
}