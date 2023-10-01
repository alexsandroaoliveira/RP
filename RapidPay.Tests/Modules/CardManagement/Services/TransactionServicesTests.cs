using RapidPay.Modules.CardManagement.Data;
using RapidPay.Modules.CardManagement.Models;
using RapidPay.Modules.CardManagement.Services;

namespace RapidPay.Tests.Modules.CardManagement.Services
{
    public class TransactionServicesTests
    {
        [Fact]
        public void CreateTransaction_Tests()
        {
            // Arrange
            var card = new Card { Number = "000000000000001" };
            decimal amount = 10;

            var repository = new RapidPayRepository();

            var service = new TransactionServices(repository);

            // Act
            var transaction = service.CreateTransaction(card, amount);

            // Assert
            Assert.NotNull(transaction);
            Assert.Equal(card, transaction.Card);
            Assert.Equal(amount, transaction.Amount);

            Assert.Single(repository.Transactions);
        }

        [Fact]
        public void GetBalance_Tests()
        {
            // Arrange
            var repository = new RapidPayRepository();
            var service = new TransactionServices(repository);

            var card1 = new Card { Number = "000000000000001" };
            var card2 = new Card { Number = "000000000000002" };
            var card3 = new Card { Number = "000000000000003" };

            service.CreateTransaction(card1, 10);
            service.CreateTransaction(card2, 10);
            service.CreateTransaction(card1, 10);
            service.CreateTransaction(card3, 10);
            service.CreateTransaction(card1, 10);
            service.CreateTransaction(card2, 10);
            service.CreateTransaction(card1, 10);

            // Act
            var balanceCard1 = service.GetBalance(card1);
            var balanceCard2 = service.GetBalance(card2);
            var balanceCard3 = service.GetBalance(card3);

            // Assert
            Assert.Equal(40, balanceCard1);
            Assert.Equal(20, balanceCard2);
            Assert.Equal(10, balanceCard3);
        }
    }
}
