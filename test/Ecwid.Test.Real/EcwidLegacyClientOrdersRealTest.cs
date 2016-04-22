// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Models.Legacy;
using Ecwid.Legacy;
using Xunit;

namespace Ecwid.Test.Real
{
    /// <summary>
    /// Tests with real http responces.
    /// </summary>
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    [SuppressMessage("ReSharper", "MissingXmlDoc")]
    public class EcwidLegacyClientOrdersRealTest
    {
        private const int ShopId = 123;
        private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";
        private readonly EcwidLegacyCredentials _credentials;

        public EcwidLegacyClientOrdersRealTest()
        {
            _credentials = new EcwidLegacyCredentials(ShopId, Token, Token);
        }

        /// <summary>
        /// Checks the orders authentication asynchronous pass.
        /// </summary>
        [Fact]
        public async void CheckOrdersTokenAsync()
        {
            var client = new EcwidLegacyClient
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa69ff110000c627a72174" //empty set with count and total
                }
            }.Configure(_credentials);

            var result = await client.CheckOrdersTokenAsync();

            Assert.True(result);
        }

        /// <summary>
        /// Checks the orders authentication asynchronous fail.
        /// </summary>
        [Fact]
        public async void CheckOrdersAuthAsyncFail()
        {
            var client = new EcwidLegacyClient
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa70921100009b28a72180"
                    //empty set with count, total and 403 code
                }
            }.Configure(_credentials);

            var result = await client.CheckOrdersTokenAsync();

            Assert.False(result);
        }

        /// <summary>
        /// Gets the orders count asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetOrdersCountAsync()
        {
            var client = new EcwidLegacyClient
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa69ff110000c627a72174" //empty set with count and total
                }
            }.Configure(_credentials);

            var result = await client.GetOrdersCountAsync();

            Assert.Equal(1021, result);
        }

        /// <summary>
        /// Gets the new orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetNewOrdersAsync()
        {
            var client = new EcwidLegacyClient
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183"
                    //one orders set with count and total and empty next url
                }
            }.Configure(_credentials);

            var result = await client.GetNewOrdersAsync();

            Assert.Equal(1, result.Count);
        }

        /// <summary>
        /// Gets the non paid orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetNonPaidOrdersAsync()
        {
            var client = new EcwidLegacyClient
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183"
                    //one orders set with count and total and empty next url
                }
            }.Configure(_credentials);

            var result = await client.GetNonPaidOrdersAsync();

            Assert.Equal(1, result.Count);
        }

        /// <summary>
        /// Gets the paid not shipped orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetPaidNotShippedOrdersAsync()
        {
            var client = new EcwidLegacyClient
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183"
                    //one orders set with count and total and empty next url
                }
            }.Configure(_credentials);

            var result = await client.GetPaidNotShippedOrdersAsync();

            Assert.Equal(1, result.Count);
        }

        /// <summary>
        /// Gets the shipped not delivered orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetShippedOrdersAsync()
        {
            var client = new EcwidLegacyClient
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183"
                    //one orders set with count and total and empty next url
                }
            }.Configure(_credentials);

            var result = await client.GetShippedOrdersAsync();

            Assert.Equal(1, result.Count);
        }

        /// <summary>
        /// Gets the orders asynchronous query multi pages result pass.
        /// </summary>
        [Fact]
        public async void GetOrdersAsyncQueryMultiPagesResult()
        {
            var client = new EcwidLegacyClient
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187"
                    //one orders set with count and total and NON empty next url
                }
            }.Configure(_credentials);

            var result = await client.GetOrdersAsync(new {statuses = "paid"});

            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        /// <summary>
        /// Gets the orders asynchronous query multi pages result cancellation pass.
        /// </summary>
        [Fact]
        public void GetOrdersAsyncQueryMultiPagesResultCancellation()
        {
            var client = new EcwidLegacyClient
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187"
                    //one orders set with count and total and NON empty next url
                }
            }.Configure(_credentials);

            try
            {
                var source = new CancellationTokenSource();
                var task = client.GetOrdersAsync(new {limit = 1}, source.Token);
                source.Cancel();
                Assert.NotNull(task);
            }
            catch (EcwidHttpException)
            {
                Assert.True(true);
            }
        }

        /// <summary>
        /// Gets the orders asynchronous query builder multi pages result pass.
        /// </summary>
        [Fact]
        public async void GetOrdersAsyncQueryBuilderMultiPagesResult()
        {
            var client = new EcwidLegacyClient
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187"
                    //one orders set with count and total and NON empty next url
                }
            }.Configure(_credentials);

            var result = await client.Orders.Order(5).GetAsync();

            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        /// <summary>
        /// Gets the orders asynchronous multi threading pass.
        /// </summary>
        [Fact]
        public void GetOrdersAsyncMultiThreading()
        {
            var orders = new List<LegacyOrder>();
            var tasks = new List<Task<List<LegacyOrder>>>();

            // max 100 in 5 sec - real 50
            for (var i = 0; i < 50; i++)
            {
                var client = new EcwidLegacyClient
                {
                    Settings =
                    {
                        ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187"
                        //one orders set with count and total and NON empty next url
                    }
                }.Configure(_credentials);

                var task = client.Orders.Order(5).GetAsync();
                tasks.Add(task);
            }

            // ReSharper disable once CoVariantArrayConversion
            Task.WaitAll(tasks.ToArray());
            tasks.ForEach(t => { orders.AddRange(t.Result); });

            Assert.NotEmpty(orders);
            Assert.Equal(100, orders.Count);
        }
    }
}