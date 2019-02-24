// Licensed under the MIT License. See LICENSE in the git repository root for license information.

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

		/// <inheritdoc />
		public async Task<IList<LegacyCategoryEntry>> GetCategoriesAsync(int? parentCategoryId = null)
			=> await GetCategoriesAsync(CancellationToken.None, parentCategoryId);

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<LegacyCategory> GetCategoryAsync(int categoryId)
			=> await GetCategoryAsync(categoryId, CancellationToken.None);

		/// <inheritdoc />
		public async Task<LegacyCategory> GetCategoryAsync(int categoryId, CancellationToken cancellationToken)
		{
			if (categoryId <= 0)
				throw new ArgumentOutOfRangeException(nameof(categoryId));

			return await
				GetApiAsync<LegacyCategory>(GetUrl("category", true), new { id = categoryId }, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<IList<LegacyProductEntry>> GetProductsAsync(int? categoryId = null, bool hiddenProducts = false) 
			=> await GetProductsAsync(CancellationToken.None, categoryId, hiddenProducts);

		/// <inheritdoc />
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

		/// <inheritdoc />
		public async Task<LegacyProduct> GetProductAsync(int productId, bool hiddenProducts = false) 
			=> await GetProductAsync(productId, CancellationToken.None, hiddenProducts);

		/// <inheritdoc />
		public async Task<LegacyProduct> GetProductAsync(int productId, CancellationToken cancellationToken, bool hiddenProducts = false)
		{
			if (productId <= 0)
				throw new ArgumentOutOfRangeException(nameof(productId));

			return await GetApiAsync<LegacyProduct>(GetUrl("product", !hiddenProducts), new { id = productId, hidden_products = hiddenProducts },
						cancellationToken);
		}

		/// <inheritdoc />
		public async Task<bool> UpdateProductAsync(int productId, object updatedFields) 
			=> await UpdateProductAsync(productId, updatedFields, CancellationToken.None);

		/// <inheritdoc />
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