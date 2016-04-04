using System;
using System.Collections.Generic;
using Ecwid.Services.Legacy;
using Xunit;

namespace Ecwid.Test.Services.Legacy
{
    /// <summary>
    /// Test for one limit functionality of Ecwid Legacy
    /// </summary>
    public class OneLimitTest
    {
        private readonly List<bool> _results = new List<bool>();

        [Fact]
        public void TickExpectPass()
        {
            var limit = new Limit(1000, 3);

            3.Times(() => _results.Add(limit.Tick()));

            Assert.DoesNotContain(false, _results);
        }

        [Fact]
        public void TickExpectFail()
        {
            var limit = new Limit(1000, 3);

            4.Times(() => _results.Add(limit.Tick()));

            Assert.Contains(false, _results);
        }

        [Theory]
        [InlineData(5, 3)]
        [InlineData(50, 3)]
        [InlineData(500, 3)]
        public void TickExpectPass(int timeInterval, int limitValue)
        {
            var mockTime = Moqs.MockTime();

            var limit = new Limit(timeInterval, limitValue) { TimeProvider = mockTime.Object };

            limitValue.Times(() => _results.Add(limit.Tick()));

            //Switch time to + [n] sec.
            mockTime.SetupGet(t => t.Now).Returns(DateTime.Now.AddSeconds(timeInterval));

            // Check again
            limitValue.Times(() => _results.Add(limit.Tick()));

            Assert.DoesNotContain(false, _results);
        }

        [Theory]
        [InlineData(5, 3, 4)]
        [InlineData(50, 3, 4)]
        [InlineData(500, 3, 4)]
        public void TickExpectFail(int timeInterval, int limitValue, int realLimit)
        {
            var mockTime = Moqs.MockTime();

            var limit = new Limit(timeInterval, limitValue) { TimeProvider = mockTime.Object };

            limitValue.Times(() => _results.Add(limit.Tick()));

            //Switch time to + [n] sec.
            mockTime.SetupGet(t => t.Now).Returns(DateTime.Now.AddSeconds(timeInterval));

            // Check again - it will be fail
            realLimit.Times(() => _results.Add(limit.Tick()));

            Assert.Contains(false, _results);
        }
    }
}