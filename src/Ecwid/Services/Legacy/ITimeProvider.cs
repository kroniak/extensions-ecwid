// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;

namespace Ecwid.Legacy
{
	/// <summary>
	/// Interface to shim DateTime.Now
	/// </summary>
	public interface ITimeProvider
	{
		/// <summary>
		/// Gets the now.
		/// </summary>
		/// <value>
		/// The now.
		/// </value>
		DateTime Now { get; }
	}

	/// <summary>
	/// Real provider for non test env.
	/// </summary>
	/// <seealso cref="ITimeProvider" />
	public class RealTimeProvider : ITimeProvider
	{
		/// <summary>
		/// Gets the now.
		/// </summary>
		/// <value>
		/// The now.
		/// </value>
		public DateTime Now => DateTime.Now;
	}
}