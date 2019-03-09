// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Ecwid.Legacy
{
    /// <inheritdoc />
    public class EcwidLimitException : EcwidException
    {
        /// <summary>
        /// Info about current value of limit value. Key is time in seconds, Value is value (int)
        /// </summary>
        private IDictionary<int, int> CurrentLimitValues;

        /// <inheritdoc />
        public EcwidLimitException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public EcwidLimitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc />
        internal EcwidLimitException(string message, IDictionary<int, int> values) : base(message)
        {
            CurrentLimitValues = values;
        }
    }
}