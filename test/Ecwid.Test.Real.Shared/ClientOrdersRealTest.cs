// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Diagnostics.CodeAnalysis;
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
        public async void CheckOrdersAuthAsyncFail()
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

            Assert.Equal(false, result);
        }

        [Fact]
        public async void CheckOrdersTokenAsync()
        {
            IEcwidOrdersClient client = new EcwidClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/57209e300f0000f208387249" //response with one order and limit = 1.
                }
            };

            var result = await client.CheckOrdersTokenAsync();

            Assert.Equal(true, result);
        }

        [Fact]
        public async void GetOrderAsync()
        {
            //http://www.mocky.io/v2/57209cae0f0000a208387242 - one order

            IEcwidOrdersClient client = new EcwidClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/57209e580f0000f20838724a" //response with one order.
                }
            };

            var result = await client.GetOrderAsync(18);

            Assert.Equal(18, result.OrderNumber);
        }

        [Fact]
        public async void GetOrdersCountAsync()
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
    }
}