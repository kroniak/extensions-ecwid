// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Ecwid.Test.Services
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class CredentialsTest
    {
        // Tests params
        private const int ShopId = 123;
        private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";

        [Fact]
        public void Credentials_ReturnNotNull()
        {
            var result = new EcwidCredentials(ShopId, Token);

            Assert.NotNull(result);
        }

        [Fact]
        public void Credentials_InvalidShopId_Exception()
        {
            var ecwidConfigException = Assert.Throws<EcwidConfigException>(() => new EcwidCredentials(0, Token));
            Assert.Equal("The shop identifier is invalid.", ecwidConfigException.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void EcwidCredentials_TokenInvalid_Exception(string token)
        {
            var exception = Assert.Throws<EcwidConfigException>(() => new EcwidCredentials(ShopId, token));
            Assert.Equal("The authorization token is invalid.", exception.Message);
        }
    }
}