// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Ecwid.Tools;
using Xunit;

namespace Ecwid.Test.Services
{
    public class ExtensionsTest
    {
        [Fact]
        public void ExtractFirstStatus_CorrectResult()
        {
            var expectedAvailable = new[] {"ONE", "SECOND"};
            const string expected = "ONE";
            var result = "one, second".ExtractFirstStatus(expectedAvailable);

            Assert.Equal(expected, result);
        }
    }
}