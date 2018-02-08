// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;
using Flurl.Util;

namespace Ecwid.Legacy
{
    public partial class EcwidLegacyClient
    {
        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
        public async Task<bool> CheckOrdersTokenAsync()
            => await CheckOrdersTokenAsync(CancellationToken.None);

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
        public async Task<bool> CheckOrdersTokenAsync(CancellationToken cancellationToken)
            => await CheckTokenAsync<LegacyOrderResponse<LegacyOrder>>(GetUrl("orders"), cancellationToken);

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        /// <exception cref="EcwidLimitException">Limit overheat exception</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetNewOrdersAsync()
            => await GetNewOrdersAsync(CancellationToken.None);

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
            => await GetNonPaidOrdersAsync(CancellationToken.None);

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
            => await GetOrdersAsync(query, CancellationToken.None);

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
                await GetApiAsync<LegacyOrderResponse<LegacyOrder>>(GetUrl("orders"), query, cancellationToken);

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
                    await GetApiAsync<LegacyOrderResponse<LegacyOrder>>(response.NextUrl, cancellationToken);

                // ReSharper disable once ExceptionNotDocumentedOptional
                if (response.Orders != null)
                    result.AddRange(response.Orders);
            }
            return result;
        }

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
        public async Task<int> GetOrdersCountAsync()
            => await GetOrdersCountAsync(CancellationToken.None);

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
        public async Task<int> GetOrdersCountAsync(CancellationToken cancellationToken)
            =>
                (await
                    GetApiAsync<LegacyOrderResponse<LegacyOrder>>(GetUrl("orders"), new {limit = 0}, cancellationToken))
                    .Total;

        /// <summary>
        /// Gets the paid and not shipped orders asynchronous.
        /// </summary>
        /// <exception cref="EcwidLimitException">Limit overheat exception</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<LegacyOrder>> GetPaidNotShippedOrdersAsync()
            => await GetPaidNotShippedOrdersAsync(CancellationToken.None);

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
            => await GetShippedOrdersAsync(CancellationToken.None);

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
        /// Gets the orders query builder.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Orders
            => new OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders>(this);

        /// <summary>
        /// Updates the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        internal async Task<LegacyUpdatedOrders> UpdateOrdersAsync(
            OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, CancellationToken cancellationToken)
        {
            var responce =
                await
                    PostApiAsync<LegacyOrderResponse<LegacyUpdatedOrder>>(GetUrl("orders"), query.Query,
                        cancellationToken);

            return responce != null ? new LegacyUpdatedOrders(responce.Orders) : new LegacyUpdatedOrders();
        }
    }
}