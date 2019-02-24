// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;

namespace Ecwid.Test.Services
{
    internal static class Tools
    {
        /// <summary>
        /// Times the specified action.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static void Times(this int count, Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            if (count <= 0)
                throw new ArgumentException(nameof(count));
            for (var i = 0; i < count; i++)
                action();
        }
    }
}