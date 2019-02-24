// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models.Legacy
{
    /// <inheritdoc />
    public class LegacyOrder : LegacyBaseOrder
    {
        /// <summary>
        /// Gets or sets the transaction identifier returned by payment system (PayPal for example).
        /// </summary>
        /// <value>
        /// The external transaction identifier.
        /// </value>
        [JsonProperty("externalTransactionId")]
        public string ExternalTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the the date of order placement. Date format: "yyyy-mm-dd HH:MM:SS" (EST timezone).
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the shipping method name.
        /// </summary>
        /// <value>
        /// The shipping method.
        /// </value>
        [JsonProperty("shippingMethod")]
        public string ShippingMethod { get; set; }

        /// <summary>
        /// Gets or sets the shipping person.
        /// </summary>
        /// <value>
        /// The shipping person.
        /// </value>
        [JsonProperty("shippingPerson")]
        public LegacyPerson ShippingLegacyPerson { get; set; }

        /// <summary>
        /// Gets or sets the payment method name.
        /// </summary>
        /// <value>
        /// The payment method.
        /// </value>
        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the billing person.
        /// </summary>
        /// <value>
        /// The billing person.
        /// </value>
        [JsonProperty("billingPerson")]
        public LegacyPerson BillingLegacyPerson { get; set; }

        /// <summary>
        /// Gets or sets the additional payment parameters entered by customer on checkout.
        /// </summary>
        /// <value>
        /// The payment parameters.
        /// </value>
        [JsonProperty("paymentParameters")]
        public Dictionary<string, string> PaymentParameters { get; set; }

        /// <summary>
        /// Gets or sets the address verification status returned by some payment systems.
        /// </summary>
        /// <value>
        /// The avs message.
        /// </value>
        [JsonProperty("avsMessage")]
        public string AvsMessage { get; set; }

        /// <summary>
        /// Gets or sets the credit card verification status returned by some payment systems.
        /// </summary>
        /// <value>
        /// The CVV message.
        /// </value>
        [JsonProperty("cvvMessage")]
        public string CvvMessage { get; set; }

        /// <summary>
        /// Gets or sets the unique customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the customer email.
        /// </summary>
        /// <value>
        /// The customer email.
        /// </value>
        [JsonProperty("customerEmail")]
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Gets or sets the customer ip.
        /// </summary>
        /// <value>
        /// The customer ip.
        /// </value>
        [JsonProperty("customerIP")]
        public string CustomerIp { get; set; }

        /// <summary>
        /// Gets or sets the country code determined by customer IP address (if possible).
        /// </summary>
        /// <value>
        /// The customer country code by ip.
        /// </value>
        [JsonProperty("customerCountryCodeByIP")]
        public string CustomerCountryCodeByIp { get; set; }

        /// <summary>
        /// Gets or sets the discount coupon code ('AX-545DF-DD' for example).
        /// </summary>
        /// <value>
        /// The discount coupon.
        /// </value>
        [JsonProperty("discountCoupon")]
        public string DiscountCoupon { get; set; }

        /// <summary>
        /// Gets or sets the subtotal (products) cost.
        /// </summary>
        /// <value>
        /// The subtotal cost.
        /// </value>
        [JsonProperty("subtotalCost")]
        public double SubtotalCost { get; set; }

        /// <summary>
        /// Gets or sets the discount cost.
        /// </summary>
        /// <value>
        /// The discount cost.
        /// </value>
        [JsonProperty("discountCost")]
        public double DiscountCost { get; set; }

        /// <summary>
        /// Gets or sets the shipping cost.
        /// </summary>
        /// <value>
        /// The shipping cost.
        /// </value>
        [JsonProperty("shippingCost")]
        public double ShippingCost { get; set; }

        /// <summary>
        /// Gets or sets the tax cost.
        /// </summary>
        /// <value>
        /// The tax cost.
        /// </value>
        [JsonProperty("taxCost")]
        public double TaxCost { get; set; }

        /// <summary>
        /// Total order cost.
        /// </summary>
        /// <value>
        /// The total cost.
        /// </value>
        [JsonProperty("totalCost")]
        public double TotalCost { get; set; }

        /// <summary>
        /// Gets or sets the affiliate identifier.
        /// </summary>
        /// <value>
        /// The affiliate identifier.
        /// </value>
        [JsonProperty("affiliateId")]
        public string AffiliateId { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty("items")]
        public List<LegacyOrderItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the last change date.
        /// </summary>
        /// <value>
        /// The last change date.
        /// </value>
        [JsonProperty("lastChangeDate")]
        public DateTime LastChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>
        /// The updated.
        /// </value>
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Gets or sets the order comments.
        /// </summary>
        /// <value>
        /// The order comments.
        /// </value>
        [JsonProperty("orderComments")]
        public string OrderComments { get; set; }

        /// <summary>
        /// Gets or sets the discounts.
        /// </summary>
        /// <value>
        /// The discounts
        /// </value>
        [JsonProperty("discounts")]
        public List<LegacyOrderDiscount> Discounts { get; set; }
    }
}