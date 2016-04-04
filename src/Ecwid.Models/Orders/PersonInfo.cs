using Newtonsoft.Json;

namespace Ecwid.Models
{
    public class PersonInfo
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
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>
        /// The street.
        /// </value>
        [JsonProperty("street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the two-letter country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the state code, e.g. NY.
        /// </summary>
        /// <value>
        /// The state or province code.
        /// </value>
        [JsonProperty("stateOrProvinceCode")]
        public string StateOrProvinceCode { get; set; }

        /// <summary>
        /// Gets or sets the state/province name, e.g. New York.
        /// </summary>
        /// <value>
        /// The name of the state or province.
        /// </value>
        [JsonProperty("stateOrProvinceName")]
        public string StateOrProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}