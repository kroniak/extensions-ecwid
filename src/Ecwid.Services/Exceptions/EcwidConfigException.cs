// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;

namespace Ecwid.Services
{
    /// <summary>
    /// Throws when credentials are invalid or query builder can not build a query with some errors in parameters.
    /// </summary>
    public class EcwidConfigException : EcwidException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidConfigException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        internal EcwidConfigException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidConfigException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        internal EcwidConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}