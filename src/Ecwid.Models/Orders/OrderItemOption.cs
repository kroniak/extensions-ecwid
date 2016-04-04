using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// This object represents options of purchased product.
    /// </summary>
    public class OrderItemOption
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the selected/entered option value(s) as a string. 
        /// For the CHOICES type, provides a string with all chosen values (comma-separated). You can use this to simply print out all selected values.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the selected option values as an array. 
        /// For the CHOICES type, provides an array with the chosen values so you can iterate through them in your app..
        /// </summary>
        /// <value>
        /// The values array.
        /// </value>
        [JsonProperty("valuesArray")]
        public IList<string> ValuesArray { get; set; }

        /// <summary>
        /// Gets or sets the option type. One of:
        /// CHOICE(dropdown or radio button)
        /// CHOICES(checkboxes)
        /// TEXT(text input and text area)
        /// DATE(date/time)
        /// FILES(upload file option).
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the attached files (if the option type is FILES).
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        [JsonProperty("files")]
        public IList<OrderItemOptionFile> Files { get; set; }
    }
}