// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;

namespace Ecwid.Legacy
{
	public partial class EcwidLegacyClient
	{
		#region Implementation of IEcwidProductsLegacyClient

		/// <summary>
		/// Gets the categories asynchronous. Returns an array of immediate subcategories of a given parent category. Disabled
		/// categories are not returned, but enabled subcategories of disabled categories are.
		/// </summary>
		/// <param name="parentCategoryId">
		/// The parent category identifier. If parent parameter is null, returns all categories. If
		/// parent parameter = 0, returns a list of root categories.
		/// </param>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		public async Task<IList<LegacyCategoryEntry>> GetCategoriesAsync(int? parentCategoryId = null)
			=> await GetCategoriesAsync(CancellationToken.None, parentCategoryId);

		/// <summary>
		/// Gets the categories asynchronous. Returns an array of immediate subcategories of a given parent category. Disabled
		/// categories are not returned, but enabled subcategories of disabled categories are.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <param name="parentCategoryId">
		/// The parent category identifier. If parent parameter is null, returns all categories. If
		/// parent parameter = 0, returns a list of root categories.
		/// </param>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="parentCategoryId" /> must be positive, 0 or null.</exception>
		public async Task<IList<LegacyCategoryEntry>> GetCategoriesAsync(CancellationToken cancellationToken,
			int? parentCategoryId = null)
		{
			switch (parentCategoryId)
			{
				case null:
					return await GetApiAsync<IList<LegacyCategoryEntry>>(GetUrl("categories", true), cancellationToken);

				default:
					if (parentCategoryId < 0)
						throw new ArgumentOutOfRangeException(nameof(parentCategoryId));

					return
						await
							GetApiAsync<IList<LegacyCategoryEntry>>(GetUrl("categories", true),
								new { parent = parentCategoryId },
								cancellationToken);
			}
		}

		/// <summary>
		/// Gets the category asynchronous. Returns single category with given category id, include subcategories. Disabled
		/// categories are not returned, but enabled subcategories of disabled categories are.
		/// </summary>
		/// <param name="categoryId">The category identifier.</param>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="categoryId" /> must be greater than 0.</exception>
		public async Task<LegacyCategory> GetCategoryAsync(int categoryId)
			=> await GetCategoryAsync(categoryId, CancellationToken.None);

		/// <summary>
		/// Gets the category asynchronous. Returns single category with given category id, include subcategories. Disabled
		/// categories are not returned, but enabled subcategories of disabled categories are.
		/// </summary>
		/// <param name="categoryId">The category identifier.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="categoryId" /> must be greater than 0.</exception>
		public async Task<LegacyCategory> GetCategoryAsync(int categoryId, CancellationToken cancellationToken)
		{
			if (categoryId <= 0)
				throw new ArgumentOutOfRangeException(nameof(categoryId));

			return await
				GetApiAsync<LegacyCategory>(GetUrl("category", true), new { id = categoryId }, cancellationToken);
		}

		/// <summary>
		/// Returns an array of products of a given category asynchronous. If the <paramref name="categoryId"/> is absent, returns all products. 
		/// If <paramref name="categoryId"/>=0, then returns a list of products which are not assigned to any category. 
		/// The sort order of returned products array corresponds to the order defined in the store settings. 
		/// If the <paramref name="hiddenProducts"/> is set as 'true' and Product Update API key is provided, the method also returns disabled and hidden out-of-stock products.
		/// </summary>
		/// <param name="categoryId">The category identifier.</param>
		/// <param name="hiddenProducts">if set to <c>true</c> [hidden products].</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="categoryId" /> must be positive, 0 or null.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		public async Task<IList<LegacyProductEntry>> GetProductsAsync(int? categoryId = null, bool hiddenProducts = false) 
			=> await GetProductsAsync(CancellationToken.None, categoryId, hiddenProducts);

		/// <summary>
		/// Returns an array of products of a given category asynchronous. If the <paramref name="categoryId" /> is absent, returns all products.
		/// If <paramref name="categoryId" />=0, then returns a list of products which are not assigned to any category.
		/// The sort order of returned products array corresponds to the order defined in the store settings.
		/// If the <paramref name="hiddenProducts" /> is set as 'true' and Product Update API key is provided, the method also returns disabled and hidden out-of-stock products.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <param name="categoryId">The category identifier.</param>
		/// <param name="hiddenProducts">if set to <c>true</c> [hidden products].</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="categoryId" /> must be positive, 0 or null.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		public async Task<IList<LegacyProductEntry>> GetProductsAsync(CancellationToken cancellationToken, int? categoryId = null, bool hiddenProducts = false)
		{
			switch (categoryId)
			{
				case null:
					return await GetApiAsync<IList<LegacyProductEntry>>(GetUrl("products", !hiddenProducts), new { hidden_products = hiddenProducts }, cancellationToken);

				default:
					if (categoryId < 0)
						throw new ArgumentOutOfRangeException(nameof(categoryId));

					return await GetApiAsync<IList<LegacyProductEntry>>(GetUrl("products", !hiddenProducts), new { category = categoryId, hidden_products = hiddenProducts },
						cancellationToken);
			}
		}

		/// <summary>
		/// Returns a product information by a product ID asynchronous.
		/// If the <paramref name="hiddenProducts"/> is set as 'true' and Product Update API key is provided, the method also returns disabled and hidden out-of-stock products.
		/// </summary>
		/// <param name="productId">The product identifier.</param>
		/// <param name="hiddenProducts">if set to <c>true</c> [hidden products].</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="productId" /> must be positive.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		public async Task<LegacyProduct> GetProductAsync(int productId, bool hiddenProducts = false) 
			=> await GetProductAsync(productId, CancellationToken.None, hiddenProducts);

		/// <summary>
		/// Returns a product information by a product ID asynchronous.
		/// If the <paramref name="hiddenProducts" /> is set as 'true' and Product Update API key is provided, the method also returns disabled and hidden out-of-stock products.
		/// </summary>
		/// <param name="productId">The product identifier.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <param name="hiddenProducts">if set to <c>true</c> [hidden products].</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="productId" /> must be positive.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		public async Task<LegacyProduct> GetProductAsync(int productId, CancellationToken cancellationToken, bool hiddenProducts = false)
		{
			if (productId <= 0)
				throw new ArgumentOutOfRangeException(nameof(productId));

			return await GetApiAsync<LegacyProduct>(GetUrl("product", !hiddenProducts), new { id = productId, hidden_products = hiddenProducts },
						cancellationToken);
		}

		/// <summary>
		/// Updates a product information by a product ID asynchronous.
		/// </summary>
		/// <param name="productId">The product identifier.</param>
		/// <param name="updatedFields">The updated fields.</param>
		/// <exception cref="ArgumentNullException"><paramref name="updatedFields"/>must by not null.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="productId" /> must be positive.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		public async Task<bool> UpdateProductAsync(int productId, object updatedFields) 
			=> await UpdateProductAsync(productId, updatedFields, CancellationToken.None);

		/// <summary>
		/// Updates a product information by a product ID asynchronous.
		/// </summary>
		/// <param name="productId">The product identifier.</param>
		/// <param name="updatedFields">The updated fields.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <exception cref="ArgumentNullException"><paramref name="updatedFields"/>must by not null.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="productId" /> must be positive.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		public async Task<bool> UpdateProductAsync(int productId, object updatedFields, CancellationToken cancellationToken)
		{
			if (updatedFields == null) throw new ArgumentNullException(nameof(updatedFields));

			if (productId <= 0)
				throw new ArgumentOutOfRangeException(nameof(productId));

			return await PutApiAsync(GetUrl("product"), new { id = productId}, updatedFields,
						cancellationToken);
		}

		#endregion
	}
}