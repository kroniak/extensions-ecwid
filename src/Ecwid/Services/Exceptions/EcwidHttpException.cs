// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Net;

namespace Ecwid
{
    /// <inheritdoc />
    public class EcwidHttpException : EcwidException
    {
        /// <inheritdoc />
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