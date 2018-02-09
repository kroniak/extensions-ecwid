// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// Products tax.
    /// </summary>
    public class LegacyProductTax
    {
        /// <summary>
        /// Gets or sets the tax display name, e.g. 'VAT'.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the default tax value (the one calculated from "Other zones" rate), in store currency. Deprecated.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public double Value { get; set; }
    }
}