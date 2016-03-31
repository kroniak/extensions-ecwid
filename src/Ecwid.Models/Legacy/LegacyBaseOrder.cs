using Newtonsoft.Json;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Ecwid.Models
{
    /// <summary>
    /// Base order for <see cref="LegacyUpdatedOrder"/> and <see cref="LegacyOrder"/>
    /// </summary>
    public abstract class LegacyBaseOrder
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
    }
}