// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using Ecwid.Legacy;
using Flurl.Http.Testing;
using Xunit;

namespace Ecwid.Test.Services.Legacy
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class LegacyClientTest : IDisposable
    {
        // Tests params
        private const int ShopId = 123;
        private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";

        // Urls for checking
        private readonly string _checkCategoryLegacyUrl =
            $"https://app.ecwid.com/api/v1/{ShopId}/category?";

        private readonly string _checkOrdersLegacyUrl =
            $"https://app.ecwid.com/api/v1/{ShopId}/orders?secure_auth_key={Token}";

        // Global objects for testing
        private readonly IEcwidLegacyClient _defaultLegacyClient = new EcwidLegacyClient();

        private readonly HttpTest _httpTest = new HttpTest();
        private readonly IEcwidLegacyClient _legacyClient = new EcwidLegacyClient(ShopId, Token, Token);

        #region LegacyProducts

        [Fact]
        public async void GetCategoriesAsync_LegacyCategoriesUrl_Exception()
        {
            var exception = await
                Assert.ThrowsAsync<EcwidConfigException>(
                    () => _defaultLegacyClient.GetCategoriesAsync());
            Assert.Contains("Credentials are null. Can not do a request.", exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void GetCategoryAsync_IncorrectInt_Exception(int i)
        {
            var exception = await
                Assert.ThrowsAsync<ArgumentException>(
                    () => _legacyClient.GetCategoryAsync(i));
            Assert.Contains("Category is is 0.", exception.Message);
        }

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
        public async void LegacyOrders_OrdersUrl_Exception()
        {
            var exception = await
                Assert.ThrowsAsync<EcwidConfigException>(
                    () => _defaultLegacyClient.CheckOrdersTokenAsync());
            Assert.Contains("Credentials are null. Can not do a request.", exception.Message);
        }

        [Fact]
        public async void LegacyOrders_CheckOrdersTokenAsync_ReturnTrue()
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
        public async void LegacyOrders_CheckOrdersAuthAsync_ReturnFalse()
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
        public async void LegacyOrders_GetOrdersCountAsync_ReturnCorrectList()
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
        public async void LegacyOrders_GetNewOrdersAsync_ReturnSingleList()
        {
            var response = Mocks.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(response);

            var result = await _legacyClient.GetNewOrdersAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Single(result);
        }

        [Fact]
        public async void LegacyOrders_GetNonPaidOrdersAsync_ReturnSingleList()
        {
            var response = Mocks.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(response);

            var result = await _legacyClient.GetNonPaidOrdersAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Single(result);
        }

        [Fact]
        public async void LegacyOrders_GetPaidNotShippedOrdersAsync_ReturnSingleList()
        {
            var response = Mocks.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(response);

            var result = await _legacyClient.GetPaidNotShippedOrdersAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Single(result);
        }

        [Fact]
        public async void LegacyOrders_GetShippedOrdersAsync_ReturnSingle()
        {
            var response = Mocks.MockLegacyOrderResponseWithOneOrder;

            _httpTest
                .RespondWithJson(response);

            var result = await _legacyClient.GetShippedOrdersAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Single(result);
        }

        [Fact]
        public async void Legacy_Orders_GetOrdersAsync_QueryMultiPages_ReturnCorrectResult()
        {
            const int count = 200;
            const string query = "statuses=paid";

            _httpTest
                .RespondWithJson(
                    Mocks.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&{query}&offset={count}"))
                .RespondWithJson(Mocks.MockLegacyOrderResponseWithManyOrder(count));

            var result = await _legacyClient.GetOrdersAsync(new {statuses = "paid"});

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&{query}")
                .WithVerb(HttpMethod.Get)
                .Times(2);

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&{query}&offset={count}")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            Assert.Equal(count * 2, result.Count());
        }

        [Fact]
        public async void Legacy_Orders_GetOrdersAsync_QueryMultiPagesResultOnePage_ReturnCorrectResult()
        {
            _httpTest
                .RespondWithJson(
                    Mocks.MockLegacyOrderResponseWithManyOrderAndPages(
                        $"{_checkOrdersLegacyUrl}&limit=5&offset=5"));

            var result = await _legacyClient.Orders.Limit(5).GetAsync();

            _httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
                .WithVerb(HttpMethod.Get)
                .Times(1);

            _httpTest.ShouldNotHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5");

            Assert.Equal(200, result.Count());
        }

        [Fact]
        public async void LegacyOrders_UpdateAsyncNullBuilder_Exception()
        {
            var exception = await
                Assert.ThrowsAsync<EcwidException>(
                    async () => await _legacyClient.Orders.UpdateAsync("", "", ""));

            Assert.Contains("Can not add or update statuses. Look inner exception.", exception.Message);
            Assert.Contains("Query is empty. Prevent change all orders.", exception.InnerException.Message);

            exception = await
                Assert.ThrowsAsync<EcwidException>(
                    async () =>
                        await
                            _legacyClient.Orders.Limit(5)
                                .Offset(5)
                                .UpdateAsync("", "", ""));
            Assert.Contains("Can not add or update statuses. Look inner exception.", exception.Message);
            Assert.Contains("Query is empty. Prevent change all orders.", exception.InnerException.Message);
        }

        [Fact]
        public async void UpdateAsyncNullStrings_Exception()
        {
            var exception = await Assert.ThrowsAsync<EcwidException>(async ()
                => await _legacyClient.Orders.Order(1).UpdateAsync("", "", ""));

            Assert.Contains("Can not add or update statuses. Look inner exception.", exception.Message);
            Assert.Contains("All new statuses are null or empty.", exception.InnerException.Message);
        }

        [Fact]
        public async void LegacyOrders_UpdateAsync_ReturnEmptyList()
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
        public async void LegacyOrders_UpdateAsync_ReturnSingle()
        {
            _httpTest
                .RespondWithJson(Mocks.MockLegacyOrderResponseForUpdate);

            var result = await _legacyClient.Orders.Order(1).UpdateAsync("PAID", "PROCESSING", "test_code");

            _httpTest.ShouldHaveCalled(
                    $"{_checkOrdersLegacyUrl}&order=1&new_payment_status=PAID&new_fulfillment_status=PROCESSING&new_shipping_tracking_code=test_code")
                .WithVerb(HttpMethod.Post)
                .Times(1);

            Assert.Single(result);
        }

        [Fact]
        public async void LegacyOrders_UpdateAsync_Exception()
        {
            _httpTest
                .RespondWithJson(Mocks.MockLegacyOrderResponseForUpdate, 400);

            var exception = await
                Assert.ThrowsAsync<EcwidHttpException>(
                    () => _legacyClient.Orders.Order(1).UpdateAsync("PAID", "PROCESSING", "test_code"));

            Assert.Contains("Call failed with status code 400 (Bad Request):", exception.Message);

            _httpTest.ShouldHaveCalled(
                    $"{_checkOrdersLegacyUrl}&order=1&new_payment_status=PAID&new_fulfillment_status=PROCESSING&new_shipping_tracking_code=test_code")
                .WithVerb(HttpMethod.Post)
                .Times(1);
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