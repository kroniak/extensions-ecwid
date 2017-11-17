// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Net;

namespace Ecwid
{
    /// <summary>
    /// Throws when Ecwid API responses with status other than 200.
    /// </summary>
    public class EcwidHttpException : EcwidException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EcwidHttpException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="innerException">The exception.</param>
        internal EcwidHttpException(string message, HttpStatusCode? statusCode, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode? StatusCode { get; }
    }
}