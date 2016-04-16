// Licensed under the GPL License, Version 3.0. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using Ecwid.Services;
using Ecwid.Services.Legacy;
using Xunit;

namespace Ecwid.Test.Services
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class QueryBuilderExtensionsTest
    {
        private readonly IEcwidLegacyClient _defaultLegacyClient = new EcwidLegacyClient();

        [Fact]
        public void ExtensionFail()
        {
            Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.Order(null));
            Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.Custom(null, new {a = 1}));
            Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.Custom("", new {a = 1}));
            Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.Custom(" ", new {a = 1}));
        }

        [Fact]
        public void DatePass()
        {
            var result = _defaultLegacyClient.Orders.Date(Convert.ToDateTime("2000-01-01")).Query["date"];
            var result2 = _defaultLegacyClient.Orders.FromDate(Convert.ToDateTime("2000-01-01")).Query["from_date"];
            var result3 = _defaultLegacyClient.Orders.ToDate(Convert.ToDateTime("2000-01-01")).Query["to_date"];
            var result4 =
                _defaultLegacyClient.Orders.FromUpdateDate(Convert.ToDateTime("2000-01-01")).Query["from_update_date"];
            var result5 =
                _defaultLegacyClient.Orders.ToUpdateDate(Convert.ToDateTime("2000-01-01")).Query["to_update_date"];

            Assert.Equal(result, "2000-01-01");
            Assert.Equal(result2, "2000-01-01");
            Assert.Equal(result3, "2000-01-01");
            Assert.Equal(result4, "2000-01-01");
            Assert.Equal(result5, "2000-01-01");
        }

        [Fact]
        public void OrderPass()
        {
            var result = _defaultLegacyClient.Orders.Order(123).Query["order"];
            var result2 = _defaultLegacyClient.Orders.FromOrder(123).Query["from_order"];
            var result3 = _defaultLegacyClient.Orders.Order("123").Query["order"];
            var result4 = _defaultLegacyClient.Orders.FromOrder("123").Query["from_order"];

            Assert.Equal(result, 123);
            Assert.Equal(result2, 123);
            Assert.Equal(result3, "123");
            Assert.Equal(result4, "123");
        }

        [Fact]
        public void CustomerPass()
        {
            var result = _defaultLegacyClient.Orders.CustomerId(123).Query["customer_id"];
            var result2 = _defaultLegacyClient.Orders.CustomerId(null).Query["customer_id"];
            var result3 = _defaultLegacyClient.Orders.CustomerEmail("test").Query["customer_email"];
            var result4 = _defaultLegacyClient.Orders.CustomerEmail(null).Query["customer_email"];
            var result5 = _defaultLegacyClient.Orders.CustomerEmail("").Query["customer_email"];

            Assert.Equal(result, 123);
            Assert.Equal(result2, "null");
            Assert.Equal(result3, "test");
            Assert.Equal(result4, "");
            Assert.Equal(result5, "");
        }

        [Fact]
        public void StatusesPass()
        {
            var result = _defaultLegacyClient.Orders.Statuses("PAID, DECLINED", "NEW PROCESSING").Query["statuses"];
            Assert.Equal(result, "PAID,DECLINED,NEW,PROCESSING");
        }

        [Theory]
        [InlineData("PAID, DECLIN", "NEW PROCESSING")]
        [InlineData("PAID, DECLINED", "NEW PROCESS")]
        [InlineData("", "")]
        public void StatusesFail(string paid, string full)
        {
            Assert.Throws<EcwidConfigException>(() => _defaultLegacyClient.Orders.Statuses(paid, full));
        }

        [Fact]
        public void AddPaymentStatusesPass()
        {
            var result = _defaultLegacyClient.Orders.PaymentStatuses("PAID, DECLINED").Query["statuses"];
            Assert.Equal(result, "PAID,DECLINED");
        }

        [Fact]
        public void AddAddPaymentStatusesPass()
        {
            var result =
                _defaultLegacyClient.Orders.PaymentStatuses("PAID, DECLINED").PaymentStatuses("Cancelled").Query[
                    "statuses"];
            Assert.Equal(result, "PAID,DECLINED,CANCELLED");
        }

        [Fact]
        public void AddFulfillmentStatusesPass()
        {
            var result = _defaultLegacyClient.Orders.FulfillmentStatuses("NEW PROCESSING").Query["statuses"];
            Assert.Equal(result, "NEW,PROCESSING");
        }

        [Fact]
        public void AddAddFulfillmentStatusesPass()
        {
            var result =
                _defaultLegacyClient.Orders.FulfillmentStatuses("NEW").FulfillmentStatuses("PROCESSING").Query[
                    "statuses"];
            Assert.Equal(result, "NEW,PROCESSING");
        }

        [Fact]
        public void AddBothStatusesPass()
        {
            var result =
                _defaultLegacyClient.Orders.PaymentStatuses("PAID, DECLINED")
                    .FulfillmentStatuses("NEW PROCESSING")
                    .Query["statuses"];
            Assert.Equal(result, "PAID,DECLINED,NEW,PROCESSING");
        }

        [Fact]
        public void LimitOffsetPass()
        {
            var result = _defaultLegacyClient.Orders.Limit(123).Query["limit"];
            var result2 = _defaultLegacyClient.Orders.Offset(100).Query["offset"];

            Assert.Equal(result, 123);
            Assert.Equal(result2, 100);
        }
    }
}