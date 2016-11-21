// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
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
		public async void GetApiResponceAsync400Exception()
		{
			_httpTest
				.RespondWithJson("{\"errorMessage\":\"\nStatus QUEUED is deprecated, use AWAITING_PAYMENT instead.\"}", 400)
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

		#region Default

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
			var settings = new EcwidSettings { ApiUrl = "https://app.ecwid.com/api/v1/" };
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

		#region Orders

		[Fact]
		public async void OrdersUrlException()
			=>
				await
					Assert.ThrowsAsync<EcwidConfigException>(
						() => _defaultClient.CheckOrdersTokenAsync());

		[Fact]
		public void OrdersGetEmpty()
		{
			var queryBuilder = _client.Orders;
			Assert.NotNull(queryBuilder);
		}

		[Fact]
		public async void OrdersCheckOrdersAuthAsync()
		{
			_httpTest
				.RespondWithJson(Moqs.MockSearchResultWithLimit1)
				.RespondWithJson(Moqs.MockSearchResultWithLimit1, 403);

			var result = await _client.CheckOrdersTokenAsync();
			Assert.Equal(true, result);

			result = await _client.CheckOrdersTokenAsync();
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
					async () => await _client.CheckOrdersTokenAsync());

			_httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=1")
				.WithVerb(HttpMethod.Get)
				.Times(1);
		}

		[Fact]
		public async void GetOrderAsyncFail() => await
			Assert.ThrowsAsync<ArgumentOutOfRangeException>(
				async () => await _client.GetOrderAsync(0));

		[Fact]
		public async void GetOrderAsyncNull()
		{
			_httpTest
				.RespondWithJson(Moqs.MockSearchResultZeroResult)
				.RespondWithJson(Moqs.MockSearchResultZeroResult);

			var result = await _client.GetOrderAsync(1);

			Assert.Null(result);
			_httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&orderNumber=1")
				.WithVerb(HttpMethod.Get)
				.Times(1);
		}

		[Fact]
		public async void OrdersGetOrdersCountAsync()
		{
			_httpTest
				.RespondWithJson(Moqs.MockSearchResultWithLimit1)
				.RespondWithJson(Moqs.MockSearchResultZeroResult);

			var result = await _client.GetOrdersCountAsync();
			Assert.Equal(100, result);

			result = await _client.GetOrdersCountAsync();
			Assert.Equal(0, result);

			_httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&limit=1")
				.WithVerb(HttpMethod.Get)
				.Times(2);
		}

		[Fact]
		public async void OrdersGetNewOrdersAsync()
		{
			_httpTest
				.RespondWithJson(Moqs.MockSearchResultWithOneOrder)
				.RespondWithJson(Moqs.MockSearchResultWithOneOrder);

			var result = await _client.GetNewOrdersAsync();
			Assert.NotEmpty(result);

			_httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&fulfillmentStatus=*")
				.WithVerb(HttpMethod.Get)
				.Times(1);
		}

		[Fact]
		public async void OrdersGetIncompleteOrdersAsync()
		{
			_httpTest
				.RespondWithJson(Moqs.MockSearchResultWithOneOrder)
				.RespondWithJson(Moqs.MockSearchResultWithOneOrder);

			var result = await _client.GetIncompleteOrdersAsync();
			Assert.NotEmpty(result);

			_httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&paymentStatus=*")
				.WithVerb(HttpMethod.Get)
				.Times(1);
		}

		[Fact]
		public async void OrdersGetNonPaidOrdersAsync()
		{
			_httpTest
				.RespondWithJson(Moqs.MockSearchResultWithOneOrder)
				.RespondWithJson(Moqs.MockSearchResultWithOneOrder);

			var result = await _client.GetNonPaidOrdersAsync();
			Assert.NotEmpty(result);

			_httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&paymentStatus=*")
				.WithVerb(HttpMethod.Get)
				.Times(1);
		}

		[Fact]
		public async void OrdersGetPaidNotShippedOrdersAsync()
		{
			_httpTest
				.RespondWithJson(Moqs.MockSearchResultWithOneOrder)
				.RespondWithJson(Moqs.MockSearchResultWithOneOrder);

			var result = await _client.GetPaidNotShippedOrdersAsync();
			Assert.NotEmpty(result);

			_httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&paymentStatus=*&fulfillmentStatus=*")
				.WithVerb(HttpMethod.Get)
				.Times(1);
		}

		[Fact]
		public async void OrdersGetShippedOrdersAsync()
		{
			_httpTest
				.RespondWithJson(Moqs.MockSearchResultWithOneOrder)
				.RespondWithJson(Moqs.MockSearchResultWithOneOrder);

			var result = await _client.GetShippedOrdersAsync();
			Assert.NotEmpty(result);

			_httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&fulfillmentStatus=*")
				.WithVerb(HttpMethod.Get)
				.Times(1);
		}

		[Fact]
		public async void OrdersGetOrdersAsyncQueryMultiPagesResult()
		{
			const int count = 100;
			const string query = "paymentStatus=paid";

			_httpTest
				.RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count * 0, count))
				.RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count * 1, count))
				.RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count * 2, count))
				.RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, count * 3, 0));

			var result = await _client.GetOrdersAsync(new { paymentStatus = "paid" });

			_httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&{query}")
				.WithVerb(HttpMethod.Get)
				.Times(1);

			_httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&offset=*&{query}")
				.WithVerb(HttpMethod.Get)
				.Times(3);

			Assert.Equal(count * 3, result.Count);
		}

		[Fact]
		public async void OrdersGetOrdersAsyncQueryOnePagesResult()
		{
			const int count = 100;
			const string query = "limit=100&paymentStatus=paid";

			_httpTest
				.RespondWithJson(Moqs.MockSearchResultWithManyOrderAndPages(count, 0, count));

			var result = await _client.GetOrdersAsync(new { limit = count, paymentStatus = "paid" });


			_httpTest.ShouldHaveCalled($"{CheckOrdersUrl}&{query}")
				.WithVerb(HttpMethod.Get)
				.Times(1);

			_httpTest.ShouldNotHaveCalled($"{CheckOrdersUrl}&offset=*&{query}");
				//.WithVerb(HttpMethod.Get)
				//.Times(6);

			Assert.Equal(count, result.Count);
		}

		#endregion

		#region Profile

		[Fact]
		public async void GetProfileException()
			=> await Assert.ThrowsAsync<EcwidConfigException>(() => _defaultClient.GetProfileAsync());

		[Fact]
		public async void UpdateProfileAsync()
		{
			_httpTest
				.RespondWithJson(new UpdateStatus { Success = true, UpdateCount = 1 });

			var profile = new Profile { Account = new Account { AccountName = "John", AccountNickname = "John" } };

			var result = await _client.UpdateProfileAsync(profile);
			Assert.Equal(true, result.Success);

			_httpTest.ShouldHaveCalled($"{CheckProfileUrl}")
				.WithVerb(HttpMethod.Put)
				.Times(1);
		}

		[Fact]
		public async void UpdateProfileFail()
			=> await Assert.ThrowsAsync<EcwidHttpException>(async () => await _client.UpdateProfileAsync(null));

		[Fact]
		public async void UpdateProfileAsyncHttpExceptions()
		{
			_httpTest
				.RespondWithJson(new UpdateStatus { Success = false, UpdateCount = 0 }, 400);

			var profile = new Profile { Account = new Account { AccountName = "John", AccountNickname = "John" } };

			await Assert.ThrowsAsync<EcwidHttpException>(async () => await _client.UpdateProfileAsync(profile));

			_httpTest.ShouldHaveCalled($"{CheckProfileUrl}")
				.WithVerb(HttpMethod.Put)
				.Times(1);
		}

		#endregion

		#region DefaultLegacy

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
			var settings = new EcwidLegacySettings { ApiUrl = "https://app.ecwid.com/api/v1/" };
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

		#region LegacyProducts

		[Fact]
		public async void LegacyCategoriesUrlException()
			=>
				await
					Assert.ThrowsAsync<EcwidConfigException>(
						() => _defaultLegacyClient.GetCategoriesAsync());

		[Fact]
		public async void GetCategoryAsyncException()
			=>
				await
					Assert.ThrowsAsync<ArgumentOutOfRangeException>(
						() => _legacyClient.GetCategoryAsync(-1));

		[Fact]
		public async void GetCategoryAsync404()
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
		public async void LegacyOrdersOrdersUrlException()
			=>
				await
					Assert.ThrowsAsync<EcwidConfigException>(
						() => _defaultLegacyClient.CheckOrdersTokenAsync());

		[Fact]
		public async void LegacyOrdersCheckOrdersTokenAsync()
		{
			_httpTest
				.RespondWithJson(new { count = 0, total = 0, order = "[]" });

			var result = await _legacyClient.CheckOrdersTokenAsync();

			_httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=1")
				.WithVerb(HttpMethod.Get)
				.Times(1);

			Assert.Equal(true, result);
		}

		[Fact]
		public async void LegacyOrdersCheckOrdersAuthAsyncFail()
		{
			_httpTest
				.RespondWithJson(new { count = 0, total = 0, order = "[]" }, 403);

			var result = await _legacyClient.CheckOrdersTokenAsync();

			_httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=1")
				.WithVerb(HttpMethod.Get)
				.Times(1);

			Assert.Equal(false, result);
		}

		[Fact]
		public async void LegacyOrdersGetOrdersCountAsync()
		{
			_httpTest
				.RespondWithJson(new { count = 0, total = 10, order = "[]" });

			var result = await _legacyClient.GetOrdersCountAsync();

			_httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=0")
				.WithVerb(HttpMethod.Get)
				.Times(1);

			Assert.Equal(10, result);
		}

		[Fact]
		public async void LegacyOrdersGetNewOrdersAsync()
		{
			var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

			_httpTest
				.RespondWithJson(responce);

			var result = await _legacyClient.GetNewOrdersAsync();

			_httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
				.WithVerb(HttpMethod.Get)
				.Times(1);

			Assert.Equal(1, result.Count);
		}

		[Fact]
		public async void LegacyOrdersGetNonPaidOrdersAsync()
		{
			var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

			_httpTest
				.RespondWithJson(responce);

			var result = await _legacyClient.GetNonPaidOrdersAsync();

			_httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
				.WithVerb(HttpMethod.Get)
				.Times(1);

			Assert.Equal(1, result.Count);
		}

		[Fact]
		public async void LegacyOrdersGetPaidNotShippedOrdersAsync()
		{
			var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

			_httpTest
				.RespondWithJson(responce);

			var result = await _legacyClient.GetPaidNotShippedOrdersAsync();

			_httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
				.WithVerb(HttpMethod.Get)
				.Times(1);

			Assert.Equal(1, result.Count);
		}

		[Fact]
		public async void LegacyOrdersGetShippedOrdersAsync()
		{
			var responce = Legacy.Moqs.MockLegacyOrderResponseWithOneOrder;

			_httpTest
				.RespondWithJson(responce);

			var result = await _legacyClient.GetShippedOrdersAsync();

			_httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&statuses=*")
				.WithVerb(HttpMethod.Get)
				.Times(1);

			Assert.Equal(1, result.Count);
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
				.RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseWithManyOrder(count));

			var result = await _legacyClient.GetOrdersAsync(new { statuses = "paid" });

			_httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&{query}")
				.WithVerb(HttpMethod.Get)
				.Times(2);

			_httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&{query}&offset={count}")
				.WithVerb(HttpMethod.Get)
				.Times(1);

			Assert.Equal(count * 2, result.Count);
		}

		[Fact]
		public async void LegacyOrdersGetOrdersAsyncQueryMultiPagesResultOnePage()
		{
			_httpTest
				.RespondWithJson(
					Legacy.Moqs.MockLegacyOrderResponseWithManyOrderAndPages(
						$"{_checkOrdersLegacyUrl}&limit=5&offset=5"));

			var result = await _legacyClient.Orders.Limit(5).GetAsync();

			_httpTest.ShouldHaveCalled($"{_checkOrdersLegacyUrl}&limit=5")
				//.WithVerb(HttpMethod.Get)
				.Times(1);

			_httpTest.ShouldNotHaveCalled($"{_checkOrdersLegacyUrl}&limit=5&offset=5");
				//.WithVerb(HttpMethod.Get)
				//.Times(1);

			Assert.Equal(200, result.Count);
		}

		[Fact]
		public async void LegacyOrdersUpdateAsyncNullBuilderFail()
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
		public async void UpdateAsyncNullStringsFail() => await Assert.ThrowsAsync<EcwidConfigException>(async ()
			=> await _legacyClient.Orders.Order(1).UpdateAsync("", "", ""));

		[Fact]
		public async void LegacyOrdersUpdateAsyncNullResult()
		{
			_httpTest
				.RespondWithJson(new { count = 0, total = 10, order = "[]" });

			var result = await _legacyClient.Orders.Order(1).UpdateAsync("PAID", "PROCESSING", "");

			_httpTest.ShouldHaveCalled(
				$"{_checkOrdersLegacyUrl}&order=1&new_payment_status=PAID&new_fulfillment_status=PROCESSING")
				.WithVerb(HttpMethod.Post)
				.Times(1);

			Assert.Empty(result);
		}

		[Fact]
		public async void LegacyOrdersUpdateAsyncResult()
		{
			_httpTest
				.RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseForUpdate);

			var result = await _legacyClient.Orders.Order(1).UpdateAsync("PAID", "PROCESSING", "test_code");

			_httpTest.ShouldHaveCalled(
				$"{_checkOrdersLegacyUrl}&order=1&new_payment_status=PAID&new_fulfillment_status=PROCESSING&new_shipping_tracking_code=test_code")
				.WithVerb(HttpMethod.Post)
				.Times(1);

			Assert.Equal(1, result.Count);
		}

		[Fact]
		public async void LegacyOrdersUpdateAsyncException()
		{
			_httpTest
				.RespondWithJson(Legacy.Moqs.MockLegacyOrderResponseForUpdate, 400);

			await
				Assert.ThrowsAsync<EcwidHttpException>(
					() => _legacyClient.Orders.Order(1).UpdateAsync("PAID", "PROCESSING", "test_code"));

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