using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecwid.Services
{
    /// <summary>
    /// Query builder for Ecwid client
    /// </summary>
    public abstract class QueryBuilder
    {
        /// <summary>
        /// The query params
        /// </summary>
        internal readonly Dictionary<string, object> QueryParams = new Dictionary<string, object>();

        /// <summary>
        /// Add or update.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        protected void Add(string name, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            QueryParams[name] = value;
        }
    }

    /// <summary>
    /// Orders query builder for Ecwid client
    /// </summary>
    public class OrdersQueryBuilder : QueryBuilder
    {
        /// <summary>
        /// The client
        /// </summary>
        internal IEcwidOrdersClient Client { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        internal OrdersQueryBuilder(IEcwidOrdersClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Adds the or update.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal OrdersQueryBuilder AddOrUpdate(string name, object value)
        {
            Add(name, value);
            return this;
        }

        /// <summary>
        /// Add or update.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        internal OrdersQueryBuilder AddOrUpdateStatuses(ICollection<string> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            string result;

            if (QueryParams.ContainsKey("statuses"))
            {
                var oldValue = (string)QueryParams["statuses"];
                var addStatuses = values.Except(oldValue.Split(','));

                result = string.Concat(oldValue, ",", string.Join(",", addStatuses));
            }
            else
            {
                result = string.Join(",", values);
            }

            QueryParams["statuses"] = result;
            return this;
        }
    }
}
