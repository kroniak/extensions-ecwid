// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;

namespace Ecwid.Models.Legacy
{
    /// <inheritdoc />
    public class LegacyUpdatedOrders : List<LegacyUpdatedOrder>
    {
        /// <inheritdoc />
        public LegacyUpdatedOrders()
        {
        }

        /// <inheritdoc />
        public LegacyUpdatedOrders(ICollection<LegacyUpdatedOrder> list)
        {
            if (list != null && list.Count > 0)
                AddRange(list);
        }
    }
}