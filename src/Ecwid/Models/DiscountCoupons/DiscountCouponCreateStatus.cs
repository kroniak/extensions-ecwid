namespace Ecwid
{
    /// <summary>
    ///     The object returned from the Discount Coupon API when creating new Discount Coupons
    /// </summary>
    public class DiscountCouponCreateStatus
    {
        /// <summary>
        /// Internal unique coupon Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Code of the created coupon
        /// </summary>
        public string Code { get; set; }
    }
}