// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace Ecwid.Services
{
    /// <summary>
    /// Abstract client class contains shared methods.
    /// </summary>
    public abstract class BaseEcwidClient
    {
        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <exception cref="FlurlHttpException">Condition.</exception>
        protected async Task<bool> CheckTokenAsync<T>(Url url)
            where T : class
        {
            try
            {
                await GetApiResponceAsync<T>(url, new {limit = 1});
                return true;
            }
            catch (FlurlHttpException exception)
            {
                var status = exception.Call.Response?.StatusCode;
                if (status == HttpStatusCode.Forbidden)
                    return false;

                throw;
            }
        }

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <exception cref="FlurlHttpException">Condition.</exception>
        protected async Task<bool> CheckTokenAsync<T>(Url url, CancellationToken cancellationToken)
            where T : class
        {
            try
            {
                await GetApiResponceAsync<T>(url, new {limit = 1}, cancellationToken);
                return true;
            }
            catch (FlurlHttpException exception)
            {
                var status = exception.Call.Response?.StatusCode;
                if (status == HttpStatusCode.Forbidden)
                    return false;

                throw;
            }
        }

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        protected virtual async Task<T> GetApiResponceAsync<T>(Url baseUrl) where T : class
            => await baseUrl.GetJsonAsync<T>();

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected virtual async Task<T> GetApiResponceAsync<T>(Url baseUrl, CancellationToken cancellationToken)
            where T : class
            => await baseUrl.GetJsonAsync<T>(cancellationToken);

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        protected virtual async Task<T> UpdateApiAsync<T>(Url baseUrl) where T : class
            => await baseUrl.PostAsync().ReceiveJson<T>();

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected virtual async Task<T> UpdateApiAsync<T>(Url baseUrl, CancellationToken cancellationToken)
            where T : class
            => await baseUrl.PostAsync(cancellationToken).ReceiveJson<T>();

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        protected async Task<T> UpdateApiAsync<T>(Url baseUrl, object query) where T : class
        {
            var url = query != null ? baseUrl.SetQueryParams(query) : baseUrl;
            return await UpdateApiAsync<T>(url);
        }

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<T> UpdateApiAsync<T>(Url baseUrl, object query, CancellationToken cancellationToken)
            where T : class
        {
            var url = query != null ? baseUrl.SetQueryParams(query) : baseUrl;
            return await UpdateApiAsync<T>(url, cancellationToken);
        }

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        protected async Task<T> GetApiResponceAsync<T>(Url baseUrl, object query) where T : class
        {
            var url = query != null ? baseUrl.SetQueryParams(query) : baseUrl;

            return await GetApiResponceAsync<T>(url);
        }

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<T> GetApiResponceAsync<T>(Url baseUrl, object query, CancellationToken cancellationToken)
            where T : class
        {
            var url = query != null ? baseUrl.SetQueryParams(query) : baseUrl;

            return await GetApiResponceAsync<T>(url, cancellationToken);
        }
    }
}