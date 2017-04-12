// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Diagnostics.CodeAnalysis;
using Ecwid.Legacy;
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
        public void Credentials()
        {
            var result = new EcwidCredentials(ShopId, Token);
            var result2 = new EcwidLegacyCredentials(ShopId, Token, Token);

            Assert.NotNull(result);
            Assert.NotNull(result2);
        }

        [Fact]
        public void CredentialsFail()
        {
            Assert.Throws<EcwidConfigException>(() => new EcwidCredentials(0, Token));
            Assert.Throws<EcwidConfigException>(() => new EcwidCredentials(ShopId, null));
            Assert.Throws<EcwidConfigException>(() => new EcwidCredentials(ShopId, ""));
            Assert.Throws<EcwidConfigException>(() => new EcwidCredentials(ShopId, " "));

            // https://github.com/kroniak/extensions-ecwid/issues/34
            //Assert.Throws<EcwidConfigException>(() => new EcwidCredentials(ShopId, "test"));
            Assert.Throws<EcwidConfigException>(() => new EcwidLegacyCredentials(0, Token, Token));
            Assert.Throws<EcwidConfigException>(() => new EcwidLegacyCredentials(ShopId));
            Assert.Throws<EcwidConfigException>(() => new EcwidLegacyCredentials(ShopId, " ", " "));
            Assert.Throws<EcwidConfigException>(() => new EcwidLegacyCredentials(ShopId, "", ""));
            Assert.Throws<EcwidConfigException>(() => new EcwidLegacyCredentials(ShopId, Token, Token).OrderToken = " ");

            // https://github.com/kroniak/extensions-ecwid/issues/34
            //Assert.Throws<EcwidConfigException>(
            //    () => new EcwidLegacyCredentials(ShopId, Token, Token).OrderToken = "test");
            Assert.Throws<EcwidConfigException>(
                () => new EcwidLegacyCredentials(ShopId, Token, Token).ProductToken = " ");

            // https://github.com/kroniak/extensions-ecwid/issues/34
            //Assert.Throws<EcwidConfigException>(
            //    () => new EcwidLegacyCredentials(ShopId, Token, Token).ProductToken = "test");
        }
    }
}