using System;
using System.Collections.Generic;
using Ecwid.Models.Legacy;
using Ecwid.Services.Legacy;
using Moq;

namespace Ecwid.Test.Services.Legacy
{
    internal abstract class Moqs
    {
        /// <summary>
        /// Moqs the responce with one order.
        /// </summary>
        public static LegacyOrderResponse<LegacyOrder> MockLegacyOrderResponseWithOneOrder
            => new LegacyOrderResponse<LegacyOrder>() { Count = 1, Total = 10, NextUrl = null, Orders = MockLegacyOrders(1) };

        /// <summary>
        /// Moqs the responce with many order.
        /// </summary>
        public static LegacyOrderResponse<LegacyOrder> MockLegacyOrderResponseWithManyOrder
           => new LegacyOrderResponse<LegacyOrder>() { Count = 10, Total = 10, NextUrl = null, Orders = MockLegacyOrders(10) };

        /// <summary>
        /// Mocks the legacy order response with many order and pages.
        /// </summary>
        /// <param name="nextUrl">The next URL.</param>
        public static LegacyOrderResponse<LegacyOrder> MockLegacyOrderResponseWithManyOrderAndPages(string nextUrl)
           => new LegacyOrderResponse<LegacyOrder>() { Count = 10, Total = 10, NextUrl = nextUrl, Orders = MockLegacyOrders(10) };

        /// <summary>
        /// Gets the mock legacy order response for update.
        /// </summary>
        /// <value>
        /// The mock legacy order response for update.
        /// </value>
        public static LegacyOrderResponse<LegacyUpdatedOrder> MockLegacyOrderResponseForUpdate
           => new LegacyOrderResponse<LegacyUpdatedOrder>() { Count = 1, Total = 10, Orders = MockBaseOrders(1) };

        /// <summary>
        /// Moqs the orders.
        /// </summary>
        /// <param name="count">The count.</param>
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
        /// Mocks the base orders.
        /// </summary>
        /// <param name="count">The count.</param>
        private static LegacyUpdatedOrders MockBaseOrders(int count)
        {
            var orders = new LegacyUpdatedOrders();
            var order = new LegacyUpdatedOrder()
            {
                Number = 123,
                VendorNumber = "123",
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