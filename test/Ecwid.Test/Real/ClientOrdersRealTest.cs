// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Net;
using Ecwid.Models;
using Xunit;

namespace Ecwid.Test.Real
{
    /// <summary>
    /// Tests with real http responses.
    /// </summary>
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    [SuppressMessage("ReSharper", "MissingXmlDoc")]
    public class ClientOrdersRealTest
    {
        private const int ShopId = 123;
        private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";
        private readonly EcwidCredentials _credentials = new EcwidCredentials(ShopId, Token);

        [Fact]
        public async void CheckOrdersAuthAsync_ReturnFalse()
        {
            IEcwidOrdersClient client = new EcwidClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa70921100009b28a72180"
                    //empty set with count, total and 403 code
                }
            };

            var result = await client.CheckOrdersTokenAsync();

            Assert.False(result);
        }

        [Fact]
        public async void CheckOrdersTokenAsync_ReturnTrue()
        {
            IEcwidOrdersClient client = new EcwidClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/57209e300f0000f208387249" //response with one order and limit = 1.
                }
            };

            var result = await client.CheckOrdersTokenAsync();

            Assert.True(result);
        }

        [Fact]
        public async void GetOrderAsync_ReturnCorrectNumbers()
        {
            //http://www.mocky.io/v2/57209cae0f0000a208387242 - one order

            IEcwidOrdersClient client = new EcwidClient(_credentials)
            {
                Settings =
                {
//                    Mute by mutation testing FirstOrDefault
//                    ApiUrl = "http://www.mocky.io/v2/57209e580f0000f20838724a" //response with one order.
                    ApiUrl = "http://www.mocky.io/v2/5c83049d3000002b006b0bf3" //response with one order.
                }
            };

            var result = await client.GetOrderAsync(18);

            Assert.Equal(18, result.OrderNumber);
        }

        [Fact]
        public async void GetOrdersCountAsync_ReturnCorrectOrdersCount()
        {
            IEcwidOrdersClient client = new EcwidClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/5720a20b0f0000f108387258" //set with count=1, limit=1 and total
                }
            };

            var result = await client.GetOrdersCountAsync();

            Assert.Equal(1021, result);
        }

        [Fact]
        public async void UpdateOrderAsync_ReturnCorrectUpdatesCount()
        {
            IEcwidOrdersClient client = new EcwidClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/5967f962110000b9006149e5"
                }
            };

            var result = await client.UpdateOrderAsync(new OrderEntry {Email = "test@test.com", OrderNumber = 123});

            Assert.Equal(1, result.UpdateCount);
        }

        [Fact]
        public async void UpdateOrderAsync_ReturnBadRequest()
        {
            IEcwidOrdersClient client = new EcwidClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/5967f9f8110000b8006149e6"
                }
            };

            var exception = await Assert.ThrowsAsync<EcwidHttpException>(() =>
                client.UpdateOrderAsync(new OrderEntry {Email = "test@test.com", OrderNumber = 123}));

            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }
    }
}