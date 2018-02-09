// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// A JSON object of type 'UpdateStatus’ with the following fields.
    /// </summary>
    public class UpdateStatus
    {
        /// <summary>
        /// Gets or sets the number of updated orders (1 or 0 depending on whether the update was successful).
        /// </summary>
        /// <value>
        /// The update count.
        /// </value>
        [JsonProperty("updateCount")]
        public int UpdateCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UpdateStatus"/> is success. DEPRECATED
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}