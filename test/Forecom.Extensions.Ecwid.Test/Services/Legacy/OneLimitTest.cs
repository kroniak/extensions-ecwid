using System;
using System.Collections.Generic;
using Forecom.Extensions.Ecwid.Services.Legacy;
using Moq;
using Xunit;

namespace Forecom.Extensions.Ecwid.Test.Services.Legacy
{
    /// <summary>
    /// Test for one limit functionality of Ecwid Lagacy
    /// </summary>
    public class OneLimitTest
    {
        [Theory]
        [InlineData(5, 100)]
        [InlineData(50, 400)]
        [InlineData(500, 1400)]
        public void TickExpectPass(int timeInterval, int limitValue)
        {
            // Mock current time
            var mockTime = new Mock<ITimeProvider>();
            // Init time
            mockTime.SetupGet(t => t.Now).Returns(DateTime.Now);

            var limit = new Limit(timeInterval, limitValue) { TimeProvider = mockTime.Object };
            var results = new List<bool>();

            for (var i = 1; i <= limitValue; i++)
                results.Add(limit.Tick());

            //Switch time to + [n] sec.
            mockTime.SetupGet(t => t.Now).Returns(DateTime.Now.AddSeconds(timeInterval));

            // Check again
            for (var i = 1; i <= limitValue; i++)
                results.Add(limit.Tick());

            Assert.DoesNotContain(false, results);
        }

        [Theory]
        [InlineData(5, 100, 105)]
        [InlineData(50, 400, 405)]
        [InlineData(500, 1400, 2000)]
        public void TickExpectFail(int timeInterval, int limitValue, int realLimit)
        {
            // Mock current time
            var mockTime = new Mock<ITimeProvider>();
            // Init time
            mockTime.SetupGet(t => t.Now).Returns(DateTime.Now);

            var limit = new Limit(timeInterval, limitValue) { TimeProvider = mockTime.Object };
            var results = new List<bool>();

            for (var i = 1; i <= limitValue; i++)
                results.Add(limit.Tick());

            //Switch time to + [n] sec.
            mockTime.SetupGet(t => t.Now).Returns(DateTime.Now.AddSeconds(timeInterval));

            // Check again
            for (var i = 1; i <= realLimit; i++)
                results.Add(limit.Tick());

            Assert.Contains(false, results);
        }
    }
}