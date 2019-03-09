// Licensed under the MIT License. See LICENSE in the git repository root for license information.

// ReSharper disable CheckNamespace

namespace Ecwid.Legacy
{
    /// <inheritdoc />
    public class EcwidLegacySettings : EcwidSettings
    {
        /// <inheritdoc />
        public override string ApiUrl { get; set; } = "https://app.ecwid.com/api/v1/";

        /// <summary>
        /// Gets or sets the maximum seconds to wait lock limit. From 1. Default is 600 sec. = 10 min.
        /// </summary>
        /// <value>
        /// The maximum seconds to wait lock limit.
        /// </value>
        public int MaxSecondsToWait { get; set; } = 600;

        /// <summary>
        /// Gets or sets the retry interval to ask for lock limit in sec. From 1. Default is 1 sec.
        /// </summary>
        /// <value>
        /// The retry interval in sec.
        /// </value>
        public int RetryInterval { get; set; } = 1;
    }
}