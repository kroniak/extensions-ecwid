using System;
using Ecwid.Misc;
using Xunit;

namespace Kroniak.Ecwid.Test.Misc
{
    public class ValidatorsTest
    {
        [Fact]
        public void ShopIdValidateRangePass()
        {
            var result = Validators.ShopIdValidate(1);
            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ShopIdValidateRangeFail(int shopId)
        {
            Exception ex = Assert.Throws<ConfigException>(() => Validators.ShopIdValidate(shopId));
            Assert.Equal("The shop identificator is invalid. Please config the client.", ex.Message);
        }

        [Fact]
        public void ShopIdValidateNullPass()
        {
            var result = Validators.ShopIdValidate(1);
            Assert.True(result);
        }

        [Fact]
        public void ShopIdValidateNullFail()
        {
            Exception ex = Assert.Throws<ConfigException>(() => Validators.ShopIdValidate(null));
            Assert.Equal("The shop identificator is null. Please config the client.", ex.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShopAuthValidateNullOrEmptyFail(string str)
        {
            Exception ex = Assert.Throws<ConfigException>(() => Validators.ShopAuthValidate(str));
            Assert.Equal("The shop auth identificator is null or empty. Please config the client.", ex.Message);
        }

        [Fact]
        public void ShopAuthValidateNullOrEmptyPass()
        {
            var result = Validators.ShopAuthValidate("teststring");
            Assert.True(result);
        }
    }
}
