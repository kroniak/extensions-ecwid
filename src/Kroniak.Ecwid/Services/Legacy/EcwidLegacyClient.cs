using Ecwid.Misc;
using Flurl;
using Flurl.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecwid.Services.Legacy
{
    /// <summary>
    /// Ecwid API Service v1 (Legacy)
    /// </summary>
    /// <seealso cref="IEcwidOrdersLegacy" />
    public partial class EcwidLegacyClient
    {
        /// <summary>
        /// The shared limits service for API limits
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
        /// Gets the orders URL.
        /// </summary>
        /// <value>
        /// The orders URL.
        /// </value>
        private string OrdersUrl => Validators.ShopIdValidate(Options.ShopId) && Validators.ShopAuthValidate(Options.ShopOrderAuthId)
            ? Options.ApiUrl
                .AppendPathSegments(Options.ShopId.ToString(), "orders")
                .SetQueryParam("secure_auth_key", Options.ShopOrderAuthId)
            : null;

        /// <summary>
        /// Configures the specified options.
        /// </summary>
        /// <param name="opt">The opt.</param>
        public EcwidLegacyClient Configure(EcwidLegacyOptions opt)
        {
            if (!Validators.ShopIdValidate(opt.ShopId))
                return null;

            Options = opt;
            return this;
        }

        /// <summary>
        /// Configures the shop.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="shopOrderAuthId">The shop order authentication identifier.</param>
        /// <param name="shopProductAuthId">The shop product authentication identifier.</param>
        public EcwidLegacyClient ConfigureShop(int shopId, string shopOrderAuthId, string shopProductAuthId)
        {
            if (!Validators.ShopIdValidate(shopId))
                return null;

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
        /// <param name="query">The query.</param>
        private async Task<T> GetApiResponceAsync<T>(Url baseUrl, object query)
            where T : class
        {
            var url = query != null ? baseUrl.SetQueryParams(query) : baseUrl;

            return await GetApiResponceAsync<T>(url);
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
                Thread.Sleep(Options.RetryInterval * 1000);
                agreement = LimitsService.Tick();
            }

            return true;
        }
        #endregion
    }
}