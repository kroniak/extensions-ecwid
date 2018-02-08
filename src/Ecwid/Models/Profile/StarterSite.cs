// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// System Settings → General → Starter site.
    /// </summary>
    public class StarterSite
    {
        /// <summary>
        /// Gets or sets the custom domain, e.g. www.mysuperstore.com.
        /// </summary>
        /// <value>
        /// The custom domain.
        /// </value>
        [JsonProperty("customDomain")]
        public string CustomDomain { get; set; }

        /// <summary>
        /// Gets or sets the ecwid subdomain, e.g. mysuperstore.ecwid.com.
        /// </summary>
        /// <value>
        /// The ecwid subdomain.
        /// </value>
        [JsonProperty("ecwidSubdomain")]
        public string EcwidSubdomain { get; set; }

        /// <summary>
        /// Gets or sets the generated URL, e.g. http://mysuperstore.ecwid.com/.
        /// </summary>
        /// <value>
        /// The generated URL.
        /// </value>
        [JsonProperty("generatedUrl")]
        public string GeneratedUrl { get; set; }

        /// <summary>
        /// Gets or sets the store logo URL.
        /// </summary>
        /// <value>
        /// The store logo URL.
        /// </value>
        [JsonProperty("storeLogoUrl")]
        public string StoreLogoUrl { get; set; }
    }
}