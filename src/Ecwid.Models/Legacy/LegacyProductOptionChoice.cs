// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    public class LegacyProductOptionChoice
    {
        /// <summary>
        /// Gets or sets the text displayed as a choice in a drop-down or a radio box, e.g. 'Green'.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the specifies the way the product price is modified.
        /// One of 'PERCENT' or 'ABSOLUTE'. If 'PERCENT', then priceModifier is a number of percents to add to the price.
        /// If 'ABSOLUTE', then priceModifier is a number of currency units to add to the price.
        /// </summary>
        /// <value>
        /// The type of the price modifier.
        /// </value>
        [JsonProperty("priceModifierType")]
        public string PriceModifierType { get; set; }

        /// <summary>
        /// Gets or sets the number of percents or currency units to add to the product price when this choice is selected. May be
        /// negative or zero.
        /// </summary>
        /// <value>
        /// The price modifier.
        /// </value>
        [JsonProperty("priceModifier")]
        public double PriceModifier { get; set; }

        /// <summary>
        /// Gets or sets the SKU prefix to add to the product's SKU to obtain the full SKU of the option combination. This is
        /// optional.
        /// </summary>
        /// <value>
        /// The sku.
        /// </value>
        [JsonProperty("sku")]
        public string Sku { get; set; }
    }
}