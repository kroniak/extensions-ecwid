// Licensed under the MIT License. See LICENSE in the git repository root for license information.

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
    public class ClientProfileRealTest
    {
        private const int ShopId = 123;
        private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";
        private readonly EcwidCredentials _credentials;

        public ClientProfileRealTest()
        {
            _credentials = new EcwidCredentials(ShopId, Token);
        }

        [Fact]
        public async void GetProfileAsync()
        {
            IEcwidProfileClient client = new EcwidClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/572a0c0a0f0000bf0ea0557c" //response with profile from ecwid.
                }
            };

            var result = await client.GetProfileAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async void GetProfileRealAsync()
        {
            IEcwidProfileClient client = new EcwidClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/572a0e360f00001e0fa05580" //response with profile from real world.
                }
            };

            var result = await client.GetProfileAsync();

            Assert.NotNull(result);
        }
    }
}