using System;

namespace Ecwid.Misc
{
    public class ConfigException : Exception
    {
        public ConfigException(string message) : base(message) { }
    }
}
