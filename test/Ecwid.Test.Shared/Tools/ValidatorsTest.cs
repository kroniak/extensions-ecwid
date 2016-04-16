// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using Ecwid.Tools;
using Xunit;

namespace Ecwid.Test.Tools
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class ValidatorsTest
    {
        [Theory]
        [InlineData("DECLINED, CANCELLED")]
        [InlineData("DECLINED,CANCELLED")]
        [InlineData("DECLINED ,CANCELLED")]
        [InlineData("DECLINED CANCELLED")]
        public void StatusesValidatePass(string str)
        {
            var result = Validators.StatusesValidate(str, Validators.AvailableLegacyPaymentStatuses);
            Assert.True(result);
        }

        [Theory]
        [InlineData("DECLINED, CANCEL")]
        [InlineData("DECLINED,CANCEL")]
        [InlineData("DECLINED ,CANCEL")]
        [InlineData("DECLINED CANCELLED FAIL")]
        public void StatusesValidateFail(string str)
            =>
                Assert.Throws<ArgumentException>(
                    () => Validators.StatusesValidate(str, Validators.AvailableLegacyPaymentStatuses));

        [Fact]
        public void StatusesValidateNullFail()
            =>
                Assert.Throws<ArgumentException>(
                    () => Validators.StatusesValidate(null, Validators.AvailableLegacyPaymentStatuses));

        [Theory]
        [InlineData("DECLINED")]
        [InlineData("DECLINED,")]
        [InlineData("declined")]
        public void StatusValidatePass(string str)
        {
            var result = Validators.StatusValidate(str, Validators.AvailableLegacyPaymentStatuses);
            Assert.Equal("DECLINED", result);
        }

        [Fact]
        public void StatusValidateFail()
            =>
                Assert.Throws<ArgumentException>(
                    () => Validators.StatusValidate("FAIL", Validators.AvailableLegacyPaymentStatuses));

        [Fact]
        public void StatusValidateMultiFail()
            =>
                Assert.Throws<ArgumentException>(
                    () => Validators.StatusValidate("DECLINED, ACCEPTED", Validators.AvailableLegacyPaymentStatuses));

        [Fact]
        public void StringsValidatePass() => Assert.False(Validators.AreNullOrEmpty("A", "B"));

        [Theory]
        [InlineData("", "", "")]
        [InlineData("", "", null)]
        [InlineData(null, null, null)]
        public void StringsValidateFail(string str, string str2, string str3)
            => Assert.True(Validators.AreNullOrEmpty(str, str2, str3));
    }
}