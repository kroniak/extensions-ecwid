// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

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
    }
}