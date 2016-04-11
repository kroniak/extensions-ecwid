﻿// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Diagnostics.CodeAnalysis;
using Ecwid.Tools;
using Xunit;

namespace Ecwid.Test.Tools
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
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