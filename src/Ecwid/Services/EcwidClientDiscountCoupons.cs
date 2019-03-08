using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;
using Flurl;
using Flurl.Util;
// ReSharper disable PossibleMultipleEnumeration

namespace Ecwid
{
    public partial class EcwidClient
    {
        private const string DiscountCouponsUrl = "discount_coupons";

        #region Implementation of IEcwidDiscountCouponsClient

        /// <inheritdoc />
        public async Task<bool> CheckDiscountCouponsTokenAsync()
            => await CheckDiscountCouponsTokenAsync(CancellationToken.None);

        /// <inheritdoc />
        public async Task<bool> CheckDiscountCouponsTokenAsync(CancellationToken cancellationToken)
            => await CheckTokenAsync<DiscountCouponSearchResults>(GetUrl(DiscountCouponsUrl), cancellationToken);

        /// <inheritdoc />
        public async Task<DiscountCouponInfo> GetDiscountCouponAsync(string couponIdentifier) =>
            await GetDiscountCouponAsync(couponIdentifier, CancellationToken.None);

        /// <inheritdoc />
        public async Task<DiscountCouponInfo> GetDiscountCouponAsync(string couponIdentifier,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(couponIdentifier))
                throw new ArgumentNullException(nameof(couponIdentifier));

            // ReSharper disable once RedundantAnonymousTypePropertyName
            var discountCoupons =
                await GetDiscountCouponsAsync(new {couponIdentifier = couponIdentifier}, cancellationToken);

            return discountCoupons.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DiscountCouponInfo>> GetDiscountCouponsAsync(object query) =>
            await GetDiscountCouponsAsync(query, CancellationToken.None);

        /// <inheritdoc />
        public async Task<IEnumerable<DiscountCouponInfo>> GetDiscountCouponsAsync(object query,
            CancellationToken cancellationToken)
        {
            var response =
                await GetApiAsync<DiscountCouponSearchResults>(GetUrl(DiscountCouponsUrl), query, cancellationToken);

            var result = response.DiscountCoupons ?? Enumerable.Empty<DiscountCouponInfo>();

            if (result.FirstOrDefault() == null)
            {
                return result;
            }

            if (response.Total == response.Count)
            {
                return result;
            }

            if (query?.ToKeyValuePairs().Count(pair => pair.Key == "limit" || pair.Key == "offset") > 0)
            {
                return result;
            }

            while (response.Count == response.Limit)
            {
                response =
                    await
                        GetApiAsync<DiscountCouponSearchResults>(
                            GetUrl(DiscountCouponsUrl).SetQueryParams(new {offset = response.Offset + response.Limit}),
                            query,
                            cancellationToken);

                // ReSharper disable once ExceptionNotDocumentedOptional
                if (response.DiscountCoupons != null)
                {
                    result = result.Concat(response.DiscountCoupons);
                }
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<DiscountCouponCreateStatus> CreateDiscountCouponAsync(DiscountCouponInfo coupon)
            => await CreateDiscountCouponAsync(coupon, CancellationToken.None);

        /// <inheritdoc />
        public async Task<DiscountCouponCreateStatus> CreateDiscountCouponAsync(DiscountCouponInfo coupon,
            CancellationToken cancellationToken)
        {
            return await PostJsonApiAsync<DiscountCouponCreateStatus>(GetUrl(DiscountCouponsUrl), coupon,
                cancellationToken);
        }

        /// <inheritdoc />
        public async Task<UpdateStatus> UpdateDiscountCouponAsync(DiscountCouponInfo coupon)
            => await UpdateDiscountCouponAsync(coupon, CancellationToken.None);

        /// <inheritdoc />
        public async Task<UpdateStatus> UpdateDiscountCouponAsync(DiscountCouponInfo coupon,
            CancellationToken cancellationToken)
        {
            if (coupon == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(coupon.Code))
            {
                throw new ArgumentException("Coupon code must have a value", nameof(coupon.Code));
            }

            return await PutApiAsync<UpdateStatus>(GetUrl($"{DiscountCouponsUrl}/{coupon.Code}"), coupon,
                cancellationToken);
        }

        /// <inheritdoc />
        public async Task<DeleteStatus> DeleteDiscountCouponAsync(DiscountCouponInfo coupon)
            => await DeleteDiscountCouponAsync(coupon.Code);

        /// <inheritdoc />
        public async Task<DeleteStatus> DeleteDiscountCouponAsync(string couponIdentifier,
            CancellationToken cancellationToken)
        {
            return await DeleteApiAsync<DeleteStatus>(GetUrl($"{DiscountCouponsUrl}/{couponIdentifier}"),
                cancellationToken);
        }

        /// <inheritdoc />
        public async Task<DeleteStatus> DeleteDiscountCouponAsync(string couponIdentifier)
            => await DeleteDiscountCouponAsync(couponIdentifier, CancellationToken.None);

        #endregion
    }
}