using Ecwid.Tools;
using Flurl;
using Flurl.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecwid.Services
{
    /// <summary>
    /// Ecwid API Client v1 (Legacy).
    /// </summary>
    /// <seealso cref="IEcwidOrdersClientLegacy" />
    public partial class EcwidLegacyClient
    {
        /// <summary>
        /// The shared limits service for API limits.
        /// </summary>
        private static readonly LimitsService LimitsService = new LimitsService();

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
        /// <exception cref="ArgumentException">The shop identificator is null. Please reconfig the client.
        /// or
        /// The shop identificator is invalid. Please reconfig the client.</exception>
        public EcwidLegacyClient Configure(EcwidLegacyOptions options)
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
        /// <exception cref="ArgumentException">The shop identificator is null. Please reconfig the client.
        /// or
        /// The shop identificator is invalid. Please reconfig the client.</exception>
        public EcwidLegacyClient ConfigureShop(int shopId, string shopOrderAuthId = null, string shopProductAuthId = null)
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
        private async Task<T> GetApiResponceAsync<T>(Url baseUrl)
            where T : class
        {
            // Wait open window for request
            WaitLimit();
            return await baseUrl.GetJsonAsync<T>();
        }

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task<T> GetApiResponceAsync<T>(Url baseUrl, CancellationToken cancellationToken)
            where T : class
        {
            // Wait open window for request
            WaitLimit();
            return await baseUrl.GetJsonAsync<T>(cancellationToken);
        }

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        private async Task<T> UpdateApiAsync<T>(Url baseUrl)
            where T : class
        {
            // Wait open window for request
            WaitLimit();
            return await baseUrl.PostAsync().ReceiveJson<T>();
        }

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task<T> UpdateApiAsync<T>(Url baseUrl, CancellationToken cancellationToken)
            where T : class
        {
            // Wait open window for request
            WaitLimit();
            return await baseUrl.PostAsync(cancellationToken).ReceiveJson<T>();
        }

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        private async Task<T> UpdateApiAsync<T>(Url baseUrl, object query)
            where T : class
        {
            var url = query != null ? baseUrl.SetQueryParams(query) : baseUrl;
            return await UpdateApiAsync<T>(url);
        }

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task<T> UpdateApiAsync<T>(Url baseUrl, object query, CancellationToken cancellationToken)
            where T : class
        {
            var url = query != null ? baseUrl.SetQueryParams(query) : baseUrl;
            return await UpdateApiAsync<T>(url, cancellationToken);
        }

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        private async Task<T> GetApiResponceAsync<T>(Url baseUrl, object query)
            where T : class
        {
            var url = query != null ? baseUrl.SetQueryParams(query) : baseUrl;

            return await GetApiResponceAsync<T>(url);
        }

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task<T> GetApiResponceAsync<T>(Url baseUrl, object query, CancellationToken cancellationToken)
            where T : class
        {
            var url = query != null ? baseUrl.SetQueryParams(query) : baseUrl;

            return await GetApiResponceAsync<T>(url, cancellationToken);
        }

        /// <summary>
        /// Waits the limit.
        /// </summary>
        /// <exception cref="Ecwid.Tools.LimitException">Limit overheat exception</exception>
        private void WaitLimit()
        {
            var start = DateTime.Now;

            // Get agreement from limits service
            var agreement = LimitsService.Tick();

            while (!agreement)
            {
                // If time limit is over
                if (start.AddSeconds(Options.MaxSecondsToWait) < DateTime.Now)
                    // TODO tests
                    throw new LimitException("Limit overheat exception");

                Task.Delay(Options.RetryInterval * 1000).Wait();
                agreement = LimitsService.Tick();
            }
        }
        #endregion
    }
}