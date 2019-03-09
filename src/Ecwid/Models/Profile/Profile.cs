// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// Store settings and information.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Gets or sets the account data.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        [JsonProperty("account")]
        public Account Account { get; set; }

        /// <summary>
        /// Gets or sets the business registration identifier, e.g. VAT reg number or company ID, which is set under Settings /
        /// Invoice in Control panel.
        /// </summary>
        /// <value>
        /// The business registration identifier.
        /// </value>
        [JsonProperty("businessRegistrationID")]
        public BusinessRegistrationId BusinessRegistrationId { get; set; }

        /// <summary>
        /// Gets or sets the company information.
        /// </summary>
        /// <value>
        /// The company information.
        /// </value>
        [JsonProperty("company")]
        public Company Company { get; set; }

        /// <summary>
        /// Gets or sets the formats and units.
        /// </summary>
        /// <value>
        /// The formats and units.
        /// </value>
        [JsonProperty("formatsAndUnits")]
        public FormatsAndUnits FormatsAndUnits { get; set; }

        /// <summary>
        /// Gets or sets the general information.
        /// </summary>
        /// <value>
        /// The general information.
        /// </value>
        [JsonProperty("generalInfo")]
        public GeneralInfo GeneralInfo { get; set; }

        /// <summary>
        /// Gets or sets the language settings.
        /// </summary>
        /// <value>
        /// The language settings.
        /// </value>
        [JsonProperty("languages")]
        public Languages Languages { get; set; }

        /// <summary>
        /// Gets or sets the mail notifications.
        /// </summary>
        /// <value>
        /// The mail notifications.
        /// </value>
        [JsonProperty("mailNotifications")]
        public MailNotifications MailNotifications { get; set; }

        /// <summary>
        /// Gets or sets the store general settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        [JsonProperty("settings")]
        public Settings Settings { get; set; }

        /// <summary>
        /// Gets or sets the shipping settings.
        /// </summary>
        /// <value>
        /// The shipping settings.
        /// </value>
        [JsonProperty("shipping")]
        public Shipping Shipping { get; set; }

        /// <summary>
        /// Gets or sets the taxes.
        /// </summary>
        /// <value>
        /// The taxes.
        /// </value>
        [JsonProperty("taxes")]
        public IEnumerable<Tax> Taxes { get; set; }

        /// <summary>
        /// Gets or sets the destination zones.
        /// </summary>
        /// <value>
        /// The destination zones.
        /// </value>
        [JsonProperty("zones")]
        public IEnumerable<Zone> Zones { get; set; }
    }
}