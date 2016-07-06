// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// System Settings → Shipping.
    /// </summary>
    public class Shipping
    {
        /// <summary>
        /// Gets or sets the handling fee information.
        /// </summary>
        /// <value>
        /// The handling fee information.
        /// </value>
        [JsonProperty("handlingFee")]
        public HandlingFeeInfo HandlingFeeInfo { get; set; }
    }
}