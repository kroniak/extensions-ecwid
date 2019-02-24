// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;
using Ecwid.Tools;

// ReSharper disable CheckNamespace
// ReSharper disable PossibleMultipleEnumeration

namespace Ecwid.Legacy
{
    /// <summary>
    /// LINQ like extensions for the OrdersQueryBuilder.
    /// </summary>
    public static class LegacyOrdersQueryBuilderExtensions
    {
        /// <summary>
        /// All orders placed this day.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        /// <exception cref="ArgumentException">
        /// The date and time is outside the range of dates supported by the calendar
        /// used by the current culture.
        /// </exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Date(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, DateTime date)
        {
            query.AddOrUpdate("date", date.ToString("yyyy-MM-dd"));
            return query;
        }

        /// <summary>
        /// All orders placed after this date.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        /// <exception cref="ArgumentException">
        /// The date and time is outside the range of dates supported by the calendar
        /// used by the current culture.
        /// </exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> FromDate(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, DateTime date)
        {
            query.AddOrUpdate("from_date", date.ToString("yyyy-MM-dd"));
            return query;
        }

        /// <summary>
        /// All orders placed before this date.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        /// <exception cref="ArgumentException">
        /// The date and time is outside the range of dates supported by the calendar
        /// used by the current culture.
        /// </exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> ToDate(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, DateTime date)
        {
            query.AddOrUpdate("to_date", date.ToString("yyyy-MM-dd"));
            return query;
        }

        /// <summary>
        /// All orders changed after this date.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        /// <exception cref="ArgumentException">
        /// The date and time is outside the range of dates supported by the calendar
        /// used by the current culture.
        /// </exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> FromUpdateDate(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, DateTime date)
        {
            query.AddOrUpdate("from_update_date", date.ToString("yyyy-MM-dd"));
            return query;
        }

        /// <summary>
        /// All orders changed before this date.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        /// <exception cref="ArgumentException">
        /// The date and time is outside the range of dates supported by the calendar
        /// used by the current culture.
        /// </exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> ToUpdateDate(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, DateTime date)
        {
            query.AddOrUpdate("to_update_date", date.ToString("yyyy-MM-dd"));
            return query;
        }

        /// <summary>
        /// Set query parameter "specified number".
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="number">The ordinary order number</param>
        /// <exception cref="ArgumentException"><paramref name="number" /> must be greater than 0</exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Order(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, int number)
        {
            if (number <= 0) throw new ArgumentException("Number must be greater than 0", nameof(number));

            query.AddOrUpdate("order", number);
            return query;
        }

        /// <summary>
        /// Set query parameter "specified vendor number".
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="vendorNumber">The vendor order number (order number with prefix/suffix)</param>
        /// <exception cref="ArgumentException"><paramref name="vendorNumber" /> is null or empty.</exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Order(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string vendorNumber)
        {
            if (string.IsNullOrWhiteSpace(vendorNumber))
                throw new ArgumentException("VendorNumber is null or empty.", nameof(vendorNumber));

            query.AddOrUpdate("order", vendorNumber);
            return query;
        }

        /// <summary>
        /// All orders with numbers bigger than or equal to value.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="number">The ordinary order number</param>
        /// <exception cref="ArgumentException"><paramref name="number" /> must be greater than 0</exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> FromOrder(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, int number)
        {
            if (number <= 0) throw new ArgumentException("Number must be greater than 0", nameof(number));

            query.AddOrUpdate("from_order", number);
            return query;
        }

        /// <summary>
        /// All orders with numbers bigger than or equal to value.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="vendorNumber">The vendor order number (order number with prefix/suffix)</param>
        /// <exception cref="ArgumentException"><paramref name="vendorNumber" /> is null or empty.</exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> FromOrder(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string vendorNumber)
        {
            if (string.IsNullOrWhiteSpace(vendorNumber))
                throw new ArgumentException("VendorNumber is null or empty.", nameof(vendorNumber));

            query.AddOrUpdate("from_order", vendorNumber);
            return query;
        }

        /// <summary>
        /// Unique numeric customer identifier or null for anonymous orders.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="customerId">The customer identifier.</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> CustomerId(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, int? customerId)
        {
            if (customerId == null)
                query.AddOrUpdate("customer_id", "null");
            else
                query.AddOrUpdate("customer_id", customerId);
            return query;
        }

        /// <summary>
        /// Customer email or <c>null</c> for orders with empty or absent emails.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="customerEmail">The customer email or <c>null</c>.</param>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> CustomerEmail(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string customerEmail)
        {
            query.AddOrUpdate("customer_email", customerEmail ?? "");
            return query;
        }

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
        /// <exception cref="EcwidConfigException">Can not add or update statuses. Look inner exception.</exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Statuses(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string paymentStatuses,
            string fulfillmentStatuses)
        {
            try
            {
                Validators.StatusesValidate(paymentStatuses, Validators.AvailableLegacyPaymentStatuses);
                Validators.StatusesValidate(fulfillmentStatuses, Validators.AvailableLegacyFulfillmentStatuses);

                var resultList = paymentStatuses.TrimUpperReplaceSplit();
                var result = new List<string>(resultList);
                result.AddRange(fulfillmentStatuses.TrimUpperReplaceSplit());

                query.AddOrUpdateStatuses("statuses", result);
            }
            catch (ArgumentException exception)
            {
                throw new EcwidConfigException("Can not add or update statuses. Look inner exception.", exception);
            }

            return query;
        }

        /// <summary>
        /// Add payment comma or space separated status names.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="paymentStatuses">
        /// Payment statuses: PAID==ACCEPTED, DECLINED, CANCELLED, AWAITING_PAYMENT==QUEUED,
        /// CHARGEABLE, REFUNDED, INCOMPLETE
        /// </param>
        /// <exception cref="EcwidConfigException">Can not add or update statuses. Look inner exception.</exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> PaymentStatuses(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string paymentStatuses)
        {
            try
            {
                Validators.StatusesValidate(paymentStatuses, Validators.AvailableLegacyPaymentStatuses);

                query.AddOrUpdateStatuses("statuses", paymentStatuses.TrimUpperReplaceSplit());
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
        /// Fulfillment statuses: AWAITING_PROCESSING==NEW, PROCESSING, SHIPPED, DELIVERED,
        /// WILL_NOT_DELIVER, RETURNED
        /// </param>
        /// <exception cref="EcwidConfigException">Can not add or update statuses. Look inner exception.</exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> FulfillmentStatuses(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string fulfillmentStatuses)
        {
            try
            {
                Validators.StatusesValidate(fulfillmentStatuses, Validators.AvailableLegacyFulfillmentStatuses);

                query.AddOrUpdateStatuses("statuses", fulfillmentStatuses.TrimUpperReplaceSplit());
            }
            catch (ArgumentException exception)
            {
                throw new EcwidConfigException("Can not add or update statuses. Look inner exception.", exception);
            }

            return query;
        }

        /// <summary>
        /// Number of orders returned in response. The default and maximal value is 200, any greater value is reset to 200.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="limit">
        /// Number of orders returned in response. The default and maximal value is 200, any greater value is
        /// reset to 200.
        /// </param>
        /// <exception cref="ArgumentException"><paramref name="limit" /> must be greater than 0</exception>
        public static OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> Limit(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, int limit)
        {
            if (limit <= 0)
                throw new ArgumentException("Limit must be greater than 0", nameof(limit));

            if (limit > 200) limit = 200;

            query.AddOrUpdate("limit", limit);
            return query;
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
        /// <exception cref="EcwidConfigException">Can not add or update statuses. Look inner exception.</exception>
        /// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        public static async Task<LegacyUpdatedOrders> UpdateAsync(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string newPaymentStatus,
            string newFulfillmentStatus, string newShippingTrackingCode)
            =>
                await query.UpdateAsync(newPaymentStatus, newFulfillmentStatus, newShippingTrackingCode,
                    CancellationToken.None);

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
        /// <exception cref="EcwidConfigException">Can not add or update statuses. Look inner exception.</exception>
        /// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        public static async Task<LegacyUpdatedOrders> UpdateAsync(
            this OrdersQueryBuilder<LegacyOrder, LegacyUpdatedOrders> query, string newPaymentStatus,
            string newFulfillmentStatus, string newShippingTrackingCode, CancellationToken cancellationToken)
        {
            try
            {
                Validators.ValidateNewLegacyStatuses(query.Query, newPaymentStatus, newFulfillmentStatus,
                    newShippingTrackingCode);

                if (!string.IsNullOrWhiteSpace(newPaymentStatus))
                    query.AddOrUpdate("new_payment_status",
                        newPaymentStatus.ExtractFirstStatus(Validators.AvailableLegacyPaymentStatuses));
                if (!string.IsNullOrWhiteSpace(newFulfillmentStatus))
                    query.AddOrUpdate("new_fulfillment_status",
                        newFulfillmentStatus.ExtractFirstStatus(Validators.AvailableLegacyFulfillmentStatuses));
                if (!string.IsNullOrWhiteSpace(newShippingTrackingCode))
                    query.AddOrUpdate("new_shipping_tracking_code", newShippingTrackingCode);
            }
            catch (ArgumentException exception)
            {
                throw new EcwidException("Can not add or update statuses. Look inner exception.", exception);
            }

            var client = (EcwidLegacyClient) query.Client;

            return await client.UpdateOrdersAsync(query, cancellationToken);
        }
    }
}