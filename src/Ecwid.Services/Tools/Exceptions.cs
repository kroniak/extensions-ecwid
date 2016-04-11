﻿// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;

namespace Ecwid.Tools
{
    public class LimitException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LimitException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public LimitException(string message) : base(message)
        {
        }
    }
}