// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using Flurl;
// ReSharper disable UnusedMember.Global

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

        /// <inheritdoc />
        public EcwidClient(int shopId, string token) : this()
        {
            Configure(shopId, token);
        }

        /// <inheritdoc />
        public EcwidClient(int shopId, string token, string scope) : this()
        {
            Configure(shopId, token, scope);
        }

        /// <inheritdoc />
        public EcwidClient(EcwidCredentials credentials) : this()
        {
            Configure(credentials);
        }

        /// <inheritdoc />
        public IEcwidClient Configure(EcwidSettings settings)
        {
            Settings = settings;
            return this;
        }

        /// <inheritdoc />
        public IEcwidClient Configure(int shopId, string token)
        {
            Credentials = new EcwidCredentials(shopId, token);

            return this;
        }

        /// <inheritdoc />
        public IEcwidClient Configure(int shopId, string token, string scope)
        {
            Credentials = new EcwidCredentials(shopId, token, scope);

            return this;
        }

        /// <inheritdoc />
        public IEcwidClient Configure(EcwidCredentials credentials)
        {
            Credentials = credentials;
            return this;
        }

        /// <inheritdoc />
        public EcwidCredentials Credentials { get; set; }

        /// <inheritdoc />
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