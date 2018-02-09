// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// System Settings → Zones.
    /// </summary>
    public class Zone
    {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        /// <value>
        /// The identifier
        /// </value>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the country codes.
        /// </summary>
        /// <value>
        /// The country codes.
        /// </value>
        [JsonProperty("countryCodes")]
        public IList<string> CountryCodes { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the postcode(or zipcode) templates this zone includes. More details: Destination zones in Ecwid.
        /// </summary>
        /// <value>
        /// The post codes.
        /// </value>
        [JsonProperty("postCodes")]
        public IList<string> PostCodes { get; set; }

        /// <summary>
        /// Gets or sets the state or province codes.
        /// </summary>
        /// <value>
        /// The state or province codes.
        /// </value>
        [JsonProperty("stateOrProvinceCodes")]
        public IList<string> StateOrProvinceCodes { get; set; }
    }
}