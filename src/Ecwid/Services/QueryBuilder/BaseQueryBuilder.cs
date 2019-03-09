// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;

namespace Ecwid
{
    /// <summary>
    /// Base query builder for Ecwid client
    /// </summary>
    public abstract class BaseQueryBuilder
    {
        /// <summary>
        /// The query params
        /// </summary>
        public readonly Dictionary<string, object> Query = new Dictionary<string, object>();

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        public object GetParam(string name)
        {
            object result;
            try
            {
                result = Query[name];
            }
            catch (ArgumentNullException)
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// Add or update param in dictionary.
        /// </summary>
        /// <param name="name">The name. Must be not <see langword="null" /> or <see langword="empty" />.</param>
        /// <param name="value">The value. Must be not <see langword="null" />.</param>
        /// <exception cref="ArgumentException">Can not add value to query. Look inner exception.</exception>
        /// <exception cref="ArgumentException"><paramref name="name" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException"><paramref name="value" /> is <see langword="null" />.</exception>
        public void AddOrUpdate(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Can not add value to query.", nameof(name));

            if (value == null)
                throw new ArgumentException("Can not add value to query.", nameof(value));

            try
            {
                Query[name] = value;
            }
            // ReSharper disable once CatchAllClause
            catch (Exception exception)
            {
                throw new ArgumentException("Can not add value to query. Look inner exception.", exception);
            }
        }
    }
}