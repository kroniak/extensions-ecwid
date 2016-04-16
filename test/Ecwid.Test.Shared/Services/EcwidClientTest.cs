// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using Ecwid.Services;
using Ecwid.Services.Legacy;
using Flurl.Http.Testing;
using Xunit;

namespace Ecwid.Test.Services
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class EcwidClientTest
    {
        // Tests params
        private const int ShopId = 123;
        private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";

        // Urls for checking
        private readonly string _checkOrdersLegacyUrl =
            $"https://app.ecwid.com/api/v1/{ShopId}/orders?secure_auth_key={Token}";

        private readonly string _checkOrdersUrl = $"https://app.ecwid.com/api/v3/{ShopId}/orders?token={Token}";

        // Global objects for testing
        private readonly IEcwidClient _defaultClient = new EcwidClient();
        private readonly IEcwidLegacyClient _defaultLegacyClient = new EcwidLegacyClient();

        private readonly HttpTest _httpTest = new HttpTest();
        private readonly IEcwidOrdersClient _ordersClient = new EcwidClient().Configure(ShopId, Token);

        private readonly IEcwidOrdersLegacyClient _ordersLegacyClient = new EcwidLegacyClient().Configure(ShopId, Token,
            Token);

        #region ApiExceptions

        [Fact]
        public async void GetApiResponceAsync400Exception()
        {
            _httpTest
                .RespondWithJson(400,
                    "{\"errorMessage\":\"\nStatus QUEUED is deprecated, use AWAITING_PAYMENT instead.\"}");

            await Assert.ThrowsAsync<EcwidHttpException>(async () => await _ordersClient.CheckOrdersTokenAsync());

            _httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        #endregion

        #region DefaultClient

        [Fact]
        public void DefaultCreatePass()
        {
            var settings = _defaultClient.Settings;
            var credentials = _defaultClient.Credentials;

            Assert.Null(credentials);
            Assert.Equal("https://app.ecwid.com/api/v3/", settings.ApiUrl);
        }

        [Fact]
        public void DefaultChangeApiUrlPass()
        {
            var settings = _defaultClient.Settings;
            settings.ApiUrl = "https://app.ecwid.com/api/v1/";

            Assert.Equal("https://app.ecwid.com/api/v1/", settings.ApiUrl);
        }

        [Fact]
        public void DefaultConfigurePass()
        {
            var result = _defaultClient.Configure(123, Token);

            Assert.NotNull(result);
            Assert.StrictEqual(_defaultClient, result);
            Assert.Equal(123, result.Credentials.ShopId);
            Assert.Equal(Token, result.Credentials.Token);
        }

        [Fact]
        public void DefaultConfigureFail()
        {
            Assert.Throws<EcwidConfigException>(() => _defaultClient.Configure(0, null));
        }

        #endregion

        #region OrdersClient

        [Fact]
        public async void OrdersUrlException()
            =>
                await
                    Assert.ThrowsAsync<EcwidConfigException>(
                        () => _defaultClient.CheckOrdersTokenAsync(new CancellationToken()));

        [Fact]
        public void OrdersGetEmptyPass()
        {
            var query = _ordersClient.Orders;
            Assert.NotNull(query);
            Assert.Empty(query.Query);
            Assert.NotNull(query.Client);
            Assert.StrictEqual(_ordersClient, query.Client);
        }

        [Fact]
        public async void OrdersCheckOrdersAuthAsyncPass()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithLimit1);

            var result = await _ordersClient.CheckOrdersTokenAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Equal(true, result);
        }

        [Fact]
        public async void OrdersCheckOrdersAuthAsyncFail()
        {
            _httpTest
                .RespondWithJson(403, Moqs.MockSearchResultWithLimit1);

            var result = await _ordersClient.CheckOrdersTokenAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Equal(false, result);
        }

        [Fact]
        public async void OrdersCheckOrdersAuthAsyncException()
        {
            _httpTest
                .SimulateTimeout()
                .SimulateTimeout();

            await
                Assert.ThrowsAsync<EcwidHttpException>(
                    async () => await _ordersClient.CheckOrdersTokenAsync());

            await
                Assert.ThrowsAsync<EcwidHttpException>(
                    async () => await _ordersClient.CheckOrdersTokenAsync(new CancellationToken()));

            _httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void OrdersGetOrdersCountAsyncPass()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithLimit1)
                .RespondWithJson(Moqs.MockSearchResultWithLimit1)
                .RespondWithJson(Moqs.MockSearchResultZeroResult);

            var result = await _ordersClient.GetOrdersCountAsync();
            Assert.Equal(100, result);
            result = await _ordersClient.GetOrdersCountAsync(new CancellationToken());
            Assert.Equal(100, result);
            result = await _ordersClient.GetOrdersCountAsync();
            Assert.Equal(0, result);

            _httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(3);
        }

        [Fact]
        public async void OrdersGetNewOrdersAsyncPass()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithLimit1)
                .RespondWithJson(Moqs.MockSearchResultWithLimit1)
                .RespondWithJson(Moqs.MockSearchResultZeroResult);

            var result = await _ordersClient.GetNewOrdersAsync();
            Assert.NotEmpty(result);
            result = await _ordersClient.GetNewOrdersAsync(new CancellationToken());
            Assert.NotEmpty(result);
            result = await _ordersClient.GetNewOrdersAsync();
            Assert.Empty(result);

            _httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&fulfillmentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(3);
        }

        [Fact]
        public async void OrdersGetNonPaidOrdersAsyncPass()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithLimit1)
                .RespondWithJson(Moqs.MockSearchResultWithLimit1);

            var result = await _ordersClient.GetNonPaidOrdersAsync();
            Assert.NotEmpty(result);
            result = await _ordersClient.GetNonPaidOrdersAsync(new CancellationToken());
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&paymentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void OrdersGetPaidNotShippedOrdersAsyncPass()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithLimit1)
                .RespondWithJson(Moqs.MockSearchResultWithLimit1);

            var result = await _ordersClient.GetPaidNotShippedOrdersAsync();
            Assert.NotEmpty(result);
            result = await _ordersClient.GetPaidNotShippedOrdersAsync(new CancellationToken());
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&paymentStatus=*&fulfillmentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void OrdersGetShippedNotDeliveredOrdersAsyncPass()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithLimit1)
                .RespondWithJson(Moqs.MockSearchResultWithLimit1);

            var result = await _ordersClient.GetShippedNotDeliveredOrdersAsync();
            Assert.NotEmpty(result);
            result = await _ordersClient.GetShippedNotDeliveredOrdersAsync(new CancellationToken());
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&fulfillmentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        #endregion

        #region DefaultLegacyClient

        [Fact]
        public void DefaultLegacyCreatePass()
        {
            var settings = _defaultLegacyClient.Settings;
            var credentials = _defaultLegacyClient.Credentials;

            Assert.Null(credentials);
            Assert.Equal("https://app.ecwid.com/api/v1/", settings.ApiUrl);
            Assert.Equal(600, settings.MaxSecondsToWait);
            Assert.Equal(1, settings.RetryInterval);
        }

        [Fact]
        public void DefaultLegacyConfigureShopPass()
        {
            var client3 = _defaultLegacyClient.Configure(123, Token, Token);

            Assert.NotNull(client3);
            Assert.StrictEqual(_defaultLegacyClient, client3);
            Assert.Equal(Token, client3.Credentials.OrderToken);
            Assert.Equal(Token, client3.Credentials.ProductToken);
        }

        [Fact]
        public void DefaultLegacyConfigureShopFail()
        {
            Assert.Throws<EcwidConfigException>(
                () => _defaultLegacyClient.Configure(123));
            Assert.Throws<EcwidConfigException>(
                () => _defaultLegacyClient.Configure(123, "", ""));
            Assert.Throws<EcwidConfigException>(
                () => _defaultLegacyClient.Configure(123, "test", "test"));
        }

        #endregion

        #region LegacyOrdersClient

        [Fact]
        public async void LegacyOrdersOrdersUrlException()
            =>
                await
                    Assert.ThrowsAsync<EcwidConfigException>(
                        () => _defaultLegacyClient.CheckOrdersTokenAsync(new CancellationToken()));

        [Fact]
        public void LegacyOrdersGetEmptyPass()
        {
            var query = _ordersLegacyClient.Orders;
            Assert.NotNull(query);
            Assert.Empty(query.Query);
            Assert.NotNull(query.Client);
            Assert.StrictEqual(_ordersLegacyClient, query.Client);
        }

        [Fact]
        public async void LegacyOrdersCheckOrdersAuthAsyncPass()
        {
            _httpTest
                .RespondWithJson(new {count = 0, total = 0, order = "[]"})
                .RespondWithJson(new {count = 0, total = 0, order = "[]"});

            var result = await _ordersLegacyClient.CheckOrdersTokenAsync();
            var result2 = await _ordersLegacyClient.CheckOrdersTokenAsync(new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(true, result);
            Assert.Equal(true, result2);
        }

        [Fact]
        public async void LegacyOrdersCheckOrdersAuthAsyncFail()
        {
            _httpTest
                .RespondWithJson(403, new {count = 0, total = 0, order = "[]"})
                .RespondWithJson(403, new {count = 0, total = 0, order = "[]"});

            var result = await _ordersLegacyClient.CheckOrdersTokenAsync();
            var result2 = await _ordersLegacyClient.CheckOrdersTokenAsync(new CancellationToken());


            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(false, result);
            Assert.Equal(false, result2);
        }

        [Fact]
        public async void LegacyOrdersGetOrdersCountAsyncPass()
        {
            _httpTest
                .RespondWithJson(new {count = 0, total = 10, order = "[]"})
                .RespondWithJson(new {count = 0, total = 10, order = "[]"});

            var result = await _ordersLegacyClient.GetOrdersCountAsync();
            var result2 = await _ordersLegacyClient.GetOrdersCountAsync(new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=0")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(10, result);
            Assert.Equal(10, result2);
        }

        [Fact]
        public async void LegacyOrdersGetNewOrdersAsyncPass()
        {
            var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(responce)
                .RespondWithJson(responce);

            var result = await _ordersLegacyClient.GetNewOrdersAsync();
            var result2 = await _ordersLegacyClient.GetNewOrdersAsync(new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(1, result.Count);
            Assert.Equal(1, result2.Count);
        }

        [Fact]
        public async void LegacyOrdersGetNonPaidOrdersAsyncPass()
        {
            var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(responce)
                .RespondWithJson(responce);

            var result = await _ordersLegacyClient.GetNonPaidOrdersAsync();
            var result2 = await _ordersLegacyClient.GetNonPaidOrdersAsync(new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(1, result.Count);
            Assert.Equal(1, result2.Count);
        }

        [Fact]
        public async void LegacyOrdersGetPaidNotShippedOrdersAsyncPass()
        {
            var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(responce)
                .RespondWithJson(responce);

            var result = await _ordersLegacyClient.GetPaidNotShippedOrdersAsync();
            var result2 = await _ordersLegacyClient.GetPaidNotShippedOrdersAsync(new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(1, result.Count);
            Assert.Equal(1, result2.Count);
        }

        [Fact]
        public async void LegacyOrdersGetShippedNotDeliveredOrdersAsyncPass()
        {
            var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(responce)
                .RespondWithJson(responce);

            var result = await _ordersLegacyClient.GetShippedNotDeliveredOrdersAsync();
            var result2 = await _ordersLegacyClient.GetShippedNotDeliveredOrdersAsync(new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(1, result.Count);
            Assert.Equal(1, result2.Count);
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryMultiPagesResultPass()
        {
            _httpTest
                .RespondWithJson(
                    Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder);

            var result = await _ordersLegacyClient.GetOrdersAsync(new {limit = 5});

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Equal(20, result.Count);
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryMultiPagesResultOnePagePass()
        {
            _httpTest
                .RespondWithJson(
                    Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder).RespondWithJson(
                    Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder);

            var result = await _ordersLegacyClient.Orders.Limit(5).GetPageAsync();
            var result2 = await _ordersLegacyClient.Orders.Limit(5).GetPageAsync(new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldNotHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(10, result.Count);
            Assert.Equal(10, result2.Count);
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryMultiPagesResultCancellationPass()
        {
            _httpTest
                .RespondWithJson(
                    Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder);

            var result = await _ordersLegacyClient.GetOrdersAsync(new {limit = 5}, new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Equal(20, result.Count);
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryBuilderMultiPagesResultPass()
        {
            _httpTest
                .RespondWithJson(
                    Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder);

            var result = await _ordersLegacyClient.Orders.Limit(5).GetAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Equal(20, result.Count);
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryBuilderMultiPagesResultCancellationPass()
        {
            _httpTest
                .RespondWithJson(
                    Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder);

            var result = await _ordersLegacyClient.Orders.Limit(5).GetAsync(new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Equal(20, result.Count);
        }

        [Fact]
        public async void LegacyOrdersUpdateAsyncNullBuilderFail()
        {
            await
                Assert.ThrowsAsync<EcwidConfigException>(
                    async () => await _ordersLegacyClient.Orders.UpdateAsync("", "", ""));
            await
                Assert.ThrowsAsync<EcwidConfigException>(
                    async () => await _ordersLegacyClient.Orders.Limit(5).Offset(5).UpdateAsync("", "", ""));
        }

        [Fact]
        public async void UpdateAsyncNullStringsFail() => await Assert.ThrowsAsync<EcwidConfigException>(async ()
            => await _ordersLegacyClient.Orders.Order(1).UpdateAsync("", "", ""));

        [Fact]
        public async void LegacyOrdersUpdateAsyncNullResultPass()
        {
            _httpTest
                .RespondWithJson(new {count = 0, total = 10, order = "[]"})
                .RespondWithJson(new {count = 0, total = 10, order = "[]"});

            var result = await _ordersLegacyClient.Orders.Order(123).UpdateAsync("PAID", "PROCESSING", "");
            var result2 =
                await
                    _ordersLegacyClient.Orders.Order(123)
                        .UpdateAsync("PAID", "PROCESSING", "", new CancellationToken());

            _httpTest.ShouldHaveCalled(
                $"{_checkOrdersLegacyUrl}&order=123&new_payment_status=PAID&new_fulfillment_status=PROCESSING")
                .WithVerb(HttpMethod.Post)
                .Times(2);

            Assert.Empty(result);
            Assert.Empty(result2);
        }

        [Fact]
        public async void LegacyOrdersUpdateAsyncResultPass()
        {
            _httpTest
                .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseForUpdate);

            var result = await _ordersLegacyClient.Orders.Order(123).UpdateAsync("PAID", "PROCESSING", "123");

            _httpTest.ShouldHaveCalled(
                $"{_checkOrdersLegacyUrl}&order=123&new_payment_status=PAID&new_fulfillment_status=PROCESSING&new_shipping_tracking_code=123")
                .WithVerb(HttpMethod.Post)
                .Times(1);

            Assert.Equal(1, result.Count);
        }

        #endregion
    }
}