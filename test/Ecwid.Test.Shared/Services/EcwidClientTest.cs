// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using Ecwid.Legacy;
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

        private static readonly string CheckOrdersUrl = $"https://app.ecwid.com/api/v3/{ShopId}/orders?token={Token}";

        // Urls for checking
        private readonly string _checkOrdersLegacyUrl =
            $"https://app.ecwid.com/api/v1/{ShopId}/orders?secure_auth_key={Token}";

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
                    "{\"errorMessage\":\"\nStatus QUEUED is deprecated, use AWAITING_PAYMENT instead.\"}")
                .RespondWith(400, "Wrong numeric parameter 'orderNumber' value: not a number or a number out of range");

            await Assert.ThrowsAsync<EcwidHttpException>(async () => await _ordersClient.CheckOrdersTokenAsync());
            await Assert.ThrowsAsync<EcwidHttpException>(async () => await _ordersClient.CheckOrdersTokenAsync());

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        #endregion

        #region Readme and Wiki tests

        [Fact]
        public async void ReadmeTest()
        {
            const int someShopId = 123;
            const string someToken = "4843094390fdskldgsfkldkljKLLKklfdkldsffds";

            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithManyOrder());

            var client = new EcwidClient();
            var result = await client.Configure(someShopId, someToken).Orders
                .Limit(10)
                .CreatedFrom(DateTime.Today)
                .PaymentStatuses("PAID")
                .GetAsync();

            Assert.Equal(10, result.Count);
        }

        #endregion

        #region DefaultClient

        [Fact]
        public void DefaultCreate()
        {
            var settings = _defaultClient.Settings;
            var credentials = _defaultClient.Credentials;

            Assert.Null(credentials);
            Assert.NotNull(settings);
        }

        [Fact]
        public void DefaultChangeApiUrl()
        {
            var settings = new EcwidSettings {ApiUrl = "https://app.ecwid.com/api/v1/"};
            _defaultClient.Settings = settings;
            var client = _defaultClient.Configure(settings);

            Assert.Equal("https://app.ecwid.com/api/v1/", settings.ApiUrl);
            Assert.Equal(_defaultClient, client);
        }

        [Fact]
        public void DefaultConfigure()
        {
            var result = _defaultClient.Configure(ShopId, Token);

            Assert.NotNull(result);
            Assert.StrictEqual(_defaultClient, result);
            Assert.Equal(ShopId, result.Credentials.ShopId);
            Assert.Equal(Token, result.Credentials.Token);
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
        public void OrdersGetEmpty()
        {
            var query = _ordersClient.Orders;
            Assert.NotNull(query);
            Assert.Empty(query.Query);
            Assert.NotNull(query.Client);
            Assert.StrictEqual(_ordersClient, query.Client);
        }

        [Fact]
        public async void OrdersCheckOrdersAuthAsync()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithLimit1)
                .RespondWithJson(403, Moqs.MockSearchResultWithLimit1);

            var result = await _ordersClient.CheckOrdersTokenAsync();
            Assert.Equal(true, result);

            result = await _ordersClient.CheckOrdersTokenAsync();
            Assert.Equal(false, result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(2);
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

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void GetOrderAsyncFail()
        {
            await
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                    async () => await _ordersClient.GetOrderAsync(0));

            await
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                    async () => await _ordersClient.GetOrderAsync(0, new CancellationToken()));
        }

        [Fact]
        public async void GetOrderAsyncNull()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultZeroResult)
                .RespondWithJson(Moqs.MockSearchResultZeroResult);

            var result = await _ordersClient.GetOrderAsync(1);
            Assert.Null(result);

            result = await _ordersClient.GetOrderAsync(1, new CancellationToken());
            Assert.Null(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&orderNumber=1")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void OrdersGetOrdersCountAsync()
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

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(3);
        }

        [Fact]
        public async void OrdersGetNewOrdersAsync()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithOneOrder)
                .RespondWithJson(Moqs.MockSearchResultWithOneOrder);

            var result = await _ordersClient.GetNewOrdersAsync();
            Assert.NotEmpty(result);

            result = await _ordersClient.GetNewOrdersAsync(new CancellationToken());
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&fulfillmentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void OrdersGetIncompleteOrdersAsync()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithOneOrder)
                .RespondWithJson(Moqs.MockSearchResultWithOneOrder);

            var result = await _ordersClient.GetIncompleteOrdersAsync();
            Assert.NotEmpty(result);

            result = await _ordersClient.GetIncompleteOrdersAsync(new CancellationToken());
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&paymentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void OrdersGetNonPaidOrdersAsync()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithOneOrder)
                .RespondWithJson(Moqs.MockSearchResultWithOneOrder);

            var result = await _ordersClient.GetNonPaidOrdersAsync();
            Assert.NotEmpty(result);

            result = await _ordersClient.GetNonPaidOrdersAsync(new CancellationToken());
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&paymentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void OrdersGetPaidNotShippedOrdersAsync()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithOneOrder)
                .RespondWithJson(Moqs.MockSearchResultWithOneOrder);

            var result = await _ordersClient.GetPaidNotShippedOrdersAsync();
            Assert.NotEmpty(result);

            result = await _ordersClient.GetPaidNotShippedOrdersAsync(new CancellationToken());
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&paymentStatus=*&fulfillmentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void OrdersGetShippedOrdersAsync()
        {
            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithOneOrder)
                .RespondWithJson(Moqs.MockSearchResultWithOneOrder);

            var result = await _ordersClient.GetShippedOrdersAsync();
            Assert.NotEmpty(result);

            result = await _ordersClient.GetShippedOrdersAsync(new CancellationToken());
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&fulfillmentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void OrdersGetOrdersAsyncQueryMultiPagesResult()
        {
            const int count = 100;
            const string query = "paymentStatus=paid";

            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count*0, count))
                .RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count*1, count))
                .RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count*2, count))
                .RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count*3, 0))
                .RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count*0, count))
                .RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count*1, count))
                .RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count*2, count))
                .RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count*3, 0));

            var result = await _ordersClient.GetOrdersAsync(new {paymentStatus = "paid"});
            var result2 = await _ordersClient.GetOrdersAsync(new {paymentStatus = "paid"}, new CancellationToken());

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&offset=*&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(6);

            Assert.Equal(count*3, result.Count);
            Assert.Equal(count*3, result2.Count);
        }

        [Fact]
        public async void OrdersGetOrdersAsyncQueryOnePagesResult()
        {
            const int count = 100;
            const string query = "limit=100&paymentStatus=paid";

            _httpTest
                .RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, 0, count))
                .RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, 0, count));

            var result = await _ordersClient.GetOrdersAsync(new {limit = count, paymentStatus = "paid"});
            var result2 =
                await _ordersClient.GetOrdersAsync(new {limit = count, paymentStatus = "paid"}, new CancellationToken());

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldNotHaveCalled($"{CheckOrdersUrl}&offset=*&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(6);

            Assert.Equal(count, result.Count);
            Assert.Equal(count, result2.Count);
        }

        #endregion

        #region DefaultLegacyClient

        [Fact]
        public void DefaultLegacyCreate()
        {
            var settings = _defaultLegacyClient.Settings;
            var credentials = _defaultLegacyClient.Credentials;

            Assert.Null(credentials);
            Assert.Equal("https://app.ecwid.com/api/v1/", settings.ApiUrl);
            Assert.Equal(600, settings.MaxSecondsToWait);
            Assert.Equal(1, settings.RetryInterval);
        }

        [Fact]
        public void DefaultLegacyChangeApiUrl()
        {
            var settings = new EcwidLegacySettings {ApiUrl = "https://app.ecwid.com/api/v1/"};
            _defaultLegacyClient.Settings = settings;
            var client = _defaultLegacyClient.Configure(settings);

            Assert.Equal("https://app.ecwid.com/api/v1/", settings.ApiUrl);
            Assert.Equal(_defaultLegacyClient, client);
        }

        [Fact]
        public void DefaultLegacyConfigureShop()
        {
            var client = _defaultLegacyClient.Configure(ShopId, Token, Token);
            var client2 = _defaultLegacyClient.Configure(new EcwidLegacyCredentials(ShopId, Token, Token));

            Assert.NotNull(client);
            Assert.StrictEqual(_defaultLegacyClient, client);
            Assert.Equal(Token, client.Credentials.OrderToken);
            Assert.Equal(Token, client.Credentials.ProductToken);
            Assert.NotNull(client2);
            Assert.StrictEqual(_defaultLegacyClient, client2);
            Assert.Equal(Token, client2.Credentials.OrderToken);
            Assert.Equal(Token, client2.Credentials.ProductToken);
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
        public void LegacyOrdersGetEmpty()
        {
            var query = _ordersLegacyClient.Orders;
            Assert.Empty(query.Query);
            Assert.StrictEqual(_ordersLegacyClient, query.Client);
        }

        [Fact]
        public async void LegacyOrdersCheckOrdersTokenAsync()
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
        public async void LegacyOrdersGetOrdersCountAsync()
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
        public async void LegacyOrdersGetNewOrdersAsync()
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
        public async void LegacyOrdersGetNonPaidOrdersAsync()
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
        public async void LegacyOrdersGetPaidNotShippedOrdersAsync()
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
        public async void LegacyOrdersGetShippedOrdersAsync()
        {
            var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(responce)
                .RespondWithJson(responce);

            var result = await _ordersLegacyClient.GetShippedOrdersAsync();
            var result2 = await _ordersLegacyClient.GetShippedOrdersAsync(new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(1, result.Count);
            Assert.Equal(1, result2.Count);
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryMultiPagesResult()
        {
            const int count = 200;
            const string query = "statuses=paid";

            _httpTest
                .RespondWithJson(
                    Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&{query}&offset={count}"))
                .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder(count))
                .RespondWithJson(
                    Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&{query}&offset={count}"))
                .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder(count));

            var result = await _ordersLegacyClient.GetOrdersAsync(new {statuses = "paid"});
            var result2 = await _ordersLegacyClient.GetOrdersAsync(new {statuses = "paid"}, new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(4);

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&{query}&offset={count}")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(count*2, result.Count);
            Assert.Equal(count*2, result2.Count);
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryMultiPagesResultOnePage()
        {
            _httpTest
                .RespondWithJson(
                    Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                .RespondWithJson(
                    Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&limit=5&offset=5"));

            var result = await _ordersLegacyClient.Orders.Limit(5).GetAsync();
            var result2 = await _ordersLegacyClient.Orders.Limit(5).GetAsync(new CancellationToken());

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldNotHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            Assert.Equal(200, result.Count);
            Assert.Equal(200, result2.Count);
        }

        [Fact]
        public async void LegacyOrdersUpdateAsyncNullBuilderFail()
        {
            await
                Assert.ThrowsAsync<EcwidConfigException>(
                    async () => await _ordersLegacyClient.Orders.UpdateAsync("", "", ""));
            await
                Assert.ThrowsAsync<EcwidConfigException>(
                    async () =>
                        await
                            _ordersLegacyClient.Orders.Limit(5)
                                .Offset(5)
                                .UpdateAsync("", "", "", new CancellationToken()));
        }

        [Fact]
        public async void UpdateAsyncNullStringsFail() => await Assert.ThrowsAsync<EcwidConfigException>(async ()
            => await _ordersLegacyClient.Orders.Order(1).UpdateAsync("", "", ""));

        [Fact]
        public async void LegacyOrdersUpdateAsyncNullResult()
        {
            _httpTest
                .RespondWithJson(new {count = 0, total = 10, order = "[]"})
                .RespondWithJson(new {count = 0, total = 10, order = "[]"});

            var result = await _ordersLegacyClient.Orders.Order(ShopId).UpdateAsync("PAID", "PROCESSING", "");
            var result2 =
                await
                    _ordersLegacyClient.Orders.Order(ShopId)
                        .UpdateAsync("PAID", "PROCESSING", "testCode", new CancellationToken());

            _httpTest.ShouldHaveCalled(
                $"{_checkOrdersLegacyUrl}&order={ShopId}&new_payment_status=PAID&new_fulfillment_status=PROCESSING")
                .WithVerb(HttpMethod.Post)
                .Times(2);

            Assert.Empty(result);
            Assert.Empty(result2);
        }

        [Fact]
        public async void LegacyOrdersUpdateAsyncResult()
        {
            _httpTest
                .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseForUpdate);

            var result = await _ordersLegacyClient.Orders.Order(1).UpdateAsync("PAID", "PROCESSING", "test_code");

            _httpTest.ShouldHaveCalled(
                $"{_checkOrdersLegacyUrl}&order=1&new_payment_status=PAID&new_fulfillment_status=PROCESSING&new_shipping_tracking_code=test_code")
                .WithVerb(HttpMethod.Post)
                .Times(1);

            Assert.Equal(1, result.Count);
        }

        #endregion
    }
}