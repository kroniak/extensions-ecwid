## Extensions for Ecwid

[![Build status](https://ci.appveyor.com/api/projects/status/4mgx59ese69wjx7d?svg=true)](https://ci.appveyor.com/project/kroniak/extensions-ecwid)

### What is it?
This repo contains models and services C# classes for Ecwid API v1 and v3.

You can learn about v1 (Legacy) API:
- Orders API https://help.ecwid.com/customer/en/portal/articles/1166917-legacy-order-api
- Products API https://help.ecwid.com/customer/en/portal/articles/1163920-legacy-product-api
- Instant Order Notifications API https://help.ecwid.com/customer/en/portal/articles/1167200-instant-order-notifications-api

You can learn abount v3 (general) API https://developers.ecwid.com/api-documentation

### What in the box?
2 nuget packages:

`PM> Install-Package Ecwid -Pre` - services and models

`PM> Install-Package Ecwid.OAuth2 -Pre` - pipeline O2Auth  wrapper for ASP.NET Core (aka ASP.NET5) and ASP.NET 4

### Namespaces
- Ecwid.Models - shared classes and v3 classes
- Ecwid.Models.Legacy - classes for Legacy (v1) API
- Ecwid.Services - service classes for v3 API
- Ecwid.Services.Legacy - service classes for Legacy (v1) API

### How to use
[Look wiki pages](https://github.com/kroniak/extensions-ecwid/wiki)
