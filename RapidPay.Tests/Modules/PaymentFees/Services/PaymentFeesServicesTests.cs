using Moq;
using RapidPay.Modules.PaymentFees.Services;
using UFE;

namespace RapidPay.Tests.Modules.PaymentFees.Services
{
    public class PaymentFeesServicesTests
    {
        [Fact]
        public void GetPaymentFee_FirstTime_Test()
        {
            // Arrange
            var client = new Mock<IUFEClient>();
            var service = new PaymentFeesServices(client.Object);

            client.Setup(o => o.GetCurrentFee()).Returns(2);

            // Act
            var fee = service.GetPaymentFee();

            // Assert
            Assert.Equal(2, fee);
            Assert.Equal(2, RapidPayContext.LastFee);
        }

        [Fact]
        public void GetPaymentFeeTest()
        {
            // Arrange
            var client = new Mock<IUFEClient>();
            var service = new PaymentFeesServices(client.Object);
            RapidPayContext.LastFee = 0.5m;

            client.Setup(o => o.GetCurrentFee()).Returns(1.5m);

            // Act
            var fee = service.GetPaymentFee();

            // Assert
            Assert.Equal(0.75m, fee);
            Assert.Equal(0.75m, RapidPayContext.LastFee);
        }
    }
}
