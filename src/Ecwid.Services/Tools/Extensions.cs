using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ecwid.Tools
{
    /// <summary>
    /// Some string extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Trim and replace and split to words.
        /// </summary>
        /// <param name="str">The string.</param>
        public static List<string> TrimUpperReplaceSplit(this string str)
        {
            var result = new Regex("[^A-Za-z]").Replace(str, " ");//delete all non character symbols
            result = new Regex("[ ]{2,}", RegexOptions.None).Replace(result, " ");//replace all double spaces
            result = result.Trim().Replace(" ", ",");//trim and replace " " to ","
            result = result.ToUpper();
            return result.Split(',').ToList();
        }
    }
}