using System;

namespace Ecwid.Services.Legacy
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