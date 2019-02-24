// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;

namespace Ecwid.Legacy
{
    /// <summary>
    /// Interface to shim DateTime.Now
    /// </summary>
    public interface ITimeProvider
    {
        /// <summary>
        /// Gets the now.
        /// </summary>
        /// <value>
        /// The now.
        /// </value>
        DateTime Now { get; }
    }

    /// <inheritdoc />
    public class RealTimeProvider : ITimeProvider
    {
        /// <inheritdoc />
        public DateTime Now => DateTime.Now;
    }
}