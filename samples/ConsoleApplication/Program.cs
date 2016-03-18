using System;
using Ecwid.Services.Legacy;

namespace ConsoleApplication
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            // Create instance of v1 API Orders service and config way
            var ecwidLegacyClient = new EcwidLegacyClient
            {
                Options =
                {
                    ShopId = 123, // Your shop id
                    ShopOrderAuthId = "" // Your shop orders auth
                }
            };

            // Reconfig way 2
            ecwidLegacyClient.Options.ShopId = 123;
            ecwidLegacyClient.Options.ShopOrderAuthId = "";

            // Reconfig way 3 and get
            ecwidLegacyClient.Configure(new EcwidLegacyOptions
            {
                ShopId = 123,
                ShopOrderAuthId = ""
            });

            // Reconfig only shop params and check shop orders auth credentials
            var check = ecwidLegacyClient.ConfigureShop(123, "", "").CheckOrdersAuthAsync();

            if (!check.Result)
            {
                Console.WriteLine("Order auth key is not valid");
                return;
            }

            // Get shop orders counts
            var count = ecwidLegacyClient.GetOrdersCountAsync();
            Console.WriteLine($"Orders count is {count.Result}");

            // Get all shop orders 
            // WARNING! CAN PRODUCT VARY MANY REQUESTS
            var orders = ecwidLegacyClient.GetOrdersAsync();
            Console.WriteLine($"Orders count in result is {orders.Result.Count}");

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
