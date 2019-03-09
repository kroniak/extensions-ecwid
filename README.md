# C# client for Ecwid API

[![Build status](https://ci.appveyor.com/api/projects/status/4mgx59ese69wjx7d?svg=true)](https://ci.appveyor.com/project/kroniak/extensions-ecwid)
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/Ecwid.svg)](https://www.nuget.org/packages/Ecwid/)
> This repository contains models and services C# classes for Ecwid API v1 and v3.

C# Ecwid client is a modern, asynchronous, fast, testable client for [Ecwid API](https://developers.ecwid.com/api-documentation)

```c#
var client = new EcwidClient();
var result = await client.Configure(someShopId, someToken).Orders
                .Limit(10)
                .CreatedFrom(DateTime.Today)
                .PaymentStatuses("PAID")
                .GetAsync();
```

## Install

> The package is compiled for NET45 and .NET Platform Standard 1.4 which include .NET Core and other targets. [Read about it](https://github.com/dotnet/corefx/blob/master/Documentation/architecture/net-platform-standard.md#mapping-the-net-platform-standard-to-platforms).

`PM> Install-Package Ecwid -Pre` - services and models

`PM> Install-Package Ecwid.Legacy -Pre` - Legacy v1 services and models

`PM> Install-Package Ecwid.OAuth -Pre` - a pipeline OAuth2 wrapper for ASP.NET Core

## Ecwid API

You can learn about v3 (general) API [here](https://developers.ecwid.com/api-documentation)

You can learn about v1 (Legacy) API:

- Orders API [here](https://help.ecwid.com/customer/en/portal/articles/1166917-legacy-order-api)
- Products API [here](https://help.ecwid.com/customer/en/portal/articles/1163920-legacy-product-api)
- Instant Order Notifications API [here](https://help.ecwid.com/customer/en/portal/articles/1167200-instant-order-notifications-api)

## Namespaces

- Ecwid - client for API v3
- Ecwid.Models - model classes for API v3
- Ecwid.Legacy - client for API v1
- Ecwid.Legacy.Models - model classes for API v1

## How to use

[Look wiki pages](https://github.com/kroniak/extensions-ecwid/wiki)