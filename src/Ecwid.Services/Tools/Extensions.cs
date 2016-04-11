// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ecwid.Tools
{
    /// <summary>
    /// Some string extensions.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Trim and replace and split to words.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <exception cref="System.ArgumentNullException">is null. </exception>
        /// <exception cref="System.ArgumentException">is the empty string (""). </exception>
        public static List<string> TrimUpperReplaceSplit(this string str)
        {
            var result = new Regex("[^A-Za-z]").Replace(str, " "); //delete all non character symbols
            result = new Regex("[ ]{2,}", RegexOptions.None).Replace(result, " "); //replace all double spaces
            result = result.Trim().Replace(" ", ","); //trim and replace " " to ","
            result = result.ToUpper();
            return result.Split(',').ToList();
        }
    }
}