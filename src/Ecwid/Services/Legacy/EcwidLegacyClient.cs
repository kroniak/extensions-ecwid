// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Threading.Tasks;
using Flurl;

// ReSharper disable once CheckNamespace
namespace Ecwid.Legacy
{
    /// <summary>
    /// Ecwid API Client v1 (Legacy).
    /// </summary>
    public partial class EcwidLegacyClient : BaseEcwidClient, IEcwidLegacyClient
    {
        /// <summary>
        /// The shared limits service for API limits.
        /// </summary>
        private static readonly Lazy<LimitsService> LimitsService = new Lazy<LimitsService>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidLegacyClient" /> class withput configuration.
        /// </summary>
        public EcwidLegacyClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidLegacyClient" /> class and configures it.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="orderToken">The shop order authorization token.</param>
        /// <param name="productToken">The shop product authorization token.</param>
        /// <exception cref="EcwidConfigException">The shop identifier is invalid.</exception>
        /// <exception cref="EcwidConfigException">The authorization tokens are null.</exception>
        /// <exception cref="EcwidConfigException">The order authorization token is invalid.</exception>
        /// <exception cref="EcwidConfigException">The product authorization token is invalid.</exception>
        public EcwidLegacyClient(int shopId, string orderToken = null, string productToken = null) : this()
        {
            Configure(shopId, orderToken, productToken);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidLegacyClient" /> class and configures it.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        public EcwidLegacyClient(EcwidLegacyCredentials credentials) : this()
        {
            Configure(credentials);
        }

        /// <summary>
        /// Configures with specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public IEcwidLegacyClient Configure(EcwidLegacySettings settings)
        {
            Settings = settings;
            return this;
        }

        /// <summary>
        /// Configures the shop credentials.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="orderToken">The shop order authorization token.</param>
        /// <param name="productToken">The shop product authorization token.</param>
        /// <exception cref="EcwidConfigException">The shop identifier is invalid.</exception>
        /// <exception cref="EcwidConfigException">The authorization tokens are null.</exception>
        /// <exception cref="EcwidConfigException">The order authorization token is invalid.</exception>
        /// <exception cref="EcwidConfigException">The product authorization token is invalid.</exception>
        public IEcwidLegacyClient Configure(int shopId, string orderToken = null, string productToken = null)
        {
            Credentials = new EcwidLegacyCredentials(shopId, orderToken, productToken);
            return this;
        }

        /// <summary>
        /// Configures with specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        public IEcwidLegacyClient Configure(EcwidLegacyCredentials credentials)
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
        public EcwidLegacyCredentials Credentials { get; set; }

        /// <summary>
        /// Gets and sets the settings. Created by default.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public EcwidLegacySettings Settings { get; set; } = new EcwidLegacySettings();

        #region CORE

        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
        private string GetUrl(string segment, bool withoutToken = false)
        {
            string token = null;

            if (segment == "orders")
                token = Credentials?.OrderToken;
            if (segment == "products" || segment == "product" || segment == "category" || segment == "categories")
                token = Credentials?.ProductToken;

            if (token == null)
                throw new EcwidConfigException("Credentials are null. Can not do a request.");

            // Wait open window for request
            WaitLimit();

            if (withoutToken)
                return Settings.ApiUrl
                    .AppendPathSegments(Credentials.ShopId.ToString(), segment);

            return Settings.ApiUrl
                .AppendPathSegments(Credentials.ShopId.ToString(), segment)
                .SetQueryParam("secure_auth_key", token);
        }

        /// <summary>
        /// Waits the limit.
        /// </summary>
        /// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
        private void WaitLimit()
        {
            var start = DateTime.Now;

            try
            {
                // Get agreement from limits service
                var agreement = LimitsService.Value.Tick();

                while (!agreement)
                {
                    // If time limit is over
                    if (start.AddSeconds(Settings.MaxSecondsToWait) < DateTime.Now)
                        throw new EcwidLimitException("Limit overheat exception", LimitsService.Value.GetInfo());

                    Task.Delay(Settings.RetryInterval*1000).Wait();
                    agreement = LimitsService.Value.Tick();
                }
            }
            // ReSharper disable once CatchAllClause
            catch (Exception exception)
            {
                throw new EcwidLimitException("Internal error in limits sevices. Look inner exception.", exception);
            }
        }

        #endregion
    }
}