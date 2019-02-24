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
    public static class StringExtensions
    {
        /// <summary>
        /// Trim and replace and split to words.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <exception cref="ArgumentException">Unable replace and split string</exception>
        public static IEnumerable<string> TrimUpperReplaceSplit(this string str)
        {
            string[] returnList;

            try
            {
                var result = new Regex("[,]", RegexOptions.None).Replace(str, " "); //replace all double spaces
                result = new Regex("[ ]{2,}", RegexOptions.None).Replace(result, " "); //replace all double spaces
                result = result.Trim().Replace(" ", ","); //trim and replace " " to ","
                result = result.ToUpper();
                returnList = result.Split(',');
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException("Unable replace chars and split string", exception);
            }

            return returnList;
        }

        /// <summary>
        /// Validate the status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="statusesAvailable">The statuses available.</param>
        /// <exception cref="ArgumentException">Status string is invalid.</exception>
        /// <exception cref="ArgumentException">Status string is invalid. Support only one status. </exception>
        public static string ExtractFirstStatus(this string status, IEnumerable<string> statusesAvailable)
        {
            Validators.StatusesValidate(status, statusesAvailable);

            var result = status.TrimUpperReplaceSplit();

            return result.First();
        }
    }
}