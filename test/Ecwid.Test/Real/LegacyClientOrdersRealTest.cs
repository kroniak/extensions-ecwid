// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Ecwid.Legacy;
using Ecwid.Models.Legacy;
using Xunit;

namespace Ecwid.Test.Real
{
    /// <summary>
    /// Tests with real http responses.
    /// </summary>
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    [SuppressMessage("ReSharper", "MissingXmlDoc")]
    public class LegacyClientOrdersRealTest
    {
        private const int ShopId = 123;
        private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";
        private readonly EcwidLegacyCredentials _credentials = new EcwidLegacyCredentials(ShopId, Token, Token);

        /// <summary>
        /// Checks the orders authentication asynchronous fail.
        /// </summary>
        [Fact]
        public async void CheckOrdersAuthAsync_ReturnFalse()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa70921100009b28a72180"
                    //empty set with count, total and 403 code
                }
            };

            var result = await client.CheckOrdersTokenAsync();

            Assert.False(result);
        }

        /// <summary>
        /// Checks the orders authentication asynchronous pass.
        /// </summary>
        [Fact]
        public async void CheckOrdersTokenAsync_ReturnTrue()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa69ff110000c627a72174" //empty set with count and total
                }
            };

            var result = await client.CheckOrdersTokenAsync();

            Assert.True(result);
        }

        /// <summary>
        /// Gets the new orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetNewOrdersAsync__ReturnSingleResult()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183"
                    //one orders set with count and total and empty next url
                }
            };

            var result = await client.GetNewOrdersAsync();

            Assert.Single(result);
        }

        /// <summary>
        /// Gets the non paid orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetNonPaidOrdersAsync_ReturnSingleResult()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183"
                    //one orders set with count and total and empty next url
                }
            };

            var result = await client.GetNonPaidOrdersAsync();

            Assert.Single(result);
        }

        /// <summary>
        /// Gets the orders asynchronous multi threading pass.
        /// </summary>
        [Fact]
        public void GetOrdersAsyncMultiThreading_ReturnCorrectList()
        {
            var orders = new List<LegacyOrder>();
            var tasks = new List<Task<List<LegacyOrder>>>();

            // max 100 in 5 sec - real 50
            for (var i = 0; i < 50; i++)
            {
                var client = new EcwidLegacyClient(_credentials)
                {
                    Settings =
                    {
                        ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187"
                        //one orders set with count and total and NON empty next url
                    }
                };

                var task = client.Orders.Order(5).GetAsync();
                tasks.Add(task);
            }

            // ReSharper disable once CoVariantArrayConversion
            Task.WaitAll(tasks.ToArray());
            tasks.ForEach(t => { orders.AddRange(t.Result); });

            Assert.Equal(100, orders.Count);
        }

        /// <summary>
        /// Gets the orders asynchronous query builder multi pages result pass.
        /// </summary>
        [Fact]
        public async void GetOrdersAsyncQueryBuilderMultiPagesResult_ReturnCorrectList()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187"
                    //one orders set with count and total and NON empty next url
                }
            };

            var result = await client.Orders.Order(5).GetAsync();

            Assert.Equal(2, result.Count);
        }

        /// <summary>
        /// Gets the orders asynchronous query multi pages result pass.
        /// </summary>
        [Fact]
        public async void GetOrdersAsyncQueryMultiPagesResult_ReturnCorrectList()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187"
                    //one orders set with count and total and NON empty next url
                }
            };

            var result = await client.GetOrdersAsync(new {statuses = "paid"});

            Assert.Equal(2, result.Count);
        }

        /// <summary>
        /// Gets the orders asynchronous query multi pages result cancellation pass.
        /// </summary>
        [Fact]
        public void GetOrdersAsyncQueryMultiPagesResultCancellation_Exception()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa76b61100007629a72187"
                    //one orders set with count and total and NON empty next url
                }
            };

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
        /// Gets the orders count asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetOrdersCountAsync_ReturnCorrectList()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa69ff110000c627a72174" //empty set with count and total
                }
            };

            var result = await client.GetOrdersCountAsync();

            Assert.Equal(1021, result);
        }

        /// <summary>
        /// Gets the paid not shipped orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetPaidNotShippedOrdersAsync_ReturnSingleResult()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183"
                    //one orders set with count and total and empty next url
                }
            };

            var result = await client.GetPaidNotShippedOrdersAsync();

            Assert.Single(result);
        }

        /// <summary>
        /// Gets the shipped not delivered orders asynchronous pass.
        /// </summary>
        [Fact]
        public async void GetShippedOrdersAsync_ReturnSingleResult()
        {
            var client = new EcwidLegacyClient(_credentials)
            {
                Settings =
                {
                    ApiUrl = "http://www.mocky.io/v2/56fa73b51100000429a72183"
                    //one orders set with count and total and empty next url
                }
            };

            var result = await client.GetShippedOrdersAsync();

            Assert.Single(result);
        }
    }
}