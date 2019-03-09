// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <inheritdoc />
    public class Tax : BaseEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether [applied by default].
        /// </summary>
        /// <value>
        /// <c>true</c> if the tax is applied to all products. <c>false</c> is the tax is only applied to this product that have
        /// this tax enabled.
        /// </value>
        [JsonProperty("appliedByDefault")]
        public bool AppliedByDefault { get; set; }

        /// <summary>
        /// Gets or sets the default tax value, in %, when none of the destination zones match.
        /// </summary>
        /// <value>
        /// The default tax.
        /// </value>
        [JsonProperty("defaultTax")]
        public int DefaultTax { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Tax" /> is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include in price].
        /// </summary>
        /// <value>
        /// <c>true</c> if [include in price]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("includeInPrice")]
        public bool IncludeInPrice { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        [JsonProperty("rules")]
        public IEnumerable<TaxRule> Rules { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [tax shipping].
        /// </summary>
        /// <value>
        /// <c>true</c> is the tax applies to subtotal+shipping cost. <c>false</c> if the tax is applied to subtotal only.
        /// </value>
        [JsonProperty("taxShipping")]
        public bool TaxShipping { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use shipping address].
        /// </summary>
        /// <value>
        /// <c>true</c> if the tax is calculated based on shipping address, <c>false</c> if billing address is used.
        /// </value>
        [JsonProperty("useShippingAddress")]
        public bool UseShippingAddress { get; set; }
    }
}