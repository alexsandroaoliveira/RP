using Moq;
using RapidPay.Modules.CardManagement.Data;
using RapidPay.Modules.CardManagement.Models;
using RapidPay.Modules.CardManagement.Services;
using RapidPay.Modules.PaymentFees.Services;

namespace RapidPay.Tests.Modules.CardManagement.Services
{
    public class TransactionServicesTests
    {
        [Fact]
        public async Task CreateTransaction_Tests()
        {
            // Arrange
            var card = new Card { Number = "000000000000001" };
            decimal amount = 10;
            decimal fee = 1.5m;

            var repository = new RapidPayRepository();
            var mockPaymentFeeService = new Mock<IPaymentFeesServices>();
            mockPaymentFeeService.Setup(o => o.GetPaymentFee(It.IsAny<decimal>())).Returns(fee);

            var service = new TransactionServices(repository, mockPaymentFeeService.Object);

            // Act
            var transaction = await service.CreateTransactionAsync(card, amount);

            // Assert
            Assert.NotNull(transaction);
            Assert.Equal(card, transaction.Card);
            Assert.Equal(amount, transaction.Amount);
            Assert.Equal(fee, transaction.Fee);

            Assert.Single(repository.Transactions);
        }

        [Fact]
        public async Task GetBalance_Tests()
        {
            // Arrange
            var repository = new RapidPayRepository();

            var mockPaymentFeeService = new Mock<IPaymentFeesServices>();
            mockPaymentFeeService.Setup(o => o.GetPaymentFee(It.IsAny<decimal>())).Returns(1.5m);

            var service = new TransactionServices(repository, mockPaymentFeeService.Object);

            var card1 = new Card { Number = "000000000000001" };
            var card2 = new Card { Number = "000000000000002" };
            var card3 = new Card { Number = "000000000000003" };

            await service.CreateTransactionAsync(card1, 10);
            await service.CreateTransactionAsync(card2, 10);
            await service.CreateTransactionAsync(card1, 10);
            await service.CreateTransactionAsync(card3, 10);
            await service.CreateTransactionAsync(card1, 10);
            await service.CreateTransactionAsync(card2, 10);
            await service.CreateTransactionAsync(card1, 10);

            // Act
            var balanceCard1 = await service.GetBalanceAsync(card1);
            var balanceCard2 = await service.GetBalanceAsync(card2);
            var balanceCard3 = await service.GetBalanceAsync(card3);

            // Assert
            Assert.Equal(46m, balanceCard1);
            Assert.Equal(23m, balanceCard2);
            Assert.Equal(11.5m, balanceCard3);
        }

        [Fact]
        public async Task LastFee_Tests()
        {
            // Arrange
            var repository = new RapidPayRepository();

            var feesList = new Queue<decimal>();
            feesList.Enqueue(0.5m);
            feesList.Enqueue(0.1m);
            feesList.Enqueue(1.4m);
            feesList.Enqueue(0.1m);
            feesList.Enqueue(1.1m);
            feesList.Enqueue(0.9m);
            feesList.Enqueue(1.0m);

            var mockPaymentFeeService = new Mock<IPaymentFeesServices>();
            mockPaymentFeeService.Setup(o => o.GetPaymentFee(It.IsAny<decimal>()))
                .Returns(() => feesList.Dequeue());

            var service = new TransactionServices(repository, mockPaymentFeeService.Object);

            var card1 = new Card { Number = "000000000000001" };
            var card2 = new Card { Number = "000000000000002" };
            var card3 = new Card { Number = "000000000000003" };

            decimal LastFee(Card card) =>
                repository?.Transactions.Where(o => o.Card == card)?
                .OrderBy(x=>x.Date)
                .LastOrDefault()?.Fee ?? -1;

            // Act
            await service.CreateTransactionAsync(card1, 10);
            await service.CreateTransactionAsync(card2, 10);
            await service.CreateTransactionAsync(card1, 10);
            await service.CreateTransactionAsync(card3, 10);
            await service.CreateTransactionAsync(card1, 10);
            await service.CreateTransactionAsync(card2, 10);
            await service.CreateTransactionAsync(card1, 10);

            // Assert
            Assert.Equal(1.0m, LastFee(card1));
            Assert.Equal(0.9m, LastFee(card2));
            Assert.Equal(0.1m, LastFee(card3));
        }
    }
}
