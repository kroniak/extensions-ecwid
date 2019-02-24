// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using Ecwid.Legacy;
using Ecwid.Models;
using Flurl.Http.Testing;
using Xunit;

namespace Ecwid.Test.Services
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class ClientTest : IDisposable
    {
        // Tests params
        private const int ShopId = 123;
        private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";

        // Urls for checking
        private static readonly string CheckOrdersUrl = $"https://app.ecwid.com/api/v3/{ShopId}/orders?token={Token}";
        private static readonly string CheckProfileUrl = $"https://app.ecwid.com/api/v3/{ShopId}/profile?token={Token}";

        private static readonly string CheckDiscountCouponsUrl =
            $"https://app.ecwid.com/api/v3/{ShopId}/discount_coupons?token={Token}";

        private readonly string _checkCategoryLegacyUrl =
            $"https://app.ecwid.com/api/v1/{ShopId}/category?";

        private readonly string _checkOrdersLegacyUrl =
            $"https://app.ecwid.com/api/v1/{ShopId}/orders?secure_auth_key={Token}";

        // Global objects for testing
        private readonly IEcwidClient _client = new EcwidClient(ShopId, Token);

        private readonly IEcwidClient _defaultClient = new EcwidClient();
        private readonly IEcwidLegacyClient _defaultLegacyClient = new EcwidLegacyClient();

        private readonly HttpTest _httpTest = new HttpTest();
        private readonly IEcwidLegacyClient _legacyClient = new EcwidLegacyClient(ShopId, Token, Token);

        #region ApiExceptions

        [Fact]
        public async void GetApiResponseAsync_Return400Exception()
        {
            _httpTest
                .RespondWithJson("{\"errorMessage\":\"\nStatus QUEUED is deprecated, use AWAITING_PAYMENT instead.\"}",
                    400)
                .RespondWith("Wrong numeric parameter 'orderNumber' value: not a number or a number out of range", 400);

            await Assert.ThrowsAsync<EcwidHttpException>(async () => await _client.CheckOrdersTokenAsync());
            await Assert.ThrowsAsync<EcwidHttpException>(async () => await _client.CheckOrdersTokenAsync());

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        #endregion

        #region Readme and Wiki tests

        [Fact]
        public async void ReadmeTest_ReturnCorrectList()
        {
            const int someShopId = 123;
            const string someToken = "4843094390fdskldgsfkldkljKLLKklfdkldsffds";

            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithManyOrder());

            var client = new EcwidClient();
            var result = await client.Configure(someShopId, someToken).Orders
                .Limit(10)
                .CreatedFrom(DateTime.Today)
                .PaymentStatuses("PAID")
                .GetAsync();

            Assert.Equal(10, result.Count);
        }

        #endregion

        #region Default

        [Fact]
        public void DefaultCreate_CorrectSettingCreated()
        {
            var settings = _defaultClient.Settings;
            var credentials = _defaultClient.Credentials;

            Assert.Null(credentials);
            Assert.NotNull(settings);
        }

        [Fact]
        public void DefaultChangeApiUrl_ApiUrlChanged()
        {
            var settings = new EcwidSettings {ApiUrl = "https://app.ecwid.com/api/v1/"};
            _defaultClient.Settings = settings;
            var client = _defaultClient.Configure(settings);

            Assert.Equal("https://app.ecwid.com/api/v1/", settings.ApiUrl);
            Assert.Equal(_defaultClient, client);
        }

        [Fact]
        public void DefaultConfigure_CorrectSetSettings()
        {
            var result = _defaultClient.Configure(ShopId, Token);

            Assert.NotNull(result);
            Assert.StrictEqual(_defaultClient, result);
            Assert.Equal(ShopId, result.Credentials.ShopId);
            Assert.Equal(Token, result.Credentials.Token);
        }

        #endregion

        #region Orders

        [Fact]
        public async void OrdersUrl_Exception_ThrowsAsync()
            =>
                await
                    Assert.ThrowsAsync<EcwidConfigException>(
                        () => _defaultClient.CheckOrdersTokenAsync());

        [Fact]
        public void OrdersGet_ReturnNotNull()
        {
            var queryBuilder = _client.Orders;
            Assert.NotNull(queryBuilder);
        }

        [Fact]
        public async void OrdersCheckOrdersAuthAsync_ReturnFalse()
        {
            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithLimit1)
                .RespondWithJson(Mocks.MockSearchResultWithLimit1, 403);

            var result = await _client.CheckOrdersTokenAsync();
            Assert.True(result);

            result = await _client.CheckOrdersTokenAsync();
            Assert.False(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void OrdersCheckOrdersAuthAsync_Exception()
        {
            _httpTest
                .SimulateTimeout()
                .SimulateTimeout();

            await
                Assert.ThrowsAsync<EcwidHttpException>(
                    async () => await _client.CheckOrdersTokenAsync());

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        public async void GetOrderAsync_Exception() => await
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _client.GetOrderAsync(0));

        [Fact]
        public async void GetOrderAsync_ReturnNull()
        {
            _httpTest
                .RespondWithJson(Mocks.MockSearchResultZeroResult)
                .RespondWithJson(Mocks.MockSearchResultZeroResult);

            var result = await _client.GetOrderAsync(1);

            Assert.Null(result);
            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&orderNumber=1")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        public async void GetOrdersCountAsync_ReturnZero()
        {
            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithLimit1)
                .RespondWithJson(Mocks.MockSearchResultZeroResult);

            var result = await _client.GetOrdersCountAsync();
            Assert.Equal(100, result);

            result = await _client.GetOrdersCountAsync();
            Assert.Equal(0, result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void GetNewOrdersAsync_ReturnNotEmpty()
        {
            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithOneOrder)
                .RespondWithJson(Mocks.MockSearchResultWithOneOrder);

            var result = await _client.GetNewOrdersAsync();
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&fulfillmentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        public async void GetIncompleteOrdersAsync_ReturnNotEmpty()
        {
            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithOneOrder)
                .RespondWithJson(Mocks.MockSearchResultWithOneOrder);

            var result = await _client.GetIncompleteOrdersAsync();
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&paymentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        public async void GetNonPaidOrdersAsync_ReturnNotEmpty()
        {
            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithOneOrder)
                .RespondWithJson(Mocks.MockSearchResultWithOneOrder);

            var result = await _client.GetNonPaidOrdersAsync();
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&paymentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        public async void GetPaidNotShippedOrdersAsync_ReturnNotEmpty()
        {
            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithOneOrder)
                .RespondWithJson(Mocks.MockSearchResultWithOneOrder);

            var result = await _client.GetPaidNotShippedOrdersAsync();
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&paymentStatus=*&fulfillmentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        public async void GetShippedOrdersAsync_ReturnNotEmpty()
        {
            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithOneOrder)
                .RespondWithJson(Mocks.MockSearchResultWithOneOrder);

            var result = await _client.GetShippedOrdersAsync();
            Assert.NotEmpty(result);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&fulfillmentStatus=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        public async void OrdersGetOrdersAsyncQueryMultiPagesResult_ReturnNotEmpty()
        {
            const int count = 100;
            const string query = "paymentStatus=paid";

            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithManyOrderAndPages(count, count * 0, count))
                .RespondWithJson(Mocks.MockSearchResultWithManyOrderAndPages(count, count * 1, count))
                .RespondWithJson(Mocks.MockSearchResultWithManyOrderAndPages(count, count * 2, count))
                .RespondWithJson(Mocks.MockSearchResultWithManyOrderAndPages(count, count * 3, 0));

            var result = await _client.GetOrdersAsync(new {paymentStatus = "paid"});

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&offset=*&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(3);

            Assert.Equal(count * 3, result.Count);
        }

        [Fact]
        public async void OrdersGetOrdersAsyncQueryOnePagesResult_ReturnNotEmpty()
        {
            const int count = 100;
            const string query = "limit=100&paymentStatus=paid";

            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithManyOrderAndPages(count, 0, count));

            var result = await _client.GetOrdersAsync(new {limit = count, paymentStatus = "paid"});


            _httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            _httpTest.ShouldNotHaveCalled($"{CheckOrdersUrl}&offset=*&{query}");

            Assert.Equal(count, result.Count);
        }

        [Fact]
        public async void UpdateOrderAsync_ReturnNotEmptyUpdateCount()
        {
            _httpTest
                .RespondWithJson(new UpdateStatus {UpdateCount = 1});

            var result = await _client.UpdateOrderAsync(new OrderEntry {Email = "test@test.com", OrderNumber = 123});

            _httpTest.ShouldHaveCalled($"https://app.ecwid.com/api/v3/{ShopId}/orders/123?token={Token}")
                .WithVerb(HttpMethod.Put)
                .Times(1);

            Assert.Equal(1, result.UpdateCount);
        }

        [Fact]
        public async void UpdateOrderAsync_Exception()
        {
            _httpTest
                .RespondWithJson("Status QUEUED is deprecated, use AWAITING_PAYMENT instead", 400);

            await Assert.ThrowsAsync<EcwidConfigException>(() =>
                _client.UpdateOrderAsync(new OrderEntry {Email = "test@test.com"}));

            _httpTest.ShouldNotHaveMadeACall();
        }

        [Fact]
        public async void DeleteOrderAsync_ReturnNotEmpty()
        {
            _httpTest
                .RespondWithJson(new DeleteStatus {DeleteCount = 1});

            var result = await _client.DeleteOrderAsync(new OrderEntry {Email = "test@test.com", OrderNumber = 123});

            _httpTest.ShouldHaveCalled($"https://app.ecwid.com/api/v3/{ShopId}/orders/123?token={Token}")
                .WithVerb(HttpMethod.Delete)
                .Times(1);

            Assert.Equal(1, result.DeleteCount);
        }

        [Fact]
        public async void DeleteOrderAsync_Exception()
        {
            _httpTest
                .RespondWithJson("The order with given number is not found", 404);

            await Assert.ThrowsAsync<EcwidConfigException>(() =>
                _client.DeleteOrderAsync(new OrderEntry {Email = "test@test.com"}));

            _httpTest.ShouldNotHaveMadeACall();
        }

        [Fact]
        public async void DeleteOrderAsync_ReturnBadRequest()
        {
            _httpTest
                .RespondWithJson("The order with given number is not found", 404);

            var exception = await Assert.ThrowsAsync<EcwidHttpException>(() =>
                _client.DeleteOrderAsync(new OrderEntry {Email = "test@test.com", OrderNumber = 123}));

            _httpTest.ShouldHaveCalled($"https://app.ecwid.com/api/v3/{ShopId}/orders/123?token={Token}")
                .WithVerb(HttpMethod.Delete)
                .Times(1);

            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        #endregion

        #region Profile

        [Fact]
        public async void GetProfileAsync_Exception()
            => await Assert.ThrowsAsync<EcwidConfigException>(() => _defaultClient.GetProfileAsync());

        [Fact]
        public async void UpdateProfileAsync_ReturnTrue()
        {
            _httpTest
                .RespondWithJson(new UpdateStatus {Success = true, UpdateCount = 1});

            var profile = new Profile {Account = new Account {AccountName = "John", AccountNickname = "John"}};

            var result = await _client.UpdateProfileAsync(profile);
            Assert.True(result.Success);

            _httpTest.ShouldHaveCalled($"{CheckProfileUrl}")
                .WithVerb(HttpMethod.Put)
                .Times(1);
        }

        [Fact]
        public async void UpdateProfile_Exception()
            => await Assert.ThrowsAsync<EcwidHttpException>(async () => await _client.UpdateProfileAsync(null));

        [Fact]
        public async void UpdateProfileAsyncHttp_Exceptions()
        {
            _httpTest
                .RespondWithJson(new UpdateStatus {Success = false, UpdateCount = 0}, 400);

            var profile = new Profile {Account = new Account {AccountName = "John", AccountNickname = "John"}};

            await Assert.ThrowsAsync<EcwidHttpException>(async () => await _client.UpdateProfileAsync(profile));

            _httpTest.ShouldHaveCalled($"{CheckProfileUrl}")
                .WithVerb(HttpMethod.Put)
                .Times(1);
        }

        #endregion

        #region DefaultLegacy

        [Fact]
        public void DefaultLegacyCreate_CorrectSettings()
        {
            var settings = _defaultLegacyClient.Settings;
            var credentials = _defaultLegacyClient.Credentials;

            Assert.Null(credentials);
            Assert.Equal("https://app.ecwid.com/api/v1/", settings.ApiUrl);
            Assert.Equal(600, settings.MaxSecondsToWait);
            Assert.Equal(1, settings.RetryInterval);
        }

        [Fact]
        public void DefaultLegacy_CorrectChangeApiUrl()
        {
            var settings = new EcwidLegacySettings {ApiUrl = "https://app.ecwid.com/api/v1/"};
            _defaultLegacyClient.Settings = settings;
            var client = _defaultLegacyClient.Configure(settings);

            Assert.Equal("https://app.ecwid.com/api/v1/", settings.ApiUrl);
            Assert.Equal(_defaultLegacyClient, client);
        }

        [Fact]
        public void DefaultLegacy_CorrectConfigureShop()
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

        #region LegacyProducts

        [Fact]
        public async void GetCategoriesAsync_LegacyCategoriesUrl_Exception()
            =>
                await
                    Assert.ThrowsAsync<EcwidConfigException>(
                        () => _defaultLegacyClient.GetCategoriesAsync());

        [Fact]
        public async void GetCategoryAsync_Exception()
            =>
                await
                    Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                        () => _legacyClient.GetCategoryAsync(-1));

        [Fact]
        public async void GetCategoryAsync_404()
        {
            _httpTest
                .RespondWith("", 404);

            var result = await _legacyClient.GetCategoryAsync(1);

            _httpTest.ShouldHaveCalled($"{_checkCategoryLegacyUrl}id=1")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Null(result);
        }

        #endregion

        #region LegacyOrders

        [Fact]
        public async void LegacyOrdersOrdersUrl_Exception()
            =>
                await
                    Assert.ThrowsAsync<EcwidConfigException>(
                        () => _defaultLegacyClient.CheckOrdersTokenAsync());

        [Fact]
        public async void LegacyOrdersCheckOrdersTokenAsync_ReturnTrue()
        {
            _httpTest
                .RespondWithJson(new {count = 0, total = 0, order = "[]"});

            var result = await _legacyClient.CheckOrdersTokenAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.True(result);
        }

        [Fact]
        public async void LegacyOrdersCheckOrdersAuthAsync_ReturnFalse()
        {
            _httpTest
                .RespondWithJson(new {count = 0, total = 0, order = "[]"}, 403);

            var result = await _legacyClient.CheckOrdersTokenAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.False(result);
        }

        [Fact]
        public async void LegacyOrdersGetOrdersCountAsync_ReturnCorrectList()
        {
            _httpTest
                .RespondWithJson(new {count = 0, total = 10, order = "[]"});

            var result = await _legacyClient.GetOrdersCountAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=0")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Equal(10, result);
        }

        [Fact]
        public async void LegacyOrdersGetNewOrdersAsync_ReturnSingleList()
        {
            var response = Legacy.Mocks.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(response);

            var result = await _legacyClient.GetNewOrdersAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Single(result);
        }

        [Fact]
        public async void LegacyOrdersGetNonPaidOrdersAsync_ReturnSingleList()
        {
            var response = Legacy.Mocks.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(response);

            var result = await _legacyClient.GetNonPaidOrdersAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Single(result);
        }

        [Fact]
        public async void LegacyOrdersGetPaidNotShippedOrdersAsync_ReturnSingleList()
        {
            var response = Legacy.Mocks.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(response);

            var result = await _legacyClient.GetPaidNotShippedOrdersAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Single(result);
        }

        [Fact]
        public async void LegacyOrdersGetShippedOrdersAsync_ReturnSingle()
        {
            var response = Legacy.Mocks.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(response);

            var result = await _legacyClient.GetShippedOrdersAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Single(result);
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryMultiPages_ReturnCorrectResult()
        {
            const int count = 200;
            const string query = "statuses=paid";

            _httpTest
                .RespondWithJson(
                    Legacy.Mocks.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&{query}&offset={count}"))
                .RespondWithJson(Legacy.Mocks.MockLegacyOrderResponseWithManyOrder(count));

            var result = await _legacyClient.GetOrdersAsync(new {statuses = "paid"});

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&{query}&offset={count}")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Equal(count * 2, result.Count);
        }

        [Fact]
        public async void LegacyOrdersGetOrdersAsyncQueryMultiPagesResultOnePage_ReturnCorrectResult()
        {
            _httpTest
                .RespondWithJson(
                    Legacy.Mocks.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&limit=5&offset=5"));

            var result = await _legacyClient.Orders.Limit(5).GetAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            _httpTest.ShouldNotHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5");

            Assert.Equal(200, result.Count);
        }

        [Fact]
        public async void LegacyOrdersUpdateAsyncNullBuilder_Exception()
        {
            await
                Assert.ThrowsAsync<EcwidConfigException>(
                    async () => await _legacyClient.Orders.UpdateAsync("", "", ""));
            await
                Assert.ThrowsAsync<EcwidConfigException>(
                    async () =>
                        await
                            _legacyClient.Orders.Limit(5)
                                .Offset(5)
                                .UpdateAsync("", "", ""));
        }

        [Fact]
        public async void UpdateAsyncNullStrings_Exception() => await Assert.ThrowsAsync<EcwidConfigException>(async ()
            => await _legacyClient.Orders.Order(1).UpdateAsync("", "", ""));

        [Fact]
        public async void LegacyOrdersUpdateAsync_ReturnEmptyList()
        {
            _httpTest
                .RespondWithJson(new {count = 0, total = 10, order = "[]"});

            var result = await _legacyClient.Orders.Order(1).UpdateAsync("PAID", "PROCESSING", "");

            _httpTest.ShouldHaveCalled(
                    $"{_checkOrdersLegacyUrl}&order=1&new_payment_status=PAID&new_fulfillment_status=PROCESSING")
                .WithVerb(HttpMethod.Post)
                .Times(1);

            Assert.Empty(result);
        }

        [Fact]
        public async void LegacyOrdersUpdateAsync_ReturnSingle()
        {
            _httpTest
                .RespondWithJson(Legacy.Mocks.MockLegacyOrderResponseForUpdate);

            var result = await _legacyClient.Orders.Order(1).UpdateAsync("PAID", "PROCESSING", "test_code");

            _httpTest.ShouldHaveCalled(
                    $"{_checkOrdersLegacyUrl}&order=1&new_payment_status=PAID&new_fulfillment_status=PROCESSING&new_shipping_tracking_code=test_code")
                .WithVerb(HttpMethod.Post)
                .Times(1);

            Assert.Single(result);
        }

        [Fact]
        public async void LegacyOrdersUpdateAsync_Exception()
        {
            _httpTest
                .RespondWithJson(Legacy.Mocks.MockLegacyOrderResponseForUpdate, 400);

            await
                Assert.ThrowsAsync<EcwidHttpException>(
                    () => _legacyClient.Orders.Order(1).UpdateAsync("PAID", "PROCESSING", "test_code"));

            _httpTest.ShouldHaveCalled(
                    $"{_checkOrdersLegacyUrl}&order=1&new_payment_status=PAID&new_fulfillment_status=PROCESSING&new_shipping_tracking_code=test_code")
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }

        #endregion

        #region DiscountCoupons

        [Fact]
        public async void DiscountCouponsCheckDiscountCouponsAuthAsync_ReturnCorrectResult()
        {
            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithLimit1)
                .RespondWithJson(Mocks.MockSearchResultWithLimit1, 403);

            var result = await _client.CheckDiscountCouponsTokenAsync();
            Assert.True(result);

            result = await _client.CheckDiscountCouponsTokenAsync();
            Assert.False(result);

            _httpTest.ShouldHaveCalled($"{CheckDiscountCouponsUrl}&limit=1")
                .WithVerb(HttpMethod.Get)
                .Times(2);
        }

        [Fact]
        public async void GetDiscountCouponAsync_Exception() => await
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _client.GetDiscountCouponAsync(null));


        [Fact]
        public async void GetDiscountCouponsAsync_ReturnNull()
        {
            _httpTest
                .RespondWithJson(Mocks.MockSearchResultZeroResult)
                .RespondWithJson(Mocks.MockSearchResultZeroResult);

            const string couponIdentifier = "abc123";

            var result = await _client.GetDiscountCouponAsync(couponIdentifier);

            Assert.Null(result);
            _httpTest.ShouldHaveCalled($"{CheckDiscountCouponsUrl}&couponIdentifier={couponIdentifier}")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }


        [Fact]
        public async void DiscountCouponsGetDiscountCouponsAsyncQueryMultiPages_ReturnCorrectResult()
        {
            const int count = 100;
            const string query = "discount_type=ABS_AND_SHIPPING";

            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithManyDiscountCouponsAndPages(count, count * 0, count))
                .RespondWithJson(Mocks.MockSearchResultWithManyDiscountCouponsAndPages(count, count * 1, count))
                .RespondWithJson(Mocks.MockSearchResultWithManyDiscountCouponsAndPages(count, count * 2, count))
                .RespondWithJson(Mocks.MockSearchResultWithManyDiscountCouponsAndPages(count, count * 3, 0));

            var result = await _client.GetDiscountCouponsAsync(new {discount_type = "ABS_AND_SHIPPING"});

            _httpTest.ShouldHaveCalled($"{CheckDiscountCouponsUrl}&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            _httpTest.ShouldHaveCalled($"{CheckDiscountCouponsUrl}&offset=*&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(3);

            Assert.Equal(count * 3, result.Count);
        }

        [Fact]
        public async void DiscountCouponsGetDiscountCouponsAsyncQueryOnePages_ReturnCorrectResult()
        {
            const int count = 100;
            const string query = "limit=100&paymentStatus=paid";

            _httpTest
                .RespondWithJson(Mocks.MockSearchResultWithManyDiscountCouponsAndPages(count, 0, count));

            var result = await _client.GetDiscountCouponsAsync(new {limit = count, paymentStatus = "paid"});


            _httpTest.ShouldHaveCalled($"{CheckDiscountCouponsUrl}&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            _httpTest.ShouldNotHaveCalled($"{CheckDiscountCouponsUrl}&offset=*&{query}");

            Assert.Equal(count, result.Count);
        }

        [Fact]
        public async void CreateDiscountCouponAsync_ReturnCorrectResult()
        {
            const long expectedId = 1223423459837;
            _httpTest.RespondWithJson(new DiscountCouponCreateStatus
            {
                Code = "ABC123DEF",
                Id = expectedId
            });

            var result = await _client.CreateDiscountCouponAsync(new DiscountCouponInfo
            {
                Discount = 10,
                DiscountType = "PERCENT"
            });

            _httpTest.ShouldHaveCalled($"https://app.ecwid.com/api/v3/{ShopId}/discount_coupons?token={Token}")
                .WithVerb(HttpMethod.Post)
                .Times(1);

            Assert.Equal(expectedId, result.Id);
        }

        [Fact]
        public async void UpdateDiscountCouponAsync_ReturnCorrectResult()
        {
            _httpTest
                .RespondWithJson(new UpdateStatus {UpdateCount = 1});

            const string discountCode = "ABC123DEF";
            var result = await _client.UpdateDiscountCouponAsync(new DiscountCouponInfo
            {
                Discount = 15,
                Code = discountCode
            });

            _httpTest.ShouldHaveCalled(
                    $"https://app.ecwid.com/api/v3/{ShopId}/discount_coupons/{discountCode}?token={Token}")
                .WithVerb(HttpMethod.Put)
                .Times(1);

            Assert.Equal(1, result.UpdateCount);
        }

        [Fact]
        public async void UpdateDiscountCouponAsync_Exception()
        {
            _httpTest
                .RespondWithJson("Status QUEUED is deprecated, use AWAITING_PAYMENT instead", 400);

            await Assert.ThrowsAsync<ArgumentException>(() => _client.UpdateDiscountCouponAsync(new DiscountCouponInfo
            {
                Code = ""
            }));
            _httpTest.ShouldNotHaveMadeACall();
        }

        [Fact]
        public async void DeleteDiscountCouponAsync_ReturnCorrectResult()
        {
            _httpTest
                .RespondWithJson(new DeleteStatus {DeleteCount = 1});

            const string discountCode = "ABC123DEF";

            var result = await _client.DeleteDiscountCouponAsync(new DiscountCouponInfo
            {
                Code = discountCode
            });

            _httpTest.ShouldHaveCalled(
                    $"https://app.ecwid.com/api/v3/{ShopId}/discount_coupons/{discountCode}?token={Token}")
                .WithVerb(HttpMethod.Delete)
                .Times(1);

            Assert.Equal(1, result.DeleteCount);
        }

        [Fact]
        public async void DeleteDiscountCouponAsync_Return404()
        {
            _httpTest
                .RespondWithJson("The DiscountCoupon with given number is not found", 404);

            const string discountCode = "ABC123DEF";

            var exception = await Assert.ThrowsAsync<EcwidHttpException>(() => _client.DeleteDiscountCouponAsync(
                new DiscountCouponInfo
                {
                    Code = discountCode
                }));

            _httpTest.ShouldHaveCalled(
                    $"https://app.ecwid.com/api/v3/{ShopId}/discount_coupons/{discountCode}?token={Token}")
                .WithVerb(HttpMethod.Delete)
                .Times(1);

            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            _httpTest.Dispose();
        }

        #endregion
    }
}