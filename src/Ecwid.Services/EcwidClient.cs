// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

namespace Ecwid
{
    /// <summary>
    /// Ecwid API Client v3.
    /// </summary>
    public partial class EcwidClient : BaseEcwidClient, IEcwidClient
    {
        /// <summary>
        /// Gets and sets the settings. Created by default.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public EcwidSettings Settings { get; set; } = new EcwidSettings();

        /// <summary>
        /// Gets and sets the credentials. Default value is <see langword="null" />.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        public EcwidCredentials Credentials { get; set; }

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
    }
}