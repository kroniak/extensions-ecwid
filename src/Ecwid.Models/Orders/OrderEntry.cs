using System.Collections.Generic;
using Newtonsoft.Json;
using System;
// ReSharper disable ClassNeverInstantiated.Global

namespace Ecwid.Models
{
    /// <summary>
    /// This object represents information on a whole order
    /// </summary>
    public class OrderEntry : BaseOrder
    {
        /// <summary>
        /// Gets or sets the order number with prefix and suffix defined by admin, e.g. ABC34-q.
        /// </summary>
        /// <value>
        /// The vendor order number
        /// </value>
        [JsonProperty("vendorOrderNumber")]
        public string VendorOrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the subtotal
        /// </summary>
        /// <value>
        /// The subtotal
        /// </value>
        [JsonProperty("subtotal")]
        public int Subtotal { get; set; }

        /// <summary>
        /// Gets or sets the total cost.
        /// </summary>
        /// <value>
        /// The total
        /// </value>
        [JsonProperty("total")]
        public double Total { get; set; }

        /// <summary>
        /// Gets or sets the customer email address.
        /// </summary>
        /// <value>
        /// The email
        /// </value>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the transaction ID / invoice number of the order in the payment system (e.g. PayPal transaction ID).
        /// </summary>
        /// <value>
        /// The external transaction identifier.
        /// </value>
        [JsonProperty("externalTransactionId")]
        public string ExternalTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the payment method.
        /// </summary>
        /// <value>
        /// The payment method
        /// </value>
        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the payment processor name.
        /// </summary>
        /// <value>
        /// The payment method.
        /// </value>
        [JsonProperty("paymentModule")]
        public string PaymentModule { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax
        /// </value>
        [JsonProperty("tax")]
        public int Tax { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>
        /// The ip address
        /// </value>
        [JsonProperty("ipAddress")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to order using a coupon.
        /// </summary>
        /// <value>
        /// The coupon discount
        /// </value>
        [JsonProperty("couponDiscount")]
        public int CouponDiscount { get; set; }

        /// <summary>
        /// Gets or sets the credit card status.
        /// </summary>
        /// <value>
        /// The credit card status.
        /// </value>
        [JsonProperty("creditCardStatus")]
        public CreditCardStatus CreditCardStatus { get; set; }

        /// <summary>
        /// Gets or sets the shipping tracking code.
        /// </summary>
        /// <value>
        /// The tracking number.
        /// </value>
        [JsonProperty("trackingNumber")]
        public string TrackingNumber { get; set; }

        /// <summary>
        /// Gets or sets the payment status. Supported values: AWAITING_PAYMENT, PAID, CANCELLED, REFUNDED, INCOMPLETE.
        /// </summary>
        /// <value>
        /// The payment status. Supported values: AWAITING_PAYMENT, PAID, CANCELLED, REFUNDED, INCOMPLETE.
        /// </value>
        [JsonProperty("paymentStatus")]
        public string PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the payment message.
        /// </summary>
        /// <value>
        /// The payment message.
        /// </value>
        [JsonProperty("paymentMessage")]
        public string PaymentMessage { get; set; }

        /// <summary>
        /// Gets or sets the fulfilment status. Supported values: AWAITING_PROCESSING, PROCESSING, SHIPPED, DELIVERED, WILL_NOT_DELIVER, RETURNED.
        /// </summary>
        /// <value>
        /// The fulfillment status. Supported values: AWAITING_PROCESSING, PROCESSING, SHIPPED, DELIVERED, WILL_NOT_DELIVER, RETURNED.
        /// </value>
        [JsonProperty("fulfillmentStatus")]
        public string FulfillmentStatus { get; set; }

        /// <summary>
        /// Gets or sets the unique order number without prefixes/suffixes, e.g. 34.
        /// </summary>
        /// <value>
        /// The order number
        /// </value>
        [JsonProperty("orderNumber")]
        public int OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the order comments.
        /// </summary>
        /// <value>
        /// The order comments
        /// </value>
        [JsonProperty("orderComments")]
        public int OrderComments { get; set; }

        /// <summary>
        /// Gets or sets the URL of the page when order was placed (without hash (#) part).
        /// </summary>
        /// <value>
        /// The referer URL
        /// </value>
        [JsonProperty("refererUrl")]
        public string RefererUrl { get; set; }

        /// <summary>
        /// Gets or sets the sum of discounts based on subtotal. Is included into the discount field.
        /// </summary>
        /// <value>
        /// The volume discount
        /// </value>
        [JsonProperty("volumeDiscount")]
        public int VolumeDiscount { get; set; }

        /// <summary>
        /// Gets or sets the sum of discounts based on customer group. Is included into the discount field.
        /// </summary>
        /// <value>
        /// The membership based discount.
        /// </value>
        [JsonProperty("membershipBasedDiscount")]
        public int MembershipBasedDiscount { get; set; }

        /// <summary>
        /// Gets or sets the sum of discount based on subtotal AND customer group. Is included into the discount field.
        /// </summary>
        /// <value>
        /// The total and membership based discount.
        /// </value>
        [JsonProperty("totalAndMembershipBasedDiscount")]
        public int TotalAndMembershipBasedDiscount { get; set; }

        /// <summary>
        /// Gets or sets the sum of all applied discounts except for the coupon discount. 
        /// To get the total order discount, take the sum of couponDiscount and discount field values.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        [JsonProperty("discount")]
        public int Discount { get; set; }

        /// <summary>
        /// Gets or sets the information about applied discounts (coupons are not included).
        /// </summary>
        /// <value>
        /// The discount information.
        /// </value>
        [JsonProperty("discountInfo")]
        public IList<DiscountInfo> DiscountInfo { get; set; }

        /// <summary>
        /// Gets or sets the information about applied coupon.
        /// </summary>
        /// <value>
        /// The discount coupon.
        /// </value>
        [JsonProperty("discountCoupon")]
        public DiscountCouponInfo DiscountCouponInfo { get; set; }

        /// <summary>
        /// Gets or sets the order total in USD.
        /// </summary>
        /// <value>
        /// The usd total.
        /// </value>
        [JsonProperty("usdTotal")]
        public double UsdTotal { get; set; }

        /// <summary>
        /// Gets or sets the URL that the customer came to the store from.
        /// </summary>
        /// <value>
        /// The global referer.
        /// </value>
        [JsonProperty("globalReferer")]
        public string GlobalReferer { get; set; }

        /// <summary>
        /// Gets or sets the date/time of order placement, e.g 2014-06-06 18:57:19 +0000.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the date/time of the last order change, e.g 2014-06-06 18:57:19 +0000.
        /// </summary>
        /// <value>
        /// The update date.
        /// </value>
        [JsonProperty("updateDate")]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the date of order placement in UNIX Timestamp format, e.g 1427268654.
        /// </summary>
        /// <value>
        /// The create timestamp.
        /// </value>
        [JsonProperty("createTimestamp")]
        public int CreateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the date of the last order change in UNIX Timestamp format, e.g 1427268654.
        /// </summary>
        /// <value>
        /// The update timestamp.
        /// </value>
        [JsonProperty("updateTimestamp")]
        public int UpdateTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty("items")]
        public IList<OrderItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the name and address of the person entered in shipping information.
        /// </summary>
        /// <value>
        /// The shipping person.
        /// </value>
        [JsonProperty("shippingPerson")]
        public PersonInfo ShippingPerson { get; set; }

        /// <summary>
        /// Gets or sets the information about selected shipping option.
        /// </summary>
        /// <value>
        /// The shipping option.
        /// </value>
        [JsonProperty("shippingOption")]
        public ShippingOptionInfo ShippingOptionInfo { get; set; }

        /// <summary>
        /// Gets or sets the handling fee details.
        /// </summary>
        /// <value>
        /// The handling fee.
        /// </value>
        [JsonProperty("handlingFee")]
        public HandlingFeeInfo HandlingFeeInfo { get; set; }

        /// <summary>
        /// Gets or sets the additional order information if any (reserved for future use).
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        [JsonProperty("additionalInfo")]
        public Dictionary<string, string> AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the affiliate identifier.
        /// </summary>
        /// <value>
        /// The affiliate identifier.
        /// </value>
        [JsonProperty("affiliateId")]
        public string AffiliateId { get; set; }

        /// <summary>
        /// Gets or sets the name and billing address of the customer.
        /// </summary>
        /// <value>
        /// The billing person.
        /// </value>
        [JsonProperty("billingPerson")]
        public PersonInfo BillingPerson { get; set; }

        /// <summary>
        /// Gets or sets the additional payment parameters entered by customer on checkout, e.g. PO number in “Purchase order” payments.
        /// </summary>
        /// <value>
        /// The payment parameters.
        /// </value>
        [JsonProperty("paymentParams")]
        public Dictionary<string, string> PaymentParams { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OrderEntry"/> is hidden (removed from the list). Applies to unfinished orders only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if hidden; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        /// <summary>
        /// Gets or sets the unique customer internal ID (if the order is placed by a registered user).
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        [JsonProperty("customerId")]
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer group identifier.
        /// </summary>
        /// <value>
        /// The customer group identifier.
        /// </value>
        [JsonProperty("customerGroupId")]
        public int CustomerGroupId { get; set; }

        /// <summary>
        /// Gets or sets the name of group (membership) the customer belongs to.
        /// </summary>
        /// <value>
        /// The customer group.
        /// </value>
        [JsonProperty("customerGroup")]
        public string CustomerGroup { get; set; }
    }
}