using System;
using Forecom.Extensions.Ecwid.Services.Legacy;
using Moq;

namespace Forecom.Extensions.Ecwid.Test.Services.Legacy
{
    internal static class Tools
    {
        public static Mock<ITimeProvider> MockTime()
        {
            // Mock current time
            var mockTime = new Mock<ITimeProvider>();
            // Init time
            mockTime.SetupGet(t => t.Now).Returns(DateTime.Now);
            return mockTime;
        }

        public static void Times(this int count, Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            for (var i = 0; i < count; i++)
            {
                action();
            }
        }
    }
}