// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// This object represents order item.
    /// </summary>
    public class OrderItem : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the category this product belongs to. If the product belongs to many categories, categoryID will
        /// return the ID
        /// of the default product category. If the product doesn’t belong to any category, 0 is returned.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the price of ordered item in the cart.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty("price")]
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets the basic product price without options markups, wholesale discounts etc..
        /// </summary>
        /// <value>
        /// The product price.
        /// </value>
        [JsonProperty("productPrice")]
        public int ProductPrice { get; set; }

        /// <summary>
        /// Gets or sets the product SKU. If the chosen options match a combination, this will be a combination SKU..
        /// </summary>
        /// <value>
        /// The sku.
        /// </value>
        [JsonProperty("sku")]
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the product description truncated to 120 characters.
        /// </summary>
        /// <value>
        /// The short description.
        /// </value>
        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the tax amount applied to the item.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        [JsonProperty("tax")]
        public int Tax { get; set; }

        /// <summary>
        /// Gets or sets the taxes applied to this order item.
        /// </summary>
        /// <value>
        /// The taxes.
        /// </value>
        [JsonProperty("taxes")]
        public IList<OrderItemTax> Taxes { get; set; }

        /// <summary>
        /// Gets or sets the order item shipping cost.
        /// </summary>
        /// <value>
        /// The shipping.
        /// </value>
        [JsonProperty("shipping")]
        public double Shipping { get; set; }

        /// <summary>
        /// Gets or sets the number of products in stock in the store.
        /// </summary>
        /// <value>
        /// The quantity in stock.
        /// </value>
        [JsonProperty("quantityInStock")]
        public int QuantityInStock { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is shipping required.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is shipping required; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("isShippingRequired")]
        public bool IsShippingRequired { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        [JsonProperty("weight")]
        public int Weight { get; set; }

        /// <summary>
        /// Shows whether the store admin set to track the quantity of this product and get low stock notifications
        /// </summary>
        /// <value>
        /// <c>true</c> if [track quantity]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("trackQuantity")]
        public bool TrackQuantity { get; set; }

        /// <summary>
        /// Shows whether the fixed shipping rate is set for the product.
        /// </summary>
        /// <value>
        /// <c>true</c> if [fixed shipping rate only]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("fixedShippingRateOnly")]
        public bool FixedShippingRateOnly { get; set; }

        /// <summary>
        /// Gets or sets the product image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the small thumbnail URL.
        /// </summary>
        /// <value>
        /// The small thumbnail URL.
        /// </value>
        [JsonProperty("smallThumbnailUrl")]
        public string SmallThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets the fixed shipping rate for the product.
        /// </summary>
        /// <value>
        /// The fixed shipping rate.
        /// </value>
        [JsonProperty("fixedShippingRate")]
        public int FixedShippingRate { get; set; }

        /// <summary>
        /// Shows whether the item has downloadable files attached.
        /// </summary>
        /// <value>
        /// <c>true</c> if digital; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("digital")]
        public bool Digital { get; set; }

        /// <summary>
        /// Shows whether the product is available in the store.
        /// </summary>
        /// <value>
        /// <c>true</c> if [product available]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("productAvailable")]
        public bool ProductAvailable { get; set; }

        /// <summary>
        /// Shows whether a discount coupon is applied for this item.
        /// </summary>
        /// <value>
        /// <c>true</c> if [coupon applied]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("couponApplied")]
        public bool CouponApplied { get; set; }

        /// <summary>
        /// Gets or sets the product options values selected by the customer.
        /// </summary>
        /// <value>
        /// The selected options.
        /// </value>
        [JsonProperty("selectedOptions")]
        public IList<OrderItemOption> SelectedOptions { get; set; }

        /// <summary>
        /// Gets or sets the files attached to the order item.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        [JsonProperty("files")]
        public IList<OrderItemProductFile> Files { get; set; }
    }
}