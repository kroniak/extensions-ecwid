// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// Information about account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the account email.
        /// </summary>
        /// <value>
        /// The account email.
        /// </value>
        [JsonProperty("accountEmail")]
        public string AccountEmail { get; set; }

        /// <summary>
        /// Gets or sets the full store owner name of the account.
        /// </summary>
        /// <value>
        /// The name of the account.
        /// </value>
        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the name of the account nickname on the Ecwid forums.
        /// </summary>
        /// <value>
        /// The name of the account nickname.
        /// </value>
        [JsonProperty("accountNickName")]
        public string AccountNickname { get; set; }

        /// <summary>
        /// Gets or sets a list of the premium features available on the store’s pricing plan.
        /// </summary>
        /// <value>
        /// The available features.
        /// </value>
        [JsonProperty("availableFeatures")]
        public IList<string> AvailableFeatures { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Account" /> is suspended.
        /// </summary>
        /// <value>
        /// <c>true</c> if suspended; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("suspended")]
        public bool Suspended { get; set; }
    }
}