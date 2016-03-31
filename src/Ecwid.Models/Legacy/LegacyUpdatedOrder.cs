using Newtonsoft.Json;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Ecwid.Models
{
    /// <summary>
    /// Represent updated order
    /// </summary>
    public class LegacyUpdatedOrder : LegacyBaseOrder
    {
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
