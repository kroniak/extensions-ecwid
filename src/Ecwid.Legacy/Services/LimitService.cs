// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Ecwid.Legacy
{
    /// <summary>
    /// Represent limitation functionality of Ecwid Legacy API
    /// </summary>
    public class LimitsService
    {
        // All limits
        private readonly BlockingCollection<Limit> _limits = new BlockingCollection<Limit>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LimitsService" /> class.
        /// </summary>
        public LimitsService()
        {
            var rates = new SortedList<int, int>(3) {{5, 100}, {50, 400}, {500, 1400}};

            rates.ToList().ForEach(rate => _limits.Add(new Limit(rate.Key, rate.Value)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LimitsService" /> class.
        /// </summary>
        public LimitsService(Dictionary<int, int> rates = null, ITimeProvider timeProvider = null)
        {
            rates = rates ?? new Dictionary<int, int>(3);

            if (rates.Count == 0)
            {
                rates.Add(5, 100);
                rates.Add(50, 400);
                rates.Add(500, 1400);
            }

            foreach (var rate in rates)
            {
                _limits.Add(timeProvider != null
                    ? new Limit(rate.Key, rate.Value) {TimeProvider = timeProvider}
                    : new Limit(rate.Key, rate.Value));
            }
        }

        /// <summary>
        /// Gets the agreement and tick. Return true if agreement was got and tick was success.
        /// </summary>
        public bool Tick()
        {
            var result = GetAgreement();
            return result && _limits.Aggregate(true, (current, limit) => current & limit.Tick());
        }

        /// <summary>
        /// Get info about current value of limit value
        /// </summary>
        public IDictionary<int, int> GetInfo()
        {
            IDictionary<int, int> result = new Dictionary<int, int>(3);

            foreach (var limit in _limits)
            {
                result.Add(limit.GetInfo());
            }

            return result;
        }

        /// <summary>
        /// Gets the agreement. Return true if all limits is not exceed.
        /// </summary>
        private bool GetAgreement()
        {
            return _limits.Aggregate(true, (current, limit) => current & limit.Check());
        }
    }
}