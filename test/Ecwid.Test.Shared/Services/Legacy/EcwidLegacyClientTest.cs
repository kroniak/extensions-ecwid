using Ecwid.Services;
using Ecwid.Tools;
using Xunit;

namespace Ecwid.Test.Services.Legacy
{
    public class EcwidLegacyClientTest
    {
        private readonly EcwidLegacyClient _defaultClient = new EcwidLegacyClient();

        [Fact]
        public void DefaultCreatePass()
        {
            var options = _defaultClient.Options;

            Assert.Null(options.ShopId);
            Assert.Equal("https://app.ecwid.com/api/v1/", options.ApiUrl);
            Assert.Equal(600, options.MaxSecondsToWait);
            Assert.Equal(1, options.RetryInterval);
            Assert.Null(options.ShopOrderAuthId);
            Assert.Null(options.ShopProductAuthId);
        }

        [Fact]
        public void ConfigurePass()
        {
            var result = _defaultClient.Configure(new EcwidLegacyOptions() { ShopId = 123 });

            Assert.NotNull(result);
            Assert.StrictEqual(_defaultClient, result);
        }

        [Fact]
        public void ConfigureFail() => Assert.Throws<ConfigException>(() => _defaultClient.Configure(new EcwidLegacyOptions() { ShopId = 0 }));

        [Fact]
        public void ConfigureShopPass()
        {
            var client = _defaultClient.ConfigureShop(123);
            var client2 = _defaultClient.ConfigureShop(123, "", "");
            var client3 = _defaultClient.ConfigureShop(123, "test", "test");

            Assert.NotNull(client);
            Assert.NotNull(client2);
            Assert.NotNull(client3);
            Assert.StrictEqual(_defaultClient, client);
            Assert.StrictEqual(_defaultClient, client2);
            Assert.StrictEqual(_defaultClient, client3);
            Assert.Equal("test", client3.Options.ShopOrderAuthId);
            Assert.Equal("test", client3.Options.ShopProductAuthId);
        }
    }
}
