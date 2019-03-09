// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;

namespace Ecwid.Legacy.Models
{
    /// <inheritdoc />
    public class LegacyUpdatedOrders : List<LegacyUpdatedOrder>
    {
        /// <inheritdoc />
        public LegacyUpdatedOrders()
        {
        }

        /// <inheritdoc />
        public LegacyUpdatedOrders(IEnumerable<LegacyUpdatedOrder> list)
        {
            if (list != null) AddRange(list);
        }
    }
}