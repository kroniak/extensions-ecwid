using System;

namespace Ecwid.Tools
{
    public class ConfigException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ConfigException(string message) : base(message) { }
    }

    public class InvalidArgumentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidArgumentException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InvalidArgumentException(string message) : base(message) { }
    }

    public class LimitException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LimitException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public LimitException(string message) : base(message) { }
    }
}
