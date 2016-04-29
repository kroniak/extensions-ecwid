// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Threading.Tasks;

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
                        // TODO tests
                        throw new EcwidLimitException("Limit overheat exception");

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