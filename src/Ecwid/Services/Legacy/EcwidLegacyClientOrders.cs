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
        /// <inheritdoc />
        public async Task<bool> CheckOrdersTokenAsync()
            => await CheckOrdersTokenAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<bool> CheckOrdersTokenAsync(CancellationToken cancellationToken)
            => await CheckTokenAsync<LegacyOrderResponse<LegacyOrder>>(GetUrl("orders"), cancellationToken);

        /// <inheritdoc />
        public async Task<IEnumerable<LegacyOrder>> GetNewOrdersAsync()
            => await GetNewOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<LegacyOrder>> GetNewOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {statuses = "NEW,AWAITING_PROCESSING"}, cancellationToken);

        /// <inheritdoc />
        public async Task<IEnumerable<LegacyOrder>> GetNonPaidOrdersAsync()
            => await GetNonPaidOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<LegacyOrder>> GetNonPaidOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {statuses = "QUEUED,AWAITING_PAYMENT"}, cancellationToken);

        /// <inheritdoc />
        public async Task<IEnumerable<LegacyOrder>> GetOrdersAsync(object query)
            => await GetOrdersAsync(query, CancellationToken.None);

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
        public async Task<int> GetOrdersCountAsync()
            => await GetOrdersCountAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<int> GetOrdersCountAsync(CancellationToken cancellationToken)
            =>
                (await
                    GetApiAsync<LegacyOrderResponse<LegacyOrder>>(GetUrl("orders"), new {limit = 0}, cancellationToken))
                .Total;

        /// <inheritdoc />
        public async Task<IEnumerable<LegacyOrder>> GetPaidNotShippedOrdersAsync()
            => await GetPaidNotShippedOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<LegacyOrder>> GetPaidNotShippedOrdersAsync(CancellationToken cancellationToken)
            =>
                await
                    GetOrdersAsync(new {statuses = "PAID,ACCEPTED,NEW,AWAITING_PROCESSING,PROCESSING"},
                        cancellationToken);

        /// <inheritdoc />
        public async Task<IEnumerable<LegacyOrder>> GetShippedOrdersAsync()
            => await GetShippedOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<LegacyOrder>> GetShippedOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {statuses = "SHIPPED"}, cancellationToken);

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