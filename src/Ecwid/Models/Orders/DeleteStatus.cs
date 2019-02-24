// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <inheritdoc />
    public class DeleteStatus : UpdateStatus
    {
        /// <summary>
        /// Gets or sets the number of deleted orders (1 or 0 depending on whether the delete was successful).
        /// </summary>
        /// <value>
        /// The update count.
        /// </value>
        [JsonProperty("deleteCount")]
        public int DeleteCount { get; set; }
    }
}