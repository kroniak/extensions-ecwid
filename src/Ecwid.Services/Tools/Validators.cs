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
        /// <exception cref="ConfigException">The shop identificator is null. Please reconfig the client.
        /// or
        /// The shop identificator is invalid. Please reconfig the client.</exception>
        public static bool ShopIdValidate(int? shopId)
        {
            if (shopId == null)
                throw new ConfigException("The shop identificator is null. Please reconfig the client.");
            if (shopId <= 0)
                throw new ConfigException("The shop identificator is invalid. Please reconfig the client.");

            return true;
        }

        /// <summary>
        /// Shops the authentication validate.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <exception cref="ConfigException">The shop auth identificator is null or empty. Please config the client.</exception>
        public static bool ShopAuthValidate(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ConfigException("The shop auth identificator is null or empty. Please reconfig the client.");

            return true;
        }

        /// <summary>
        /// Payments statuses validate.
        /// </summary>
        /// <param name="statuses">The statuses.</param>
        /// <returns></returns>
        /// <exception cref="InvalidArgumentException">Payment statuses string is invalid</exception>
        public static bool PaymentStatusesValidate(string statuses)
        {
            if (string.IsNullOrEmpty(statuses))
                throw new InvalidArgumentException("Payment statuses string is invalid.");

            var paymentStatusesAvailable = new List<string>()
                { "PAID", "ACCEPTED", "DECLINED", "CANCELLED", "AWAITING_PAYMENT", "QUEUED", "CHARGEABLE, REFUNDED", "INCOMPLETE" };

            if (!CheckContainsString(statuses, paymentStatusesAvailable))
                throw new InvalidArgumentException("Payment statuses string is invalid.");

            return true;
        }

        /// <summary>
        /// Fulfillments statuses validate.
        /// </summary>
        /// <param name="statuses">The statuses.</param>
        /// <returns></returns>
        /// <exception cref="InvalidArgumentException">Fulfillment statuses string is invalid</exception>
        public static bool FulfillmentStatusesValidate(string statuses)
        {
            if (string.IsNullOrEmpty(statuses))
                throw new InvalidArgumentException("Fulfillment statuses string is invalid.");

            var fulfillmentStatusesAvailable = new List<string>()
                {"AWAITING_PROCESSING", "NEW", "PROCESSING", "SHIPPED", "DELIVERED", "WILL_NOT_DELIVER", "RETURNED"};

            if (!CheckContainsString(statuses, fulfillmentStatusesAvailable))
                throw new InvalidArgumentException("Fulfillment statuses string is invalid.");

            return true;
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
