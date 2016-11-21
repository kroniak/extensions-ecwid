// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// System settings → General → Formats and Units.
    /// </summary>
    public class FormatsAndUnits
    {
        /// <summary>
        /// Gets or sets the 3-letters code of the store currency (ISO 4217). Examples: USD, CAD.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the price decimal separator. Possible values: . or ,
        /// </summary>
        /// <value>
        /// The currency decimal separator.
        /// </value>
        [JsonProperty("currencyDecimalSeparator")]
        public string CurrencyDecimalSeparator { get; set; }

        /// <summary>
        /// Gets or sets the price thousands separator. Supported values: space , dot ., comma , or empty value “.
        /// </summary>
        /// <value>
        /// The currency group separator.
        /// </value>
        [JsonProperty("currencyGroupSeparator")]
        public string CurrencyGroupSeparator { get; set; }

        /// <summary>
        /// Gets or sets the numbers of digits after decimal point in the store prices. E.g. 2 ($2.99) or 0 (¥500).
        /// </summary>
        /// <value>
        /// The currency precision.
        /// </value>
        [JsonProperty("currencyPrecision")]
        public int CurrencyPrecision { get; set; }

        /// <summary>
        /// Gets or sets the currency prefix (e.g. $).
        /// </summary>
        /// <value>
        /// The currency prefix.
        /// </value>
        [JsonProperty("currencyPrefix")]
        public string CurrencyPrefix { get; set; }

        /// <summary>
        /// Gets or sets the currency rate in U.S. dollars, as set in the merchant control panel.
        /// </summary>
        /// <value>
        /// The currency rate.
        /// </value>
        [JsonProperty("currencyRate")]
        public double CurrencyRate { get; set; }

        /// <summary>
        /// Gets or sets the currency suffix.
        /// </summary>
        /// <value>
        /// The currency suffix.
        /// </value>
        [JsonProperty("currencySuffix")]
        public string CurrencySuffix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [currency truncate zero fractional].
        /// </summary>
        /// <value>
        /// <c>true</c> if [currency truncate zero fractional]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("currencyTruncateZeroFractional")]
        public bool CurrencyTruncateZeroFractional { get; set; }

        /// <summary>
        /// Gets or sets the date format. E.g. "MMM d, yyyy".
        /// </summary>
        /// <value>
        /// The date format.
        /// </value>
        [JsonProperty("dateFormat")]
        public string DateFormat { get; set; }

        /// <summary>
        /// Gets or sets the time format. E.g. "hh:mm a".
        /// </summary>
        /// <value>
        /// The time format.
        /// </value>
        [JsonProperty("timeFormat")]
        public string TimeFormat { get; set; }

        /// <summary>
        /// Gets or sets the timezone. E.g. Europe/Moscow.
        /// </summary>
        /// <value>
        /// The timezone.
        /// </value>
        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        /// <summary>
        /// Gets or sets the weight decimal separator. Possible values: . or ,
        /// </summary>
        /// <value>
        /// The weight decimal separator.
        /// </value>
        [JsonProperty("weightDecimalSeparator")]
        public string WeightDecimalSeparator { get; set; }

        /// <summary>
        /// Gets or sets the weight thousands separator. Supported values: space , dot ., comma , or empty value ”.
        /// </summary>
        /// <value>
        /// The weight group separator.
        /// </value>
        [JsonProperty("weightGroupSeparator")]
        public string WeightGroupSeparator { get; set; }

        /// <summary>
        /// Gets or sets the numbers of digits after decimal point in weights displayed in the store.
        /// </summary>
        /// <value>
        /// The weight precision.
        /// </value>
        [JsonProperty("weightPrecision")]
        public int WeightPrecision { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [weight truncate zero fractional].
        /// </summary>
        /// <value>
        /// <c>true</c> if [weight truncate zero fractional]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("weightTruncateZeroFractional")]
        public bool WeightTruncateZeroFractional { get; set; }

        /// <summary>
        /// Gets or sets the weight unit. Supported values: CARAT, GRAM, OUNCE, POUND, KILOGRAM.
        /// </summary>
        /// <value>
        /// The weight unit.
        /// </value>
        [JsonProperty("weightUnit")]
        public string WeightUnit { get; set; }
    }
}