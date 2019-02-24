// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;

namespace Ecwid
{
    /// <inheritdoc />
    public class EcwidException : Exception
    {
        /// <inheritdoc />
        protected EcwidException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        protected EcwidException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}