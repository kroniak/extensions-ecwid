// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;

namespace Ecwid
{
    public partial class EcwidClient
    {
        #region Implementation of IEcwidProfileClient

        /// <summary>
        /// Gets the store profile asynchronous.
        /// </summary>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        public async Task<Profile> GetProfileAsync()
            => await GetProfileAsync(CancellationToken.None);

        /// <summary>
        /// Gets the store profile asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        public async Task<Profile> GetProfileAsync(CancellationToken cancellationToken)
            => await GetApiAsync<Profile>(GetUrl("profile"), cancellationToken);

        /// <summary>
        /// Update the store profile asynchronous.
        /// </summary>
        /// <param name="profile">The store profile.</param>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        public async Task<UpdateStatus> UpdateProfileAsync(Profile profile)
            => await UpdateProfileAsync(profile, CancellationToken.None);

        /// <summary>
        /// Update the store profile asynchronous.
        /// </summary>
        /// <param name="profile">The store profile.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidConfigException">Credentials are invalid.</exception>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        public async Task<UpdateStatus> UpdateProfileAsync(Profile profile, CancellationToken cancellationToken)
        {
            if (profile == null)
                throw new EcwidHttpException("Something happened to the HTTP call.", null,
                    new ArgumentNullException(nameof(profile)));

            return await PutApiAsync<UpdateStatus>(GetUrl("profile"), profile, cancellationToken);
        }

        #endregion
    }
}