// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;
using Flurl;
using Flurl.Util;

namespace Ecwid
{
    public partial class EcwidClient
    {
        #region Implementation of IEcwidOrdersClient

        /// <inheritdoc />
        public OrdersQueryBuilder<OrderEntry, UpdateStatus> Orders
            => new OrdersQueryBuilder<OrderEntry, UpdateStatus>(this);

        /// <inheritdoc />
        public async Task<bool> CheckOrdersTokenAsync()
            => await CheckOrdersTokenAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<bool> CheckOrdersTokenAsync(CancellationToken cancellationToken)
            => await CheckTokenAsync<SearchResult>(GetUrl("orders"), cancellationToken);

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetNewOrdersAsync()
            => await GetNewOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetNewOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {fulfillmentStatus = "AWAITING_PROCESSING"}, cancellationToken);

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetNonPaidOrdersAsync()
            => await GetNonPaidOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetNonPaidOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {paymentStatus = "AWAITING_PAYMENT"}, cancellationToken);

        /// <inheritdoc />
        public async Task<int> GetOrdersCountAsync()
            => await GetOrdersCountAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<int> GetOrdersCountAsync(CancellationToken cancellationToken)
            => (await GetApiAsync<SearchResult>(GetUrl("orders"), new {limit = 1}, cancellationToken)).Total;

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetPaidNotShippedOrdersAsync()
            => await GetPaidNotShippedOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetPaidNotShippedOrdersAsync(CancellationToken cancellationToken)
            =>
                await
                    GetOrdersAsync(new {paymentStatus = "PAID", fulfillmentStatus = "AWAITING_PROCESSING,PROCESSING"},
                        cancellationToken);

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetShippedOrdersAsync()
            => await GetShippedOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetShippedOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {fulfillmentStatus = "SHIPPED"}, cancellationToken);

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetOrdersAsync(object query)
            => await GetOrdersAsync(query, CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetOrdersAsync(object query, CancellationToken cancellationToken)
        {
            var response = await GetApiAsync<SearchResult>(GetUrl("orders"), query, cancellationToken);

            var result = response.Orders ?? new List<OrderEntry>();

            // return if response is null or response is full
            if (result.Count == 0) return result;
            if (response.Total == response.Count) return result;

            // if query is not null check it contains limit or offset.
            if (query?.ToKeyValuePairs().Count(pair => pair.Key == "limit" || pair.Key == "offset") > 0)
                return result;

            while (response.Count == response.Limit)
            {
                response =
                    await
                        GetApiAsync<SearchResult>(
                            GetUrl("orders").SetQueryParams(
                                new {offset = response.Offset + response.Limit}), query, cancellationToken);

                // ReSharper disable once ExceptionNotDocumentedOptional
                if (response.Orders != null)
                    result.AddRange(response.Orders);
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetIncompleteOrdersAsync()
            => await GetIncompleteOrdersAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<OrderEntry>> GetIncompleteOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {paymentStatus = "INCOMPLETE"}, cancellationToken);

        /// <inheritdoc />
        public async Task<OrderEntry> GetOrderAsync(int orderNumber)
            => await GetOrderAsync(orderNumber, CancellationToken.None);

        /// <inheritdoc />
        public async Task<OrderEntry> GetOrderAsync(int orderNumber, CancellationToken cancellationToken)
        {
            if (orderNumber <= 0)
                throw new ArgumentException(nameof(orderNumber));

            // ReSharper disable once RedundantAnonymousTypePropertyName
            var orders = await GetOrdersAsync(new {orderNumber = orderNumber}, cancellationToken);

            return orders.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<UpdateStatus> UpdateOrderAsync(OrderEntry order, CancellationToken cancellationToken)
        {
            if (order.OrderNumber <= 0) throw new ArgumentException("Order number is 0.");
            return await PutApiAsync<UpdateStatus>(GetUrl($"orders/{order.OrderNumber}"), order, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<UpdateStatus> UpdateOrderAsync(OrderEntry order)
            => await UpdateOrderAsync(order, CancellationToken.None);

        /// <inheritdoc />
        public async Task<DeleteStatus> DeleteOrderAsync(OrderEntry order, CancellationToken cancellationToken)
            => await DeleteOrderAsync(order.OrderNumber, cancellationToken);

        /// <inheritdoc />
        public async Task<DeleteStatus> DeleteOrderAsync(OrderEntry order)
            => await DeleteOrderAsync(order.OrderNumber);

        /// <inheritdoc />
        public async Task<DeleteStatus> DeleteOrderAsync(int orderNumber, CancellationToken cancellationToken)
        {
            if (orderNumber <= 0) throw new ArgumentException("Order number is 0.");
            return await DeleteApiAsync<DeleteStatus>(GetUrl($"orders/{orderNumber}"), cancellationToken);
        }

        /// <inheritdoc />
        public async Task<DeleteStatus> DeleteOrderAsync(int orderNumber)
            => await DeleteOrderAsync(orderNumber, CancellationToken.None);

        #endregion
    }
}