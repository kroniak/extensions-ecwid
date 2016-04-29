// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace Ecwid
{
    /// <summary>
    /// Abstract client class contains shared methods.
    /// </summary>
    public abstract class BaseEcwidClient
    {
        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        protected async Task<bool> CheckTokenAsync<T>(Url url)
            where T : class
        {
            try
            {
                await GetApiResponseAsync<T>(url, new {limit = 1});
                return true;
            }
            catch (EcwidHttpException exception)
            {
                var status = exception.StatusCode;
                if (status == HttpStatusCode.Forbidden)
                    return false;

                throw;
            }
        }

        /// <summary>
        /// Checks the shop authentication asynchronous.
        /// </summary>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        protected async Task<bool> CheckTokenAsync<T>(Url url, CancellationToken cancellationToken)
            where T : class
        {
            try
            {
                await GetApiResponseAsync<T>(url, new {limit = 1}, cancellationToken);
                return true;
            }
            catch (EcwidHttpException exception)
            {
                var status = exception.StatusCode;
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
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        protected async Task<T> GetApiResponseAsync<T>(Url baseUrl) where T : class
        {
            T poco;
            try
            {
                poco = await baseUrl.GetJsonAsync<T>();
            }
            catch (FlurlHttpException exception)
            {
                var call = exception.Call;
                var status = call.Response?.StatusCode;
                var error = call.ErrorResponseBody ?? call.Exception?.Message ?? "Something happened to the HTTP call.";

                throw new EcwidHttpException(error, status, exception);
            }

            return poco;
        }

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        protected async Task<T> GetApiResponseAsync<T>(Url baseUrl, CancellationToken cancellationToken)
            where T : class
        {
            T poco;
            try
            {
                poco = await baseUrl.GetJsonAsync<T>(cancellationToken);
            }
            catch (FlurlHttpException exception)
            {
                var call = exception.Call;
                var status = call.Response?.StatusCode;
                var error = call.ErrorResponseBody ?? call.Exception?.Message ?? "Something happened to the HTTP call.";

                throw new EcwidHttpException(error, status, exception);
            }

            return poco;
        }

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        protected async Task<T> GetApiResponseAsync<T>(Url baseUrl, object query) where T : class
            => await GetApiResponseAsync<T>(baseUrl.SetQueryParams(query));

        /// <summary>
        /// Gets the API responce asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="EcwidHttpException">Something happened to the HTTP call.</exception>
        protected async Task<T> GetApiResponseAsync<T>(Url baseUrl, object query, CancellationToken cancellationToken)
            where T : class
            => await GetApiResponseAsync<T>(baseUrl.SetQueryParams(query), cancellationToken);

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        private async Task<T> UpdateApiAsync<T>(Url baseUrl) where T : class
            => await baseUrl.PostAsync().ReceiveJson<T>();

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task<T> UpdateApiAsync<T>(Url baseUrl, CancellationToken cancellationToken)
            where T : class
            => await baseUrl.PostAsync(cancellationToken).ReceiveJson<T>();

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        protected async Task<T> UpdateApiAsync<T>(Url baseUrl, object query) where T : class
            => await UpdateApiAsync<T>(baseUrl.SetQueryParams(query));

        /// <summary>
        /// Updates the API asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task<T> UpdateApiAsync<T>(Url baseUrl, object query, CancellationToken cancellationToken)
            where T : class => await UpdateApiAsync<T>(baseUrl.SetQueryParams(query), cancellationToken);
    }
}