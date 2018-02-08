using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;
using Flurl;
using Flurl.Util;

namespace Ecwid
{
    public partial class EcwidClient
    {
        private const string DiscountCouponsUrl = "discount_coupons";

        #region Implementation of IEcwidDiscountCouponsClient

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<bool> CheckDiscountCouponsTokenAsync()
            => await CheckDiscountCouponsTokenAsync(CancellationToken.None);

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<bool> CheckDiscountCouponsTokenAsync(CancellationToken cancellationToken)
            => await CheckTokenAsync<DiscountCouponSearchResults>(GetUrl(DiscountCouponsUrl), cancellationToken);
        
        /// <summary>
        /// Gets the discount coupon with the specified <paramref name="couponIdentifier"/>.
        /// </summary>
        /// <param name="couponIdentifier">The couponIdentifier of the coupon to retrieve</param>
        /// <exception cref="ArgumentNullException">Coupon Identifier is null</exception>
        public async Task<DiscountCouponInfo> GetDiscountCouponAsync(string couponIdentifier) =>
            await GetDiscountCouponAsync(couponIdentifier, CancellationToken.None);

        /// <summary>
        /// Gets the discount coupon with the specified <paramref name="couponIdentifier"/>.
        /// </summary>
        /// <param name="couponIdentifier">The couponIdentifier of the coupon to retrieve</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <exception cref="ArgumentNullException">Coupon Identifier is null</exception>
        public async Task<DiscountCouponInfo> GetDiscountCouponAsync(string couponIdentifier, CancellationToken cancellationToken)
        {
            if (couponIdentifier == null)
                throw new ArgumentNullException(nameof(couponIdentifier));

            // ReSharper disable once RedundantAnonymousTypePropertyName
            var discountCoupons = await GetDiscountCouponsAsync(new { couponIdentifier = couponIdentifier }, cancellationToken);

            return discountCoupons.Count == 0
                       ? null
                       : discountCoupons.FirstOrDefault();
        }

        /// <summary>
        /// Gets the discount coupons asynchronously. If <paramref name="query" /> contains limit or offset parameters gets only one page.
        /// </summary>
        /// <param name="query">
        /// The query. It's a list of key-value pairs. e.g.
        /// <code>new {discount_type = "ABS_AND_SHIPPING", limit = 100}</code> or Dictionary{string, object}
        /// </param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<DiscountCouponInfo>> GetDiscountCouponsAsync(object query) =>
            await GetDiscountCouponsAsync(query, CancellationToken.None);

        /// <summary>
        /// Gets the discount coupons asynchronously. If <paramref name="query" /> contains limit or offset parameters gets only one page.
        /// </summary>
        /// <param name="query">
        /// The query. It's a list of key-value pairs. e.g.
        /// <code>new {discount_type = "ABS_AND_SHIPPING", limit = 100}</code> or Dictionary{string, object}
        /// </param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        public async Task<List<DiscountCouponInfo>> GetDiscountCouponsAsync(object query, CancellationToken cancellationToken)
        {
            var response = await GetApiAsync<DiscountCouponSearchResults>(GetUrl(DiscountCouponsUrl), query, cancellationToken);

            var result = response.DiscountCoupons?.ToList() ?? new List<DiscountCouponInfo>();

            if (result.Count == 0 ||
                response.Total == response.Count)
                return result;

            if (query?.ToKeyValuePairs().Count(pair => pair.Key == "limit" || pair.Key == "offset") > 0)
                return result;

            while (response.Count == response.Limit)
            {
                response =
                    await
                        GetApiAsync<DiscountCouponSearchResults>(
                            GetUrl(DiscountCouponsUrl).SetQueryParams(new { offset = response.Offset + response.Limit }),
                            query,
                            cancellationToken);

                // ReSharper disable once ExceptionNotDocumentedOptional
                if (response.DiscountCoupons != null)
                    result.AddRange(response.DiscountCoupons);
            }

            return result;
        }

        /// <summary>
        /// Create one discount coupon asynchronously.
        /// </summary>
        /// <param name="coupon">The discount coupon to create.</param>
        public async Task<DiscountCouponCreateStatus> CreateDiscountCouponAsync(DiscountCouponInfo coupon)
            => await CreateDiscountCouponAsync(coupon, CancellationToken.None);

        /// <summary>
        /// Create one discount coupon asynchronously.
        /// </summary>
        /// <param name="coupon">The discount coupon to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<DiscountCouponCreateStatus> CreateDiscountCouponAsync(DiscountCouponInfo coupon, CancellationToken cancellationToken)
        {
            return await PostJsonApiAsync<DiscountCouponCreateStatus>(GetUrl(DiscountCouponsUrl), coupon, cancellationToken);
        }

        /// <summary>
        /// Update one discount coupon asynchronously.
        /// </summary>
        /// <param name="coupon">The discount coupon to update.</param>
        public async Task<UpdateStatus> UpdateDiscountCouponAsync(DiscountCouponInfo coupon)
            => await UpdateDiscountCouponAsync(coupon, CancellationToken.None);

        /// <summary>
        /// Update one discount coupon asynchronously.
        /// </summary>
        /// <param name="coupon">The discount coupon to update.</param>
        /// <param name="cancellationToken">The cancellationToken</param>
        public async Task<UpdateStatus> UpdateDiscountCouponAsync(DiscountCouponInfo coupon, CancellationToken cancellationToken)
        {
            if (coupon == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(coupon.Code))
            {
                throw new ArgumentException("Coupon code must have a value", nameof(coupon.Code));
            }

            return await PutApiAsync<UpdateStatus>(GetUrl($"{DiscountCouponsUrl}/{coupon.Code}"), coupon, cancellationToken);
        }

        /// <summary>
        /// Delete one discount coupon asynchronously.
        /// </summary>
        /// <param name="coupon">The discount coupon to delete.</param>
        public async Task<DeleteStatus> DeleteDiscountCouponAsync(DiscountCouponInfo coupon)
            => await DeleteDiscountCouponAsync(coupon.Code);

        /// <summary>
        /// Delete one discount coupon asynchronously.
        /// </summary>
        /// <param name="couponIdentifier">The discount coupon code to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<DeleteStatus> DeleteDiscountCouponAsync(string couponIdentifier, CancellationToken cancellationToken)
        {
            return await DeleteApiAsync<DeleteStatus>(GetUrl($"{DiscountCouponsUrl}/{couponIdentifier}"), cancellationToken);
        }

        /// <summary>
        /// Delete one discount coupon asynchronously.
        /// </summary>
        /// <param name="couponIdentifier">The discount coupon code to delete.</param>
        public async Task<DeleteStatus> DeleteDiscountCouponAsync(string couponIdentifier)
            => await DeleteDiscountCouponAsync(couponIdentifier, CancellationToken.None);

        #endregion
    }
}