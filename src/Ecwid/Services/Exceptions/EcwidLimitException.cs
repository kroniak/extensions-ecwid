// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Ecwid
{
    /// <summary>
    /// Throws when legacy client HTTP limit is overhead.
    /// </summary>
    public class EcwidLimitException : EcwidException
    {
        /// <summary>
        /// Info about current value of limit value. Key is time in seconds, Value is value (int)
        /// </summary>
        public IDictionary<int, int> CurrentLimitValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidLimitException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        internal EcwidLimitException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidLimitException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        internal EcwidLimitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidLimitException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="values">Current limits values.</param>
        internal EcwidLimitException(string message, IDictionary<int, int> values) : base(message)
        {
            CurrentLimitValues = values;
        }
    }
}