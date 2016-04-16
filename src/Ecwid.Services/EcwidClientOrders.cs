// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;
using Flurl;

namespace Ecwid.Services
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
        /// Gets the orders query builder.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public OrdersQueryBuilder<OrderEntry, UpdateStatus> Orders
            => new OrdersQueryBuilder<OrderEntry, UpdateStatus>(this);

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
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetOrdersAsync(OrdersQueryBuilder<OrderEntry, UpdateStatus> query)
            => await GetOrdersAsync(query.Query);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetOrdersAsync(OrdersQueryBuilder<OrderEntry, UpdateStatus> query,
            CancellationToken cancellationToken)
            => await GetOrdersAsync(query.Query, cancellationToken);

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
        public async Task<List<OrderEntry>> GetShippedNotDeliveredOrdersAsync()
            => await GetOrdersAsync(new {fulfillmentStatus = "SHIPPED"});

        /// <summary>
        /// Gets the shipped and not delivered orders asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetShippedNotDeliveredOrdersAsync(CancellationToken cancellationToken)
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
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetOrdersAsync(object query)
        {
            var response = await GetApiResponseAsync<SearchResult>(OrdersUrl, query);

            return response.Orders?.ToList() ?? new List<OrderEntry>();
        }

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<OrderEntry>> GetOrdersAsync(object query, CancellationToken cancellationToken)
        {
            var response = await GetApiResponseAsync<SearchResult>(OrdersUrl, query, cancellationToken);

            return response.Orders?.ToList() ?? new List<OrderEntry>();
        }
    }
}