// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;

namespace Ecwid
{
    /// <inheritdoc />
    public class EcwidConfigException : EcwidException
    {
        /// <inheritdoc />
        public EcwidConfigException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public EcwidConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}