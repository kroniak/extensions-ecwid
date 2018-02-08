// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;

namespace Ecwid
{
    /// <summary>
    /// Base class for Ecwid client exception
    /// </summary>
    public class EcwidException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        protected EcwidException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        protected EcwidException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}