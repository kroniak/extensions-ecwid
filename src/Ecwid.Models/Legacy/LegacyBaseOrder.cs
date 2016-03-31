using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// Base order for UpdateOrders and <see cref="LegacyOrder"/>
    /// </summary>
    public class LegacyBaseOrder
    {
        /// <summary>
        /// A unique order number
        /// </summary>
        /// <value>
        /// The number
        /// </value>
        [JsonProperty("number")]
        public int Number { get; set; }

        /// <summary>
        /// Admin-defined order numbers with prefix and suffix, e.g. 2011-345-q4
        /// </summary>
        /// <value>
        /// The vendor number
        /// </value>
        [JsonProperty("vendorNumber")]
        public string VendorNumber { get; set; }

        /// <summary>
        /// Payment status. Contains one of these values: ACCEPTED*, DECLINED, CANCELLED, QUEUED, CHARGEABLE, REFUNDED
        /// </summary>
        /// <value>
        /// The payment status
        /// </value>
        [JsonProperty("paymentStatus")]
        public string PaymentStatus { get; set; }

        /// <summary>
        /// Fulfillment status. Contains one of these values: NEW, PROCESSING, SHIPPED, DELIVERED, WILL_NOT_DELIVER, RETURNED
        /// </summary>
        /// <value>
        /// The fulfillment status
        /// </value>
        [JsonProperty("fulfillmentStatus")]
        public string FulfillmentStatus { get; set; }

        /// <summary>
        /// Shipping tracking code
        /// </summary>
        /// <value>
        /// The shipping tracking code
        /// </value>
        [JsonProperty("shippingTrackingCode")]
        public string ShippingTrackingCode { get; set; }

        /// <summary>
        /// Old payment status. Contains one of these values: ACCEPTED*, DECLINED, CANCELLED, QUEUED, CHARGEABLE, REFUNDED
        /// </summary>
        /// <value>
        /// The payment status
        /// </value>
        [JsonProperty("oldPaymentStatus")]
        public string OldPaymentStatus { get; set; }

        /// <summary>
        /// Old fulfillment status. Contains one of these values: NEW, PROCESSING, SHIPPED, DELIVERED, WILL_NOT_DELIVER, RETURNED
        /// </summary>
        /// <value>
        /// The fulfillment status
        /// </value>
        [JsonProperty("oldFulfillmentStatus")]
        public string OldFulfillmentStatus { get; set; }

        /// <summary>
        /// Old shipping tracking code
        /// </summary>
        /// <value>
        /// The shipping tracking code
        /// </value>
        [JsonProperty("oldShippingTrackingCode")]
        public string OldShippingTrackingCode { get; set; }
    }
}
