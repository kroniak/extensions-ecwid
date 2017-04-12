// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// </summary>
    public class BusinessRegistrationId
    {
        /// <summary>
        /// Gets or sets the ID name, e.g. Vat ID, P.IVA, ABN.
        /// </summary>
        /// <value>
        /// The ID name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        /// <value>
        /// The ID value.
        /// </value>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}