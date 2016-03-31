using System;
using Ecwid.Tools;
using Xunit;

namespace Ecwid.Test.Tools
{
    public class ValidatorsTest
    {
        [Fact]
        public void ShopIdValidatePass()
        {
            var result = Validators.ShopIdValidate(1);
            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ShopIdValidateRangeFail(int shopId) => Assert.Throws<ArgumentException>(() => Validators.ShopIdValidate(shopId));

        [Fact]
        public void ShopIdValidateNullFail() => Assert.Throws<ArgumentException>(() => Validators.ShopIdValidate(null));

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShopAuthValidateNullOrEmptyFail(string str)
        {
            Assert.Throws<ArgumentException>(() => Validators.ShopAuthValidate(str));
        }

        [Fact]
        public void ShopAuthValidatePass()
        {
            var result = Validators.ShopAuthValidate("test");
            Assert.True(result);
        }

        [Theory]
        [InlineData("DECLINED, CANCELLED")]
        [InlineData("DECLINED,CANCELLED")]
        [InlineData("DECLINED ,CANCELLED")]
        [InlineData("DECLINED CANCELLED")]
        public void PaymentStatusesValidatePass(string str)
        {
            var result = Validators.PaymentStatusesValidate(str);
            Assert.True(result);
        }

        [Theory]
        [InlineData("DECLINED, CANCEL")]
        [InlineData("DECLINED,CANCEL")]
        [InlineData("DECLINED ,CANCEL")]
        [InlineData("DECLINED CANCELLED FAIL")]
        public void PaymentStatusesValidateFail(string str) => Assert.Throws<ArgumentException>(() => Validators.PaymentStatusesValidate(str));

        [Fact]
        public void PaymentStatusesValidateNullFail() => Assert.Throws<ArgumentException>(() => Validators.PaymentStatusesValidate(null));

        [Theory]
        [InlineData("DECLINED")]
        [InlineData("DECLINED,")]
        [InlineData("declined")]
        public void PaymentStatusValidatePass(string str)
        {
            var result = Validators.PaymentStatusValidate(str);
            Assert.Equal("DECLINED", result);
        }

        [Fact]
        public void PaymentStatusValidateFail() => Assert.Throws<ArgumentException>(() => Validators.PaymentStatusValidate("FAIL"));

        [Fact]
        public void PaymentStatusValidateMultiFail() => Assert.Throws<ArgumentException>(() => Validators.PaymentStatusValidate("DECLINED, ACCEPTED"));

        [Theory]
        [InlineData("NEW")]
        [InlineData("NEW,")]
        [InlineData("new")]
        public void FulfillmentStatusValidatePass(string str)
        {
            var result = Validators.FulfillmentStatusValidate(str);
            Assert.Equal("NEW", result);
        }

        [Fact]
        public void FulfillmentStatusValidateFail() => Assert.Throws<ArgumentException>(() => Validators.FulfillmentStatusValidate("FAIL"));

        [Fact]
        public void FulfillmentStatusValidateMultiFail() => Assert.Throws<ArgumentException>(() => Validators.FulfillmentStatusValidate("NEW, SHIPPED"));

        [Theory]
        [InlineData("NEW, PROCESSING")]
        [InlineData("NEW,PROCESSING")]
        [InlineData("NEW ,PROCESSING")]
        [InlineData("NEW PROCESSING ")]
        public void FulfillmentStatusesValidatePass(string str)
        {
            var result = Validators.FulfillmentStatusesValidate(str);
            Assert.True(result);
        }

        [Theory]
        [InlineData("NEW, PROCESSING ; FAIL")]
        [InlineData("NEW, PROCESSINGFAIL")]
        public void FulfillmentStatusesValidateFail(string str) => Assert.Throws<ArgumentException>(() => Validators.FulfillmentStatusesValidate(str));

        [Fact]
        public void FulfillmentStatusesValidateNullFail() => Assert.Throws<ArgumentException>(() => Validators.FulfillmentStatusesValidate(null));

        [Fact]
        public void StringsValidatePass() => Assert.True(Validators.StringsValidate("A", "B"));

        [Theory]
        [InlineData("", "", "")]
        [InlineData("", "", null)]
        [InlineData(null, null, null)]
        public void StringsValidateFail(string str, string str2, string str3) => Assert.Throws<ArgumentException>(() => Validators.StringsValidate(str, str2, str3));
    }
}