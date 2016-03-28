using System.Net.Http;
using System.Threading;
using Ecwid.Services.Legacy;
using Ecwid.Tools;
using Flurl.Http.Testing;
using Xunit;
// ReSharper disable MethodSupportsCancellation

namespace Ecwid.Services.Test.Services.Legacy
{
    /// <summary>
    /// Tests with fake http responces
    /// </summary>
    public class EcwidLegacyClientOrdersTest
    {
        // Tests params
        private const int ShopId = 123;
        private const string OrdersAuth = "test";

        //For checking
        private static readonly string CheckOrdersUrl = $"https://app.ecwid.com/api/v1/{ShopId}/orders?secure_auth_key={OrdersAuth}";

        // GLobal objects for testing
        private readonly IEcwidOrdersClientLegacy _defaultClient = new EcwidLegacyClient();
        private readonly IEcwidOrdersClientLegacy _client = new EcwidLegacyClient().ConfigureShop(ShopId, OrdersAuth, OrdersAuth);
        private readonly HttpTest _httpTest = new HttpTest();
        private readonly CancellationToken _cancellationToken = new CancellationToken();

        [Fact]
        public async void OrdersUrlException() => await Assert.ThrowsAsync<ConfigException>(() => _defaultClient.CheckOrdersAuthAsync(_cancellationToken));

        [Fact]
        public void OrdersGetEmptyPass()
        {
            var query = _client.Orders;
            Assert.NotNull(query);
            Assert.Empty(query.QueryParams);
            Assert.NotNull(query.Client);
            Assert.StrictEqual(_client, query.Client);
        }

        [Fact]
        public async void CheckOrdersAuthAsyncPass()
        {
            _httpTest
                .RespondWithJson(new { count = 0, total = 0, order = "[]" })
                .RespondWithJson(new { count = 0, total = 0, order = "[]" });

            var result = await _client.CheckOrdersAuthAsync();
            var result2 = await _client.CheckOrdersAuthAsync(_cancellationToken);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=0")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(true, result);
            Assert.Equal(true, result2);
        }

        [Fact]
        public async void CheckOrdersAuthAsyncFail()
        {
            _httpTest
                .RespondWithJson(401, new { count = 0, total = 0, order = "[]" })
                .RespondWithJson(403, new { count = 0, total = 0, order = "[]" })
                .RespondWithJson(401, new { count = 0, total = 0, order = "[]" })
                .RespondWithJson(403, new { count = 0, total = 0, order = "[]" });

            var result = await _client.CheckOrdersAuthAsync();
            var result2 = await _client.CheckOrdersAuthAsync();
            var result3 = await _client.CheckOrdersAuthAsync(_cancellationToken);
            var result4 = await _client.CheckOrdersAuthAsync(_cancellationToken);


            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=0")
                .WithVerb(HttpMethod.Get)
                .Times(4);
            Assert.Equal(false, result);
            Assert.Equal(false, result2);
            Assert.Equal(false, result3);
            Assert.Equal(false, result4);
        }

        [Fact]
        public async void GetOrdersCountAsyncPass()
        {
            _httpTest
                .RespondWithJson(new { count = 0, total = 10, order = "[]" })
                .RespondWithJson(new { count = 0, total = 10, order = "[]" });

            var result = await _client.GetOrdersCountAsync();
            var result2 = await _client.GetOrdersCountAsync(_cancellationToken);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=0")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(10, result);
            Assert.Equal(10, result2);
        }

        [Fact]
        public async void GetNewOrdersAsyncPass()
        {
            var responce = Moqs.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(responce)
                .RespondWithJson(responce);

            var result = await _client.GetNewOrdersAsync();
            var result2 = await _client.GetNewOrdersAsync(_cancellationToken);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.NotEmpty(result);
            Assert.NotEmpty(result2);
        }

        [Fact]
        public async void GetNonPaidOrdersAsyncPass()
        {
            var responce = Moqs.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(responce)
                .RespondWithJson(responce);

            var result = await _client.GetNonPaidOrdersAsync();
            var result2 = await _client.GetNonPaidOrdersAsync(_cancellationToken);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.NotEmpty(result);
            Assert.NotEmpty(result2);
        }

        [Fact]
        public async void GetPaidNotShippedOrdersAsyncPass()
        {
            var responce = Moqs.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(responce)
                .RespondWithJson(responce);

            var result = await _client.GetPaidNotShippedOrdersAsync();
            var result2 = await _client.GetPaidNotShippedOrdersAsync(_cancellationToken);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.NotEmpty(result);
            Assert.NotEmpty(result2);
        }

        [Fact]
        public async void GetShippedNotDeliveredOrdersAsyncPass()
        {
            var responce = Moqs.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(responce)
                .RespondWithJson(responce);

            var result = await _client.GetShippedNotDeliveredOrdersAsync();
            var result2 = await _client.GetShippedNotDeliveredOrdersAsync(_cancellationToken);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.NotEmpty(result);
            Assert.NotEmpty(result2);
        }

        [Fact]
        public async void GetOrdersAsyncQueryMultiPagesResultPass()
        {
            _httpTest
                .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrderAndPages($"{CheckOrdersUrl}&limit=5&offset=5"))
                .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrder);

            var result = await _client.GetOrdersAsync(new { limit = 5 });

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=5&offset=5")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async void GetOrdersAsyncQueryMultiPagesResultCancellationPass()
        {
            _httpTest
                .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrderAndPages($"{CheckOrdersUrl}&limit=5&offset=5"))
                .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrder);

            var result = await _client.GetOrdersAsync(new { limit = 5 }, _cancellationToken);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=5&offset=5")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async void GetOrdersAsyncQueryBuilderMultiPagesResultPass()
        {
            _httpTest
                .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrderAndPages($"{CheckOrdersUrl}&limit=5&offset=5"))
                .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrder);

            var result = await _client.Orders.Limit(5).GetAsync();

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=5&offset=5")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async void GetOrdersAsyncQueryBuilderMultiPagesResultCancellationPass()
        {
            _httpTest
                .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrderAndPages($"{CheckOrdersUrl}&limit=5&offset=5"))
                .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrder);

            var result = await _client.Orders.Limit(5).GetAsync(_cancellationToken);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=5&offset=5")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.NotEmpty(result);
        }
    }
}
