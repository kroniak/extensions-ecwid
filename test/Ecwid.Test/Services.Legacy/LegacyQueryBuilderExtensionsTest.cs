// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using Ecwid.Legacy;
using Xunit;

namespace Ecwid.Test.Services.Legacy
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class LegacyQueryBuilderExtensionsTest
    {
        private readonly IEcwidLegacyClient _defaultLegacyClient = new EcwidLegacyClient();

        [Fact]
        public void Extension_IncorrectInt_Exception()
        {
            var exception = Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.Order(null));
            Assert.Contains("VendorNumber is null or empty.", exception.Message);
        }

        [Fact]
        public void Extension_IncorrectInt_InvalidArgument_2Part_Exception()
        {
            var exception = Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.FromOrder(null));
            Assert.Contains("VendorNumber is null or empty.", exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Extension_IncorrectInt_InvalidArgument_3Part_Exception(int i)
        {
            var exception = Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.Order(i));
            Assert.Contains("Number must be greater than 0", exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Extension_IncorrectInt_InvalidArgument_4Part_Exception(int i)
        {
            var exception = Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.FromOrder(i));
            Assert.Contains("Number must be greater than 0", exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Extension_IncorrectInt_InvalidArgument_5Part_Exception(int i)
        {
            var exception = Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.Limit(i));
            Assert.Contains("Limit must be greater than 0", exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Extension_IncorrectInt_InvalidArgument_6Part_Exception(int i)
        {
            var exception = Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.Offset(i));
            Assert.Contains("Offset must be greater than 0.", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ExtensionOrder_IncorrectString_Exception(string i)
        {
            var exception = Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.Order(i));

            Assert.Contains("VendorNumber is null or empty.", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ExtensionFromOrder_IncorrectString_Exception(string i)
        {
            var exception = Assert.Throws<ArgumentException>(() => _defaultLegacyClient.Orders.FromOrder(i));

            Assert.Contains("VendorNumber is null or empty.", exception.Message);
        }

        [Fact]
        public void Date_ReturnCorrectResult()
        {
            var result = _defaultLegacyClient.Orders.Date(Convert.ToDateTime("2000-01-01")).GetParam("date");
            var result2 = _defaultLegacyClient.Orders.FromDate(Convert.ToDateTime("2000-01-01")).GetParam("from_date");
            var result3 = _defaultLegacyClient.Orders.ToDate(Convert.ToDateTime("2000-01-01")).GetParam("to_date");
            var result4 =
                _defaultLegacyClient.Orders.FromUpdateDate(Convert.ToDateTime("2000-01-01"))
                    .GetParam("from_update_date");
            var result5 =
                _defaultLegacyClient.Orders.ToUpdateDate(Convert.ToDateTime("2000-01-01")).GetParam("to_update_date");

            Assert.Equal("2000-01-01", result);
            Assert.Equal("2000-01-01", result2);
            Assert.Equal("2000-01-01", result3);
            Assert.Equal("2000-01-01", result4);
            Assert.Equal("2000-01-01", result5);
        }

        [Fact]
        public void Order_ReturnCorrectResult()
        {
            var result = _defaultLegacyClient.Orders.Order(1).GetParam("order");
            var result2 = _defaultLegacyClient.Orders.FromOrder(1).GetParam("from_order");
            var result3 = _defaultLegacyClient.Orders.Order("1").GetParam("order");
            var result4 = _defaultLegacyClient.Orders.FromOrder("1").GetParam("from_order");

            Assert.Equal(1, result);
            Assert.Equal(1, result2);
            Assert.Equal("1", result3);
            Assert.Equal("1", result4);
        }

        [Fact]
        public void Customer_ReturnCorrectResult()
        {
            var result = _defaultLegacyClient.Orders.CustomerId(1).GetParam("customer_id");
            var result2 = _defaultLegacyClient.Orders.CustomerId(null).GetParam("customer_id");
            var result3 = _defaultLegacyClient.Orders.CustomerEmail("test").GetParam("customer_email");
            var result4 = _defaultLegacyClient.Orders.CustomerEmail(null).GetParam("customer_email");
            var result5 = _defaultLegacyClient.Orders.CustomerEmail("").GetParam("customer_email");

            Assert.Equal(1, result);
            Assert.Equal("null", result2);
            Assert.Equal("test", result3);
            Assert.Equal("", result4);
            Assert.Equal("", result5);
        }

        [Fact]
        public void Statuses_ReturnCorrectResult()
        {
            var result = _defaultLegacyClient.Orders.Statuses("PAID, DECLINED", "NEW PROCESSING").GetParam("statuses");
            Assert.Equal("PAID,DECLINED,NEW,PROCESSING", result);
        }

        [Theory]
        [InlineData("PAID, DECLIN", "NEW PROCESSING")]
        [InlineData("PAID, DECLINED", "NEW PROCESS")]
        [InlineData("", "")]
        public void Statuses_Exception(string paid, string full)
        {
            var exception = Assert.Throws<EcwidConfigException>(() => _defaultLegacyClient.Orders.Statuses(paid, full));

            Assert.Equal("Can not add or update statuses. Look inner exception.", exception.Message);
            Assert.Contains("Can not add or update statuses. Look inner exception.", exception.Message);
            Assert.Contains("Statuses string is invalid.", exception.InnerException.Message);
        }

        [Theory]
        [InlineData("DECLINED, CANCELLED")]
        [InlineData("DECLINED,CANCELLED")]
        [InlineData("DECLINED ,CANCELLED")]
        [InlineData("DECLINED CANCELLED")]
        public void PaymentStatuses_ReturnCorrectResult(string statuses)
        {
            var result = _defaultLegacyClient.Orders.PaymentStatuses(statuses).GetParam("statuses");
            Assert.Equal("DECLINED,CANCELLED", result);
        }

        [Theory]
        [InlineData("PAID, DECLINE")]
        [InlineData("DECLINED, CANCEL")]
        [InlineData("DECLINED,CANCEL")]
        [InlineData("DECLINED ,CANCEL")]
        [InlineData("DECLINED CANCELLED FAIL")]
        [InlineData("")]
        public void PaymentStatuses_Exception(string paid)
        {
            var exception =
                Assert.Throws<EcwidConfigException>(() => _defaultLegacyClient.Orders.PaymentStatuses(paid));
            Assert.Equal("Can not add or update statuses. Look inner exception.", exception.Message);
            Assert.Contains("Can not add or update statuses. Look inner exception.", exception.Message);
            Assert.Contains("Statuses string is invalid.", exception.InnerException.Message);
        }

        [Fact]
        public void AddTwicePaymentStatuses_ReturnCorrectResult()
        {
            var result =
                _defaultLegacyClient.Orders.PaymentStatuses("PAID, DECLINED").PaymentStatuses("Cancelled").GetParam(
                    "statuses");
            Assert.Equal("PAID,DECLINED,CANCELLED", result);
        }

        [Fact]
        public void FulfillmentStatuses_ReturnCorrectResult()
        {
            var result = _defaultLegacyClient.Orders.FulfillmentStatuses("NEW PROCESSING").GetParam("statuses");
            Assert.Equal("NEW,PROCESSING", result);
        }

        [Fact]
        public void AddFulfillmentStatuses_ReturnCorrectResult()
        {
            var result =
                _defaultLegacyClient.Orders.FulfillmentStatuses("NEW").FulfillmentStatuses("PROCESSING").GetParam(
                    "statuses");
            Assert.Equal("NEW,PROCESSING", result);
        }

        [Theory]
        [InlineData("PAID, DECLINE")]
        [InlineData("")]
        public void FulfillmentStatuses_Exception(string paid)
        {
            var exception =
                Assert.Throws<EcwidConfigException>(() => _defaultLegacyClient.Orders.FulfillmentStatuses(paid));

            Assert.Equal("Can not add or update statuses. Look inner exception.", exception.Message);
            Assert.Contains("Can not add or update statuses. Look inner exception.", exception.Message);
            Assert.Contains("Statuses string is invalid.", exception.InnerException.Message);
        }

        [Fact]
        public void AddBothStatuses_ReturnCorrectResult()
        {
            var result =
                _defaultLegacyClient.Orders.PaymentStatuses("PAID, DECLINED")
                    .FulfillmentStatuses("NEW PROCESSING")
                    .GetParam("statuses");
            Assert.Equal("PAID,DECLINED,NEW,PROCESSING", result);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(201)]
        public void Limit_201_ReturnCorrectResult(int limit)
        {
            var result = _defaultLegacyClient.Orders.Limit(limit).GetParam("limit");

            Assert.Equal(200, result);
        }

        [Theory]
        [InlineData(120)]
        public void Limit_120_ReturnCorrectResult(int limit)
        {
            var result = _defaultLegacyClient.Orders.Limit(limit).GetParam("limit");

            Assert.Equal(120, result);
        }
    }
}