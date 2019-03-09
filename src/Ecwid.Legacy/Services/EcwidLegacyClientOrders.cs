// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Legacy.Models;
using Flurl.Util;

// ReSharper disable PossibleMultipleEnumeration

namespace Ecwid.Legacy
{
    public partial class EcwidLegacyClient
    {
        /// <inheritdoc />
        public Task<bool> CheckOrdersTokenAsync()
            => CheckOrdersTokenAsync(CancellationToken.None);

        /// <inheritdoc />
        public Task<bool> CheckOrdersTokenAsync(CancellationToken cancellationToken)
            => CheckTokenAsync<LegacyOrderResponse<LegacyOrder>>(GetUrl("orders"), cancellationToken);

        /// <inheritdoc />
        public Task<IEnumerable<LegacyOrder>> GetNewOrdersAsync()
            => GetNewOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public Task<IEnumerable<LegacyOrder>> GetNewOrdersAsync(CancellationToken cancellationToken)
            => GetOrdersAsync(new {statuses = "NEW,AWAITING_PROCESSING"}, cancellationToken);

        /// <inheritdoc />
        public Task<IEnumerable<LegacyOrder>> GetNonPaidOrdersAsync()
            => GetNonPaidOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public Task<IEnumerable<LegacyOrder>> GetNonPaidOrdersAsync(CancellationToken cancellationToken)
            => GetOrdersAsync(new {statuses = "QUEUED,AWAITING_PAYMENT"}, cancellationToken);

        /// <inheritdoc />
        public Task<IEnumerable<LegacyOrder>> GetOrdersAsync(object query)
            => GetOrdersAsync(query, CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<LegacyOrder>> GetOrdersAsync(object query, CancellationToken cancellationToken)
        {
            var response =
                await GetApiAsync<LegacyOrderResponse<LegacyOrder>>(GetUrl("orders"), query, cancellationToken);

            var result = response.Orders ?? Enumerable.Empty<LegacyOrder>();

            // return if response is null or response is full
            if (result.FirstOrDefault() == null)
            {
                return result;
            }

            if (response.Total == response.Count)
            {
                return result;
            }

            // if query is not null check it contains limit or offset.
            if (query?.ToKeyValuePairs().Count(pair => pair.Key == "limit" || pair.Key == "offset") > 0)
            {
                return result;
            }

            while (response.NextUrl != null)
            {
                response =
                    await GetApiAsync<LegacyOrderResponse<LegacyOrder>>(response.NextUrl, cancellationToken);

                // ReSharper disable once ExceptionNotDocumentedOptional
                if (response.Orders != null)
                {
                    result = result.Concat(response.Orders);
                }
            }

            return result;
        }

        /// <inheritdoc />
        public Task<int> GetOrdersCountAsync()
            => GetOrdersCountAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<int> GetOrdersCountAsync(CancellationToken cancellationToken)
            =>
                (await
                    GetApiAsync<LegacyOrderResponse<LegacyOrder>>(GetUrl("orders"), new {limit = 0}, cancellationToken))
                .Total;

        /// <inheritdoc />
        public Task<IEnumerable<LegacyOrder>> GetPaidNotShippedOrdersAsync()
            => GetPaidNotShippedOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public Task<IEnumerable<LegacyOrder>> GetPaidNotShippedOrdersAsync(CancellationToken cancellationToken)
            =>
                GetOrdersAsync(new {statuses = "PAID,ACCEPTED,NEW,AWAITING_PROCESSING,PROCESSING"},
                    cancellationToken);

        /// <inheritdoc />
        public Task<IEnumerable<LegacyOrder>> GetShippedOrdersAsync()
            => GetShippedOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public Task<IEnumerable<LegacyOrder>> GetShippedOrdersAsync(CancellationToken cancellationToken)
            => GetOrdersAsync(new {statuses = "SHIPPED"}, cancellationToken);

        /// <inheritdoc />
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
            var response =
                await
                    PostApiAsync<LegacyOrderResponse<LegacyUpdatedOrder>>(GetUrl("orders"), query.Query,
                        cancellationToken);

            return response != null ? new LegacyUpdatedOrders(response.Orders) : new LegacyUpdatedOrders();
        }
    }
}