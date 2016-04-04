using Newtonsoft.Json;

namespace Ecwid.Models
{
    public class ShippingOptionInfo
    {
        /// <summary>
        /// Gets or sets the name of the shipping carrier name, e.g. USPS.
        /// </summary>
        /// <value>
        /// The name of the shipping carrier name.
        /// </value>
        [JsonProperty("shippingCarrierName")]
        public string ShippingCarrierName { get; set; }

        /// <summary>
        /// Gets or sets the name of the shipping method name.
        /// </summary>
        /// <value>
        /// The name of the shipping method name.
        /// </value>
        [JsonProperty("shippingMethodName")]
        public string ShippingMethodName { get; set; }

        /// <summary>
        /// Gets or sets the shipping rate.
        /// </summary>
        /// <value>
        /// The shipping rate.
        /// </value>
        [JsonProperty("shippingRate")]
        public double ShippingRate { get; set; }

        /// <summary>
        /// Gets or sets the estimated transit time. Formats accepted: number “5”, several days estimate “4-9”.
        /// </summary>
        /// <value>
        /// The estimated transit time. Formats accepted: number “5”, several days estimate “4-9”.
        /// </value>
        [JsonProperty("estimatedTransitTime")]
        public string EstimatedTransitTime { get; set; }
    }
}