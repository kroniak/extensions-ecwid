// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Ecwid.Test.Services
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class QueryBuilderExtensionsTest
    {
        private readonly IEcwidClient _defaultClient = new EcwidClient();

        [Fact]
        public void AddOrUpdateStatuses_Exception() =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.AddOrUpdateStatuses("", null));

        [Fact]
        public void AddFulfillmentStatuses_ReturnCorrectResult()
        {
            var result =
                _defaultClient.Orders.FulfillmentStatuses("AWAITING_PROCESSING").FulfillmentStatuses("PROCESSING")
                    .GetParam("fulfillmentStatus");
            Assert.Equal("AWAITING_PROCESSING,PROCESSING", result);
        }

        [Fact]
        public void AddTwicePaymentStatuses_ReturnCorrectResult()
        {
            var result =
                _defaultClient.Orders.PaymentStatuses("PAID").PaymentStatuses("CANCELLED").GetParam(
                    "paymentStatus");
            Assert.Equal("PAID,CANCELLED", result);
        }

        [Fact]
        public void CouponCode_ReturnCorrectResult()
        {
            var result = _defaultClient.Orders.CouponCode(1).GetParam("couponCode");

            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void CouponCode_Exception(int i) =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.CouponCode(i));

        [Fact]
        public void Custom_ReturnCorrectResult()
        {
            var result = _defaultClient.Orders.Custom("date", "test").GetParam("date");
            Assert.Equal("test", result);
        }

        [Fact]
        public void Customer_ReturnCorrectResult()
        {
            var result = _defaultClient.Orders.Customer("test").GetParam("customer");

            Assert.Equal("test", result);
        }

        [Fact]
        public void Customer_Exception() => Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Customer(""));

        [Fact]
        public void Custom_Exception()
        {
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Custom(null, new {a = 1}));
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Custom(null, null));
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Custom("", new {a = 1}));
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Custom(" ", new {a = 1}));
        }

        [Theory]
        [InlineData("2015-04-22")]
        [InlineData("2015-04-22 18:48:38")]
        [InlineData("2015-04-22 18:48:38 -0500")]
        [InlineData("1447804800")]
        public void DateTrue_ReturnNotNull(string date) =>
            Assert.NotNull(_defaultClient.Orders.CreatedFrom(date).GetParam("createdFrom"));

        [Theory]
        [InlineData("2015-00-22")]
        [InlineData("2015-00-22 18:48:38")]
        [InlineData("2015-00-22 18:48:38 -0500")]
        [InlineData("-1")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("1111111111111111111111111111111111111111111111111111111111")]
        public void Date_Exception(string date)
            => Assert.Throws<ArgumentException>(() => _defaultClient.Orders.CreatedFrom(date).GetParam("createdFrom"));

        [Fact]
        public void Date_ReturnCorrectResult()
        {
            var date = new DateTime(2015, 4, 22, 18, 48, 38);
            const string check = "2015-04-22 18:48:38";

            var result = _defaultClient.Orders.CreatedFrom(date).GetParam("createdFrom");
            var result2 = _defaultClient.Orders.CreatedTo(date).GetParam("createdTo");
            var result3 = _defaultClient.Orders.UpdatedFrom(date).GetParam("updatedFrom");
            var result4 = _defaultClient.Orders.UpdatedTo(date).GetParam("updatedTo");

            var qb = _defaultClient.Orders.Created(date, date);
            var result5 = qb.GetParam("createdFrom");
            var result6 = qb.GetParam("createdTo");

            var qb2 = _defaultClient.Orders.Updated(date, date);
            var result7 = qb2.GetParam("updatedFrom");
            var result8 = qb2.GetParam("updatedTo");

            Assert.Equal(check, result);
            Assert.Equal(check, result2);
            Assert.Equal(check, result3);
            Assert.Equal(check, result4);
            Assert.Equal(check, result5);
            Assert.Equal(check, result6);
            Assert.Equal(check, result7);
            Assert.Equal(check, result8);
        }

        [Theory]
        [InlineData("2015-04-22")]
        [InlineData("2015-04-22 18:48:38")]
        [InlineData("2015-04-22 18:48:38 -0500")]
        [InlineData("1447804800")]
        public void DateString_CorrectString_ReturnCorrectResult(string date)
        {
            var result = _defaultClient.Orders.CreatedFrom(date).GetParam("createdFrom");
            var result2 = _defaultClient.Orders.CreatedTo(date).GetParam("createdTo");
            var result3 = _defaultClient.Orders.UpdatedFrom(date).GetParam("updatedFrom");
            var result4 = _defaultClient.Orders.UpdatedTo(date).GetParam("updatedTo");

            var qb = _defaultClient.Orders.Created(date, date);
            var result5 = qb.GetParam("createdFrom");
            var result6 = qb.GetParam("createdTo");

            var qb2 = _defaultClient.Orders.Updated(date, date);
            var result7 = qb2.GetParam("updatedFrom");
            var result8 = qb2.GetParam("updatedTo");

            Assert.Equal(date, result);
            Assert.Equal(date, result2);
            Assert.Equal(date, result3);
            Assert.Equal(date, result4);
            Assert.Equal(date, result5);
            Assert.Equal(date, result6);
            Assert.Equal(date, result7);
            Assert.Equal(date, result8);
        }

        [Theory]
        [InlineData("2015-13-22")]
        [InlineData("")]
        public void DateStringCreated_IncorrectFrom_Exception(string date) =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Created(date, "2015-04-22"));

        [Theory]
        [InlineData("2015-13-22")]
        [InlineData("")]
        public void DateStringCreated_IncorrectTo_Exception(string date) =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Created("2015-04-22", date));

        [Theory]
        [InlineData("2015-13-22")]
        [InlineData("")]
        public void DateStringUpdated_IncorrectFrom_Exception(string date) =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Updated(date, "2015-04-22"));

        [Theory]
        [InlineData("2015-13-22")]
        [InlineData("")]
        public void DateStringUpdated_IncorrectTo_Exception(string date) =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Updated("2015-04-22", date));

        [Fact]
        public void FulfillmentStatuses_ReturnCorrectResult()
        {
            var result =
                _defaultClient.Orders.FulfillmentStatuses("AWAITING_PROCESSING PROCESSING")
                    .GetParam("fulfillmentStatus");
            Assert.Equal("AWAITING_PROCESSING,PROCESSING", result);
        }

        [Theory]
        [InlineData("PAID, DECLINE")]
        [InlineData("")]
        public void FulfillmentStatuses_Exception(string paid) =>
            Assert.Throws<EcwidConfigException>(() => _defaultClient.Orders.FulfillmentStatuses(paid));

        [Fact]
        public void Keywords_ReturnCorrectResult()
        {
            var result = _defaultClient.Orders.Keywords("John").GetParam("keywords");

            Assert.Equal("John", result);
        }

        [Fact]
        public void Keywords_Exception()
        {
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Keywords(""));
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Keywords(null));
        }

        [Fact]
        public void Limit_12_ReturnCorrectResult()
        {
            var result = _defaultClient.Orders.Limit(12).GetParam("limit");

            Assert.Equal(12, result);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(101)]
        public void Limit_101_ReturnCorrectResult(int limit)
        {
            var result = _defaultClient.Orders.Limit(limit).GetParam("limit");

            Assert.Equal(100, result);
        }

        [Fact]
        public void Offset_ReturnCorrectResult()
        {
            var result = _defaultClient.Orders.Offset(100).GetParam("offset");

            Assert.Equal(100, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void LimitAndOffset_IncorrectLimit_Exception(int i)
        {
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Limit(i));
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Offset(i));
        }

        [Fact]
        public void Methods_ReturnCorrectResult()
        {
            var result = _defaultClient.Orders.PaymentMethod("test").GetParam("paymentMethod");
            var result2 = _defaultClient.Orders.ShippingMethod("test").GetParam("shippingMethod");

            Assert.Equal("test", result);
            Assert.Equal("test", result2);
        }

        [Fact]
        public void Methods_Exception()
        {
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.PaymentMethod(null));
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.PaymentMethod(""));
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.ShippingMethod(null));
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.ShippingMethod(""));
        }

        [Fact]
        public void Order_ReturnCorrectResult()
        {
            var result = _defaultClient.Orders.Order(1).GetParam("orderNumber");
            var result2 = _defaultClient.Orders.Order("test").GetParam("vendorOrderNumber");

            Assert.Equal(1, result);
            Assert.Equal("test", result2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Order_InvalidInt_Exception(int i) =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Order(i));

        [Fact]
        public void Order_EmptyString_Exception() =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Order(""));

        [Fact]
        public void PaymentStatuses_ReturnCorrectResult()
        {
            var result = _defaultClient.Orders.PaymentStatuses("PAID, CANCELLED").GetParam("paymentStatus");
            Assert.Equal("PAID,CANCELLED", result);
        }

        [Theory]
        [InlineData("PAID, DECLINE")]
        [InlineData("")]
        public void PaymentStatuses_Exception(string paid) =>
            Assert.Throws<EcwidConfigException>(() => _defaultClient.Orders.PaymentStatuses(paid));

        [Fact]
        public void Totals_ReturnCorrectResult()
        {
            var qb = _defaultClient.Orders.Totals(1, 1);
            var result = qb.GetParam("totalFrom");
            var result2 = qb.GetParam("totalTo");
            var result3 = _defaultClient.Orders.TotalFrom(1).GetParam("totalFrom");
            var result4 = _defaultClient.Orders.TotalTo(1).GetParam("totalTo");

            Assert.Equal(1.0, result);
            Assert.Equal(1.0, result2);
            Assert.Equal(1.0, result3);
            Assert.Equal(1.0, result4);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Totals_FromIncorrect_Exception(int i) =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Totals(i, 1));

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Totals_ToIncorrect_Exception(int i) =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.Totals(1, i));

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void TotalFrom_Incorrect_Exception(int i) =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.TotalFrom(i));

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void TotalTo_Incorrect_Exception(int i) =>
            Assert.Throws<ArgumentException>(() => _defaultClient.Orders.TotalTo(i));
    }
}