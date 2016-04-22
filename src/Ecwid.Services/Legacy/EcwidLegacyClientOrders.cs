// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;
using Flurl;
using Flurl.Util;

namespace Ecwid.Legacy
{
    /// <summary>
    /// Ecwid API Client v1 (Legacy).
    /// </summary>
    public partial class EcwidLegacyClient
    {
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        private string OrdersUrl
        {
            get
            {
                if (Credentials?.OrderToken == null)
                    throw new EcwidConfigException("Credentials are null. Can not do a request.");
                return Settings.ApiUrl
                    .AppendPathSegments(Credentials.ShopId.ToString(), "orders")
                    .SetQueryParam("secure_auth_key", Credentials.OrderToken);
            }
        }

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
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<bool> CheckOrdersTokenAsync()
            => await CheckTokenAsync<LegacyOrderResponse<LegacyOrder>>(OrdersUrl);

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<bool> CheckOrdersTokenAsync(CancellationToken cancellationToken)
            => await CheckTokenAsync<LegacyOrderResponse<LegacyOrder>>(OrdersUrl, cancellationToken);

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<int> GetOrdersCountAsync()
            => (await GetApiResponseAsync<LegacyOrderResponse<LegacyOrder>>(OrdersUrl, new {limit = 0})).Total;

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<int> GetOrdersCountAsync(CancellationToken cancellationToken)
            =>
                (await
                    GetApiResponseAsync<LegacyOrderResponse<LegacyOrder>>(OrdersUrl, new {limit = 0}, cancellationToken))
                    .Total;

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        /// <exception cref="EcwidLimitException">Limit overheat exception</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetNewOrdersAsync()
            => await GetOrdersAsync(new {statuses = "NEW,AWAITING_PROCESSING"});

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidLimitException">Limit overheat exception</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetNewOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {statuses = "NEW,AWAITING_PROCESSING"}, cancellationToken);

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        /// <exception cref="EcwidLimitException">Limit overheat exception</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetNonPaidOrdersAsync()
            => await GetOrdersAsync(new {statuses = "QUEUED,AWAITING_PAYMENT"});

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidLimitException">Limit overheat exception</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetNonPaidOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {statuses = "QUEUED,AWAITING_PAYMENT"}, cancellationToken);

        /// <summary>
        /// Gets the paid and not shipped orders asynchronous.
        /// </summary>
        /// <exception cref="EcwidLimitException">Limit overheat exception</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetPaidNotShippedOrdersAsync()
            => await GetOrdersAsync(new {statuses = "PAID,ACCEPTED,NEW,AWAITING_PROCESSING,PROCESSING"});

        /// <summary>
        /// Gets the paid and not shipped orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidLimitException">Limit overheat exception</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetPaidNotShippedOrdersAsync(CancellationToken cancellationToken)
            =>
                await
                    GetOrdersAsync(new {statuses = "PAID,ACCEPTED,NEW,AWAITING_PROCESSING,PROCESSING"},
                        cancellationToken);

        /// <summary>
        /// Gets the shipped and not delivered orders asynchronous.
        /// </summary>
        /// <exception cref="EcwidLimitException">Limit overheat exception</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetShippedOrdersAsync()
            => await GetOrdersAsync(new {statuses = "SHIPPED"});

        /// <summary>
        /// Gets the shipped and not delivered orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidLimitException">Limit overheat exception</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetShippedOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {statuses = "SHIPPED"}, cancellationToken);

        /// <summary>
        /// Updates the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
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
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<LegacyUpdatedOrders> UpdateOrdersAsync(
            OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, CancellationToken cancellationToken)
        {
            var responce =
                await UpdateApiAsync<LegacyOrderResponse<LegacyUpdatedOrder>>(OrdersUrl, query.Query, cancellationToken);

            return responce != null ? new LegacyUpdatedOrders(responce.Orders) : new LegacyUpdatedOrders();
        }

        /// <summary>
        /// Gets the orders asynchronous. If <paramref name="query" /> contains limit or offset parameters gets only one page.
        /// </summary>
        /// <param name="query">
        /// The query. It's a list of key-value pairs. e.g.
        /// <code>new {fulfillmentStatus = "SHIPPED", limit = 100}</code> or Dictionary{string, object}
        /// </param>
        /// <exception cref="EcwidLimitException">Limit overheat exception</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetOrdersAsync(object query)
        {
            var response = await GetApiResponseAsync<LegacyOrderResponse<LegacyOrder>>(OrdersUrl, query);

            var result = response.Orders?.ToList() ?? new List<LegacyOrder>();

            // return if responce is null or response is full
            if (result.Count == 0 || response.Total == response.Count) return result;

            // if query is not null check it contains limit or offset.
            if (query?.ToKeyValuePairs().Count(pair => pair.Key == "limit" || pair.Key == "offset") > 0)
                return result;

            while (response.NextUrl != null)
            {
                response = await GetApiResponseAsync<LegacyOrderResponse<LegacyOrder>>(response.NextUrl);

                // ReSharper disable once ExceptionNotDocumentedOptional
                if (response.Orders != null)
                    result.AddRange(response.Orders);
            }
            return result;
        }

        /// <summary>
        /// Gets the orders asynchronous. If <paramref name="query" /> contains limit or offset parameters gets only one page.
        /// </summary>
        /// <param name="query">
        /// The query. It's a list of key-value pairs. e.g.
        /// <code>new {fulfillmentStatus = "SHIPPED", limit = 100}</code> or Dictionary{string, object}
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetOrdersAsync(object query, CancellationToken cancellationToken)
        {
            var response =
                await GetApiResponseAsync<LegacyOrderResponse<LegacyOrder>>(OrdersUrl, query, cancellationToken);

            var result = response.Orders?.ToList() ?? new List<LegacyOrder>();

            // return if responce is null or response is full
            if (result.Count == 0 || response.Total == response.Count)
                return result;

            // if query is not null check it contains limit or offset.
            if (query?.ToKeyValuePairs().Count(pair => pair.Key == "limit" || pair.Key == "offset") > 0)
                return result;

            while (response.NextUrl != null)
            {
                response =
                    await GetApiResponseAsync<LegacyOrderResponse<LegacyOrder>>(response.NextUrl, cancellationToken);

                // ReSharper disable once ExceptionNotDocumentedOptional
                if (response.Orders != null)
                    result.AddRange(response.Orders);
            }
            return result;
        }
    }
}