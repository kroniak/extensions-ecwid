using System.Collections.Generic;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;

namespace Ecwid.Services
{
    /// <summary>
    /// Define general orders methods 
    /// </summary>
    public interface IEcwidOrders
    {
        /// <summary>
        /// Gets the orders count.
        /// </summary>
        Task<int> GetOrdersCountAsync();

        /// <summary>
        /// Gets all orders.
        /// </summary>
        Task<List<LegacyOrder>> GetOrdersAsync();

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        Task<List<LegacyOrder>> GetOrdersAsync(object query);

        /// <summary>
        /// Gets the new orders. This orders is not paid or processed
        /// </summary>
        Task<List<LegacyOrder>> GetNewOrdersAsync();

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        Task<List<LegacyOrder>> GetNonPaidOrdersAsync();
    }

    /// <summary>
    /// Define orders methods of Legacy API
    /// </summary>
    public interface IEcwidOrdersLegacy : IEcwidOrders
    {
        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        Task<bool> CheckOrdersAuthAsync();
    }
}
