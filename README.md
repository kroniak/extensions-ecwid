## C# client for Ecwid API
[![Build status](https://ci.appveyor.com/api/projects/status/4mgx59ese69wjx7d?svg=true)](https://ci.appveyor.com/project/kroniak/extensions-ecwid)
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/Ecwid.svg)](https://www.nuget.org/packages/Ecwid/)
> This repository contains models and services C# classes for Ecwid API v1 and v3.

C# Ecwid client is a modern, asynchronous, fast, testable client for [Ecwid API](https://developers.ecwid.com/api-documentation)

```c#
var client = new EcwidLegacyClient();
var result = await client.ConfigureShop(some_shop_id, some_orders_key, some_products_key)
                   .Orders
                   .Limit(10)
                   .FromDate(DateTime.Today)
                   .AddPaymentStatuses("PAID")
                   .GetAsync();
```
### Install
> The package is compiled for NET45 and .NET Platform Standard 5.0. 5th version of the .NET Platform Standard include .NET Core and many other targets. [Read about it](https://github.com/dotnet/corefx/blob/master/Documentation/architecture/net-platform-standard.md#mapping-the-net-platform-standard-to-platforms). 

`PM> Install-Package Ecwid -Pre` - services and models

`PM> Install-Package Ecwid.OAuth2 -Pre` - a pipeline OAuth2 wrapper for ASP.NET Core (aka ASP.NET 5)
### Ecwid API
You can learn about v1 (Legacy) API:
- Orders API [here](https://help.ecwid.com/customer/en/portal/articles/1166917-legacy-order-api)
- Products API [here](https://help.ecwid.com/customer/en/portal/articles/1163920-legacy-product-api)
- Instant Order Notifications API [here](https://help.ecwid.com/customer/en/portal/articles/1167200-instant-order-notifications-api)

You can learn abount v3 (general) API [here](https://developers.ecwid.com/api-documentation)
### Namespaces
- Ecwid.Models - model classes for API v3
- Ecwid.Models.Legacy - model classes for API v1
- Ecwid.Services - client service classes

### How to use
[Look wiki pages](https://github.com/kroniak/extensions-ecwid/wiki)