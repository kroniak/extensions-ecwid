// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// System Settings → General → Languages.
    /// </summary>
    public class Languages
    {
        /// <summary>
        /// Gets or sets the enabled languages in the storefront. First language code is the default language for the store.
        /// </summary>
        /// <value>
        /// The enabled languages.
        /// </value>
        [JsonProperty("enabledLanguages")]
        public List<string> EnabledLanguages { get; set; }

        /// <summary>
        /// Gets or sets the facebook preferred locale automatically chosen be default in Facebook storefront (if any).
        /// </summary>
        /// <value>
        /// The facebook preferred locale.
        /// </value>
        [JsonProperty("facebookPreferredLocale")]
        public string FacebookPreferredLocale { get; set; }
    }
}