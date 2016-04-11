// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Tools;
using Flurl;

namespace Ecwid.Services.Legacy
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
        /// Gets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public EcwidLegacyOptions Options { get; private set; } = new EcwidLegacyOptions();

        /// <summary>
        /// Configures the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentException">
        /// The shop identificator is null. Please reconfig the client.
        /// or
        /// The shop identificator is invalid. Please reconfig the client.
        /// </exception>
        public IEcwidLegacyClient Configure(EcwidLegacyOptions options)
        {
            Validators.ShopIdValidate(options.ShopId);

            Options = options;
            return this;
        }

        /// <summary>
        /// Configures the shop.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="shopOrderAuthId">The shop order authentication identifier.</param>
        /// <param name="shopProductAuthId">The shop product authentication identifier.</param>
        /// <exception cref="ArgumentException">
        /// The shop identificator is null. Please reconfig the client.
        /// or
        /// The shop identificator is invalid. Please reconfig the client.
        /// </exception>
        public IEcwidLegacyClient Configure(int shopId, string shopOrderAuthId = null, string shopProductAuthId = null)
        {
            Validators.ShopIdValidate(shopId);

            Options.ShopId = shopId;

            if (!string.IsNullOrEmpty(shopOrderAuthId))
                Options.ShopOrderAuthId = shopOrderAuthId;
            if (!string.IsNullOrEmpty(shopProductAuthId))
                Options.ShopProductAuthId = shopProductAuthId;

            return this;
        }

        #region CORE

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <exception cref="LimitException">Limit overheat exception</exception>
        protected override async Task<T> GetApiResponceAsync<T>(Url baseUrl)
        {
            // Wait open window for request
            WaitLimit();
            return await base.GetApiResponceAsync<T>(baseUrl);
        }

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="LimitException">Limit overheat exception</exception>
        protected override async Task<T> GetApiResponceAsync<T>(Url baseUrl, CancellationToken cancellationToken)
        {
            // Wait open window for request
            WaitLimit();
            return await base.GetApiResponceAsync<T>(baseUrl, cancellationToken);
        }

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <exception cref="LimitException">Limit overheat exception</exception>
        protected override async Task<T> UpdateApiAsync<T>(Url baseUrl)
        {
            // Wait open window for request
            WaitLimit();
            return await base.UpdateApiAsync<T>(baseUrl);
        }

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="LimitException">Limit overheat exception</exception>
        protected override async Task<T> UpdateApiAsync<T>(Url baseUrl, CancellationToken cancellationToken)
        {
            // Wait open window for request
            WaitLimit();
            return await base.UpdateApiAsync<T>(baseUrl, cancellationToken);
        }

        /// <summary>
        /// Waits the limit.
        /// </summary>
        /// <exception cref="Ecwid.Tools.LimitException">Limit overheat exception</exception>
        private void WaitLimit()
        {
            var start = DateTime.Now;

            // Get agreement from limits service
            var agreement = LimitsService.Value.Tick();

            while (!agreement)
            {
                // If time limit is over
                if (start.AddSeconds(Options.MaxSecondsToWait) < DateTime.Now)
                    // TODO tests
                    throw new LimitException("Limit overheat exception");

                Task.Delay(Options.RetryInterval*1000).Wait();
                agreement = LimitsService.Value.Tick();
            }
        }

        #endregion
    }
}