// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Ecwid.Models;

namespace Ecwid
{
    /// <inheritdoc />
    public class OrdersQueryBuilder<TOrder, TUpdateResponse> : BaseQueryBuilder
        where TOrder : BaseOrder
        where TUpdateResponse : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersQueryBuilder{TOrder,TUpdateResponse}" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        internal OrdersQueryBuilder(IEcwidOrdersClient<TOrder, TUpdateResponse> client)
        {
            Client = client;
        }

        /// <value>
        /// The client.
        /// </value>
        internal IEcwidOrdersClient<TOrder, TUpdateResponse> Client { get; }

        /// <summary>
        /// Add or update.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="values">The values.</param>
        /// <exception cref="ArgumentException">Can not add or update statuses. Look inner exception.</exception>
        public void AddOrUpdateStatuses(string name, IEnumerable<string> values)
        {
            try
            {
                string result;
                if (Query.ContainsKey(name))
                {
                    var oldValue = (string) Query[name];
                    var addStatuses = values.Except(oldValue.Split(','));

                    result = string.Concat(oldValue, ",", string.Join(",", addStatuses));
                }
                else
                {
                    result = string.Join(",", values);
                }

                AddOrUpdate(name, result);
            }
            // ReSharper disable once CatchAllClause
            catch (Exception exception)
            {
                throw new ArgumentException("Can not add or update statuses. Look inner exception.", exception);
            }
        }
    }
}