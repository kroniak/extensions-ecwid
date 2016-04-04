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
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
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