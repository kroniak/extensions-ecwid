// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Ecwid.Legacy.Models;

namespace Ecwid.Legacy
{
    /// <inheritdoc />
    public interface IEcwidOrdersLegacyClient : IEcwidOrdersClient<LegacyOrder, LegacyUpdatedOrders>
    {
    }
}