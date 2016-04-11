﻿// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Ecwid.Models;

namespace Ecwid.Services
{
    /// <summary>
    /// Orders query builder for Ecwid client.
    /// </summary>
    /// <typeparam name="TOrder">The type of the order.</typeparam>
    /// <typeparam name="TUpdateResponse">The type of the update response.</typeparam>
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
        /// Adds the or update.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException">The property is set and the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.</exception>
        internal OrdersQueryBuilder<TOrder, TUpdateResponse> AddOrUpdate(string name, object value)
        {
            Add(name, value);
            return this;
        }

        /// <summary>
        /// Add or update.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException">The property is set and the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.</exception>
        /// <exception cref="KeyNotFoundException">The property is retrieved and is not found.</exception>
        internal OrdersQueryBuilder<TOrder, TUpdateResponse> AddOrUpdateStatuses(ICollection<string> values)
        {
            if (values == null)
                // TODO tests
                throw new ArgumentNullException(nameof(values));

            string result;

            if (Query.ContainsKey("statuses"))
            {
                var oldValue = (string)Query["statuses"];
                var addStatuses = values.Except(oldValue.Split(','));

                result = string.Concat(oldValue, ",", string.Join(",", addStatuses));
            }
            else
            {
                result = string.Join(",", values);
            }

            Query["statuses"] = result;
            return this;
        }
    }
}