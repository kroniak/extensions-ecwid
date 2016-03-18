namespace Ecwid.Services.Legacy
{
    /// <summary>
    /// Settings to <see cref="EcwidLegacyClient"/> construnctor
    /// </summary>
    public class EcwidLegacyOptions : EcwidOptions
    {
        /// <summary>
        /// Gets or sets the API URL. Default is https://app.ecwid.com/api/v1/ 
        /// </summary>
        /// <value>
        /// The API URL
        /// </value>
        public override string ApiUrl { get; set; } = "https://app.ecwid.com/api/v1/";
        
        /// <summary>
        /// Gets or sets the maximum seconds to wait limit lock. From 1. Default is 600 sec. = 10 min. 
        /// </summary>
        /// <value>
        /// The maximum seconds to wait limit lock
        /// </value>
        public int MaxSecondsToWait { get; set; } = 600;

        /// <summary>
        /// Gets or sets the retry interval to ask for limit lock in sec. From 1. Default is 1 sec. 
        /// </summary>
        /// <value>
        /// The retry interval in sec.
        /// </value>
        public int RetryInterval { get; set; } = 1;

        /// <summary>
        /// Gets or sets the shop identifier
        /// </summary>
        /// <value>
        /// The shop identifier
        /// </value>
        /// 
        public int? ShopId { get; set; }

        /// <summary>
        /// Gets or sets the shop order authentication identifier
        /// </summary>
        /// <value>
        /// The shop order authentication identifier
        /// </value>
        public string ShopOrderAuthId { get; set; }

        /// <summary>
        /// Gets or sets the shop product authentication identifier
        /// </summary>
        /// <value>
        /// The shop product authentication identifier
        /// </value>
        public string ShopProductAuthId { get; set; }
    }
}
