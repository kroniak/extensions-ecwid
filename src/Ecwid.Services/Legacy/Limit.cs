// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;

namespace Ecwid.Services.Legacy
{
    /// <summary>
    /// Represent one limit by time interval and value limit
    /// </summary>
    internal class Limit
    {
        private readonly int _limitValue;

        // Internal settings
        private readonly int _timeInterval;
        // Counters
        private DateTime _start;
        private ITimeProvider _timeProvider;
        private int _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Limit" /> class.
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
        /// The time provider <see cref="ITimeProvider" />
        /// </value>
        public ITimeProvider TimeProvider
        {
            private get { return _timeProvider ?? (_timeProvider = new RealTimeProvider()); }
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