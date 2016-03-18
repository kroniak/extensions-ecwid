using System;
using Ecwid.Misc;
using Ecwid.Services.Legacy;
using Xunit;

namespace Kroniak.Ecwid.Test.Services.Legacy
{
    /// <summary>
    /// Tests for Ecwid legacy client
    /// </summary>
    public class EcwidLegacyClientTest
    {
        private readonly EcwidLegacyClient _defaultClient = new EcwidLegacyClient();

        [Fact]
        public void DefaultCreate()
        {
            var result = _defaultClient.Options;
            Assert.Null(result.ShopId);
        }

        [Fact]
        public async void DefaultApiUrlException()
        {
            Exception ex = await Assert.ThrowsAsync<ConfigException>(() => _defaultClient.CheckOrdersAuthAsync());
            Assert.Equal("The shop identificator is null. Please config the client.", ex.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ConfigureShopIdZero(int shopId)
        {
            Exception ex = Assert.Throws<ConfigException>(() => _defaultClient.ConfigureShop(shopId, "", ""));
            Assert.Equal("The shop identificator is invalid. Please config the client.", ex.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Configure2ShopIdZero(int shopId)
        {
            Exception ex = Assert.Throws<ConfigException>(() => _defaultClient.Configure(new EcwidLegacyOptions { ShopId = shopId }));
            Assert.Equal("The shop identificator is invalid. Please config the client.", ex.Message);
        }
    }
}
