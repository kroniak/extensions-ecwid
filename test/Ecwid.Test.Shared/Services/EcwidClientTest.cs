// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using Ecwid.Services;
using Ecwid.Services.Legacy;
using Flurl.Http;
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
        private const string Token = "test";

        // Urls for checking
        private readonly string _checkOrdersLegacyUrl =
            $"https://app.ecwid.com/api/v1/{ShopId}/orders?secure_auth_key={Token}";

        private readonly string _checkOrdersUrl = $"https://app.ecwid.com/api/v3/{ShopId}/orders?token={Token}";

        // Global objects for testing
        private readonly IEcwidClient _defaultClient = new EcwidClient();
        private readonly IEcwidLegacyClient _defaultLegacyClient = new EcwidLegacyClient();
        private readonly IEcwidOrdersClient _ordersClient = new EcwidClient().Configure(ShopId, Token);
        private readonly IEcwidOrdersLegacyClient _ordersLegacyClient = new EcwidLegacyClient().Configure(ShopId, Token,
            Token);

        #region DefaultClient

        [Fact]
        public void DefaultCreatePass()
        {
            var options = _defaultClient.Options;

            Assert.Null(options.ShopId);
            Assert.Equal("https://app.ecwid.com/api/v3/", options.ApiUrl);
            Assert.Null(options.Token);
        }

        [Fact]
        public void DefaultChangeApiUrlPass()
        {
            var options = _defaultClient.Options;
            options.ApiUrl = "https://app.ecwid.com/api/v1/";

            Assert.Equal("https://app.ecwid.com/api/v1/", options.ApiUrl);
        }

        [Fact]
        public void DefaultConfigurePass()
        {
            var result = _defaultClient.Configure(new EcwidOptions {ShopId = 123});
            Assert.Null(result.Options.Token);

            var result2 = _defaultClient.Configure(123, "test_token");
            var result3 = _defaultClient.Configure(123, "");
            var result4 = _defaultClient.Configure(123, null);

            Assert.NotNull(result);
            Assert.NotNull(result2);
            Assert.NotNull(result3);
            Assert.NotNull(result4);
            Assert.StrictEqual(_defaultClient, result);
            Assert.StrictEqual(_defaultClient, result2);
            Assert.StrictEqual(_defaultClient, result3);
            Assert.StrictEqual(_defaultClient, result4);
            Assert.Equal(123, result.Options.ShopId);
            Assert.Equal(123, result2.Options.ShopId);
            Assert.Equal(123, result3.Options.ShopId);
            Assert.Equal(123, result4.Options.ShopId);
            Assert.Equal("test_token", result2.Options.Token);
            Assert.Equal("test_token", result3.Options.Token);
            Assert.Equal("test_token", result4.Options.Token);
        }

        [Fact]
        public void DefaultConfigureFail()
        {
            Assert.Throws<ArgumentException>(() => _defaultClient.Configure(new EcwidOptions {ShopId = 0}));
            Assert.Throws<ArgumentException>(() => _defaultClient.Configure(0, null));
        }

        #endregion

        #region OrdersClient

        [Fact]
        public async void OrdersUrlException()
            =>
                await
                    Assert.ThrowsAsync<ArgumentException>(
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
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(Moqs.MockSearchResultWithLimit1);

                var result = await _ordersClient.CheckOrdersTokenAsync();

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=1")
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

                Assert.Equal(true, result);
            }
        }

        [Fact]
        public async void OrdersCheckOrdersAuthAsyncFail()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(403, Moqs.MockSearchResultWithLimit1);

                var result = await _ordersClient.CheckOrdersTokenAsync();

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=1")
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

                Assert.Equal(false, result);
            }
        }

        [Fact]
        public async void OrdersCheckOrdersAuthAsyncException()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .SimulateTimeout()
                    .SimulateTimeout();

                await
                    Assert.ThrowsAsync<FlurlHttpTimeoutException>(
                        async () => await _ordersClient.CheckOrdersTokenAsync());

                await
                    Assert.ThrowsAsync<FlurlHttpTimeoutException>(
                        async () => await _ordersClient.CheckOrdersTokenAsync(new CancellationToken()));

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=1")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);
            }
        }

        #endregion

        #region DefaultLegacyClient

        [Fact]
        public void DefaultLegacyCreatePass()
        {
            var options = _defaultLegacyClient.Options;

            Assert.Null(options.ShopId);
            Assert.Equal("https://app.ecwid.com/api/v1/", options.ApiUrl);
            Assert.Equal(600, options.MaxSecondsToWait);
            Assert.Equal(1, options.RetryInterval);
            Assert.Null(options.ShopOrderAuthId);
            Assert.Null(options.ShopProductAuthId);
        }

        [Fact]
        public void DefaultLegacyConfigurePass()
        {
            var result = _defaultLegacyClient.Configure(new EcwidLegacyOptions {ShopId = 123});

            Assert.NotNull(result);
            Assert.StrictEqual(_defaultLegacyClient, result);
        }

        [Fact]
        public void DefaultLegacyConfigureFail()
            =>
                Assert.Throws<ArgumentException>(
                    () => _defaultLegacyClient.Configure(new EcwidLegacyOptions {ShopId = 0}));

        [Fact]
        public void DefaultLegacyConfigureShopPass()
        {
            var client = _defaultLegacyClient.Configure(123);
            var client2 = _defaultLegacyClient.Configure(123, "", "");
            var client3 = _defaultLegacyClient.Configure(123, "test", "test");

            Assert.NotNull(client);
            Assert.NotNull(client2);
            Assert.NotNull(client3);
            Assert.StrictEqual(_defaultLegacyClient, client);
            Assert.StrictEqual(_defaultLegacyClient, client2);
            Assert.StrictEqual(_defaultLegacyClient, client3);
            Assert.Equal("test", client3.Options.ShopOrderAuthId);
            Assert.Equal("test", client3.Options.ShopProductAuthId);
        }

        #endregion

        #region LegacyOrdersClient

        [Fact]
        public async void LegacyOrdersOrdersUrlException()
            =>
                await
                    Assert.ThrowsAsync<ArgumentException>(
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
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(new {count = 0, total = 0, order = "[]"})
                    .RespondWithJson(new {count = 0, total = 0, order = "[]"});

                var result = await _ordersLegacyClient.CheckOrdersTokenAsync();
                var result2 = await _ordersLegacyClient.CheckOrdersTokenAsync(new CancellationToken());

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=1")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(true, result);
                Assert.Equal(true, result2);
            }
        }

        [Fact]
        public async void LegacyOrdersCheckOrdersAuthAsyncFail()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(403, new {count = 0, total = 0, order = "[]"})
                    .RespondWithJson(403, new {count = 0, total = 0, order = "[]"});

                var result = await _ordersLegacyClient.CheckOrdersTokenAsync();
                var result2 = await _ordersLegacyClient.CheckOrdersTokenAsync(new CancellationToken());


                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=1")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(false, result);
                Assert.Equal(false, result2);
            }
        }

        [Fact]
        public async void LegacyOrdersGetOrdersCountAsyncPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(new {count = 0, total = 10, order = "[]"})
                    .RespondWithJson(new {count = 0, total = 10, order = "[]"});

                var result = await _ordersLegacyClient.GetOrdersCountAsync();
                var result2 = await _ordersLegacyClient.GetOrdersCountAsync(new CancellationToken());

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=0")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(10, result);
                Assert.Equal(10, result2);
            }
        }

        [Fact]
        public async void LegacyOrdersGetNewOrdersAsyncPass()
        {
            using (var httpTest = new HttpTest())
            {
                var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

                httpTest
                    .RespondWithJson(responce)
                    .RespondWithJson(responce);

                var result = await _ordersLegacyClient.GetNewOrdersAsync();
                var result2 = await _ordersLegacyClient.GetNewOrdersAsync(new CancellationToken());

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(1, result.Count);
                Assert.Equal(1, result2.Count);
            }
        }

        [Fact]
        public async void LegacyOrdersGetNonPaidOrdersAsyncPass()
        {
            using (var httpTest = new HttpTest())
            {
                var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

                httpTest
                    .RespondWithJson(responce)
                    .RespondWithJson(responce);

                var result = await _ordersLegacyClient.GetNonPaidOrdersAsync();
                var result2 = await _ordersLegacyClient.GetNonPaidOrdersAsync(new CancellationToken());

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(1, result.Count);
                Assert.Equal(1, result2.Count);
            }
        }

        [Fact]
        public async void LegacyOrdersGetPaidNotShippedOrdersAsyncPass()
        {
            using (var httpTest = new HttpTest())
            {
                var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

                httpTest
                    .RespondWithJson(responce)
                    .RespondWithJson(responce);

                var result = await _ordersLegacyClient.GetPaidNotShippedOrdersAsync();
                var result2 = await _ordersLegacyClient.GetPaidNotShippedOrdersAsync(new CancellationToken());

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(1, result.Count);
                Assert.Equal(1, result2.Count);
            }
        }

        [Fact]
        public async void LegacyOrdersGetShippedNotDeliveredOrdersAsyncPass()
        {
            using (var httpTest = new HttpTest())
            {
                var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

                httpTest
                    .RespondWithJson(responce)
                    .RespondWithJson(responce);

                var result = await _ordersLegacyClient.GetShippedNotDeliveredOrdersAsync();
                var result2 = await _ordersLegacyClient.GetShippedNotDeliveredOrdersAsync(new CancellationToken());

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(1, result.Count);
                Assert.Equal(1, result2.Count);
            }
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryMultiPagesResultPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(
                        Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                            $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                    .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder);

                var result = await _ordersLegacyClient.GetOrdersAsync(new {limit = 5});

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

                Assert.Equal(20, result.Count);
            }
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryMultiPagesResultOnePagePass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(
                        Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                            $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                    .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder).RespondWithJson(
                        Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                            $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                    .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder);

                var result = await _ordersLegacyClient.Orders.Limit(5).GetPageAsync();
                var result2 = await _ordersLegacyClient.Orders.Limit(5).GetPageAsync(new CancellationToken());

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                httpTest.ShouldNotHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(10, result.Count);
                Assert.Equal(10, result2.Count);
            }
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryMultiPagesResultCancellationPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(
                        Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                            $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                    .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder);

                var result = await _ordersLegacyClient.GetOrdersAsync(new {limit = 5}, new CancellationToken());

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

                Assert.Equal(20, result.Count);
            }
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryBuilderMultiPagesResultPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(
                        Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                            $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                    .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder);

                var result = await _ordersLegacyClient.Orders.Limit(5).GetAsync();

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

                Assert.Equal(20, result.Count);
            }
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryBuilderMultiPagesResultCancellationPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(
                        Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
                            $"{_checkOrdersLegacyUrl}&limit=5&offset=5"))
                    .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder);

                var result = await _ordersLegacyClient.Orders.Limit(5).GetAsync(new CancellationToken());

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

                Assert.Equal(20, result.Count);
            }
        }

        [Fact]
        public async void LegacyOrdersUpdateAsyncNullBuilderFail()
        {
            await
                Assert.ThrowsAsync<ArgumentException>(
                    async () => await _ordersLegacyClient.Orders.UpdateAsync("", "", ""));
            await
                Assert.ThrowsAsync<ArgumentException>(
                    async () => await _ordersLegacyClient.Orders.Limit(5).Offset(5).UpdateAsync("", "", ""));
        }

        [Fact]
        public async void UpdateAsyncNullStringsFail() => await Assert.ThrowsAsync<ArgumentException>(async ()
            => await _ordersLegacyClient.Orders.Order(1).UpdateAsync("", "", ""));

        [Fact]
        public async void LegacyOrdersUpdateAsyncNullResultPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(new {count = 0, total = 10, order = "[]"})
                    .RespondWithJson(new {count = 0, total = 10, order = "[]"});

                var result = await _ordersLegacyClient.Orders.Order(123).UpdateAsync("PAID", "PROCESSING", "");
                var result2 =
                    await
                        _ordersLegacyClient.Orders.Order(123)
                            .UpdateAsync("PAID", "PROCESSING", "", new CancellationToken());

                httpTest.ShouldHaveCalled(
                    $"{_checkOrdersLegacyUrl}&order=123&new_payment_status=PAID&new_fulfillment_status=PROCESSING")
                    .WithVerb(HttpMethod.Post)
                    .Times(2);

                Assert.Empty(result);
                Assert.Empty(result2);
            }
        }

        [Fact]
        public async void LegacyOrdersUpdateAsyncResultPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseForUpdate);

                var result = await _ordersLegacyClient.Orders.Order(123).UpdateAsync("PAID", "PROCESSING", "123");

                httpTest.ShouldHaveCalled(
                    $"{_checkOrdersLegacyUrl}&order=123&new_payment_status=PAID&new_fulfillment_status=PROCESSING&new_shipping_tracking_code=123")
                    .WithVerb(HttpMethod.Post)
                    .Times(1);

                Assert.Equal(1, result.Count);
            }
        }

        #endregion
    }
}