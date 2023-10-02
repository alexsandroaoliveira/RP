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
            var fee = service.GetPaymentFee(1);

            // Assert
            Assert.Equal(2, fee);
        }

        [Fact]
        public void GetPaymentFeeTest()
        {
            // Arrange
            var client = new Mock<IUFEClient>();
            var service = new PaymentFeesServices(client.Object);

            client.Setup(o => o.GetCurrentFee()).Returns(1.5m);

            // Act
            var fee = service.GetPaymentFee(0.5m);

            // Assert
            Assert.Equal(0.75m, fee);
        }
    }
}
