// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

namespace Ecwid.Services
{
    /// <summary>
    /// Base shop credentials class.
    /// </summary>
    public abstract class BaseCredentials
    {
        private int _shopId;

        /// <summary>
        /// Gets or sets the shop identifier.
        /// </summary>
        /// <value>
        /// The shop identifier.
        /// </value>
        /// <exception cref="EcwidConfigException" accessor="set">The shop identifier is invalid.</exception>
        public int ShopId
        {
            get { return _shopId; }
            set
            {
                if (value <= 0)
                    throw new EcwidConfigException("The shop identifier is invalid.");

                _shopId = value;
            }
        }
    }
}