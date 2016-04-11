// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecwid.Tools
{
    /// <summary>
    /// Some validators for classes
    /// </summary>
    internal abstract class Validators
    {
        /// <summary>
        /// Shops identifier validate.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <returns>
        /// True if shopId was passed validation
        /// </returns>
        /// <exception cref="System.ArgumentException">The shop identificator is invalid. Please reconfig the client.</exception>
        public static bool ShopIdValidate(int? shopId)
        {
            if (shopId == null || shopId <= 0)
                throw new ArgumentException("The shop identificator is invalid. Please reconfig the client.",
                    nameof(shopId));

            return true;
        }

        /// <summary>
        /// Shops the authentication validate.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">The token is null or empty. Please reconfig the client.</exception>
        public static bool TokenValidate(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("The token is null or empty. Please reconfig the client.", nameof(str));

            return true;
        }

        /// <summary>
        /// Strings validate.
        /// </summary>
        /// <param name="strings">The strings.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">All strings params is null or empty</exception>
        public static bool StringsValidate(params string[] strings)
        {
            if (strings.All(string.IsNullOrEmpty))
                throw new ArgumentException("All strings params is null or empty", nameof(strings));

            return true;
        }

        /// <summary>
        /// Payment statuses validate.
        /// </summary>
        /// <param name="statuses">The statuses.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Payment statuses string is empty or null.
        /// or
        /// Payment statuses string is invalid.
        /// </exception>
        public static bool PaymentStatusesValidate(string statuses)
        {
            if (string.IsNullOrEmpty(statuses))
                throw new ArgumentException("Payment statuses string is empty or null.", nameof(statuses));

            var paymentStatusesAvailable = new List<string>
            {
                "PAID",
                "ACCEPTED",
                "DECLINED",
                "CANCELLED",
                "AWAITING_PAYMENT",
                "QUEUED",
                "CHARGEABLE, REFUNDED",
                "INCOMPLETE"
            };

            if (!CheckContainsString(statuses, paymentStatusesAvailable))
                throw new ArgumentException("Payment statuses string is invalid.", nameof(statuses));

            return true;
        }

        /// <summary>
        /// Payment status validate and return.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Payment status string is invalid.
        /// or
        /// Payment status string is invalid. Support only one status.
        /// </exception>
        /// <exception cref="ArgumentNullException">is null. </exception>
        public static string PaymentStatusValidate(string status)
        {
            var paymentStatusesAvailable = new List<string>
            {
                "PAID",
                "ACCEPTED",
                "DECLINED",
                "CANCELLED",
                "AWAITING_PAYMENT",
                "QUEUED",
                "CHARGEABLE, REFUNDED",
                "INCOMPLETE"
            };

            if (!CheckContainsString(status, paymentStatusesAvailable))
                throw new ArgumentException("Payment status string is invalid.", nameof(status));

            var result = status.TrimUpperReplaceSplit();

            if (result.Count > 1)
                throw new ArgumentException("Payment status string is invalid. Support only one status.", nameof(status));

            return result.First();
        }

        /// <summary>
        /// Fulfillments statuses validate.
        /// </summary>
        /// <param name="statuses">The statuses.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Fulfillment statuses string is invalid.
        /// or
        /// Fulfillment statuses string is invalid.
        /// </exception>
        public static bool FulfillmentStatusesValidate(string statuses)
        {
            if (string.IsNullOrEmpty(statuses))
                throw new ArgumentException("Fulfillment statuses string is invalid.", nameof(statuses));

            var fulfillmentStatusesAvailable = new List<string>
            {
                "AWAITING_PROCESSING",
                "NEW",
                "PROCESSING",
                "SHIPPED",
                "DELIVERED",
                "WILL_NOT_DELIVER",
                "RETURNED"
            };

            if (!CheckContainsString(statuses, fulfillmentStatusesAvailable))
                throw new ArgumentException("Fulfillment statuses string is invalid.", nameof(statuses));

            return true;
        }

        /// <summary>
        /// Fulfillment status validate and return.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Fulfillment status string is invalid.
        /// or
        /// Fulfillment status string is invalid. Support only one status.
        /// </exception>
        /// <exception cref="ArgumentNullException">is null. </exception>
        public static string FulfillmentStatusValidate(string status)
        {
            var fulfillmentStatusesAvailable = new List<string>
            {
                "AWAITING_PROCESSING",
                "NEW",
                "PROCESSING",
                "SHIPPED",
                "DELIVERED",
                "WILL_NOT_DELIVER",
                "RETURNED"
            };

            if (!CheckContainsString(status, fulfillmentStatusesAvailable))
                throw new ArgumentException("Fulfillment status string is invalid.", nameof(status));

            var result = status.TrimUpperReplaceSplit();

            if (result.Count > 1)
                throw new ArgumentException("Fulfillment status string is invalid. Support only one status.",
                    nameof(status));

            return result.First();
        }

        /// <summary>
        /// Checks the contains words of the string in the list.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        private static bool CheckContainsString(string str, ICollection<string> list)
        {
            var result = str.TrimUpperReplaceSplit();

            return result.Aggregate(true, (current, s) => current && list.Contains(s));
        }
    }
}