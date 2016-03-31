using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;

namespace Ecwid.Services
{
    /// <summary>
    /// Public API for client
    /// </summary>
    public interface IEcwidClient { }

    /// <summary>
    /// Public orders API for client
    /// </summary>
    public interface IEcwidOrdersClient : IEcwidClient
    {
        /// <summary>
        /// Gets the orders query builder.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        OrdersQueryBuilder Orders { get; }

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
        Task<List<LegacyOrder>> GetOrdersAsync(OrdersQueryBuilder query);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<LegacyOrder>> GetOrdersAsync(OrdersQueryBuilder query, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        Task<List<LegacyOrder>> GetOrdersAsync(object query);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<LegacyOrder>> GetOrdersAsync(object query, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        Task<List<LegacyOrder>> GetNewOrdersAsync();

        /// <summary>
        /// Gets the new orders asynchronous. This orders is new or is not processed.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<LegacyOrder>> GetNewOrdersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        Task<List<LegacyOrder>> GetNonPaidOrdersAsync();

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<LegacyOrder>> GetNonPaidOrdersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the paid and not shipped orders asynchronous.
        /// </summary>
        Task<List<LegacyOrder>> GetPaidNotShippedOrdersAsync();

        /// <summary>
        /// Gets the paid and not shipped orders asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<LegacyOrder>> GetPaidNotShippedOrdersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the shipped and not delivered orders asynchronous.
        /// </summary>
        Task<List<LegacyOrder>> GetShippedNotDeliveredOrdersAsync();

        /// <summary>
        /// Gets the shipped and not delivered orders asynchronous.
        /// </summary>
        Task<List<LegacyOrder>> GetShippedNotDeliveredOrdersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Update the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        Task<List<LegacyUpdatedOrder>> UpdateOrdersAsync(OrdersQueryBuilder query);

        /// <summary>
        /// Update the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<LegacyUpdatedOrder>> UpdateOrdersAsync(OrdersQueryBuilder query, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Public orders API of legacy client
    /// </summary>
    public interface IEcwidOrdersClientLegacy : IEcwidOrdersClient
    {
        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        Task<bool> CheckOrdersAuthAsync();

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        Task<bool> CheckOrdersAuthAsync(CancellationToken cancellationToken);
    }
}