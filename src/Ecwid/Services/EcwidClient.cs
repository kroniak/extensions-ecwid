// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Flurl;

namespace Ecwid
{
    /// <summary>
    /// Ecwid API Client v3.
    /// </summary>
    public partial class EcwidClient : BaseEcwidClient, IEcwidClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidClient" /> class without configuration.
        /// </summary>
        public EcwidClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidClient" /> class and configures it.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="token">The authorization token.</param>
        /// <exception cref="EcwidConfigException">The shop identifier is invalid.</exception>
        /// <exception cref="EcwidConfigException">The authorization token is invalid.</exception>
        public EcwidClient(int shopId, string token) : this()
        {
            Configure(shopId, token);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidClient" /> class and configures it.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="token">The authorization token.</param>
        /// <param name="scope">List of permissions (API access scopes) given to the app, separated by space.</param>
        /// <exception cref="EcwidConfigException">The shop identifier is invalid.</exception>
        /// <exception cref="EcwidConfigException">The authorization token is invalid.</exception>
        public EcwidClient(int shopId, string token, string scope) : this()
        {
            Configure(shopId, token, scope);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidClient" /> class and configures it.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        public EcwidClient(EcwidCredentials credentials) : this()
        {
            Configure(credentials);
        }

        /// <summary>
        /// Configures the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public IEcwidClient Configure(EcwidSettings settings)
        {
            Settings = settings;
            return this;
        }

        /// <summary>
        /// Configures the shop credentials.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="token">The authorization token.</param>
        /// <exception cref="EcwidConfigException">The shop identifier is invalid.</exception>
        /// <exception cref="EcwidConfigException">The authorization token is invalid.</exception>
        public IEcwidClient Configure(int shopId, string token)
        {
            Credentials = new EcwidCredentials(shopId, token);

            return this;
        }

        /// <summary>
        /// Configures the shop credentials.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="token">The authorization token.</param>
        /// <param name="scope">List of permissions (API access scopes) given to the app, separated by space.</param>
        /// <exception cref="EcwidConfigException">The shop identifier is invalid.</exception>
        /// <exception cref="EcwidConfigException">The authorization token is invalid.</exception>
        public IEcwidClient Configure(int shopId, string token, string scope)
        {
            Credentials = new EcwidCredentials(shopId, token, scope);

            return this;
        }

        /// <summary>
        /// Configures with specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        public IEcwidClient Configure(EcwidCredentials credentials)
        {
            Credentials = credentials;
            return this;
        }

        /// <summary>
        /// Gets and sets the credentials. Default value is <see langword="null" />.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        public EcwidCredentials Credentials { get; set; }

        /// <summary>
        /// Gets and sets the settings. Created by default.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public EcwidSettings Settings { get; set; } = new EcwidSettings();

        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        private string GetUrl(string segment)
        {
            if (Credentials == null)
                throw new EcwidConfigException("Credentials are null. Can not do a request.");

            return Settings.ApiUrl
                .AppendPathSegments(Credentials.ShopId.ToString(), segment)
                .SetQueryParam("token", Credentials.Token);
        }
    }
}