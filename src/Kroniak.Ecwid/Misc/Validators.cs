namespace Ecwid.Misc
{
    /// <summary>
    /// Some validators for classes
    /// </summary>
    internal static class Validators
    {
        /// <summary>
        /// Shops the identifier validate.
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <exception cref="Ecwid.Misc.ConfigException">
        /// The shop identificator is null. Please config the client.
        /// or
        /// The shop identificator is invalid. Please config the client.
        /// </exception>
        public static bool ShopIdValidate(int? shopId)
        {
            if (shopId == null)
                throw new ConfigException("The shop identificator is null. Please config the client.");
            if (shopId <= 0)
                throw new ConfigException("The shop identificator is invalid. Please config the client.");

            return true;
        }

        /// <summary>
        /// Shops the authentication validate.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <exception cref="Ecwid.Misc.ConfigException">The shop auth identificator is null or empty. Please config the client.</exception>
        public static bool ShopAuthValidate(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ConfigException("The shop auth identificator is null or empty. Please config the client.");

            return true;
        }
    }
}
