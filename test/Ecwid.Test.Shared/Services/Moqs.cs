// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using Ecwid.Models;

namespace Ecwid.Test.Services
{
    internal abstract class Moqs
    {
        /// <summary>
        /// Moqs the responce with one order.
        /// </summary>
        public static SearchResult MockSearchResultWithLimit1
            => new SearchResult {Count = 1, Total = 100, Limit = 100, Offset = 0, Orders = MockOrders(1)};

        /// <summary>
        /// Moqs the responce with one order.
        /// </summary>
        public static SearchResult MockSearchResultWithOneOrder
            => new SearchResult {Count = 1, Total = 1, Limit = 100, Offset = 0, Orders = MockOrders(1)};

        /// <summary>
        /// Moqs the responce with many order.
        /// </summary>
        public static SearchResult MockSearchResultWithManyOrder
            => new SearchResult {Count = 10, Total = 10, Limit = 100, Offset = 0, Orders = MockOrders(10)};

        /// <summary>
        /// Mocks the legacy order response with many order and pages.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static SearchResult MockSearchResultWithManyOrderAndPages(int limit, int offset, int count)
            => new SearchResult {Count = limit, Total = 300, Limit = limit, Offset = offset, Orders = MockOrders(count)};

        /// <summary>
        /// Moqs the orders.
        /// </summary>
        /// <param name="count">The count.</param>
        private static IList<OrderEntry> MockOrders(int count)
        {
            var orders = new List<OrderEntry>();
            var order = new OrderEntry
            {
                OrderNumber = 111111,
                VendorOrderNumber = "111111",
                TrackingNumber = "EA222222222222222",
                CreateDate = DateTime.Parse("2016-03-24 05:51:53"),
                PaymentStatus = "ACCEPTED",
                FulfillmentStatus = "SHIPPED",
                PaymentMethod = "PayPal",
                Email = "test@test.test"
            };
            count.Times(() => orders.Add(order));

            return orders;
        }
    }
}