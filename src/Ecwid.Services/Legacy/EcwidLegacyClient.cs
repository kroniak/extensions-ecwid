using Ecwid.Tools;
using Flurl;
using Flurl.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecwid.Services.Legacy
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
        // TODO may be bug!
        public EcwidLegacyOptions Options { get; private set; } = new EcwidLegacyOptions();

        /// <summary>
        /// Configures the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
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
        /// <exception cref="System.InvalidOperationException">Limit overheat exception</exception>
        private async Task<T> GetApiResponceAsync<T>(Url baseUrl)
            where T : class
        {
            // Wait open window for request
            if (!WaitLimit())
                throw new InvalidOperationException("Limit overheat exception");

            return await baseUrl.GetJsonAsync<T>();
        }

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="System.InvalidOperationException">Limit overheat exception</exception>
        private async Task<T> GetApiResponceAsync<T>(Url baseUrl, CancellationToken cancellationToken)
            where T : class
        {
            // Wait open window for request
            if (!WaitLimit())
                throw new InvalidOperationException("Limit overheat exception");

            return await baseUrl.GetJsonAsync<T>(cancellationToken);
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
        /// <returns></returns>
        private async Task<T> GetApiResponceAsync<T>(Url baseUrl, object query, CancellationToken cancellationToken)
            where T : class
        {
            var url = query != null ? baseUrl.SetQueryParams(query) : baseUrl;

            return await GetApiResponceAsync<T>(url, cancellationToken);
        }

        /// <summary>
        /// Waits the limit.
        /// </summary>
        private bool WaitLimit()
        {
            var start = DateTime.Now;

            // Get agreement from limits service
            var agreement = LimitsService.Tick();

            while (!agreement)
            {
                // If time limit is over
                if (start.AddSeconds(Options.MaxSecondsToWait) < DateTime.Now)
                    return false;
                Task.Delay(Options.RetryInterval * 1000).Wait();
                agreement = LimitsService.Tick();
            }

            return true;
        }
        #endregion
    }
}