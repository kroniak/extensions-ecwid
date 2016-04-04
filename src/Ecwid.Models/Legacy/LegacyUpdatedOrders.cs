using System.Collections.Generic;

namespace Ecwid.Models.Legacy
{
    /// <summary>
    /// 
    /// </summary>
    public class LegacyUpdatedOrders : List<LegacyUpdatedOrder>
    {
        public LegacyUpdatedOrders() { }

        public LegacyUpdatedOrders(ICollection<LegacyUpdatedOrder> list)
        {
            if (list != null && list.Count > 0)
                AddRange(list);
        }
    }
}