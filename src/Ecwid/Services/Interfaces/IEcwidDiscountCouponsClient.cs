// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;

namespace Ecwid
{
    /// <summary>
    /// Shared public discount coupons client API.
    /// </summary>
    public interface IEcwidDiscountCouponsClient
    {
        /// <summary>
        /// Gets the discount coupon with the specified <paramref name="couponIdentifier"/>.
        /// </summary>
        /// <param name="couponIdentifier">The couponIdentifier of the coupon to retrieve</param>
        /// <exception cref="ArgumentNullException">Coupon Identifier is null</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<DiscountCouponInfo> GetDiscountCouponAsync(string couponIdentifier);

        /// <summary>
        /// Gets the discount coupon with the specified <paramref name="couponIdentifier"/>.
        /// </summary>
        /// <param name="couponIdentifier">The couponIdentifier of the coupon to retrieve</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <exception cref="ArgumentNullException">Coupon Identifier is null</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<DiscountCouponInfo> GetDiscountCouponAsync(string couponIdentifier, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the discount coupons asynchronously. If <paramref name="query" /> contains limit or offset parameters gets only one page.
        /// </summary>
        /// <param name="query">
        /// The query. It's a list of key-value pairs. e.g.
        /// <code>new {discount_type = "ABS_AND_SHIPPING", limit = 100}</code> or Dictionary{string, object}
        /// </param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<IEnumerable<DiscountCouponInfo>> GetDiscountCouponsAsync(object query);

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
        Task<IEnumerable<DiscountCouponInfo>>
            GetDiscountCouponsAsync(object query, CancellationToken cancellationToken);

        /// <summary>
        /// Create one discount coupon asynchronously.
        /// </summary>
        /// <param name="coupon">The discount coupon to create.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<DiscountCouponCreateStatus> CreateDiscountCouponAsync(DiscountCouponInfo coupon);

        /// <summary>
        /// Create one discount coupon asynchronously.
        /// </summary>
        /// <param name="coupon">The discount coupon to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<DiscountCouponCreateStatus> CreateDiscountCouponAsync(DiscountCouponInfo coupon,
            CancellationToken cancellationToken);

        /// <summary>
        /// Update one discount coupon asynchronously.
        /// </summary>
        /// <param name="coupon">The discount coupon to update.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<UpdateStatus> UpdateDiscountCouponAsync(DiscountCouponInfo coupon);

        /// <summary>
        /// Update one discount coupon asynchronously.
        /// </summary>
        /// <param name="coupon">The discount coupon to update.</param>
        /// <param name="cancellationToken">The cancellationToken</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<UpdateStatus> UpdateDiscountCouponAsync(DiscountCouponInfo coupon, CancellationToken cancellationToken);

        /// <summary>
        /// Delete one discount coupon asynchronously.
        /// </summary>
        /// <param name="coupon">The discount coupon to delete.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<DeleteStatus> DeleteDiscountCouponAsync(DiscountCouponInfo coupon);

        /// <summary>
        /// Delete one discount coupon asynchronously.
        /// </summary>
        /// <param name="couponIdentifier">The discount coupon code to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<DeleteStatus> DeleteDiscountCouponAsync(string couponIdentifier, CancellationToken cancellationToken);

        /// <summary>
        /// Delete one discount coupon asynchronously.
        /// </summary>
        /// <param name="couponIdentifier">The discount coupon code to delete.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<DeleteStatus> DeleteDiscountCouponAsync(string couponIdentifier);

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<bool> CheckDiscountCouponsTokenAsync();

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        Task<bool> CheckDiscountCouponsTokenAsync(CancellationToken cancellationToken);
    }
}