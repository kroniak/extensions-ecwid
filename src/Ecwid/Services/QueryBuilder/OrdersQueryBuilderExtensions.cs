// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;
using Ecwid.Tools;

namespace Ecwid
{
    /// <summary>
    /// LINQ like extensions for the OrdersQueryBuilder.
    /// </summary>
    public static class OrdersQueryBuilderExtensions
    {
        #region General

        /// <summary>
        /// Set custom query parameter.
        /// </summary>
        /// <typeparam name="TOrder">The type of the order.</typeparam>
        /// <typeparam name="TUpdateResponse">The type of the update response.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="name">The name. Must be not <see langword="null" /> or <see langword="empty" />.</param>
        /// <param name="value">The value. Must be not <see langword="null" />.</param>
        /// <exception cref="ArgumentException"><paramref name="value" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException"><paramref name="name" /> is <see langword="null" /> or empty</exception>
        public static OrdersQueryBuilder<TOrder, TUpdateResponse> Custom<TOrder, TUpdateResponse>(
            this OrdersQueryBuilder<TOrder, TUpdateResponse> query, string name, object value)
            where TOrder : BaseOrder
            where TUpdateResponse : class
        {
            if (value == null) throw new ArgumentException(nameof(value));
            if (Validators.IsNullOrEmpty(name)) throw new ArgumentException("Name is null or empty.", nameof(name));

            query.AddOrUpdate(name, value);
            return query;
        }

        /// <summary>
        /// Customer search term (searches by customer).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="customer">The customer.</param>
        /// <exception cref="ArgumentException"><paramref name="customer" /> is null or empty.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Customer(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string customer)
        {
            if (string.IsNullOrEmpty(customer))
                throw new ArgumentException("Customer is null or empty.", nameof(customer));

            query.AddOrUpdate("customer", customer);
            return query;
        }

        /// <summary>
        /// Set query parameter "specified number".
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="number">The ordinary order number</param>
        /// <exception cref="ArgumentException"><paramref name="number" /> must be greater than 0.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Order(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, int number)
        {
            if (number <= 0) throw new ArgumentException("Number must be greater than 0.", nameof(number));

            query.AddOrUpdate("orderNumber", number);
            return query;
        }

        /// <summary>
        /// Set query parameter "specified vendor number".
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="vendorNumber">The vendor order number (order number with prefix/suffix)</param>
        /// <exception cref="ArgumentException"><paramref name="vendorNumber" /> is null or empty.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Order(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string vendorNumber)
        {
            if (string.IsNullOrEmpty(vendorNumber))
                throw new ArgumentException("VendorNumber is null or empty.", nameof(vendorNumber));

            query.AddOrUpdate("vendorOrderNumber", vendorNumber);
            return query;
        }

        /// <summary>
        /// The code of coupon applied to order.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="couponCode">The order coupon code number</param>
        /// <exception cref="ArgumentException"><paramref name="couponCode" /> must be greater than 0.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> CouponCode(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, int couponCode)
        {
            if (couponCode <= 0) throw new ArgumentException("Coupon code must be greater than 0.", nameof(couponCode));

            query.AddOrUpdate("couponCode", couponCode);
            return query;
        }

        /// <summary>
        /// Search term. Ecwid will look for this term in order number, ordered items and customer details.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <exception cref="ArgumentException"><paramref name="keywords" /> is null or empty.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Keywords(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string keywords)
        {
            if (string.IsNullOrEmpty(keywords))
                throw new ArgumentException("Keywords is null or empty.", nameof(keywords));

            query.AddOrUpdate("keywords", keywords);
            return query;
        }

        #endregion

        #region Created

        /// <summary>
        /// Order placement date/time (lower bound). Supported formats:
        /// <list type="bullet">
        ///     <item>
        ///         <description>UNIX timestamp</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss Z</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd</description>
        ///     </item>
        /// </list>
        /// Examples:
        /// <list type="bullet">
        ///     <item>
        ///         <description>"1447804800"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38 -0500"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22" (this is 2015-04-22 00:00:00 UTC)</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <exception cref="ArgumentException"><paramref name="dateFrom" /> string is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="dateFrom" /> string is invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> CreatedFrom(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateFrom)
        {
            if (Validators.ValidateDateTime(dateFrom))
                query.AddOrUpdate("createdFrom", dateFrom);

            return query;
        }

        /// <summary>
        /// Order placement date/time (lower bound).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <exception cref="ArgumentException"><paramref name="dateFrom" /> is invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> CreatedFrom(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateFrom)
        {
            try
            {
                query.AddOrUpdate("createdFrom", dateFrom.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException exception)
            {
                throw new ArgumentException("Date is invalid.", nameof(dateFrom), exception);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                throw new ArgumentException("Date is invalid.", nameof(dateFrom), exception);
            }
            return query;
        }

        /// <summary>
        /// Order placement date/time (upper bound). Supported formats:
        /// <list type="bullet">
        ///     <item>
        ///         <description>UNIX timestamp</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss Z</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd</description>
        ///     </item>
        /// </list>
        /// Examples:
        /// <list type="bullet">
        ///     <item>
        ///         <description>"1447804800"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38 -0500"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22" (this is 2015-04-22 00:00:00 UTC)</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateTo">The date to.</param>
        /// <exception cref="ArgumentException"><paramref name="dateTo" /> string is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="dateTo" /> string is invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> CreatedTo(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateTo)
        {
            if (Validators.ValidateDateTime(dateTo))
                query.AddOrUpdate("createdTo", dateTo);

            return query;
        }

        /// <summary>
        /// Order placement date/time (upper bound).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateTo">The date from.</param>
        /// <exception cref="ArgumentException"><paramref name="dateTo" /> is invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> CreatedTo(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateTo)
        {
            try
            {
                query.AddOrUpdate("createdTo", dateTo.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException exception)
            {
                throw new ArgumentException("Date is invalid", nameof(dateTo), exception);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                throw new ArgumentException("Date is invalid", nameof(dateTo), exception);
            }
            return query;
        }

        /// <summary>
        /// Order placement date/time (lower and upper bounds). Supported formats:
        /// <list type="bullet">
        ///     <item>
        ///         <description>UNIX timestamp</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss Z</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd</description>
        ///     </item>
        /// </list>
        /// Examples:
        /// <list type="bullet">
        ///     <item>
        ///         <description>"1447804800"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38 -0500"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22" (this is 2015-04-22 00:00:00 UTC)</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date from.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="dateFrom" /> or <paramref name="dateTo" /> strings are null or
        /// empty.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="dateFrom" /> or <paramref name="dateTo" />strings are invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Created(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateFrom, string dateTo)
        {
            // ReSharper disable once InvertIf
            if (Validators.ValidateDateTime(dateFrom) && Validators.ValidateDateTime(dateTo))
            {
                query.AddOrUpdate("createdFrom", dateFrom);
                query.AddOrUpdate("createdTo", dateTo);
            }

            return query;
        }

        /// <summary>
        /// Order placement date/time (lowe and upper bounds).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from</param>
        /// <param name="dateTo">The date from.</param>
        /// <exception cref="ArgumentException"><paramref name="dateFrom" /> or <paramref name="dateTo" /> is invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Created(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                query.AddOrUpdate("createdFrom", dateFrom.ToString("yyyy-MM-dd HH:mm:ss"));
                query.AddOrUpdate("createdTo", dateTo.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException exception)
            {
                throw new ArgumentException("Date is invalid.", exception);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                throw new ArgumentException("Date is invalid.", exception);
            }

            return query;
        }

        #endregion

        #region Updated

        /// <summary>
        /// Order update date/time (lower bound). Supported formats:
        /// <list type="bullet">
        ///     <item>
        ///         <description>UNIX timestamp</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss Z</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd</description>
        ///     </item>
        /// </list>
        /// Examples:
        /// <list type="bullet">
        ///     <item>
        ///         <description>"1447804800"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38 -0500"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22" (this is 2015-04-22 00:00:00 UTC)</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <exception cref="ArgumentException"><paramref name="dateFrom" /> string is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="dateFrom" /> string is invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> UpdatedFrom(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateFrom)
        {
            if (Validators.ValidateDateTime(dateFrom))
                query.AddOrUpdate("updatedFrom", dateFrom);

            return query;
        }

        /// <summary>
        /// Order update date/time (lower bound).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <exception cref="ArgumentException"><paramref name="dateFrom" /> is invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> UpdatedFrom(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateFrom)
        {
            try
            {
                query.AddOrUpdate("updatedFrom", dateFrom.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException exception)
            {
                throw new ArgumentException("Date is invalid.", nameof(dateFrom), exception);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                throw new ArgumentException("Date is invalid.", nameof(dateFrom), exception);
            }
            return query;
        }

        /// <summary>
        /// Order update date/time (upper bound). Supported formats:
        /// <list type="bullet">
        ///     <item>
        ///         <description>UNIX timestamp</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss Z</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd</description>
        ///     </item>
        /// </list>
        /// Examples:
        /// <list type="bullet">
        ///     <item>
        ///         <description>"1447804800"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38 -0500"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22" (this is 2015-04-22 00:00:00 UTC)</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateTo">The date to.</param>
        /// <exception cref="ArgumentException"><paramref name="dateTo" /> string is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="dateTo" /> string is invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> UpdatedTo(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateTo)
        {
            if (Validators.ValidateDateTime(dateTo))
                query.AddOrUpdate("updatedTo", dateTo);

            return query;
        }

        /// <summary>
        /// Order update date/time (upper bound).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateTo">The date from.</param>
        /// <exception cref="ArgumentException"><paramref name="dateTo" /> is invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> UpdatedTo(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateTo)
        {
            try
            {
                query.AddOrUpdate("updatedTo", dateTo.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException exception)
            {
                throw new ArgumentException("Date is invalid.", nameof(dateTo), exception);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                throw new ArgumentException("Date is invalid.", nameof(dateTo), exception);
            }
            return query;
        }

        /// <summary>
        /// Order update date/time (lower and upper bounds). Supported formats:
        /// <list type="bullet">
        ///     <item>
        ///         <description>UNIX timestamp</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss Z</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd HH:mm:ss</description>
        ///     </item>
        ///     <item>
        ///         <description>yyyy-MM-dd</description>
        ///     </item>
        /// </list>
        /// Examples:
        /// <list type="bullet">
        ///     <item>
        ///         <description>"1447804800"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38 -0500"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22 18:48:38"</description>
        ///     </item>
        ///     <item>
        ///         <description>"2015-04-22" (this is 2015-04-22 00:00:00 UTC)</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date from.</param>
        /// <exception cref="ArgumentException"><paramref name="dateFrom" /> or <paramref name="dateTo" /> string is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="dateFrom" /> or <paramref name="dateTo" /> string is invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Updated(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string dateFrom, string dateTo)
        {
            // ReSharper disable once InvertIf
            if (Validators.ValidateDateTime(dateFrom) && Validators.ValidateDateTime(dateTo))
            {
                query.AddOrUpdate("updatedFrom", dateFrom);
                query.AddOrUpdate("updatedTo", dateTo);
            }

            return query;
        }

        /// <summary>
        /// Order update date/time (lowe and upper bounds).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="dateFrom">The date from</param>
        /// <param name="dateTo">The date from.</param>
        /// <exception cref="ArgumentException"><paramref name="dateFrom" /> or <paramref name="dateTo" /> is invalid.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Updated(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                query.AddOrUpdate("updatedFrom", dateFrom.ToString("yyyy-MM-dd HH:mm:ss"));
                query.AddOrUpdate("updatedTo", dateTo.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (FormatException exception)
            {
                throw new ArgumentException("Date is invalid.", exception);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                throw new ArgumentException("Date is invalid.", exception);
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
        /// <exception cref="ArgumentException"><paramref name="totalFrom" /> must greater than 0.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> TotalFrom(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, double totalFrom)
        {
            if (totalFrom <= 0) throw new ArgumentException("TotalFrom must be greater than 0.", nameof(totalFrom));

            query.AddOrUpdate("totalFrom", totalFrom);
            return query;
        }

        /// <summary>
        /// Set query parameter "total to".
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="totalTo">The total to.</param>
        /// <exception cref="ArgumentException"><paramref name="totalTo" /> must be greater than 0.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> TotalTo(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, double totalTo)
        {
            if (totalTo <= 0)
                throw new ArgumentException("TotalTo must be greater than 0.", nameof(totalTo));

            query.AddOrUpdate("totalTo", totalTo);
            return query;
        }

        /// <summary>
        /// Set query parameters "total from" and "total to".
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="totalFrom">The total from.</param>
        /// <param name="totalTo">The total to.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="totalFrom" /> must be greater than 0.</exception>
        /// <exception cref="ArgumentException"><paramref name="totalTo" /> must be greater than 0.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Totals(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, double totalFrom, double totalTo)
        {
            if (totalFrom <= 0) throw new ArgumentException("TotalFrom must be greater than 0.", nameof(totalFrom));
            if (totalTo <= 0) throw new ArgumentException("TotalTo must be greater than 0.", nameof(totalTo));

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
        /// <exception cref="ArgumentException"><paramref name="limit" /> must be greater than 0.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Limit(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, int limit)
        {
            if (limit <= 0) throw new ArgumentException("Limit must be greater than 0.", nameof(limit));

            if (limit > 100) limit = 100;

            query.AddOrUpdate("limit", limit);
            return query;
        }

        /// <summary>
        /// How many orders skip from beginning.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="offset">The offset.</param>
        /// <exception cref="ArgumentException"><paramref name="offset" /> must be greater than 0.</exception>
        public static OrdersQueryBuilder<TOrder, TUpdateResponse> Offset<TOrder, TUpdateResponse>(
            this OrdersQueryBuilder<TOrder, TUpdateResponse> query, int offset)
            where TOrder : BaseOrder
            where TUpdateResponse : class
        {
            if (offset <= 0) throw new ArgumentException("Offset must be greater than 0.", nameof(offset));

            query.AddOrUpdate("offset", offset);
            return query;
        }

        #endregion

        #region Payment\Fullfilment

        /// <summary>
        /// Payment method used by customer.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="paymentMethod">The payment method.</param>
        /// <exception cref="ArgumentException"><paramref name="paymentMethod" /> is null or empty.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> PaymentMethod(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string paymentMethod)
        {
            if (string.IsNullOrEmpty(paymentMethod))
                throw new ArgumentException("PaymentMethod is null or empty.", nameof(paymentMethod));

            query.AddOrUpdate("paymentMethod", paymentMethod);
            return query;
        }

        /// <summary>
        /// Shipping method used by customer.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="shippingMethod">The payment method.</param>
        /// <exception cref="ArgumentException"><paramref name="shippingMethod" /> is null or empty.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> ShippingMethod(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string shippingMethod)
        {
            if (string.IsNullOrEmpty(shippingMethod))
                throw new ArgumentException("ShippingMethod is null or empty.", nameof(shippingMethod));

            query.AddOrUpdate("shippingMethod", shippingMethod);
            return query;
        }

        /// <summary>
        /// Add payment comma or space separated status names.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="paymentStatuses">
        /// Payment statuses: AWAITING_PAYMENT, PAID, CANCELLED, REFUNDED, INCOMPLETE.
        /// </param>
        /// <exception cref="EcwidConfigException">Can not add or update statuses. Look inner exception.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> PaymentStatuses(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string paymentStatuses)
        {
            try
            {
                Validators.StatusesValidate(paymentStatuses, Validators.AvailablePaymentStatuses);

                query.AddOrUpdateStatuses("paymentStatus", paymentStatuses.TrimUpperReplaceSplit());
            }
            catch (ArgumentException exception)
            {
                throw new EcwidConfigException("Can not add or update statuses. Look inner exception.", exception);
            }
            return query;
        }

        /// <summary>
        /// Add fulfillment comma or space separated status names.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="fulfillmentStatuses">
        /// Fulfillment statuses: AWAITING_PROCESSING, PROCESSING, SHIPPED, DELIVERED, WILL_NOT_DELIVER, RETURNED.
        /// </param>
        /// <exception cref="EcwidConfigException">Can not add or update statuses. Look inner exception.</exception>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> FulfillmentStatuses(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string fulfillmentStatuses)
        {
            try
            {
                Validators.StatusesValidate(fulfillmentStatuses, Validators.AvailableFulfillmentStatuses);

                query.AddOrUpdateStatuses("fulfillmentStatus", fulfillmentStatuses.TrimUpperReplaceSplit());
            }
            catch (ArgumentException exception)
            {
                throw new EcwidConfigException("Can not add or update statuses. Look inner exception.", exception);
            }
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
            => await query.GetAsync(CancellationToken.None);

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
            => await query.Client.GetOrdersAsync(query.Query, cancellationToken);

        #endregion
    }
}