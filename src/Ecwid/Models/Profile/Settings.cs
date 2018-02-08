// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// The shop's settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Settings" /> is closed.
        /// </summary>
        /// <value>
        /// <c>true</c> if closed; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("closed")]
        public bool Closed { get; set; }

        /// <summary>
        /// Gets or sets the logo URL displayed in the store email notifications.
        /// </summary>
        /// <value>
        /// The invoice logo URL.
        /// </value>
        [JsonProperty("emailLogoUrl")]
        public string EmailLogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the google analytic identifier.
        /// </summary>
        /// <value>
        /// The google analytic identifier.
        /// </value>
        [JsonProperty("googleAnalyticsId")]
        public string GoogleAnalyticsId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [google remarketing enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [google remarketing enabled]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("googleRemarketingEnabled")]
        public bool GoogleRemarketingEnabled { get; set; }

        /// <summary>
        /// Gets or sets the invoice logo URL displayed on the invoice.
        /// </summary>
        /// <value>
        /// The invoice logo URL.
        /// </value>
        [JsonProperty("invoiceLogoUrl")]
        public string InvoiceLogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the name displayed in Starter Site.
        /// </summary>
        /// <value>
        /// The name of the store.
        /// </value>
        [JsonProperty("storeName")]
        public string StoreName { get; set; }
    }
}