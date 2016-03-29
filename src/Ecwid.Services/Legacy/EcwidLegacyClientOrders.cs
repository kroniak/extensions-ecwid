using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;
using Flurl.Http;
using Ecwid.Tools;
using Flurl;

namespace Ecwid.Services.Legacy
{
    /// <summary>
    /// Ecwid API Client v1 (Legacy).
    /// </summary>
    /// <seealso cref="IEcwidOrdersClientLegacy" />
    public partial class EcwidLegacyClient : IEcwidOrdersClientLegacy
    {
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
        /// Gets the orders query builder.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public OrdersQueryBuilder Orders => new OrdersQueryBuilder(this);

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        public async Task<bool> CheckOrdersAuthAsync()
        {
            try
            {
                await GetApiResponceAsync<LegacyOrderResponse>(OrdersUrl, new { limit = 0 });
                return true;
            }
            catch (FlurlHttpException exception)
            {
                var status = exception.Call.Response.StatusCode;
                if (status == HttpStatusCode.Forbidden)
                    return false;
                throw;
            }
        }

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<bool> CheckOrdersAuthAsync(CancellationToken cancellationToken)
        {
            try
            {
                await GetApiResponceAsync<LegacyOrderResponse>(OrdersUrl, new { limit = 0 }, cancellationToken);
                return true;
            }
            catch (FlurlHttpException exception)
            {
                var status = exception.Call.Response.StatusCode;
                if (status == HttpStatusCode.Forbidden)
                    return false;
                throw;
            }
        }

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        public async Task<int> GetOrdersCountAsync()
            => (await GetApiResponceAsync<LegacyOrderResponse>(OrdersUrl, new { limit = 0 })).Total;

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<int> GetOrdersCountAsync(CancellationToken cancellationToken)
            => (await GetApiResponceAsync<LegacyOrderResponse>(OrdersUrl, new { limit = 0 }, cancellationToken)).Total;

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        public async Task<List<LegacyOrder>> GetOrdersAsync(OrdersQueryBuilder query)
            => await GetOrdersAsync(OrdersUrl, query.QueryParams);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<List<LegacyOrder>> GetOrdersAsync(OrdersQueryBuilder query, CancellationToken cancellationToken)
            => await GetOrdersAsync(OrdersUrl, query.QueryParams, cancellationToken);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        public async Task<List<LegacyOrder>> GetOrdersAsync(object query) => await GetOrdersAsync(OrdersUrl, query);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<List<LegacyOrder>> GetOrdersAsync(object query, CancellationToken cancellationToken) => await GetOrdersAsync(OrdersUrl, query, cancellationToken);

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        public async Task<List<LegacyOrder>> GetNewOrdersAsync() => await GetOrdersAsync(new { statuses = "NEW,AWAITING_PROCESSING" });

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<List<LegacyOrder>> GetNewOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new { statuses = "NEW,AWAITING_PROCESSING" }, cancellationToken);

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        public async Task<List<LegacyOrder>> GetNonPaidOrdersAsync() => await GetOrdersAsync(new { statuses = "QUEUED,AWAITING_PAYMENT" });

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<List<LegacyOrder>> GetNonPaidOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new { statuses = "QUEUED,AWAITING_PAYMENT" }, cancellationToken);

        /// <summary>
        /// Gets the paid and not shipped orders asynchronous.
        /// </summary>
        public async Task<List<LegacyOrder>> GetPaidNotShippedOrdersAsync()
            => await GetOrdersAsync(new { statuses = "PAID,ACCEPTED,NEW,AWAITING_PROCESSING" });

        /// <summary>
        /// Gets the paid and not shipped orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<List<LegacyOrder>> GetPaidNotShippedOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new { statuses = "PAID,ACCEPTED,NEW,AWAITING_PROCESSING" }, cancellationToken);

        /// <summary>
        /// Gets the shipped and not delivered orders asynchronous.
        /// </summary>
        public async Task<List<LegacyOrder>> GetShippedNotDeliveredOrdersAsync()
            => await GetOrdersAsync(new { statuses = "SHIPPED" });

        /// <summary>
        /// Gets the shipped and not delivered orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<List<LegacyOrder>> GetShippedNotDeliveredOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new { statuses = "SHIPPED" }, cancellationToken);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        private async Task<List<LegacyOrder>> GetOrdersAsync(Url url, object query)
        {
            var response = query == null ? await GetApiResponceAsync<LegacyOrderResponse>(url)
                : await GetApiResponceAsync<LegacyOrderResponse>(url, query);

            var orders = new List<LegacyOrder>(response.Orders);

            while (response.NextUrl != null)
            {
                response = await GetApiResponceAsync<LegacyOrderResponse>(response.NextUrl);
                orders.AddRange(response.Orders);
            }
            return orders;
        }

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        private async Task<List<LegacyOrder>> GetOrdersAsync(Url url, object query, CancellationToken cancellationToken)
        {
            var response = query == null ? await GetApiResponceAsync<LegacyOrderResponse>(url, cancellationToken)
                : await GetApiResponceAsync<LegacyOrderResponse>(url, query, cancellationToken);

            var orders = new List<LegacyOrder>(response.Orders);

            while (response.NextUrl != null)
            {
                response = await GetApiResponceAsync<LegacyOrderResponse>(response.NextUrl, cancellationToken);
                orders.AddRange(response.Orders);
            }
            return orders;
        }
    }
}