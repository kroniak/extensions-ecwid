// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models;

namespace Ecwid
{
    public partial class EcwidClient
    {
        #region Implementation of IEcwidProfileClient

        /// <inheritdoc />
        public Task<Profile> GetProfileAsync()
            => GetProfileAsync(CancellationToken.None);

        /// <inheritdoc />
        public Task<Profile> GetProfileAsync(CancellationToken cancellationToken)
            => GetApiAsync<Profile>(GetUrl("profile"), cancellationToken);

        /// <inheritdoc />
        public Task<UpdateStatus> UpdateProfileAsync(Profile profile)
            => UpdateProfileAsync(profile, CancellationToken.None);

        /// <inheritdoc />
        public Task<UpdateStatus> UpdateProfileAsync(Profile profile, CancellationToken cancellationToken)
        {
            if (profile == null)
                throw new EcwidHttpException("Something happened to the HTTP call.", null,
                    new ArgumentNullException(nameof(profile)));

            return PutApiAsync<UpdateStatus>(GetUrl("profile"), profile, cancellationToken);
        }

        #endregion
    }
}