using Newtonsoft.Json;

namespace Ecwid.Models
{
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
