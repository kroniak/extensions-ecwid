using System;

namespace Forecom.Extensions.Ecwid.Services.Legacy
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
    /// <seealso cref="Forecom.Extensions.Ecwid.Services.Legacy.ITimeProvider" />
    internal class RealTimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.Now;
    }

    /// <summary>
    /// Represent one limit by time interval and value limit
    /// </summary>
    internal class Limit
    {
        // Counters
        private DateTime _start;
        private int _value;

        // Internal settings
        private readonly int _timeInterval;
        private readonly int _limitValue;
        private ITimeProvider _timeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Limit"/> class.
        /// </summary>
        /// <param name="timeInterval">The time interval in seconds.</param>
        /// <param name="limitValue">The count limit.</param>
        public Limit(int timeInterval, int limitValue)
        {
            _timeInterval = timeInterval;
            _limitValue = limitValue;
            _start = TimeProvider.Now;
        }

        /// <summary>
        /// Gets or sets the time provider
        /// </summary>
        /// <value>
        /// The time provider <see cref="ITimeProvider"/>
        /// </value>
        public ITimeProvider TimeProvider
        {
            private get
            {
                return _timeProvider ?? (_timeProvider = new RealTimeProvider());
            }
            set { _timeProvider = value; }
        }

        /// <summary>
        /// Ticks this instance. Return true if success
        /// </summary>
        public bool Tick()
        {
            if (!Check())
                return false;

            _value++;
            return true;
        }

        /// <summary>
        /// Checks this limit. Return false if limit is over
        /// </summary>
        public bool Check()
        {
            var time = TimeProvider.Now;

            if (_start.AddSeconds(_timeInterval) > time)
                return _value < _limitValue;
            _start = time;
            _value = 0;
            return true;
        }
    }
}