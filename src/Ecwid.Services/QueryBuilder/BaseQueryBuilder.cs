// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;

namespace Ecwid.Services
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
        /// Add or update.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException">The property is set and the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.</exception>
        protected void Add(string name, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Query[name] = value;
        }
    }
}