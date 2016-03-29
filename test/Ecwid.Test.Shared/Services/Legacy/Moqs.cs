using System;
using System.Collections.Generic;
using Ecwid.Models.Legacy;
using Ecwid.Services.Legacy;
using Ecwid.Test.Services.Legacy;
using Moq;

namespace Ecwid.Services.Test.Services.Legacy
{
    internal static class Moqs
    {
        /// <summary>
        /// Moqs the responce with one order.
        /// </summary>
        /// <returns></returns>
        public static LegacyOrderResponse MockLegacyOrderResponseWithOneOrder
            => new LegacyOrderResponse() { Count = 1, Total = 10, NextUrl = null, Orders = MockLegacyOrders(1) };

        /// <summary>
        /// Moqs the responce with many order.
        /// </summary>
        /// <returns></returns>
        public static LegacyOrderResponse MockLegacyOrderResponseWithManyOrder
           => new LegacyOrderResponse() { Count = 10, Total = 10, NextUrl = null, Orders = MockLegacyOrders(10) };

        public static LegacyOrderResponse MockLegacyOrderResponseWithManyOrderAndPages(string nextUrl)
           => new LegacyOrderResponse() { Count = 1, Total = 10, NextUrl = nextUrl, Orders = MockLegacyOrders(10) };

        /// <summary>
        /// Moqs the orders.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private static List<LegacyOrder> MockLegacyOrders(int count)
        {
            var orders = new List<LegacyOrder>();
            var order = new LegacyOrder()
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
            count.Times(() => orders.Add(order));

            return orders;
        }

        /// <summary>
        /// Mocks the time.
        /// </summary>
        public static Mock<ITimeProvider> MockTime()
        {
            // Mock current time
            var mockTime = new Mock<ITimeProvider>();
            // Init time
            mockTime.SetupGet(t => t.Now).Returns(DateTime.Now);
            return mockTime;
        }
    }
}
