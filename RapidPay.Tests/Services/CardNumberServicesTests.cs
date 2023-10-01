using RapidPay.Services;
using System.Text.RegularExpressions;

namespace RapidPay.Tests.Services
{
    public class CardNumberServicesTests
    {
        [Fact]
        public void GenetateCardNumberTest()
        {
            // Arrange
            var cardNumberServices = new CardNumberServices();
            var pattern = new Regex("^\\d{15}$");

            // Act
            var number = cardNumberServices.GenetateCardNumber();

            // Assert
            Assert.Matches(pattern, number);
        }
    }
}
