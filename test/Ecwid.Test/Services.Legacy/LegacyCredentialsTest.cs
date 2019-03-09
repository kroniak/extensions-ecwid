// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Diagnostics.CodeAnalysis;
using Ecwid.Legacy;
using Xunit;

namespace Ecwid.Test.Services.Legacy
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class LegacyCredentialsTest
    {
        // Tests params
        private const int ShopId = 123;
        private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";

        [Fact]
        public void Credentials_ReturnNotNull()
        {
            var result2 = new EcwidLegacyCredentials(ShopId, Token, Token);

            Assert.NotNull(result2);
        }

        [Fact]
        public void Credentials_InvalidShopId_Exception()
        {
            var ecwidConfigException =
                Assert.Throws<EcwidConfigException>(() => new EcwidLegacyCredentials(0, Token, Token));
            Assert.Equal("The shop identifier is invalid.", ecwidConfigException.Message);
        }

        [Fact]
        public void EcwidLegacyCredentials_TokenInvalid_Exception1()
        {
            var exception = Assert.Throws<EcwidConfigException>(() => new EcwidLegacyCredentials(ShopId));
            Assert.Equal("The authorization token is invalid.", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void EcwidLegacyCredentials_TokenInvalid_InvalidArgument_2Part_Exception(string token)
        {
            var exception = Assert.Throws<EcwidConfigException>(() => new EcwidLegacyCredentials(ShopId, token, " "));
            Assert.Equal("The authorization token is invalid.", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void EcwidLegacyCredentials_ProductTokenInvalid_InvalidArgument_2Part_Exception(string token)
        {
            var exception = Assert.Throws<EcwidConfigException>(() => new EcwidLegacyCredentials(ShopId, Token, token));
            Assert.Equal("The authorization token is invalid.", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void EcwidLegacyCredentials_TokenInvalid_InvalidArgument_3Part_Exception(string token)
        {
            var exception = Assert.Throws<EcwidConfigException>(() =>
                new EcwidLegacyCredentials(ShopId, Token, Token).OrderToken = token);
            Assert.Equal("The order authorization token is invalid.", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void EcwidLegacyCredentials_ProductTokenInvalid_InvalidArgument_4Part_Exception(string token)
        {
            var exception = Assert.Throws<EcwidConfigException>(
                () => new EcwidLegacyCredentials(ShopId, Token, Token).ProductToken = token);
            Assert.Equal("The product authorization token is invalid.", exception.Message);
        }
    }
}