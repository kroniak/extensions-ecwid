// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ecwid.Models
{
    /// <summary>
    /// Notification settings.
    /// </summary>
    public class MailNotifications
    {
        /// <summary>
        /// Gets or sets the notification email address, which the store admin notifications are sent to.
        /// </summary>
        /// <value>
        /// The admin notification emails.
        /// </value>
        [JsonProperty("adminNotificationEmails")]
        public IList<string> AdminNotificationEmails { get; set; }

        /// <summary>
        /// Gets or sets the customer notification email address used as the 'reply-to’ field in the notifications to customers.
        /// </summary>
        /// <value>
        /// The customer notification from email.
        /// </value>
        [JsonProperty("customerNotificationFromEmail")]
        public string CustomerNotificationFromEmail { get; set; }
    }
}