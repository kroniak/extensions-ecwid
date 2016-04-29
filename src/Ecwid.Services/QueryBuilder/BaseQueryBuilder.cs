// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using Ecwid.Tools;

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
        internal readonly Dictionary<string, object> Query = new Dictionary<string, object>();

        /// <summary>
        /// Add or update param in dictionary.
        /// </summary>
        /// <param name="name">The name. Must be not <see langword="null" /> or <see langword="empty" />.</param>
        /// <param name="value">The value. Must be not <see langword="null" />.</param>
        /// <exception cref="ArgumentException">Can not add value to query. Look inner exception.</exception>
        /// <exception cref="ArgumentException"><paramref name="name" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException"><paramref name="value" /> is <see langword="null" />.</exception>
        internal void AddOrUpdate(string name, object value)
        {
            if (Validators.IsNullOrEmpty(name))
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