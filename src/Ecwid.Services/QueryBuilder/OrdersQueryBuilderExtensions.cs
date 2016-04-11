// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;
using Ecwid.Models.Legacy;
using Ecwid.Tools;

namespace Ecwid.Services
{
    /// <summary>
    /// LINQ like extensions for the OrdersQueryBuilder.
    /// </summary>
    public static class OrdersQueryBuilderExtensions
    {
        /// <summary>
        /// Add custom query parameters.
        /// </summary>
        /// <typeparam name="TOrder">The type of the order.</typeparam>
        /// <typeparam name="TUpdateResponse">The type of the update response.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static OrdersQueryBuilder<TOrder, TUpdateResponse> Custom<TOrder, TUpdateResponse>(
            this OrdersQueryBuilder<TOrder, TUpdateResponse> query, string name, object value)
            where TOrder : BaseOrder
            where TUpdateResponse : class
            => query.AddOrUpdate(name, value);

        /// <summary>
        /// All orders placed this day.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Date(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, DateTime date)
            => query.AddOrUpdate("date", date.ToString("yyyy-MM-dd"));

        /// <summary>
        /// All orders placed after this date.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> FromDate(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, DateTime date)
            => query.AddOrUpdate("from_date", date.ToString("yyyy-MM-dd"));

        /// <summary>
        /// All orders placed before this date.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> ToDate(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, DateTime date)
            => query.AddOrUpdate("to_date", date.ToString("yyyy-MM-dd"));

        /// <summary>
        /// All orders changed after this date.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> FromUpdateDate(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, DateTime date)
            => query.AddOrUpdate("from_update_date", date.ToString("yyyy-MM-dd"));

        /// <summary>
        /// All orders changed before this date.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> ToUpdateDate(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, DateTime date)
            => query.AddOrUpdate("to_update_date", date.ToString("yyyy-MM-dd"));

        /// <summary>
        /// Set where property by the specified number.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="number">The ordinary order number</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Order(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, int number)
            => query.AddOrUpdate("order", number);

        /// <summary>
        /// Set where property by the specified vendor number.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="vendorNumber">The vendor order number (order number with prefix/suffix)</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Order(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string vendorNumber)
            => query.AddOrUpdate("order", vendorNumber);

        /// <summary>
        /// Set where property by the specified number.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="number">The ordinary order number</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Order(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, int number)
            => query.AddOrUpdate("orderNumber", number);

        /// <summary>
        /// Set where property by the specified vendor number.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="vendorNumber">The vendor order number (order number with prefix/suffix)</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Order(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string vendorNumber)
            => query.AddOrUpdate("vendorOrderNumber", vendorNumber);

        /// <summary>
        /// All orders with numbers bigger than or equal to value.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="number">The ordinary order number</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> FromOrder(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, int number)
            => query.AddOrUpdate("from_order", number);

        /// <summary>
        /// All orders with numbers bigger than or equal to value.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="vendorNumber">The vendor order number (order number with prefix/suffix)</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> FromOrder(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string vendorNumber)
            => query.AddOrUpdate("from_order", vendorNumber);

        /// <summary>
        /// Unique numeric customer identifier or null for anonymous orders.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="customerId">The customer identifier.</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> CustomerId(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, int? customerId)
            =>
                customerId == null
                    ? query.AddOrUpdate("customer_id", "null")
                    : query.AddOrUpdate("customer_id", customerId);

        /// <summary>
        /// Customer email or null for orders with empty or absent emails.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="customerEmail">The customer email.</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> CustomerEmail(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string customerEmail)
            => query.AddOrUpdate("customer_email", customerEmail ?? "");

        /// <summary>
        /// Set payment and/or fulfillment comma or space separated status names.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="paymentStatuses">
        /// Payment statuses: PAID==ACCEPTED, DECLINED, CANCELLED, AWAITING_PAYMENT==QUEUED,
        /// CHARGEABLE, REFUNDED, INCOMPLETE
        /// </param>
        /// <param name="fulfillmentStatuses">
        /// Fulfillment statuses: AWAITING_PROCESSING==NEW, PROCESSING, SHIPPED, DELIVERED,
        /// WILL_NOT_DELIVER, RETURNED
        /// </param>
        /// <exception cref="ArgumentException">Payment statuses string is invalid</exception>
        /// <exception cref="ArgumentException">Fulfillment statuses string is invalid</exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Statuses(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string paymentStatuses,
            string fulfillmentStatuses)
        {
            Validators.PaymentStatusesValidate(paymentStatuses);
            Validators.FulfillmentStatusesValidate(fulfillmentStatuses);

            var resultList = paymentStatuses.TrimUpperReplaceSplit();
            resultList.AddRange(fulfillmentStatuses.TrimUpperReplaceSplit());

            return query.AddOrUpdateStatuses(resultList);
        }

        /// <summary>
        /// Add payment comma or space separated status names.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="paymentStatuses">
        /// Payment statuses: PAID==ACCEPTED, DECLINED, CANCELLED, AWAITING_PAYMENT==QUEUED,
        /// CHARGEABLE, REFUNDED, INCOMPLETE
        /// </param>
        /// <exception cref="ArgumentException">Payment statuses string is invalid</exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> AddPaymentStatuses(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string paymentStatuses)
        {
            return Validators.PaymentStatusesValidate(paymentStatuses)
                ? query.AddOrUpdateStatuses(paymentStatuses.TrimUpperReplaceSplit())
                : query;
        }

        /// <summary>
        /// Add fulfillment comma or space separated status names.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="fulfillmentStatuses">
        /// Fulfillment statuses: AWAITING_PROCESSING==NEW, PROCESSING, SHIPPED, DELIVERED,
        /// WILL_NOT_DELIVER, RETURNED
        /// </param>
        /// <exception cref="ArgumentException">Fulfillment statuses string is invalid</exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> AddFulfillmentStatuses(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string fulfillmentStatuses)
        {
            return Validators.FulfillmentStatusesValidate(fulfillmentStatuses)
                ? query.AddOrUpdateStatuses(fulfillmentStatuses.TrimUpperReplaceSplit())
                : query;
        }

        /// <summary>
        /// Number of orders returned in response. The default and maximal value is 200, any greater value is reset to 200.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="limit">
        /// Number of orders returned in response. The default and maximal value is 200, any greater value is
        /// reset to 200.
        /// </param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Limit(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, int limit)
            => query.AddOrUpdate("limit", limit);

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
            => query.AddOrUpdate("limit", limit);

        /// <summary>
        /// How many orders skip from beginning
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="offset">The offset.</param>
        public static OrdersQueryBuilder<TOrder, TUpdateResponse> Offset<TOrder, TUpdateResponse>(
            this OrdersQueryBuilder<TOrder, TUpdateResponse> query, int offset)
            where TOrder : BaseOrder
            where TUpdateResponse : class
            => query.AddOrUpdate("offset", offset);

        /// <summary>
        /// Keywordses the specified keywords.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        public static OrdersQueryBuilder<OrderEntry, UpdateStatus> Keywords(
            this OrdersQueryBuilder<OrderEntry, UpdateStatus> query, string keywords)
            => query.AddOrUpdate("keywords", keywords);

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

        /// <summary>
        /// Gets the one page of orders asynchronous. It ignores next url.
        /// </summary>
        /// <param name="query">The query.</param>
        public static async Task<List<LegacyOrder>> GetPageAsync(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query)
            => await query.Client.GetOrdersPageAsync(query);

        /// <summary>
        /// Gets the one page of orders asynchronous. It ignores next url.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static async Task<List<LegacyOrder>> GetPageAsync(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, CancellationToken cancellationToken)
            => await query.Client.GetOrdersPageAsync(query, cancellationToken);

        /// <summary>
        /// Update the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <param name="newPaymentStatus">
        /// New payment status: PAID == ACCEPTED, DECLINED, CANCELLED, AWAITING_PAYMENT == QUEUED,
        /// CHARGEABLE, REFUNDED, INCOMPLETE
        /// </param>
        /// <param name="newFulfillmentStatus">
        /// New fulfillment status: AWAITING_PROCESSING == NEW, PROCESSING, SHIPPED, DELIVERED,
        /// WILL_NOT_DELIVER, RETURNED
        /// </param>
        /// <param name="newShippingTrackingCode">
        /// New shipping tracking code. Change of shipping tracking number will also change
        /// the order's fulfillment status to SHIPPED.
        /// </param>
        public static async Task<LegacyUpdatedOrders> UpdateAsync(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string newPaymentStatus,
            string newFulfillmentStatus, string newShippingTrackingCode)
        {
            ValidateAddNewParams(query, newPaymentStatus, newFulfillmentStatus, newShippingTrackingCode);
            return await query.Client.UpdateOrdersAsync(query);
        }

        /// <summary>
        /// Update the orders asynchronous.
        /// </summary>
        /// <param name="query">The orders query builder</param>
        /// <param name="newPaymentStatus">
        /// New payment status: PAID == ACCEPTED, DECLINED, CANCELLED, AWAITING_PAYMENT == QUEUED,
        /// CHARGEABLE, REFUNDED, INCOMPLETE
        /// </param>
        /// <param name="newFulfillmentStatus">
        /// New fulfillment status: AWAITING_PROCESSING == NEW, PROCESSING, SHIPPED, DELIVERED,
        /// WILL_NOT_DELIVER, RETURNED
        /// </param>
        /// <param name="newShippingTrackingCode">
        /// New shipping tracking code. Change of shipping tracking number will also change
        /// the order's fulfillment status to SHIPPED.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="ArgumentException">Query is empty. Prevent change all orders.</exception>
        public static async Task<LegacyUpdatedOrders> UpdateAsync(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string newPaymentStatus,
            string newFulfillmentStatus, string newShippingTrackingCode, CancellationToken cancellationToken)
        {
            ValidateAddNewParams(query, newPaymentStatus, newFulfillmentStatus, newShippingTrackingCode);
            return await query.Client.UpdateOrdersAsync(query, cancellationToken);
        }

        /// <summary>
        /// Validates and add new parameters.
        /// </summary>
        /// <typeparam name="TOrder">The type of the order.</typeparam>
        /// <typeparam name="TUpdateResponse">The type of the update response.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="newPaymentStatus">The new payment status.</param>
        /// <param name="newFulfillmentStatus">The new fulfillment status.</param>
        /// <param name="newShippingTrackingCode">The new shipping tracking code.</param>
        /// <exception cref="ArgumentException">Query is empty. Prevent change all orders.</exception>
        private static void ValidateAddNewParams<TOrder, TUpdateResponse>(
            OrdersQueryBuilder<TOrder, TUpdateResponse> query, string newPaymentStatus, string newFulfillmentStatus,
            string newShippingTrackingCode)
            where TOrder : BaseOrder
            where TUpdateResponse : class
        {
            //check query builder query params count
            var exceptionList = new List<string> {"limit", "offset"};
            var count = query.Query.Keys.Count(s => !exceptionList.Contains(s));
            if (count == 0)
                throw new ArgumentException("Query is empty. Prevent change all orders.", nameof(query));

            // Throw ex if all string is NullOrEmpty 
            Validators.StringsValidate(newPaymentStatus, newFulfillmentStatus, newShippingTrackingCode);

            if (!string.IsNullOrEmpty(newPaymentStatus))
                query.AddOrUpdate("new_payment_status", Validators.PaymentStatusValidate(newPaymentStatus));
            if (!string.IsNullOrEmpty(newFulfillmentStatus))
                query.AddOrUpdate("new_fulfillment_status", Validators.FulfillmentStatusValidate(newFulfillmentStatus));
            if (!string.IsNullOrEmpty(newShippingTrackingCode))
                query.AddOrUpdate("new_shipping_tracking_code", newShippingTrackingCode);
        }
    }
}