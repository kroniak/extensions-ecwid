// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;

namespace Ecwid
{
    /// <inheritdoc />
    public class EcwidConfigException : EcwidException
    {
        /// <inheritdoc />
        internal EcwidConfigException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        internal EcwidConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}