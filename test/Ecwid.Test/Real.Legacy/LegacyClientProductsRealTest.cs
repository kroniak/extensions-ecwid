﻿// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Ecwid.Legacy;
using Xunit;

namespace Ecwid.Test.Real.Legacy
{
    /// <summary>
    /// Tests with real http responses.
    /// </summary>
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    [SuppressMessage("ReSharper", "MissingXmlDoc")]
    public class LegacyClientProductsRealTest
    {
        private const int ShopId = 123;
        private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";
        private readonly EcwidLegacyCredentials _credentials = new EcwidLegacyCredentials(ShopId, Token, Token);

        [Fact]
        public async void GetCategoriesAsync_ReturnCorrectList()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/572b6b651300000912e2b85c" //categories entries
                }
            };

            var result = await client.GetCategoriesAsync();
            Assert.Equal(15, result.Count());

            result = await client.GetCategoriesAsync(1);
            Assert.Equal(15, result.Count());
        }

        [Fact]
        public async void GetCategoriesAsync0_ReturnSingle()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/572b6c411300002612e2b85d" //categories entries with 0
                }
            };

            var result = await client.GetCategoriesAsync(0);

            Assert.Single(result);
        }

        [Fact]
        public async void GetCategoryAsync_ReturnCorrectList()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/572b7a1f1300006414e2b86c" //one category
                }
            };

            var result = await client.GetCategoryAsync(1);

            Assert.NotEmpty(result.Subcategories);
            Assert.NotEmpty(result.Products);
        }
    }
}