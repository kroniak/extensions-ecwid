// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using Ecwid.Tools;
using Xunit;

// ReSharper disable PossibleMultipleEnumeration

namespace Ecwid.Test.Tools
{
    //TODO Convert tests
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class ExtensionsTest
    {
        [Theory]
        [InlineData("A B C   D, E  ")]
        [InlineData("A B,, , C   D, E   ")]
        public void TrimReplaceSplit(string str)
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

        [Fact]
        public void TrimReplaceSplitFail()
        {
            var exception = Assert.Throws<ArgumentException>(() => ((string) null).TrimUpperReplaceSplit());
            Assert.Contains("Unable replace chars and split string", exception.Message);
        }
    }
}