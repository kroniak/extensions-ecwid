// ReSharper disable UnusedMemberInSuper.Global
namespace Ecwid.Services
{
    /// <summary>
    /// Setting for Ecwid Client.
    /// </summary>
    public class EcwidOptions
    {
        /// <summary>
        /// Gets or sets the API URL. Default is https://app.ecwid.com/api/v3/
        /// </summary>
        /// <value>
        /// The API URL
        /// </value>
        public virtual string ApiUrl { get; set; } = "https://app.ecwid.com/api/v3/";

        /// <summary>
        /// Gets or sets the shop identifier
        /// </summary>
        /// <value>
        /// The shop identifier
        /// </value>
        public int? ShopId { get; set; }
    }
}