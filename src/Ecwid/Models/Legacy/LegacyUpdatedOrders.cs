// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// List of updated orders
    /// </summary>
    public class LegacyUpdatedOrders : List<LegacyUpdatedOrder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LegacyUpdatedOrders" /> class.
        /// </summary>
        public LegacyUpdatedOrders()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LegacyUpdatedOrders" /> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public LegacyUpdatedOrders(ICollection<LegacyUpdatedOrder> list)
        {
            if (list != null && list.Count > 0)
                AddRange(list);
        }
    }
}