// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// This object represents file attached by customer to purchased product on checkout.
    /// </summary>
    public class OrderItemProductFile
    {
        /// <summary>
        /// Gets or sets the product file identifier.
        /// </summary>
        /// <value>
        /// The product file identifier.
        /// </value>
        [JsonProperty("productFileId")]
        public int ProductFileId { get; set; }

        /// <summary>
        /// Gets or sets the max allowed number of file downloads. See E-goods article in Ecwid Help center for the details.
        /// </summary>
        /// <value>
        /// The maximum downloads.
        /// </value>
        [JsonProperty("maxDownloads")]
        public int MaxDownloads { get; set; }

        /// <summary>
        /// Gets or sets the remaining number of download attempts.
        /// </summary>
        /// <value>
        /// The remaining downloads.
        /// </value>
        [JsonProperty("remainingDownloads")]
        public int RemainingDownloads { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date/time of the customer download link expiration.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("expire")]
        public string Expire { get; set; }

        /// <summary>
        /// Gets or sets the file description defined by the store administrator.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the file size, bytes (64-bit integer).
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        [JsonProperty("size")]
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the link to the file. Be careful: the link contains the API access token.
        /// Make sure you do not display the link as is in your application and not give it to a customer.
        /// </summary>
        /// <value>
        /// The admin URL.
        /// </value>
        [JsonProperty("adminUrl")]
        public string AdminUrl { get; set; }

        /// <summary>
        /// Gets or sets the file download link that is sent to the customer when the order is paid.
        /// </summary>
        /// <value>
        /// The customer URL.
        /// </value>
        [JsonProperty("customerUrl")]
        public string CustomerUrl { get; set; }
    }
}