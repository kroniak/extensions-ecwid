// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;

namespace Ecwid.Services
{
    /// <summary>
    /// LINQ like extensions for the OrdersQueryBuilder.
    /// </summary>
    public static class OrdersQueryBuilderExtensions
    {
        /// <summary>
        /// Set custom query parameter.
        /// </summary>
        /// <typeparam name="TOrder">The type of the order.</typeparam>
        /// <typeparam name="TUpdateResponse">The type of the update response.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="name">The name. Must be not <see langword="null" /> or <see langword="empty" />.</param>
        /// <param name="value">The value. Must be not <see langword="null" />.</param>
        public static OrdersQueryBuilder<TOrder, TUpdateResponse> Custom<TOrder, TUpdateResponse>(
            this OrdersQueryBuilder<TOrder, TUpdateResponse> query, string name, object value)
            where TOrder : BaseOrder
            where TUpdateResponse : class
        {
            query.AddOrUpdate(name, value);
            return query;
        }

        /// <summary>
        /// Customer search term (searches by customer).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="customer">The customer.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Customer(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string customer)
        {
            query.AddOrUpdate("customer", customer);
            return query;
        }

        /// <summary>
        /// Set query parameter "specified number".
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="number">The ordinary order number</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Order(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, int number)
        {
            query.AddOrUpdate("orderNumber", number);
            return query;
        }

        /// <summary>
        /// Set query parameter "specified vendor number".
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="vendorNumber">The vendor order number (order number with prefix/suffix)</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Order(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string vendorNumber)
        {
            query.AddOrUpdate("vendorOrderNumber", vendorNumber);
            return query;
        }

        /// <summary>
        /// The code of coupon applied to order.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="couponCode">The order coupon code number</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> CouponCode(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, int couponCode)
        {
            query.AddOrUpdate("couponCode", couponCode);
            return query;
        }

        /// <summary>
        /// Search term. Ecwid will look for this term in order number, ordered items and customer details.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Keywords(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string keywords)
        {
            query.AddOrUpdate("keywords", keywords);
            return query;
        }

        #region Created

        /// <summary>
        /// Order placement date/time (lower bound). Supported formats:
        /// UNIX timestamp
        /// yyyy-MM-dd HH:mm:ss Z
        /// yyyy-MM-dd HH:mm:ss
        /// yyyy-MM-dd
        /// Examples:
        /// "1447804800"
        /// "2015-04-22 18:48:38 -0500"
        /// "2015-04-22" (this is 2015-04-22 00:00:00 UTC)
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> CreatedFrom(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateFrom)
        {
            query.AddOrUpdate("createdFrom", dateFrom);
            return query;
        }

        /// <summary>
        /// Order placement date/time (lower bound).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The date and time is outside the range of dates supported by the calendar
        /// used by the current culture.
        /// </exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> CreatedFrom(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateFrom)
        {
            try
            {
                query.AddOrUpdate("createdFrom", dateFrom.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException)
            {
            }
            return query;
        }

        /// <summary>
        /// Order placement date/time (upper bound). Supported formats:
        /// UNIX timestamp
        /// yyyy-MM-dd HH:mm:ss Z
        /// yyyy-MM-dd HH:mm:ss
        /// yyyy-MM-dd
        /// Examples:
        /// "1447804800"
        /// "2015-04-22 18:48:38 -0500"
        /// "2015-04-22" (this is 2015-04-22 00:00:00 UTC)
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateTo">The date to.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> CreatedTo(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateTo)
        {
            query.AddOrUpdate("createdTo", dateTo);
            return query;
        }

        /// <summary>
        /// Order placement date/time (upper bound).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateTo">The date from.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The date and time is outside the range of dates supported by the calendar
        /// used by the current culture.
        /// </exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> CreatedTo(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateTo)
        {
            try
            {
                query.AddOrUpdate("createdTo", dateTo.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException)
            {
            }
            return query;
        }

        /// <summary>
        /// Order placement date/time (lower and upper bounds). Supported formats:
        /// UNIX timestamp
        /// yyyy-MM-dd HH:mm:ss Z
        /// yyyy-MM-dd HH:mm:ss
        /// yyyy-MM-dd
        /// Examples:
        /// "1447804800"
        /// "2015-04-22 18:48:38 -0500"
        /// "2015-04-22" (this is 2015-04-22 00:00:00 UTC)
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date from.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Created(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateFrom, string dateTo)
        {
            query.AddOrUpdate("createdFrom", dateFrom);
            query.AddOrUpdate("createdTo", dateTo);
            return query;
        }

        /// <summary>
        /// Order placement date/time (lowe and upper bounds).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from</param>
        /// <param name="dateTo">The date from.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The date and time is outside the range of dates supported by the calendar
        /// used by the current culture.
        /// </exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Created(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                query.AddOrUpdate("createdFrom", dateFrom.ToString("yyyy-MM-dd HH:mm:ss"));
                query.AddOrUpdate("createdTo", dateTo.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException)
            {
            }

            return query;
        }

        #endregion

        #region Updated

        /// <summary>
        /// Order update date/time (lower bound). Supported formats:
        /// UNIX timestamp
        /// yyyy-MM-dd HH:mm:ss Z
        /// yyyy-MM-dd HH:mm:ss
        /// yyyy-MM-dd
        /// Examples:
        /// "1447804800"
        /// "2015-04-22 18:48:38 -0500"
        /// "2015-04-22" (this is 2015-04-22 00:00:00 UTC)
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> UpdatedFrom(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateFrom)
        {
            query.AddOrUpdate("updatedFrom", dateFrom);
            return query;
        }

        /// <summary>
        /// Order update date/time (lower bound).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The date and time is outside the range of dates supported by the calendar
        /// used by the current culture.
        /// </exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> UpdatedFrom(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateFrom)
        {
            try
            {
                query.AddOrUpdate("updatedFrom", dateFrom.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException)
            {
            }
            return query;
        }

        /// <summary>
        /// Order update date/time (upper bound). Supported formats:
        /// UNIX timestamp
        /// yyyy-MM-dd HH:mm:ss Z
        /// yyyy-MM-dd HH:mm:ss
        /// yyyy-MM-dd
        /// Examples:
        /// "1447804800"
        /// "2015-04-22 18:48:38 -0500"
        /// "2015-04-22" (this is 2015-04-22 00:00:00 UTC)
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateTo">The date to.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> UpdatedTo(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateTo)
        {
            query.AddOrUpdate("updatedTo", dateTo);
            return query;
        }

        /// <summary>
        /// Order update date/time (upper bound).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateTo">The date from.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The date and time is outside the range of dates supported by the calendar
        /// used by the current culture.
        /// </exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> UpdatedTo(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateTo)
        {
            try
            {
                query.AddOrUpdate("updatedTo", dateTo.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException)
            {
            }
            return query;
        }

        /// <summary>
        /// Order update date/time (lower and upper bounds). Supported formats:
        /// UNIX timestamp
        /// yyyy-MM-dd HH:mm:ss Z
        /// yyyy-MM-dd HH:mm:ss
        /// yyyy-MM-dd
        /// Examples:
        /// "1447804800"
        /// "2015-04-22 18:48:38 -0500"
        /// "2015-04-22" (this is 2015-04-22 00:00:00 UTC)
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date from.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Updated(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateFrom, string dateTo)
        {
            query.AddOrUpdate("updatedFrom", dateFrom);
            query.AddOrUpdate("updatedTo", dateTo);
            return query;
        }

        /// <summary>
        /// Order update date/time (lowe and upper bounds).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from</param>
        /// <param name="dateTo">The date from.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The date and time is outside the range of dates supported by the calendar
        /// used by the current culture.
        /// </exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Updated(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                query.AddOrUpdate("updatedFrom", dateFrom.ToString("yyyy-MM-dd HH:mm:ss"));
                query.AddOrUpdate("updatedTo", dateTo.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException)
            {
            }

            return query;
        }

        #endregion

        #region Totals

        /// <summary>
        /// Set query parameter "total from".
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="totalFrom">The total from.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> TotalFrom(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, double totalFrom)
        {
            query.AddOrUpdate("totalFrom", totalFrom);
            return query;
        }

        /// <summary>
        /// Set query parameter "total to".
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="totalTo">The total to.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> TotalTo(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, double totalTo)
        {
            query.AddOrUpdate("totalTo", totalTo);
            return query;
        }

        /// <summary>
        /// Set query parameters "total from" and "total to".
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="totalFrom">The total from.</param>
        /// <param name="totalTo">The total to.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Totals(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, double totalFrom, double totalTo)
        {
            query.AddOrUpdate("totalFrom", totalFrom);
            query.AddOrUpdate("totalTo", totalTo);
            return query;
        }

        #endregion

        #region Limits

        /// <summary>
        /// Number of orders returned in response. The default and maximal value is 100, any greater value is reset to 100.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="limit">
        /// Number of orders returned in response. The default and maximal value is 100, any greater value is
        /// reset to 100.
        /// </param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Limit(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, int limit)
        {
            query.AddOrUpdate("limit", limit);
            return query;
        }

        /// <summary>
        /// How many orders skip from beginning.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="offset">The offset.</param>
        public static OrdersQueryBuilder<TOrder, TUpdateResponse> Offset<TOrder, TUpdateResponse>(
            this OrdersQueryBuilder<TOrder, TUpdateResponse> query, int offset)
            where TOrder : BaseOrder
            where TUpdateResponse : class
        {
            query.AddOrUpdate("offset", offset);
            return query;
        }

        #endregion

        #region Getters

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <typeparam name="TOrder">The type of the order.</typeparam>
        /// <typeparam name="TUpdateResponse">The type of the update response.</typeparam>
        /// <param name="query">The query.</param>
        public static async Task<List<TOrder>> GetAsync<TOrder, TUpdateResponse>(
            this OrdersQueryBuilder<TOrder, TUpdateResponse> query)
            where TOrder : BaseOrder
            where TUpdateResponse : class
            => await query.Client.GetOrdersAsync(query);

        /// <summary>
        /// Gets the orders asynchronous.
        /// </summary>
        /// <typeparam name="TOrder">The type of the order.</typeparam>
        /// <typeparam name="TUpdateResponse">The type of the update response.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<List<TOrder>> GetAsync<TOrder, TUpdateResponse>(
            this OrdersQueryBuilder<TOrder, TUpdateResponse> query,
            CancellationToken cancellationToken)
            where TOrder : BaseOrder
            where TUpdateResponse : class
            => await query.Client.GetOrdersAsync(query, cancellationToken);

        #endregion
    }
}