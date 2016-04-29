// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Ecwid.Legacy;
using Xunit;

namespace Ecwid.Test.Services.Legacy
{
    /// <summary>
    /// Test for Limits functionality of Ecwid Legacy
    /// </summary>
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class LimitsServiceTest
    {
        private readonly List<bool> _results = new List<bool>();
        private LimitsService _limitsService;

        [Fact]
        public void SimpleTickExpectTrue()
        {
            _limitsService = new LimitsService();

            var result = _limitsService.Tick();

            Assert.True(result);
        }

        [Fact]
        public void TickExpect()
        {
            var mockTime = Moqs.MockTime();
            // Init service
            _limitsService = new LimitsService(timeProvider: mockTime.Object);

            var currentTime = DateTime.Now;

            // max 1400 in 500 sec - real 3
            3.Times(() =>
            {
                // max 400 in 50 sec - real 8
                8.Times(() =>
                {
                    // max 100 in 5 sec - real 50
                    50.Times(() => _results.Add(_limitsService.Tick()));

                    //Switch time to + [5] sec.
                    // ReSharper disable once AccessToModifiedClosure
                    currentTime = currentTime.AddSeconds(5);
                    mockTime.SetupGet(t => t.Now).Returns(currentTime);
                });

                //Switch time to + [50] sec.
                currentTime = currentTime.AddSeconds(50);
                mockTime.SetupGet(t => t.Now).Returns(currentTime);
            });

            //Switch time to + [500] sec.
            currentTime = currentTime.AddSeconds(500);
            mockTime.SetupGet(t => t.Now).Returns(currentTime);

            // Test again
            // max 100 in 5 sec
            80.Times(() => _results.Add(_limitsService.Tick()));

            Assert.DoesNotContain(false, _results);
        }

        [Fact]
        public void TickExpectFail()
        {
            var mockTime = Moqs.MockTime();
            // Init service
            _limitsService = new LimitsService(timeProvider: mockTime.Object);

            var currentTime = DateTime.Now;

            // max 1400 in 500 sec - real 3
            4.Times(() =>
            {
                // max 400 in 50 sec - real 8
                8.Times(() =>
                {
                    // max 100 in 5 sec - real 50
                    50.Times(() => _results.Add(_limitsService.Tick()));

                    //Switch time to + [5] sec.
                    // ReSharper disable once AccessToModifiedClosure
                    currentTime = currentTime.AddSeconds(5);
                    mockTime.SetupGet(t => t.Now).Returns(currentTime);
                });

                //Switch time to + [50] sec.
                currentTime = currentTime.AddSeconds(50);
                mockTime.SetupGet(t => t.Now).Returns(currentTime);
            });

            //Switch time to + [500] sec.
            currentTime = currentTime.AddSeconds(500);
            mockTime.SetupGet(t => t.Now).Returns(currentTime);

            // Test again
            // max 100 in 5 sec
            80.Times(() => _results.Add(_limitsService.Tick()));

            Assert.Contains(false, _results);
        }
    }
}