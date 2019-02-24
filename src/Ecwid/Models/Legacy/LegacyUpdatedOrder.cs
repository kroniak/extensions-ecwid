// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <inheritdoc />
    public class LegacyUpdatedOrder : LegacyBaseOrder
    {
        /// <summary>
        /// Gets or sets the old payment status. Contains one of these values: ACCEPTED*, DECLINED, CANCELLED, QUEUED, CHARGEABLE,
        /// REFUNDED.
        /// </summary>
        /// <value>
        /// The payment status. Contains one of these values: ACCEPTED*, DECLINED, CANCELLED, QUEUED, CHARGEABLE, REFUNDED.
        /// </value>
        [JsonProperty("oldPaymentStatus")]
        public string OldPaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the old fulfillment status. Contains one of these values: NEW, PROCESSING, SHIPPED, DELIVERED,
        /// WILL_NOT_DELIVER, RETURNED.
        /// </summary>
        /// <value>
        /// The fulfillment status. Contains one of these values: NEW, PROCESSING, SHIPPED, DELIVERED, WILL_NOT_DELIVER, RETURNED.
        /// </value>
        [JsonProperty("oldFulfillmentStatus")]
        public string OldFulfillmentStatus { get; set; }

        /// <summary>
        /// Gets or sets the old shipping tracking code.
        /// </summary>
        /// <value>
        /// The shipping tracking code.
        /// </value>
        [JsonProperty("oldShippingTrackingCode")]
        public string OldShippingTrackingCode { get; set; }
    }
}