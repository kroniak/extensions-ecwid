// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// Base order for <see cref="LegacyUpdatedOrder" /> and <see cref="LegacyOrder" />.
    /// </summary>
    public abstract class LegacyBaseOrder : BaseOrder
    {
        /// <summary>
        /// Gets or sets a unique order number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        [JsonProperty("number")]
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the admin-defined order numbers with prefix and suffix, e.g. 2011-345-q4.
        /// </summary>
        /// <value>
        /// The vendor number.
        /// </value>
        [JsonProperty("vendorNumber")]
        public string VendorNumber { get; set; }

        /// <summary>
        /// Gets or sets the payment status. Contains one of these values: ACCEPTED*, DECLINED, CANCELLED, QUEUED, CHARGEABLE,
        /// REFUNDED.
        /// </summary>
        /// <value>
        /// The payment status. Contains one of these values: ACCEPTED*, DECLINED, CANCELLED, QUEUED, CHARGEABLE, REFUNDED.
        /// </value>
        [JsonProperty("paymentStatus")]
        public string PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the fulfillment status. Contains one of these values: NEW, PROCESSING, SHIPPED, DELIVERED,
        /// WILL_NOT_DELIVER, RETURNED.
        /// </summary>
        /// <value>
        /// The fulfillment status. Contains one of these values: NEW, PROCESSING, SHIPPED, DELIVERED, WILL_NOT_DELIVER, RETURNED.
        /// </value>
        [JsonProperty("fulfillmentStatus")]
        public string FulfillmentStatus { get; set; }

        /// <summary>
        /// Gets or sets the shipping tracking code.
        /// </summary>
        /// <value>
        /// The shipping tracking code.
        /// </value>
        [JsonProperty("shippingTrackingCode")]
        public string ShippingTrackingCode { get; set; }
    }
}