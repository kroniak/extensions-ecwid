// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;
using Ecwid.Tools;
using Flurl;

namespace Ecwid.Services.Legacy
{
    /// <summary>
    /// Ecwid API Client v1 (Legacy).
    /// </summary>
    public partial class EcwidLegacyClient
    {
        /// <summary>
        /// Gets the orders URL.
        /// </summary>
        /// <value>
        /// The orders URL.
        /// </value>
        private string OrdersUrl
            => Validators.ShopIdValidate(Options.ShopId) && Validators.TokenValidate(Options.ShopOrderAuthId)
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
        public OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Orders
            => new OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders>(this);

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <exception cref="Flurl.Http.FlurlHttpException">Condition.</exception>
        public async Task<bool> CheckOrdersTokenAsync()
            => await CheckTokenAsync<LegacyOrderResponse<LegacyOrder>>(OrdersUrl);

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="Flurl.Http.FlurlHttpException">Condition.</exception>
        public async Task<bool> CheckOrdersTokenAsync(CancellationToken cancellationToken)
            => await CheckTokenAsync<LegacyOrderResponse<LegacyOrder>>(OrdersUrl, cancellationToken);

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        public async Task<int> GetOrdersCountAsync()
            => (await GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(OrdersUrl, new { limit = 0 })).Total;

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<int> GetOrdersCountAsync(CancellationToken cancellationToken)
            =>
                (await
                    GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(OrdersUrl, new { limit = 0 }, cancellationToken))
                    .Total;

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        public async Task<List<LegacyOrder>> GetOrdersAsync(OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query)
            => await GetOrdersAsync(OrdersUrl, query.Query);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<List<LegacyOrder>> GetOrdersAsync(OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query,
            CancellationToken cancellationToken)
            => await GetOrdersAsync(OrdersUrl, query.Query, cancellationToken);

        /// <summary>
        /// Gets the one page orders asynchronous. It ignores next url.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        public async Task<List<LegacyOrder>> GetOrdersPageAsync(
            OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query)
            => await GetOrdersPageAsync(OrdersUrl, query.Query);

        /// <summary>
        /// Gets the one page orders asynchronous. It ignores next url.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<List<LegacyOrder>> GetOrdersPageAsync(
            OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, CancellationToken cancellationToken)
            => await GetOrdersPageAsync(OrdersUrl, query.Query, cancellationToken);

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
        public async Task<List<LegacyOrder>> GetOrdersAsync(object query, CancellationToken cancellationToken)
            => await GetOrdersAsync(OrdersUrl, query, cancellationToken);

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        public async Task<List<LegacyOrder>> GetNewOrdersAsync()
            => await GetOrdersAsync(new { statuses = "NEW,AWAITING_PROCESSING" });

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<List<LegacyOrder>> GetNewOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new { statuses = "NEW,AWAITING_PROCESSING" }, cancellationToken);

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        public async Task<List<LegacyOrder>> GetNonPaidOrdersAsync()
            => await GetOrdersAsync(new { statuses = "QUEUED,AWAITING_PAYMENT" });

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
        /// Updates the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <exception cref="System.ArgumentNullException">Collection is null.</exception>
        public async Task<LegacyUpdatedOrders> UpdateOrdersAsync(
            OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query)
        {
            var responce = await UpdateApiAsync<LegacyOrderResponse<LegacyUpdatedOrder>>(OrdersUrl, query.Query);

            return responce != null ? new LegacyUpdatedOrders(responce.Orders) : new LegacyUpdatedOrders();
        }

        /// <summary>
        /// Updates the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="System.ArgumentNullException">Collection is null.</exception>
        public async Task<LegacyUpdatedOrders> UpdateOrdersAsync(
            OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, CancellationToken cancellationToken)
        {
            var responce =
                await UpdateApiAsync<LegacyOrderResponse<LegacyUpdatedOrder>>(OrdersUrl, query.Query, cancellationToken);

            return responce != null ? new LegacyUpdatedOrders(responce.Orders) : new LegacyUpdatedOrders();
        }

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="query">The query.</param>
        private async Task<List<LegacyOrder>> GetOrdersAsync(Url url, object query)
        {
            var response = query == null
                ? await GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(url)
                : await GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(url, query);

            var orders = new List<LegacyOrder>();

            if (response != null)
                orders.AddRange(response.Orders);

            while (response?.NextUrl != null)
            {
                response = await GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(response.NextUrl);
                if (response != null)
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
        private async Task<List<LegacyOrder>> GetOrdersAsync(Url url, object query, CancellationToken cancellationToken)
        {
            var response = query == null
                ? await GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(url, cancellationToken)
                : await GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(url, query, cancellationToken);

            var orders = new List<LegacyOrder>();

            if (response != null)
                orders.AddRange(response.Orders);

            while (response?.NextUrl != null)
            {
                response = await GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(response.NextUrl, cancellationToken);
                if (response != null)
                    orders.AddRange(response.Orders);
            }
            return orders;
        }

        /// <summary>
        /// Gets the one page of orders asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="query">The query.</param>
        private async Task<List<LegacyOrder>> GetOrdersPageAsync(Url url, object query)
        {
            var response = query == null
                ? await GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(url)
                : await GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(url, query);

            return response.Orders?.ToList() ?? new List<LegacyOrder>();
        }

        /// <summary>
        /// Gets the one page of orders asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task<List<LegacyOrder>> GetOrdersPageAsync(Url url, object query,
            CancellationToken cancellationToken)
        {
            var response = query == null
                ? await GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(url, cancellationToken)
                : await GetApiResponceAsync<LegacyOrderResponse<LegacyOrder>>(url, query, cancellationToken);

            return response.Orders?.ToList() ?? new List<LegacyOrder>();
        }
    }
}