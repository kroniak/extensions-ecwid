// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Ecwid.Models.Legacy;

namespace Ecwid.Test.Services.Legacy
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    internal abstract class Mocks
    {
        /// <summary>
        /// Mocks the response with one order.
        /// </summary>
        public static LegacyOrderResponse<LegacyOrder> MockLegacyOrderResponseWithOneOrder
            =>
                new LegacyOrderResponse<LegacyOrder>
                {
                    Count = 1,
                    Total = 1,
                    NextUrl = null,
                    Orders = new List<LegacyOrder>(MockLegacyOrders(1))
                };

        /// <summary>
        /// Gets the mock legacy order response for update.
        /// </summary>
        /// <value>
        /// The mock legacy order response for update.
        /// </value>
        public static LegacyOrderResponse<LegacyUpdatedOrder> MockLegacyOrderResponseForUpdate
            => new LegacyOrderResponse<LegacyUpdatedOrder> {Count = 1, Total = 10, Orders = MockBaseOrders(1)};

        /// <summary>
        /// Mocks the response with many order.
        /// </summary>
        public static LegacyOrderResponse<LegacyOrder> MockLegacyOrderResponseWithManyOrder(int count = 10)
            =>
                new LegacyOrderResponse<LegacyOrder>
                {
                    Count = count,
                    Total = count * 2,
                    NextUrl = null,
                    Orders = new List<LegacyOrder>(MockLegacyOrders(count))
                };

        /// <summary>
        /// Mocks the legacy order response with many order and pages.
        /// </summary>
        /// <param name="nextUrl">The next URL.</param>
        /// <param name="count">The count.</param>
        public static LegacyOrderResponse<LegacyOrder> MockLegacyOrderResponseWithManyOrderAndPages(string nextUrl,
            int count = 200)
            =>
                new LegacyOrderResponse<LegacyOrder>
                {
                    Count = count,
                    Total = count * 2,
                    NextUrl = nextUrl,
                    Orders = new List<LegacyOrder>(MockLegacyOrders(count))
                };

        /// <summary>
        /// Mocks the orders.
        /// </summary>
        /// <param name="count">The count.</param>
        private static IEnumerable<LegacyOrder> MockLegacyOrders(int count)
        {
            var order = new LegacyOrder
            {
                Number = 111111,
                VendorNumber = "111111",
                ShippingTrackingCode = "EA222222222222222",
                Created = DateTime.Parse("2016-03-24 05:51:53"),
                PaymentStatus = "ACCEPTED",
                FulfillmentStatus = "SHIPPED",
                ShippingMethod = "EMS",
                PaymentMethod = "PayPal",
                CustomerEmail = "test@test.test"
            };

            return Enumerable.Range(0, count).Select(_ => order);
        }

        /// <summary>
        /// Mocks the base orders.
        /// </summary>
        /// <param name="count">The count.</param>
        private static LegacyUpdatedOrders MockBaseOrders(int count)
        {
            var orders = new LegacyUpdatedOrders();
            var order = new LegacyUpdatedOrder
            {
                Number = 1,
                VendorNumber = "1",
                ShippingTrackingCode = "EA222222222222222",
                PaymentStatus = "ACCEPTED",
                FulfillmentStatus = "SHIPPED",
                OldFulfillmentStatus = "DELIVERED",
                OldPaymentStatus = "PAID",
                OldShippingTrackingCode = null
            };
            count.Times(() => orders.Add(order));

            return orders;
        }

        ///// <summary>
        ///// Mocks the time.
        ///// </summary>
        //public static Mock<ITimeProvider> MockTime()
        //{
        //    // Mock current time
        //    var mockTime = new Mock<ITimeProvider>();
        //    // Init time
        //    mockTime.SetupGet(t => t.Now).Returns(DateTime.Now);
        //    return mockTime;
        //}
    }
}