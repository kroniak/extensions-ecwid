// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Ecwid.Models.Legacy;

namespace Ecwid.Services.Legacy
{
    /// <summary>
    /// Public legacy client API.
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

        /// <summary>
        /// Configures the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        IEcwidLegacyClient Configure(EcwidLegacyOptions options);

        /// <summary>
        /// Configures the shop.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="shopOrderAuthId">The shop order authentication identifier.</param>
        /// <param name="shopProductAuthId">The shop product authentication identifier.</param>
        IEcwidLegacyClient Configure(int shopId, string shopOrderAuthId = null, string shopProductAuthId = null);
    }

    /// <summary>
    /// Public legacy orders client API.
    /// </summary>
    public interface IEcwidOrdersLegacyClient : IEcwidOrdersClient<LegacyOrder, LegacyUpdatedOrders>
    {
    }
}