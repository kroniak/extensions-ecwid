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
        protected virtual async Task<T> GetApiResponceAsync<T>(Url baseUrl, CancellationToken cancellationToken) where T : class
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
        protected virtual async Task<T> UpdateApiAsync<T>(Url baseUrl, CancellationToken cancellationToken) where T : class
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
        protected async Task<T> UpdateApiAsync<T>(Url baseUrl, object query, CancellationToken cancellationToken) where T : class
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
        protected async Task<T> GetApiResponceAsync<T>(Url baseUrl, object query, CancellationToken cancellationToken) where T : class
        {
            var url = query != null ? baseUrl.SetQueryParams(query) : baseUrl;

            return await GetApiResponceAsync<T>(url, cancellationToken);
        }
    }
}
