// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
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
        /// <exception cref="ArgumentException">Unable replace and split string</exception>
        public static List<string> TrimUpperReplaceSplit(this string str)
        {
            List<string> returnList;

            try
            {
                var result = new Regex("[,]", RegexOptions.None).Replace(str, " "); //replace all double spaces
                result = new Regex("[ ]{2,}", RegexOptions.None).Replace(result, " "); //replace all double spaces
                result = result.Trim().Replace(" ", ","); //trim and replace " " to ","
                result = result.ToUpper();
                returnList = result.Split(',').ToList();
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException("Unable replace chars and split string", exception);
            }

            return returnList;
        }
    }
}