using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;
using Ecwid.Services;
using Ecwid.Services.Legacy;
using Xunit;

namespace Ecwid.Test.Services.Legacy
{
    /// <summary>
    /// Tests with real http responces.
    /// </summary>
    public class EcwidLegacyClientOrdersRealTest
    {
        /// <summary>
        /// Checks the orders authentication asynchronous pass.
        /// </summary>
        [Fact]
        public async void CheckOrdersAuthAsyncPass()
        {
            IEcwidLegacyClient client = new EcwidLegacyClient()
            {
                Options =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa69ff110000c627a72174", //empty set with count and total
                    ShopId = 123,
                    ShopOrderAuthId = "test"
                }
            };

            var result = await client.CheckOrdersAuthAsync();

            Assert.Equal(true, result);
        }

        /// <summary>
        /// Checks the orders authentication asynchronous fail.
        /// </summary>
        [Fact]
        public async void CheckOrdersAuthAsyncFail()
        {
            IEcwidLegacyClient client = new EcwidLegacyClient()
            {
                Options =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa70921100009b28a72180", //empty set with count, total and 403 code
                    ShopId = 123,
                    ShopOrderAuthId = "test"
                }
            };

            var result = await client.CheckOrdersAuthAsync();

            Assert.Equal(false, result);
        }

        /// <summary>
        /// Gets the orders count asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetOrdersCountAsyncPass()
        {
            IEcwidLegacyClient client = new EcwidLegacyClient()
            {
                Options =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa69ff110000c627a72174", //empty set with count and total
                    ShopId = 123,
                    ShopOrderAuthId = "test"
                }
            };

            var result = await client.GetOrdersCountAsync();

            Assert.Equal(1021, result);
        }

        /// <summary>
        /// Gets the new orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetNewOrdersAsyncPass()
        {
            IEcwidLegacyClient client = new EcwidLegacyClient()
            {
                Options =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183", //one orders set with count and total and empty next url
                    ShopId = 123,
                    ShopOrderAuthId = "test"
                }
            };

            var result = await client.GetNewOrdersAsync();

            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Gets the non paid orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetNonPaidOrdersAsyncPass()
        {
            IEcwidLegacyClient client = new EcwidLegacyClient()
            {
                Options =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183", //one orders set with count and total and empty next url
                    ShopId = 123,
                    ShopOrderAuthId = "test"
                }
            };

            var result = await client.GetNonPaidOrdersAsync();

            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Gets the paid not shipped orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetPaidNotShippedOrdersAsyncPass()
        {
            IEcwidLegacyClient client = new EcwidLegacyClient()
            {
                Options =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183", //one orders set with count and total and empty next url
                    ShopId = 123,
                    ShopOrderAuthId = "test"
                }
            };

            var result = await client.GetPaidNotShippedOrdersAsync();

            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Gets the shipped not delivered orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetShippedNotDeliveredOrdersAsyncPass()
        {
            IEcwidLegacyClient client = new EcwidLegacyClient()
            {
                Options =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183", //one orders set with count and total and empty next url
                    ShopId = 123,
                    ShopOrderAuthId = "test"
                }
            };

            var result = await client.GetShippedNotDeliveredOrdersAsync();

            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Gets the orders asynchronous query multi pages result pass.
        /// </summary>
        [Fact]
        public async void GetOrdersAsyncQueryMultiPagesResultPass()
        {
            IEcwidLegacyClient client = new EcwidLegacyClient()
            {
                Options =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187", //one orders set with count and total and NON empty next url
                    ShopId = 123,
                    ShopOrderAuthId = "test"
                }
            };

            var result = await client.GetOrdersAsync(new { limit = 1 });

            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Gets the orders asynchronous query multi pages result cancellation pass.
        /// </summary>
        [Fact]
        public void GetOrdersAsyncQueryMultiPagesResultCancellationPass()
        {
            IEcwidLegacyClient client = new EcwidLegacyClient()
            {
                Options =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187", //one orders set with count and total and NON empty next url
                    ShopId = 123,
                    ShopOrderAuthId = "test"
                }
            };
            try
            {
                var source = new CancellationTokenSource();
                var task = client.GetOrdersAsync(new { limit = 1 }, source.Token);
                source.Cancel();
                Assert.NotNull(task);
            }
            catch (Flurl.Http.FlurlHttpException)
            {
                Assert.True(true);
            }
        }

        /// <summary>
        /// Gets the orders asynchronous query builder multi pages result pass.
        /// </summary>
        [Fact]
        public async void GetOrdersAsyncQueryBuilderMultiPagesResultPass()
        {
            IEcwidLegacyClient client = new EcwidLegacyClient()
            {
                Options =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187", //one orders set with count and total and NON empty next url
                    ShopId = 123,
                    ShopOrderAuthId = "test"
                }
            };

            var result = await client.Orders.Limit(5).GetAsync();

            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Gets the orders asynchronous multi threading pass.
        /// </summary>
        [Fact]
        public void GetOrdersAsyncMultiThreadingPass()
        {
            var orders = new List<LegacyOrder>();
            var tasks = new List<Task<List<LegacyOrder>>>();

            // max 100 in 5 sec - real 50
            for (var i = 0; i < 50; i++)
            {
                IEcwidLegacyClient client = new EcwidLegacyClient()
                {
                    Options =
                    {
                        ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187", //one orders set with count and total and NON empty next url
                        ShopId = 123,
                        ShopOrderAuthId = "test"
                    }
                };

                var task = client.Orders.Limit(5).GetAsync();
                tasks.Add(task);
            }

            // ReSharper disable once CoVariantArrayConversion
            Task.WaitAll(tasks.ToArray());
            tasks.ForEach(t => { orders.AddRange(t.Result); });

            Assert.NotEmpty(orders);
        }
    }
}