using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;

namespace Ecwid.Services
{
    /// <summary>
    /// Public client API.
    /// </summary>
    internal interface IEcwidClient : IEcwidOrdersClient { }

    /// <summary>
    /// Public orders client API.
    /// </summary>
    public interface IEcwidOrdersClient : IEcwidOrdersClient<OrderEntry, UpdateStatus>
    {
        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        Task<bool> CheckAuthAsync();

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        Task<bool> CheckAuthAsync(CancellationToken cancellationToken);
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
        /// <value>
        /// The orders.
        /// </value>
        OrdersQueryBuilder<TOrder, TUpdateResponse> Orders { get; }

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
        Task<List<TOrder>> GetOrdersAsync(OrdersQueryBuilder<TOrder, TUpdateResponse> query, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the one page orders asynchronous. It ignores next url.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        Task<List<TOrder>> GetOrdersPageAsync(OrdersQueryBuilder<TOrder, TUpdateResponse> query);

        /// <summary>
        /// Gets the one page orders asynchronous. It ignores next url.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<TOrder>> GetOrdersPageAsync(OrdersQueryBuilder<TOrder, TUpdateResponse> query, CancellationToken cancellationToken);

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
        Task<TUpdateResponse> UpdateOrdersAsync(OrdersQueryBuilder<TOrder, TUpdateResponse> query, CancellationToken cancellationToken);
    }
}