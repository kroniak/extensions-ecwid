// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// </summary>
    public class TaxRule
    {
        /// <summary>
        /// Gets or sets the tax in %.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        [JsonProperty("tax")]
        public double Tax { get; set; }

        /// <summary>
        /// Gets or sets the destination zone identifier.
        /// </summary>
        /// <value>
        /// The zone identifier.
        /// </value>
        [JsonProperty("zoneId")]
        public string ZoneId { get; set; }
    }
}