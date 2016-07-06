// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// Product options class.
    /// </summary>
    public class LegacyProductOption
    {
        /// <summary>
        /// Gets or sets the option name, like 'Color'.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the one of 'SELECT', 'RADIO', 'TEXTFIELD', 'TEXTAREA', 'DATE', 'FILES', 'CHECKBOX'.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the all possible option choices, if the type is 'SELECT' or 'RADIO'. Absent otherwise.
        /// </summary>
        /// <value>
        /// The choices.
        /// </value>
        [JsonProperty("choices")]
        public IList<LegacyProductOptionChoice> Choices { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether this <see cref="LegacyProductOption" /> is required.
        /// </summary>
        /// <value>
        /// <c>true</c> if required; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("required")]
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the number, starting from 0, of the default choice. Only present if the type is 'SELECT' or 'RADIO'.
        /// </summary>
        /// <value>
        /// The default choice. Only present if the type is 'SELECT' or 'RADIO'.
        /// </value>
        [JsonProperty("defaultChoice")]
        public int? DefaultChoice { get; set; }
    }
}