﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Forecom.Extensions.Ecwid.Domain.Legacy
{
    /// <summary>
    /// This object represents information on a whole order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// A unique order number
        /// </summary>
        /// <value>
        /// The number
        /// </value>
        [JsonProperty("number")]
        public int Number { get; set; }

        /// <summary>
        /// Admin-defined order numbers with prefix and suffix, e.g. 2011-345-q4
        /// </summary>
        /// <value>
        /// The vendor number
        /// </value>
        [JsonProperty("vendorNumber")]
        public string VendorNumber { get; set; }

        /// <summary>
        /// Transaction identifier returned by payment system (PayPal for example)
        /// </summary>
        /// <value>
        /// The external transaction identifier
        /// </value>
        [JsonProperty("externalTransactionId")]
        public string ExternalTransactionId { get; set; }

        /// <summary>
        /// The date of order placement. Date format: "yyyy-mm-dd HH:MM:SS" (EST timezone)
        /// </summary>
        /// <value>
        /// The created
        /// </value>
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Payment status. Сontains one of these values: ACCEPTED*, DECLINED, CANCELLED, QUEUED, CHARGEABLE, REFUNDED
        /// </summary>
        /// <value>
        /// The payment status
        /// </value>
        [JsonProperty("paymentStatus")]
        public string PaymentStatus { get; set; }

        /// <summary>
        /// Fulfillment status. Contains one of these values: NEW, PROCESSING, SHIPPED, DELIVERED, WILL_NOT_DELIVER, RETURNED
        /// </summary>
        /// <value>
        /// The fulfillment status
        /// </value>
        [JsonProperty("fulfillmentStatus")]
        public string FulfillmentStatus { get; set; }

        /// <summary>
        /// Shipping method name
        /// </summary>
        /// <value>
        /// The shipping method
        /// </value>
        [JsonProperty("shippingMethod")]
        public string ShippingMethod { get; set; }

        /// <summary>
        /// Gets or sets the shipping person
        /// </summary>
        /// <value>
        /// The shipping person
        /// </value>
        [JsonProperty("shippingPerson")]
        public Person ShippingPerson { get; set; }

        /// <summary>
        /// Gets or sets the payment method name
        /// </summary>
        /// <value>
        /// The payment method
        /// </value>
        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the billing person
        /// </summary>
        /// <value>
        /// The billing person
        /// </value>
        [JsonProperty("billingPerson")]
        public Person BillingPerson { get; set; }

        /// <summary>
        /// Additional payment parameters entered by customer on checkout
        /// </summary>
        /// <value>
        /// The payment parameters
        /// </value>
        [JsonProperty("paymentParameters")]
        public PaymentParameters PaymentParameters { get; set; }

        /// <summary>
        /// Address verification status returned by some payment systems
        /// </summary>
        /// <value>
        /// The avs message
        /// </value>
        [JsonProperty("avsMessage")]
        public string AvsMessage { get; set; }

        /// <summary>
        /// Credit card verification status returned by some payment systems
        /// </summary>
        /// <value>
        /// The CVV message
        /// </value>
        [JsonProperty("cvvMessage")]
        public string CvvMessage { get; set; }

        /// <summary>
        /// Unique customer identificator
        /// </summary>
        /// <value>
        /// The customer identifier
        /// </value>
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer
        /// </summary>
        /// <value>
        /// The name of the customer
        /// </value>
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the customer email
        /// </summary>
        /// <value>
        /// The customer email
        /// </value>
        [JsonProperty("customerEmail")]
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Gets or sets the customer ip
        /// </summary>
        /// <value>
        /// The customer ip
        /// </value>
        [JsonProperty("customerIP")]
        public string CustomerIP { get; set; }

        /// <summary>
        /// Country code determined by customer IP address (if possible)
        /// </summary>
        /// <value>
        /// The customer country code by ip
        /// </value>
        [JsonProperty("customerCountryCodeByIP")]
        public string CustomerCountryCodeByIP { get; set; }

        /// <summary>
        /// Discount coupon code ('AX-545DF-DD' for example)
        /// </summary>
        /// <value>
        /// The discount coupon
        /// </value>
        [JsonProperty("discountCoupon")]
        public string DiscountCoupon { get; set; }

        /// <summary>
        /// Subtotal (products) cost
        /// </summary>
        /// <value>
        /// The subtotal cost
        /// </value>
        [JsonProperty("subtotalCost")]
        public double SubtotalCost { get; set; }

        /// <summary>
        /// Gets or sets the discount cost
        /// </summary>
        /// <value>
        /// The discount cost
        /// </value>
        [JsonProperty("discountCost")]
        public double DiscountCost { get; set; }

        /// <summary>
        /// Gets or sets the shipping cost
        /// </summary>
        /// <value>
        /// The shipping cost
        /// </value>
        [JsonProperty("shippingCost")]
        public double ShippingCost { get; set; }

        /// <summary>
        /// Gets or sets the tax cost
        /// </summary>
        /// <value>
        /// The tax cost
        /// </value>
        [JsonProperty("taxCost")]
        public double TaxCost { get; set; }

        /// <summary>
        /// Total order cost
        /// </summary>
        /// <value>
        /// The total cost
        /// </value>
        [JsonProperty("totalCost")]
        public double TotalCost { get; set; }

        /// <summary>
        /// Gets or sets the affiliate identifier
        /// </summary>
        /// <value>
        /// The affiliate identifier
        /// </value>
        [JsonProperty("affiliateId")]
        public string AffiliateId { get; set; }

        /// <summary>
        /// Gets or sets the items
        /// </summary>
        /// <value>
        /// The items
        /// </value>
        [JsonProperty("items")]
        public IList<OrderItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the last change date
        /// </summary>
        /// <value>
        /// The last change date
        /// </value>
        [JsonProperty("lastChangeDate")]
        public DateTime LastChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the updated
        /// </summary>
        /// <value>
        /// The updated
        /// </value>
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Shipping tracking code
        /// </summary>
        /// <value>
        /// The shipping tracking code
        /// </value>
        [JsonProperty("shippingTrackingCode")]
        public string ShippingTrackingCode { get; set; }

        /// <summary>
        /// Gets or sets the order comments
        /// </summary>
        /// <value>
        /// The order comments
        /// </value>
        [JsonProperty("orderComments")]
        public string OrderComments { get; set; }

        /// <summary>
        /// Gets or sets the discounts
        /// </summary>
        /// <value>
        /// The discounts
        /// </value>
        [JsonProperty("discounts")]
        public List<OrderDiscount> Discounts { get; set; }
    }

    /// <summary>
    /// Order discounts
    /// </summary>
    public class OrderDiscount
    {
        /// <summary>
        /// Specifies the type of discount
        /// </summary>
        /// <value>
        /// The discount base
        /// </value>
        [JsonProperty("discountBase")]
        public string DiscountBase { get; set; }

        /// <summary>
        /// Discount in currency granted to the customer based on the volume ordered
        /// </summary>
        /// <value>
        /// The discount cost
        /// </value>
        [JsonProperty("discountCost")]
        public double DiscountCost { get; set; }

        /// <summary>
        /// Gets or sets the settings
        /// </summary>
        /// <value>
        /// The settings
        /// </value>
        [JsonProperty("settings")]
        public Settings Settings { get; set; }
    }

    public class Settings
    {
        /// <summary>
        /// Discount granted to the customer based on the volume ordered either in percents or in currency, based on the <seealso cref="DiscountType"/>
        /// <value>
        /// The value
        /// </value>
        [JsonProperty("value")]
        public double Value { get; set; }

        /// <summary>
        /// Specifies the type of discount 'ABS' or 'PERCENT'
        /// </summary>
        /// <value>
        /// The type of the discount. 'ABS' or 'PERCENT'
        /// </value>
        [JsonProperty("discountType")]
        public string DiscountType { get; set; }

        /// <summary>
        /// Gets or sets the name of the coupon
        /// </summary>
        /// <value>
        /// The name of the coupon
        /// </value>
        [JsonProperty("couponName")]
        public string CouponName { get; set; }
    }
}
