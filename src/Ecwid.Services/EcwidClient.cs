// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Ecwid.Tools;

namespace Ecwid.Services
{
    /// <summary>
    /// Ecwid API Client v3.
    /// </summary>
    public partial class EcwidClient : BaseEcwidClient, IEcwidClient
    {
        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public EcwidOptions Options { get; private set; } = new EcwidOptions();

        /// <summary>
        /// Configures the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">The shop identificator is invalid. Please reconfig the client.</exception>
        public IEcwidClient Configure(EcwidOptions options)
        {
            Validators.ShopIdValidate(options.ShopId);

            Options = options;
            return this;
        }

        /// <summary>
        /// Configures the shop.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">The shop identificator is invalid. Please reconfig the client.</exception>
        public IEcwidClient Configure(int shopId, string token)
        {
            Validators.ShopIdValidate(shopId);

            Options.ShopId = shopId;

            if (!string.IsNullOrEmpty(token))
                Options.Token = token;

            return this;
        }
    }
}