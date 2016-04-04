using Newtonsoft.Json;
// ReSharper disable ClassNeverInstantiated.Global

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// This object represents options of purchased product.
    /// </summary>
    public class LegacyOrderItemOption
    {
        /// <summary>
        /// Gets or sets the option name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the option type. Can contain one of these values: SELECT, TEXT, DATE, FILE.
        /// </summary>
        /// <value>
        /// The type. Can contain one of these values: SELECT, TEXT, DATE, FILE.
        /// </value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the text value for SELECT, DATE and TEXT option types. Contain List of <seealso cref="LegacyOrderItemOrderFile"/> in case option type is FILE.
        /// </summary>
        /// <value>
        /// The value. Contain List of <seealso cref="LegacyOrderItemOrderFile"/> in case option type is FILE.
        /// </value>
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}