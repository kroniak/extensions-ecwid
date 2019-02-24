// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Text.RegularExpressions;
using Ecwid.Tools;

namespace Ecwid.Legacy
{
    /// <inheritdoc />
    public class EcwidLegacyCredentials : BaseCredentials
    {
        private string _orderToken;
        private string _productToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidLegacyCredentials" /> class.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="orderToken">The shop order authorization token.</param>
        /// <param name="productToken">The shop product authorization token.</param>
        /// <exception cref="EcwidConfigException">The shop identifier is invalid.</exception>
        /// <exception cref="EcwidConfigException">The authorization tokens are null.</exception>
        /// <exception cref="EcwidConfigException">The order authorization token is invalid.</exception>
        /// <exception cref="EcwidConfigException">The product authorization token is invalid.</exception>
        public EcwidLegacyCredentials(int shopId, string orderToken = null, string productToken = null)
        {
            ShopId = shopId;

            if (Validators.AreNullOrEmpty(orderToken, productToken))
                throw new EcwidConfigException("The authorization tokens are null.");

            if (!string.IsNullOrWhiteSpace(orderToken))
                OrderToken = orderToken;

            if (!string.IsNullOrWhiteSpace(productToken))
                ProductToken = productToken;
        }

        /// <summary>
        /// Gets or sets the shop order authorization token. This is a key your app will use to access Ecwid API on behalf of the
        /// user.
        /// </summary>
        /// <value>
        /// The shop order authorization token.
        /// </value>
        /// <exception cref="EcwidConfigException" accessor="set">The order authorization token is invalid.</exception>
        public string OrderToken
        {
            get => _orderToken;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new EcwidConfigException("The order authorization token is invalid.");

                // https://github.com/kroniak/extensions-ecwid/issues/34
                //var regex = new Regex("^[0-9a-zA-Z]{12,}");
                //if (!regex.IsMatch(value))
                //    throw new EcwidConfigException("The order authorization token is invalid.");

                _orderToken = value;
            }
        }

        /// <summary>
        /// Gets or sets the shop product authorization token. This is a key your app will use to access Ecwid API on behalf of the
        /// user.
        /// </summary>
        /// <value>
        /// The shop product authorization token.
        /// </value>
        /// <exception cref="EcwidConfigException" accessor="set">The product authorization token is invalid.</exception>
        public string ProductToken
        {
            get => _productToken;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new EcwidConfigException("The product authorization token is invalid.");

                var regex = new Regex("^[0-9a-zA-Z]{12,}");
                if (!regex.IsMatch(value))
                    throw new EcwidConfigException("The product authorization token is invalid.");

                _productToken = value;
            }
        }
    }
}