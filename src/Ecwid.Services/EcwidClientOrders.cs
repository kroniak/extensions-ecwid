// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

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
    /// <summary>
    /// Ecwid API Client v3.
    /// </summary>
    public partial class EcwidClient
    {
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        private string OrdersUrl
        {
            get
            {
                if (Credentials == null)
                    throw new EcwidConfigException("Credentials are null. Can not do a request.");
                return Settings.ApiUrl
                    .AppendPathSegments(Credentials.ShopId.ToString(), "orders")
                    .SetQueryParam("token", Credentials.Token);
            }
        }

        #region Implementation of IEcwidOrdersClient

        /// <summary>
        /// Gets the orders query builder.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public OrdersQueryBuilder<OrderEntry, UpdateStatus> Orders
            => new OrdersQueryBuilder<OrderEntry, UpdateStatus>(this);

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<bool> CheckOrdersTokenAsync()
            => await CheckTokenAsync<SearchResult>(OrdersUrl);

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<bool> CheckOrdersTokenAsync(CancellationToken cancellationToken)
            => await CheckTokenAsync<SearchResult>(OrdersUrl, cancellationToken);

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetNewOrdersAsync()
            => await GetOrdersAsync(new {fulfillmentStatus = "AWAITING_PROCESSING"});

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetNewOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {fulfillmentStatus = "AWAITING_PROCESSING"}, cancellationToken);

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetNonPaidOrdersAsync()
            => await GetOrdersAsync(new {paymentStatus = "AWAITING_PAYMENT"});

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetNonPaidOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {paymentStatus = "AWAITING_PAYMENT"}, cancellationToken);

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<int> GetOrdersCountAsync()
            => (await GetApiResponseAsync<SearchResult>(OrdersUrl, new {limit = 1})).Total;

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<int> GetOrdersCountAsync(CancellationToken cancellationToken)
            => (await GetApiResponseAsync<SearchResult>(OrdersUrl, new {limit = 1}, cancellationToken)).Total;

        /// <summary>
        /// Gets the paid and not shipped orders asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetPaidNotShippedOrdersAsync()
            => await GetOrdersAsync(new {paymentStatus = "PAID", fulfillmentStatus = "AWAITING_PROCESSING,PROCESSING"});

        /// <summary>
        /// Gets the paid and not shipped orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetPaidNotShippedOrdersAsync(CancellationToken cancellationToken)
            =>
                await
                    GetOrdersAsync(new {paymentStatus = "PAID", fulfillmentStatus = "AWAITING_PROCESSING,PROCESSING"},
                        cancellationToken);

        /// <summary>
        /// Gets the shipped and not delivered orders asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetShippedOrdersAsync()
            => await GetOrdersAsync(new {fulfillmentStatus = "SHIPPED"});

        /// <summary>
        /// Gets the shipped and not delivered orders asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetShippedOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {fulfillmentStatus = "SHIPPED"}, cancellationToken);

        public Task<UpdateStatus> UpdateOrdersAsync(OrdersQueryBuilder<OrderEntry, UpdateStatus> query)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateStatus> UpdateOrdersAsync(OrdersQueryBuilder<OrderEntry, UpdateStatus> query,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the orders asynchronous. If <paramref name="query" /> contains limit or offset parameters gets only one page.
        /// </summary>
        /// <param name="query">
        /// The query. It's a list of key-value pairs. e.g.
        /// <code>new {fulfillmentStatus = "SHIPPED", limit = 100}</code> or Dictionary{string, object}
        /// </param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetOrdersAsync(object query)
        {
            var response = await GetApiResponseAsync<SearchResult>(OrdersUrl, query);

            var result = response.Orders?.ToList() ?? new List<OrderEntry>();

            // return if responce is null or response is full
            if (result.Count == 0 || response.Total == response.Count) return result;

            // if query is not null check it contains limit or offset.
            if (query?.ToKeyValuePairs().Count(pair => pair.Key == "limit" || pair.Key == "offset") > 0)
                return result;

            while (response.Count == response.Limit)
            {
                response =
                    await
                        GetApiResponseAsync<SearchResult>(
                            OrdersUrl.SetQueryParams(
                                new {offset = response.Offset + response.Limit}), query);

                // ReSharper disable once ExceptionNotDocumentedOptional
                if (response.Orders != null) result.AddRange(response.Orders);
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
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetOrdersAsync(object query, CancellationToken cancellationToken)
        {
            var response = await GetApiResponseAsync<SearchResult>(OrdersUrl, query, cancellationToken);

            var result = response.Orders?.ToList() ?? new List<OrderEntry>();

            // return if responce is null or response is full
            if (result.Count == 0 || response.Total == response.Count)
                return result;

            // if query is not null check it contains limit or offset.
            if (query?.ToKeyValuePairs().Count(pair => pair.Key == "limit" || pair.Key == "offset") > 0)
                return result;

            while (response.Count == response.Limit)
            {
                response =
                    await
                        GetApiResponseAsync<SearchResult>(
                            OrdersUrl.SetQueryParams(
                                new {offset = response.Offset + response.Limit}), query, cancellationToken);

                // ReSharper disable once ExceptionNotDocumentedOptional
                if (response.Orders != null)
                    result.AddRange(response.Orders);
            }

            return result;
        }

        /// <summary>
        /// Gets the incomplete orders asynchronous. This orders is new or is not processed.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetIncompleteOrdersAsync()
            => await GetOrdersAsync(new {paymentStatus = "INCOMPLETE"});


        /// <summary>
        /// Gets the incomplete orders asynchronous. This orders is new or is not processed.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        public async Task<List<OrderEntry>> GetIncompleteOrdersAsync(CancellationToken cancellationToken)
            => await GetOrdersAsync(new {paymentStatus = "INCOMPLETE"}, cancellationToken);

        /// <summary>
        /// Gets the one order asynchronous.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="orderNumber" /> is out of range.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        public async Task<OrderEntry> GetOrderAsync(int orderNumber)
        {
            if (orderNumber <= 0) throw new ArgumentOutOfRangeException(nameof(orderNumber));

            // ReSharper disable once RedundantAnonymousTypePropertyName
            var orders = await GetOrdersAsync(new {orderNumber = orderNumber});

            return orders.Count == 0 ? null : orders.FirstOrDefault();
        }

        /// <summary>
        /// Gets the one orders asynchronous.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="orderNumber" /> is out of range.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        public async Task<OrderEntry> GetOrderAsync(int orderNumber, CancellationToken cancellationToken)
        {
            if (orderNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(orderNumber));

            // ReSharper disable once RedundantAnonymousTypePropertyName
            var orders = await GetOrdersAsync(new {orderNumber = orderNumber}, cancellationToken);

            return orders.Count == 0 ? null : orders.FirstOrDefault();
        }

        #endregion
    }
}