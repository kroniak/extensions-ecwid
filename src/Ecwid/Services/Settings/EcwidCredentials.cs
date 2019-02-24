// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Ecwid.Tools;

namespace Ecwid
{
    /// <inheritdoc />
    public class EcwidCredentials : BaseCredentials
    {
        private string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidCredentials" /> class.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="token">The authorization token.</param>
        /// <exception cref="EcwidConfigException">The shop identifier is invalid.</exception>
        /// <exception cref="EcwidConfigException">The authorization token is invalid.</exception>
        public EcwidCredentials(int shopId, string token)
        {
            ShopId = shopId;
            Token = token;
        }

        /// <inheritdoc />
        public EcwidCredentials(int shopId, string token, string scope) : this(shopId, token)
        {
            Scope = scope;
        }

        /// <summary>
        /// Gets or sets the authorization token. This is a key your app will use to access Ecwid API on behalf of the user.
        /// </summary>
        /// <value>
        /// The authorization token.
        /// </value>
        /// <exception cref="EcwidConfigException" accessor="set">The authorization token is invalid.</exception>
        public string Token
        {
            get => _token;
            set
            {
                if (Validators.IsNullOrEmpty(value))
                    throw new EcwidConfigException("The authorization token is invalid.");

                // https://github.com/kroniak/extensions-ecwid/issues/34
                //var regex = new Regex("^[0-9a-zA-Z]{32,}");
                //if (!regex.IsMatch(value))
                //    throw new EcwidConfigException("The authorization token is invalid.");

                _token = value;
            }
        }

        // TODO Validation
        /// <summary>
        /// Gets or sets the scope. Scopes are permissions that identifies the scope of access your application requests from the
        /// user. <see langword="Default" /> is only read permissions.
        /// </summary>
        /// <value>
        /// List of permissions (API access scopes) given to the app, separated by space.
        /// </value>
        public string Scope { get; set; } =
            "read_store_profile read_catalog read_orders read_customers read_discount_coupons";
    }
}