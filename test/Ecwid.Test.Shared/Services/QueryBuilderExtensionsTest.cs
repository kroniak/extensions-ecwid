using System;
using Ecwid.Services;
using Ecwid.Tools;
using Xunit;

namespace Ecwid.Test.Services
{
    public class QueryBuilderExtensionsTest
    {
        private readonly EcwidLegacyClient _defaultClient = new EcwidLegacyClient();

        [Fact]
        public void ExtensionFail()
        {
            Assert.Throws<ArgumentNullException>(() => _defaultClient.Orders.Order(null));
            Assert.Throws<ArgumentNullException>(() => _defaultClient.Orders.Custom(null, new { a = 1 }));
        }

        [Fact]
        public void DatePass()
        {
            var result = _defaultClient.Orders.Date(Convert.ToDateTime("2000-01-01")).QueryParams["date"];
            var result2 = _defaultClient.Orders.FromDate(Convert.ToDateTime("2000-01-01")).QueryParams["from_date"];
            var result3 = _defaultClient.Orders.ToDate(Convert.ToDateTime("2000-01-01")).QueryParams["to_date"];
            var result4 =
                _defaultClient.Orders.FromUpdateDate(Convert.ToDateTime("2000-01-01")).QueryParams["from_update_date"];
            var result5 =
                _defaultClient.Orders.ToUpdateDate(Convert.ToDateTime("2000-01-01")).QueryParams["to_update_date"];

            Assert.Equal(result, "2000-01-01");
            Assert.Equal(result2, "2000-01-01");
            Assert.Equal(result3, "2000-01-01");
            Assert.Equal(result4, "2000-01-01");
            Assert.Equal(result5, "2000-01-01");
        }

        [Fact]
        public void OrderPass()
        {
            var result = _defaultClient.Orders.Order(123).QueryParams["order"];
            var result2 = _defaultClient.Orders.FromOrder(123).QueryParams["from_order"];
            var result3 = _defaultClient.Orders.Order("123").QueryParams["order"];
            var result4 = _defaultClient.Orders.FromOrder("123").QueryParams["from_order"];

            Assert.Equal(result, 123);
            Assert.Equal(result2, 123);
            Assert.Equal(result3, "123");
            Assert.Equal(result4, "123");

        }

        [Fact]
        public void CustomerPass()
        {
            var result = _defaultClient.Orders.CustomerId(123).QueryParams["customer_id"];
            var result2 = _defaultClient.Orders.CustomerId(null).QueryParams["customer_id"];
            var result3 = _defaultClient.Orders.CustomerEmail("test").QueryParams["customer_email"];
            var result4 = _defaultClient.Orders.CustomerEmail(null).QueryParams["customer_email"];
            var result5 = _defaultClient.Orders.CustomerEmail("").QueryParams["customer_email"];

            Assert.Equal(result, 123);
            Assert.Equal(result2, "null");
            Assert.Equal(result3, "test");
            Assert.Equal(result4, "");
            Assert.Equal(result5, "");
        }

        [Fact]
        public void StatusesPass()
        {
            var result = _defaultClient.Orders.Statuses("PAID, DECLINED", "NEW PROCESSING").QueryParams["statuses"];
            Assert.Equal(result, "PAID,DECLINED,NEW,PROCESSING");
        }

        [Theory]
        [InlineData("PAID, DECLIN", "NEW PROCESSING")]
        [InlineData("PAID, DECLINED", "NEW PROCESS")]
        [InlineData("", "")]
        public void StatusesFail(string paid, string full)
        {
            Assert.Throws<InvalidArgumentException>(() => _defaultClient.Orders.Statuses(paid, full));
        }

        [Fact]
        public void AddPaymentStatusesPass()
        {
            var result = _defaultClient.Orders.AddPaymentStatuses("PAID, DECLINED").QueryParams["statuses"];
            Assert.Equal(result, "PAID,DECLINED");
        }

        [Fact]
        public void AddAddPaymentStatusesPass()
        {
            var result =
                _defaultClient.Orders.AddPaymentStatuses("PAID, DECLINED").AddPaymentStatuses("Cancelled").QueryParams[
                    "statuses"];
            Assert.Equal(result, "PAID,DECLINED,CANCELLED");
        }

        [Fact]
        public void AddFulfillmentStatusesPass()
        {
            var result = _defaultClient.Orders.AddFulfillmentStatuses("NEW PROCESSING").QueryParams["statuses"];
            Assert.Equal(result, "NEW,PROCESSING");
        }

        [Fact]
        public void AddAddFulfillmentStatusesPass()
        {
            var result =
                _defaultClient.Orders.AddFulfillmentStatuses("NEW").AddFulfillmentStatuses("PROCESSING").QueryParams["statuses"];
            Assert.Equal(result, "NEW,PROCESSING");
        }

        [Fact]
        public void AddBothStatusesPass()
        {
            var result =
                _defaultClient.Orders.AddPaymentStatuses("PAID, DECLINED").AddFulfillmentStatuses("NEW PROCESSING").QueryParams["statuses"];
            Assert.Equal(result, "PAID,DECLINED,NEW,PROCESSING");
        }

        [Fact]
        public void LimitOffsetPass()
        {
            var result = _defaultClient.Orders.Limit(123).QueryParams["limit"];
            var result2 = _defaultClient.Orders.Offset(100).QueryParams["offset"];

            Assert.Equal(result, 123);
            Assert.Equal(result2, 100);
        }
    }
}
