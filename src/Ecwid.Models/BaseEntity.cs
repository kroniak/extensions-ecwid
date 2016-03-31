using Newtonsoft.Json;
// ReSharper disable UnusedMember.Global

namespace Ecwid.Models
{
    /// <summary>
    /// Base entity contains identifier
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        /// <value>
        /// The identifier
        /// </value>
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}