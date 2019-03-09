// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Ecwid.Models;

namespace Ecwid.Test.Services
{
    internal abstract class Mocks
    {
        /// <summary>
        /// Mocks the response with one order.
        /// </summary>
        public static SearchResult MockSearchResultWithLimit1
            => new SearchResult
                {Count = 1, Total = 100, Limit = 1, Offset = 0, Orders = MockOrders(1)};

        /// <summary>
        /// Mocks the response with one order.
        /// </summary>
        public static SearchResult MockSearchResultWithOneOrder
            => new SearchResult
                {Count = 1, Total = 1, Limit = 100, Offset = 0, Orders = MockOrders(1)};

        /// <summary>
        /// Gets the mock search result zero result.
        /// </summary>
        /// <value>
        /// The mock search result zero result.
        /// </value>
        public static SearchResult MockSearchResultZeroResult
            => new SearchResult {Count = 0, Total = 0, Limit = 100, Offset = 0, Orders = new OrderEntry[] { }};

        /// <summary>
        /// Mocks the orders.
        /// </summary>
        /// <param name="count">The count.</param>
        [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
        [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
        private static IEnumerable<OrderEntry> MockOrders(int count)
        {
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

            return Enumerable.Range(0, count).Select(_ => order);
        }

        private static IEnumerable<DiscountCouponInfo> MockDiscountCoupons(int count)
        {
            var rand = new Random();

            var coupon = new DiscountCouponInfo
            {
                Code = "ABC123DEF",
                CreationDate = "2016-03-24 05:51:53",
                Discount = 18,
                DiscountType = "ABS_AND_SHIPPING",
                LaunchDate = "2016-03-25 05:51:53",
                ExpirationDate = "2017-03-25 05:51:53",
                Name = "Test Coupon",
                RepeatCustomerOnly = false,
                UsesLimit = "UNLIMITED",
                Status = "ACTIVE",
                Id = 100000 + rand.Next(101),
                OrderCount = 0,
                TotalLimit = 18
            };

            return Enumerable.Range(0, count).Select(_ => coupon);
        }

        /// <summary>
        /// Mocks the response with many order.
        /// </summary>
        public static SearchResult MockSearchResultWithManyOrder(int count = 10, int limit = 10)
            => new SearchResult
            {
                Count = count, Total = count, Limit = limit, Offset = 0,
                Orders = MockOrders(count)
            };

        /// <summary>
        /// Mocks the order response with many order and pages.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        public static SearchResult MockSearchResultWithManyOrderAndPages(int limit, int offset, int count)
            => new SearchResult
            {
                Count = count, Total = 300, Limit = limit, Offset = offset,
                Orders = MockOrders(count)
            };

        public static DiscountCouponSearchResults MockSearchResultWithManyDiscountCouponsAndPages(int limit, int offset,
            int count)
            => new DiscountCouponSearchResults
            {
                Count = count,
                Total = 300,
                Limit = limit,
                Offset = offset,
                DiscountCoupons = MockDiscountCoupons(count)
            };
    }
}