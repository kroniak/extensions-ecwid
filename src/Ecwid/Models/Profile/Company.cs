// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// System Settings → General → Store Profile.
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the name of the company displayed on the invoice.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the two-letter ISO code of the country.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the email (store administrator).
        /// </summary>
        /// <value>
        /// The email (store administrator).
        /// </value>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the company phone number.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the postal code or ZIP code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the state or province code or region name.
        /// </summary>
        /// <value>
        /// The state or province code.
        /// </value>
        [JsonProperty("stateOrProvinceCode")]
        public string StateOrProvinceCode { get; set; }

        /// <summary>
        /// Gets or sets the company address. 1 or 2 lines separated by a new line character.
        /// </summary>
        /// <value>
        /// The street.
        /// </value>
        [JsonProperty("street")]
        public string Street { get; set; }
    }
}