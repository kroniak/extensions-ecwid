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
        public void ShopIdValidateRangeFail(int shopId)
        {
            Assert.Throws<ConfigException>(() => Validators.ShopIdValidate(shopId));
        }

        [Fact]
        public void ShopIdValidateNullFail()
        {
            Assert.Throws<ConfigException>(() => Validators.ShopIdValidate(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShopAuthValidateNullOrEmptyFail(string str)
        {
            Assert.Throws<ConfigException>(() => Validators.ShopAuthValidate(str));
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
        public void PaymentStatusesValidateFail(string str)
        {
            Assert.Throws<InvalidArgumentException>(() => Validators.PaymentStatusesValidate(str));
        }

        [Fact]
        public void PaymentStatusesValidateNullFail()
        {
            Assert.Throws<InvalidArgumentException>(() => Validators.PaymentStatusesValidate(null));
        }

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
        public void FulfillmentStatusesValidateFail(string str)
        {
            Assert.Throws<InvalidArgumentException>(() => Validators.FulfillmentStatusesValidate(str));
        }

        [Fact]
        public void FulfillmentStatusesValidateNullFail()
        {
            Assert.Throws<InvalidArgumentException>(() => Validators.FulfillmentStatusesValidate(null));
        }
    }
}
