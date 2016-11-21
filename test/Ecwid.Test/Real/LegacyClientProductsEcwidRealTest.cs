// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Ecwid.Legacy;
using Xunit;

namespace Ecwid.Test.Real
{
	/// <summary>
	/// Tests with real http responses from Ecwid API v1.
	/// </summary>
	[SuppressMessage("ReSharper", "ExceptionNotDocumented")]
	[SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
	[SuppressMessage("ReSharper", "MissingXmlDoc")]
	public class LegacyClientProductsEcwidRealTest
	{
		private const int ShopId = 1003;
		private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";
		private readonly IEcwidLegacyClient _client = new EcwidLegacyClient(ShopId, Token, Token);

		[Fact]
		public async void GetCategoriesAsync()
		{
			var result = await _client.GetCategoriesAsync();
			Assert.NotEmpty(result);
			Assert.Equal(7, result.Count);
		}

		[Fact]
		public async void GetCategoriesAsync0()
		{
			var result = await _client.GetCategoriesAsync(0);

			Assert.NotEmpty(result);
			Assert.Equal(3, result.Count);
		}

		[Fact]
		public async void GetCategoriesAsync404()
		{
			var result = await _client.GetCategoriesAsync(1);
			Assert.Null(result);
		}

		[Fact]
		public async void GetCategoryAsync()
		{
			var result = await _client.GetCategoryAsync(20671017);

			Assert.NotNull(result);
			Assert.NotNull(result.Subcategories);
			Assert.NotNull(result.Products);
		}

		[Fact]
		public async void GetCategoryAsync404()
		{
			var result = await _client.GetCategoryAsync(10221202);

			Assert.Null(result);
		}

		[Fact]
		public async void GetAllProductsAsync()
		{
			var result = await _client.GetProductsAsync();

			Assert.NotEmpty(result);
		}

		[Fact]
		public async void GetProductsAsync()
		{
			var result = await _client.GetProductsAsync(20671017);

			Assert.NotEmpty(result);
		}

		[Fact]
		public async void GetProductsAsyncException()
			=> await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _client.GetProductsAsync(-1));

		[Fact]
		public async void GetProductsAsyncExceptionWrongToken()
		{
			var ex = await Assert.ThrowsAsync<EcwidHttpException>(() => _client.GetProductsAsync(null, true));

			Assert.Equal(HttpStatusCode.Forbidden, ex.StatusCode);
		}

		[Fact]
		public async void GetProductsAsyncException404()
		{
			var result = await _client.GetProductsAsync(221233);
			Assert.Null(result);
		}

		[Fact]
		public async void GetProductAsync()
		{
			var result = await _client.GetProductAsync(70178253);

			Assert.NotNull(result);
		}

		[Fact]
		public async void GetProductAsyncException()
			=> await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _client.GetProductAsync(-1));

		[Fact]
		public async void UpdateProductAsync()
		{
			var ex = await Assert.ThrowsAsync<EcwidHttpException>(() => _client.UpdateProductAsync(123, new { weight = 600 }));

			Assert.Equal(HttpStatusCode.Forbidden, ex.StatusCode);
		}
	}
}