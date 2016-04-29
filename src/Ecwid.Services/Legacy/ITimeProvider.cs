// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;

namespace Ecwid.Legacy
{
    /// <summary>
    /// Interface to shim DateTime.Now
    /// </summary>
    internal interface ITimeProvider
    {
        DateTime Now { get; }
    }

    /// <summary>
    /// Real provider for non test env.
    /// </summary>
    /// <seealso cref="ITimeProvider" />
    internal class RealTimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}