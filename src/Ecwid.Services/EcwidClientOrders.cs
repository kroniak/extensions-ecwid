// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;
using Ecwid.Tools;
using Flurl;

namespace Ecwid.Services
{
    /// <summary>
    /// Ecwid API Client v3.
    /// </summary>
    public partial class EcwidClient
    {
        /// <summary>
        /// Gets the orders URL.
        /// </summary>
        /// <value>
        /// The orders URL.
        /// </value>
        private string OrdersUrl => Validators.ShopIdValidate(Options.ShopId) && Validators.TokenValidate(Options.Token)
            ? Options.ApiUrl
                .AppendPathSegments(Options.ShopId.ToString(), "orders")
                .SetQueryParam("token", Options.Token)
            : null;

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <exception cref="Flurl.Http.FlurlHttpException">Condition.</exception>
        public async Task<bool> CheckOrdersTokenAsync()
            => await CheckTokenAsync<SearchResult>(OrdersUrl);

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="Flurl.Http.FlurlHttpException">Condition.</exception>
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

        public Task<List<OrderEntry>> GetNewOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetNewOrdersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetNonPaidOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetNonPaidOrdersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetOrdersAsync(object query)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetOrdersAsync(OrdersQueryBuilder<OrderEntry, UpdateStatus> query)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetOrdersAsync(object query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetOrdersAsync(OrdersQueryBuilder<OrderEntry, UpdateStatus> query,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetOrdersCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetOrdersCountAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetOrdersPageAsync(OrdersQueryBuilder<OrderEntry, UpdateStatus> query)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetOrdersPageAsync(OrdersQueryBuilder<OrderEntry, UpdateStatus> query,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetPaidNotShippedOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetPaidNotShippedOrdersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetShippedNotDeliveredOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntry>> GetShippedNotDeliveredOrdersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateStatus> UpdateOrdersAsync(OrdersQueryBuilder<OrderEntry, UpdateStatus> query)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateStatus> UpdateOrdersAsync(OrdersQueryBuilder<OrderEntry, UpdateStatus> query,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}