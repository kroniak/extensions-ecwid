// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Legacy.Models;

// ReSharper disable CheckNamespace

namespace Ecwid.Legacy
{
    public partial class EcwidLegacyClient
    {
        #region Implementation of IEcwidProductsLegacyClient

        /// <inheritdoc />
        public Task<IEnumerable<LegacyCategoryEntry>> GetCategoriesAsync(int? parentCategoryId = null)
            => GetCategoriesAsync(CancellationToken.None, parentCategoryId);

        /// <inheritdoc />
        public Task<IEnumerable<LegacyCategoryEntry>> GetCategoriesAsync(CancellationToken cancellationToken,
            int? parentCategoryId = null)
        {
            switch (parentCategoryId)
            {
                case null:
                    return GetApiAsync<IEnumerable<LegacyCategoryEntry>>(GetUrl("categories", true),
                        cancellationToken);

                default:
                    if (parentCategoryId < 0)
                        throw new ArgumentException(nameof(parentCategoryId));

                    return
                        GetApiAsync<IEnumerable<LegacyCategoryEntry>>(GetUrl("categories", true),
                            new {parent = parentCategoryId},
                            cancellationToken);
            }
        }

        /// <inheritdoc />
        public Task<LegacyCategory> GetCategoryAsync(int categoryId) =>
            GetCategoryAsync(categoryId, CancellationToken.None);

        /// <inheritdoc />
        public Task<LegacyCategory> GetCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Category is is 0.", nameof(categoryId));

            return
                GetApiAsync<LegacyCategory>(GetUrl("category", true), new {id = categoryId}, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IEnumerable<LegacyProductEntry>> GetProductsAsync(int? categoryId = null,
            bool hiddenProducts = false) =>
            GetProductsAsync(CancellationToken.None, categoryId, hiddenProducts);

        /// <inheritdoc />
        public Task<IEnumerable<LegacyProductEntry>> GetProductsAsync(CancellationToken cancellationToken,
            int? categoryId = null, bool hiddenProducts = false)
        {
            switch (categoryId)
            {
                case null:
                    return GetApiAsync<IEnumerable<LegacyProductEntry>>(GetUrl("products", !hiddenProducts),
                        new {hidden_products = hiddenProducts}, cancellationToken);

                default:
                    if (categoryId < 0)
                        throw new ArgumentException("Category id is empty.", nameof(categoryId));

                    return GetApiAsync<IEnumerable<LegacyProductEntry>>(GetUrl("products", !hiddenProducts),
                        new {category = categoryId, hidden_products = hiddenProducts},
                        cancellationToken);
            }
        }

        /// <inheritdoc />
        public Task<LegacyProduct> GetProductAsync(int productId, bool hiddenProducts = false)
            => GetProductAsync(productId, CancellationToken.None, hiddenProducts);

        /// <inheritdoc />
        public Task<LegacyProduct> GetProductAsync(int productId, CancellationToken cancellationToken,
            bool hiddenProducts = false)
        {
            if (productId <= 0)
                throw new ArgumentException("Product id is empty.", nameof(productId));

            return GetApiAsync<LegacyProduct>(GetUrl("product", !hiddenProducts),
                new {id = productId, hidden_products = hiddenProducts},
                cancellationToken);
        }

        /// <inheritdoc />
        public Task<bool> UpdateProductAsync(int productId, object updatedFields)
            => UpdateProductAsync(productId, updatedFields, CancellationToken.None);

        /// <inheritdoc />
        public Task<bool> UpdateProductAsync(int productId, object updatedFields,
            CancellationToken cancellationToken)
        {
            if (updatedFields == null) throw new ArgumentNullException(nameof(updatedFields));

            if (productId <= 0)
                throw new ArgumentException("Product id is empty.", nameof(productId));

            return PutApiAsync(GetUrl("product"), new {id = productId}, updatedFields,
                cancellationToken);
        }

        #endregion
    }
}