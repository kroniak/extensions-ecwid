// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;

namespace Ecwid.Services
{
    /// <summary>
    /// Public client API.
    /// </summary>
    public interface IEcwidClient : IEcwidOrdersClient
    {
        /// <summary>
        /// Gets and sets the settings. Created by default.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        EcwidSettings Settings { get; set; }

        /// <summary>
        /// Gets and sets the credentials. Default value is <see langword="null" />.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        EcwidCredentials Credentials { get; set; }

        /// <summary>
        /// Configures with specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        IEcwidClient Configure(EcwidSettings settings);

        /// <summary>
        /// Configures the shop credentials.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="token">The authorization token.</param>
        /// <exception cref="EcwidConfigException">The shop identifier is invalid.</exception>
        /// <exception cref="EcwidConfigException">The authorization token is invalid.</exception>
        IEcwidClient Configure(int shopId, string token);

        /// <summary>
        /// Configures the shop credentials.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="token">The authorization token.</param>
        /// <param name="scope">List of permissions (API access scopes) given to the app, separated by space.</param>
        /// <exception cref="EcwidConfigException">The shop identifier is invalid.</exception>
        /// <exception cref="EcwidConfigException">The authorization token is invalid.</exception>
        IEcwidClient Configure(int shopId, string token, string scope);

        /// <summary>
        /// Configures with specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        IEcwidClient Configure(EcwidCredentials credentials);
    }

    /// <summary>
    /// Public orders client API.
    /// </summary>
    public interface IEcwidOrdersClient : IEcwidOrdersClient<OrderEntry, UpdateStatus>
    {
    }

    /// <summary>
    /// Shared public orders client API.
    /// </summary>
    /// <typeparam name="TOrder">The type of the order.</typeparam>
    /// <typeparam name="TUpdateResponse">The type of the update response.</typeparam>
    public interface IEcwidOrdersClient<TOrder, TUpdateResponse>
        where TOrder : BaseOrder
        where TUpdateResponse : class
    {
        /// <summary>
        /// Gets the orders query builder.
        /// </summary>
        /// <example>
        /// This sample shows how to gets last 10 orders by the <see cref="OrdersQueryBuilder{TOrder,TUpdateResponse}" />.
        /// <code>
        /// var orders = client.Configure(credentionals).Orders.Limit(10).GetAsync();
        /// </code>
        /// </example>
        /// <value>
        /// The orders.
        /// </value>
        OrdersQueryBuilder<TOrder, TUpdateResponse> Orders { get; }

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        Task<bool> CheckOrdersTokenAsync();

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<bool> CheckOrdersTokenAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        Task<int> GetOrdersCountAsync();

        /// <summary>
        /// Gets the orders count asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<int> GetOrdersCountAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        Task<List<TOrder>> GetOrdersAsync(OrdersQueryBuilder<TOrder, TUpdateResponse> query);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<TOrder>> GetOrdersAsync(OrdersQueryBuilder<TOrder, TUpdateResponse> query,
            CancellationToken cancellationToken);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        Task<List<TOrder>> GetOrdersAsync(object query);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<TOrder>> GetOrdersAsync(object query, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        Task<List<TOrder>> GetNewOrdersAsync();

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<TOrder>> GetNewOrdersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        Task<List<TOrder>> GetNonPaidOrdersAsync();

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<TOrder>> GetNonPaidOrdersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the paid and not shipped orders asynchronous.
        /// </summary>
        Task<List<TOrder>> GetPaidNotShippedOrdersAsync();

        /// <summary>
        /// Gets the paid and not shipped orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<TOrder>> GetPaidNotShippedOrdersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the shipped and not delivered orders asynchronous.
        /// </summary>
        Task<List<TOrder>> GetShippedNotDeliveredOrdersAsync();

        /// <summary>
        /// Gets the shipped and not delivered orders asynchronous.
        /// </summary>
        Task<List<TOrder>> GetShippedNotDeliveredOrdersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Update the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        Task<TUpdateResponse> UpdateOrdersAsync(OrdersQueryBuilder<TOrder, TUpdateResponse> query);

        /// <summary>
        /// Update the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TUpdateResponse> UpdateOrdersAsync(OrdersQueryBuilder<TOrder, TUpdateResponse> query,
            CancellationToken cancellationToken);
    }
}