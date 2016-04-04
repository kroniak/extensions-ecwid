using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;

namespace Ecwid.Services.Legacy
{
    /// <summary>
    /// Public client API.
    /// </summary>
    public interface IEcwidLegacyClient : IEcwidOrdersLegacyClient
    {
        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        EcwidLegacyOptions Options { get; }
    }

    /// <summary>
    /// Public orders client API of legacy client.
    /// </summary>
    public interface IEcwidOrdersLegacyClient : IEcwidOrdersClient<LegacyOrder, LegacyUpdatedOrders>
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