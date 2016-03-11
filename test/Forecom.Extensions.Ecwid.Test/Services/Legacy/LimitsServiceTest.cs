using System;
using System.Collections.Generic;
using Forecom.Extensions.Ecwid.Services.Legacy;
using Moq;
using Xunit;

namespace Forecom.Extensions.Ecwid.Test.Services.Legacy
{
    /// <summary>
    /// Test for Limits functionality of Ecwid Lagacy
    /// </summary>
    public class LimitsServiceTest
    {
        private LimitsService _limitsService;

        [Fact]
        public void SimpleTickExpectTrue()
        {
            _limitsService = new LimitsService();

            var result = _limitsService.Tick();
            Assert.True(result);
        }

        [Fact]
        // Bug. Test Failed
        public void TickExpectPass()
        {
            // Mock current time
            var mockTime = new Mock<ITimeProvider>();
            // Init time
            mockTime.SetupGet(t => t.Now).Returns(DateTime.Now);

            // Init service
            _limitsService = new LimitsService(timeProvider: mockTime.Object);

            var result = new List<bool>();

            // max 1400 in 500 sec
            for (var a = 1; a <= 3; a++)
            {
                // max 400 in 50 sec
                for (var b = 1; b < 5; b++)
                {
                    // max 100 in 5 sec
                    for (var c = 1; c <= 80; c++)
                        result.Add(_limitsService.Tick());
                    //Switch time to + [n] sec.
                    mockTime.SetupGet(t => t.Now).Returns(DateTime.Now.AddSeconds(6));
                }
                //Switch time to + [n] sec.
                mockTime.SetupGet(t => t.Now).Returns(DateTime.Now.AddSeconds(50));
            }
            //Switch time to + [n] sec.
            mockTime.SetupGet(t => t.Now).Returns(DateTime.Now.AddSeconds(500));

            // Test again
            // max 100 in 5 sec
            for (var c = 1; c <= 80; c++)
                result.Add(_limitsService.Tick());

            Assert.DoesNotContain(false, result);
        }
    }
}