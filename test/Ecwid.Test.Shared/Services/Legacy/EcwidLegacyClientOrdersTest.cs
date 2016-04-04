using System;
using System.Net.Http;
using System.Threading;
using Flurl.Http.Testing;
using Xunit;
using Ecwid.Services;
using Ecwid.Services.Legacy;

// ReSharper disable MethodSupportsCancellation

namespace Ecwid.Test.Services.Legacy
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
        private readonly string _checkOrdersUrl = $"https://app.ecwid.com/api/v1/{ShopId}/orders?secure_auth_key={OrdersAuth}";

        // GLobal objects for testing
        private readonly IEcwidOrdersLegacyClient _defaultClient = new EcwidLegacyClient();
        private readonly IEcwidOrdersLegacyClient _client = new EcwidLegacyClient().ConfigureShop(ShopId, OrdersAuth, OrdersAuth);

        // TODO real cancellation tests
        private readonly CancellationToken _cancellationToken = new CancellationToken();

        [Fact]
        public async void OrdersUrlException() => await Assert.ThrowsAsync<ArgumentException>(() => _defaultClient.CheckOrdersAuthAsync(_cancellationToken));

        [Fact]
        public void OrdersGetEmptyPass()
        {
            var query = _client.Orders;
            Assert.NotNull(query);
            Assert.Empty(query.Query);
            Assert.NotNull(query.Client);
            Assert.StrictEqual(_client, query.Client);
        }

        [Fact]
        public async void CheckOrdersAuthAsyncPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(new { count = 0, total = 0, order = "[]" })
                    .RespondWithJson(new { count = 0, total = 0, order = "[]" });

                var result = await _client.CheckOrdersAuthAsync();
                var result2 = await _client.CheckOrdersAuthAsync(_cancellationToken);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=0")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(true, result);
                Assert.Equal(true, result2);
            }
        }

        [Fact]
        public async void CheckOrdersAuthAsyncFail()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(403, new { count = 0, total = 0, order = "[]" })
                    .RespondWithJson(403, new { count = 0, total = 0, order = "[]" });

                var result = await _client.CheckOrdersAuthAsync();
                var result2 = await _client.CheckOrdersAuthAsync(_cancellationToken);


                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=0")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(false, result);
                Assert.Equal(false, result2);
            }
        }

        [Fact]
        public async void GetOrdersCountAsyncPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(new { count = 0, total = 10, order = "[]" })
                    .RespondWithJson(new { count = 0, total = 10, order = "[]" });

                var result = await _client.GetOrdersCountAsync();
                var result2 = await _client.GetOrdersCountAsync(_cancellationToken);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=0")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(10, result);
                Assert.Equal(10, result2);
            }
        }

        [Fact]
        public async void GetNewOrdersAsyncPass()
        {
            using (var httpTest = new HttpTest())
            {
                var responce = Moqs.MockLegacyOrderResponseWithOneOrder;

                httpTest
                    .RespondWithJson(responce)
                    .RespondWithJson(responce);

                var result = await _client.GetNewOrdersAsync();
                var result2 = await _client.GetNewOrdersAsync(_cancellationToken);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&statuses=*")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(1, result.Count);
                Assert.Equal(1, result2.Count);
            }
        }

        [Fact]
        public async void GetNonPaidOrdersAsyncPass()
        {
            using (var httpTest = new HttpTest())
            {
                var responce = Moqs.MockLegacyOrderResponseWithOneOrder;

                httpTest
                    .RespondWithJson(responce)
                    .RespondWithJson(responce);

                var result = await _client.GetNonPaidOrdersAsync();
                var result2 = await _client.GetNonPaidOrdersAsync(_cancellationToken);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&statuses=*")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(1, result.Count);
                Assert.Equal(1, result2.Count);
            }
        }

        [Fact]
        public async void GetPaidNotShippedOrdersAsyncPass()
        {
            using (var httpTest = new HttpTest())
            {
                var responce = Moqs.MockLegacyOrderResponseWithOneOrder;

                httpTest
                    .RespondWithJson(responce)
                    .RespondWithJson(responce);

                var result = await _client.GetPaidNotShippedOrdersAsync();
                var result2 = await _client.GetPaidNotShippedOrdersAsync(_cancellationToken);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&statuses=*")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(1, result.Count);
                Assert.Equal(1, result2.Count);
            }
        }

        [Fact]
        public async void GetShippedNotDeliveredOrdersAsyncPass()
        {
            using (var httpTest = new HttpTest())
            {
                var responce = Moqs.MockLegacyOrderResponseWithOneOrder;

                httpTest
                    .RespondWithJson(responce)
                    .RespondWithJson(responce);

                var result = await _client.GetShippedNotDeliveredOrdersAsync();
                var result2 = await _client.GetShippedNotDeliveredOrdersAsync(_cancellationToken);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&statuses=*")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(1, result.Count);
                Assert.Equal(1, result2.Count);
            }
        }

        [Fact]
        public async void GetOrdersAsyncQueryMultiPagesResultPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(
                        Moqs.MockLegacyOrderResponseWithManyOrderAndPages($"{_checkOrdersUrl}&limit=5&offset=5"))
                    .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrder);

                var result = await _client.GetOrdersAsync(new { limit = 5 });

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=5&offset=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

                Assert.Equal(20, result.Count);
            }
        }

        [Fact]
        public async void GetOrdersAsyncQueryMultiPagesResultOnePagePass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(
                        Moqs.MockLegacyOrderResponseWithManyOrderAndPages($"{_checkOrdersUrl}&limit=5&offset=5"))
                    .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrder).RespondWithJson(
                        Moqs.MockLegacyOrderResponseWithManyOrderAndPages($"{_checkOrdersUrl}&limit=5&offset=5"))
                    .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrder);

                var result = await _client.Orders.Limit(5).GetPageAsync();
                var result2 = await _client.Orders.Limit(5).GetPageAsync(_cancellationToken);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                httpTest.ShouldNotHaveCalled($"{_checkOrdersUrl}&limit=5&offset=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                Assert.Equal(10, result.Count);
                Assert.Equal(10, result2.Count);
            }
        }

        [Fact]
        public async void GetOrdersAsyncQueryMultiPagesResultCancellationPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(
                        Moqs.MockLegacyOrderResponseWithManyOrderAndPages($"{_checkOrdersUrl}&limit=5&offset=5"))
                    .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrder);

                var result = await _client.GetOrdersAsync(new { limit = 5 }, _cancellationToken);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=5&offset=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

                Assert.Equal(20, result.Count);
            }
        }

        [Fact]
        public async void GetOrdersAsyncQueryBuilderMultiPagesResultPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(
                        Moqs.MockLegacyOrderResponseWithManyOrderAndPages($"{_checkOrdersUrl}&limit=5&offset=5"))
                    .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrder);

                var result = await _client.Orders.Limit(5).GetAsync();

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=5&offset=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

                Assert.Equal(20, result.Count);
            }
        }

        [Fact]
        public async void GetOrdersAsyncQueryBuilderMultiPagesResultCancellationPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(
                        Moqs.MockLegacyOrderResponseWithManyOrderAndPages($"{_checkOrdersUrl}&limit=5&offset=5"))
                    .RespondWithJson(Moqs.MockLegacyOrderResponseWithManyOrder);

                var result = await _client.Orders.Limit(5).GetAsync(_cancellationToken);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(2);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&limit=5&offset=5")
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

                Assert.Equal(20, result.Count);
            }
        }

        [Fact]
        public async void UpdateAsyncNullBuilderFail()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _client.Orders.UpdateAsync("", "", ""));
            await Assert.ThrowsAsync<ArgumentException>(async () => await _client.Orders.Limit(5).Offset(5).UpdateAsync("", "", ""));
        }

        [Fact]
        public async void UpdateAsyncNullStringsFail() => await Assert.ThrowsAsync<ArgumentException>(async ()
            => await _client.Orders.Order(1).UpdateAsync("", "", ""));

        [Fact]
        public async void UpdateAsyncNullResultPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(new { count = 0, total = 10, order = "[]" })
                    .RespondWithJson(new { count = 0, total = 10, order = "[]" });

                var result = await _client.Orders.Order(123).UpdateAsync("PAID", "PROCESSING", "");
                var result2 = await _client.Orders.Order(123).UpdateAsync("PAID", "PROCESSING", "", _cancellationToken);

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&order=123&new_payment_status=PAID&new_fulfillment_status=PROCESSING")
                    .WithVerb(HttpMethod.Post)
                    .Times(2);

                Assert.Empty(result);
                Assert.Empty(result2);
            }
        }

        [Fact]
        public async void UpdateAsyncResultPass()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest
                    .RespondWithJson(Moqs.MockLegacyOrderResponseForUpdate);

                var result = await _client.Orders.Order(123).UpdateAsync("PAID", "PROCESSING", "123");

                httpTest.ShouldHaveCalled($"{_checkOrdersUrl}&order=123&new_payment_status=PAID&new_fulfillment_status=PROCESSING&new_shipping_tracking_code=123")
                    .WithVerb(HttpMethod.Post)
                    .Times(1);

                Assert.Equal(1, result.Count);
            }
        }
    }
}