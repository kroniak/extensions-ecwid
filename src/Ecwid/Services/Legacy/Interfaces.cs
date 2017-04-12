// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;

namespace Ecwid.Legacy
{
	/// <summary>
	/// Public legacy client API.
	/// </summary>
	public interface IEcwidLegacyClient : IEcwidOrdersLegacyClient, IEcwidProductsLegacyClient
	{
		/// <summary>
		/// Gets and sets the credentials. Default value is <see langword="null" />.
		/// </summary>
		/// <value>
		/// The credentials.
		/// </value>
		EcwidLegacyCredentials Credentials { get; set; }

		/// <summary>
		/// Gets and sets the settings. Created by default.
		/// </summary>
		/// <value>
		/// The settings.
		/// </value>
		EcwidLegacySettings Settings { get; set; }

		/// <summary>
		/// Configures with specified settings.
		/// </summary>
		/// <param name="settings">The settings.</param>
		IEcwidLegacyClient Configure(EcwidLegacySettings settings);

		/// <summary>
		/// Configures the shop credentials.
		/// </summary>
		/// <param name="shopId">The shop identifier.</param>
		/// <param name="orderToken">The shop order authorization token.</param>
		/// <param name="productToken">The shop product authorization token.</param>
		/// <exception cref="EcwidConfigException">The shop identifier is invalid.</exception>
		/// <exception cref="EcwidConfigException">The authorization tokens are null.</exception>
		/// <exception cref="EcwidConfigException">The order authorization token is invalid.</exception>
		/// <exception cref="EcwidConfigException">The product authorization token is invalid.</exception>
		IEcwidLegacyClient Configure(int shopId, string orderToken = null, string productToken = null);

		/// <summary>
		/// Configures with specified credentials.
		/// </summary>
		/// <param name="credentials">The credentials.</param>
		IEcwidLegacyClient Configure(EcwidLegacyCredentials credentials);
	}

	/// <summary>
	/// Public legacy orders client API.
	/// </summary>
	public interface IEcwidOrdersLegacyClient : IEcwidOrdersClient<LegacyOrder, LegacyUpdatedOrders>
	{
	}

	/// <summary>
	/// Public legacy products and categories client API.
	/// </summary>
	public interface IEcwidProductsLegacyClient
	{
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
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="parentCategoryId" /> must be positive, 0 or null.</exception>
		Task<IList<LegacyCategoryEntry>> GetCategoriesAsync(int? parentCategoryId = null);

		/// <summary>
		/// Gets the categories asynchronous. Returns an array of immediate subcategories of a given parent category. Disabled
		/// categories are not returned, but enabled subcategories of disabled categories are.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <param name="parentCategoryId">
		/// The parent category identifier. If parent parameter is null, returns all categories. If
		/// parent parameter = 0, returns a list of root categories.
		/// </param>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="parentCategoryId" /> must be positive, 0 or null.</exception>
		Task<IList<LegacyCategoryEntry>> GetCategoriesAsync(CancellationToken cancellationToken,
			int? parentCategoryId = null);

		/// <summary>
		/// Gets the category asynchronous. Returns single category with given category id, include subcategories. Disabled
		/// categories are not returned, but enabled subcategories of disabled categories are.
		/// </summary>
		/// <param name="categoryId">The category identifier.</param>
		/// <exception cref="EcwidLimitException">Limit overheat exception.</exception>
		/// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
		/// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="categoryId" /> must be greater than 0.</exception>
		Task<LegacyCategory> GetCategoryAsync(int categoryId);

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
		Task<LegacyCategory> GetCategoryAsync(int categoryId, CancellationToken cancellationToken);

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
		Task<IList<LegacyProductEntry>> GetProductsAsync(int? categoryId = null, bool hiddenProducts = false);

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
		Task<IList<LegacyProductEntry>> GetProductsAsync(CancellationToken cancellationToken, int? categoryId = null, bool hiddenProducts = false);

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
		Task<LegacyProduct> GetProductAsync(int productId, bool hiddenProducts = false);

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
		Task<LegacyProduct> GetProductAsync(int productId, CancellationToken cancellationToken, bool hiddenProducts = false);

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
		Task<bool> UpdateProductAsync(int productId, object updatedFields);

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
		Task<bool> UpdateProductAsync(int productId, object updatedFields, CancellationToken cancellationToken);
	}
}