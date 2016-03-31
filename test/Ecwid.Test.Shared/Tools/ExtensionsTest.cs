using Ecwid.Tools;
using Xunit;

namespace Ecwid.Test.Tools
{
    public class ExtensionsTest
    {
        [Theory]
        [InlineData("A B C   D, E;  ")]
        [InlineData("A B C   D, E; 0 9 ; 223  ")]
        public void TrimReplaceSplitPass(string str)
        {
            var result = str.TrimUpperReplaceSplit();

            Assert.Contains("A", result);
            Assert.Contains("B", result);
            Assert.Contains("C", result);
            Assert.Contains("D", result);
            Assert.Contains("E", result);
            Assert.DoesNotContain(" ", result);
            Assert.DoesNotContain(",", result);
            Assert.DoesNotContain(";", result);
        }
    }
}