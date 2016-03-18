using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;
using Flurl.Http;

namespace Ecwid.Services.Legacy
{
    public partial class EcwidLegacyClient : IEcwidOrdersLegacy
    {
        /// <summary>
        /// Checks the shop authentication asynchronous. URL: https://app.ecwid.com/api/v1/[STORE-ID]/orders?limit=0
        /// </summary>
        public async Task<bool> CheckOrdersAuthAsync()
        {
            try
            {
                await GetApiResponceAsync<LegacyOrderResponse>(OrdersUrl, new { limit = 0 });
            }
            catch (FlurlHttpException exception)
            {
                var status = exception.Call.Response.StatusCode;
                if (status == HttpStatusCode.Unauthorized || status == HttpStatusCode.Forbidden)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the orders count. URL: https://app.ecwid.com/api/v1/[STORE-ID]/orders?limit=0
        /// </summary>
        public async Task<int> GetOrdersCountAsync()
        {
            var response = await GetApiResponceAsync<LegacyOrderResponse>(OrdersUrl, new { limit = 0 });
            return response.Total;
        }

        /// <summary>
        /// Gets the orders asynchronous. URL: https://app.ecwid.com/api/v1/[STORE-ID]/orders
        /// </summary>
        /// <param name="query">The query.</param>
        public async Task<List<LegacyOrder>> GetOrdersAsync(object query)
        {
            var response = await GetApiResponceAsync<LegacyOrderResponse>(OrdersUrl, query);
            var orders = new List<LegacyOrder>(response.Orders);

            while (response.NextUrl != null)
            {
                response = await GetApiResponceAsync<LegacyOrderResponse>(response.NextUrl);
                orders.AddRange(response.Orders);
            }
            return orders;
        }

        /// <summary>
        /// Gets the orders asynchronous. URL: https://app.ecwid.com/api/v1/[STORE-ID]/orders
        /// </summary>
        public async Task<List<LegacyOrder>> GetOrdersAsync()
        {
            var response = await GetApiResponceAsync<LegacyOrderResponse>(OrdersUrl);
            var orders = new List<LegacyOrder>(response.Orders);

            while (response.NextUrl != null)
            {
                response = await GetApiResponceAsync<LegacyOrderResponse>(response.NextUrl);
                orders.AddRange(response.Orders);
            }
            return orders;
        }

        /// <summary>
        /// Gets the new orders. Gets the new orders. This orders is not paid or processed. URL: https://app.ecwid.com/api/v1/[STORE-ID]/orders?statuses=
        /// </summary>
        public async Task<List<LegacyOrder>> GetNewOrdersAsync()
        {
            // TODO more right
            return await GetOrdersAsync(new { statuses = "NEW" });
        }

        /// <summary>
        /// Gets the non paid orders asynchronous.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<List<LegacyOrder>> GetNonPaidOrdersAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}