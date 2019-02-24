// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.
// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;

namespace Ecwid
{
    /// <summary>
    /// Ecwid profile public API.
    /// </summary>
    public interface IEcwidProfileClient
    {
        /// <summary>
        /// Gets the store profile asynchronous.
        /// </summary>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        Task<Profile> GetProfileAsync();

        /// <summary>
        /// Gets the store profile asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        Task<Profile> GetProfileAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Update the store profile asynchronous.
        /// </summary>
        /// <param name="profile">The store profile.</param>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        Task<UpdateStatus> UpdateProfileAsync(Profile profile);

        /// <summary>
        /// Update the store profile asynchronous.
        /// </summary>
        /// <param name="profile">The store profile.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        Task<UpdateStatus> UpdateProfileAsync(Profile profile, CancellationToken cancellationToken);
    }
}