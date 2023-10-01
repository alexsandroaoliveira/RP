using Moq;
using RapidPay.Data;
using RapidPay.Models;
using RapidPay.Services;

namespace RapidPay.Tests.Services
{
    public class CardServicesTests
    {
        [Fact]
        public void CreateCard_Tests()
        {
            // Arrange
            var repository = new RapidPayRepository();

            var mockCardNumberServices = new Mock<ICardNumberServices>();
            mockCardNumberServices.Setup(o => o.GenetateCardNumber())
                .Returns("000000000000001");

            var cardService = new CardServices(repository, mockCardNumberServices.Object);

            // Act
            var card1 = cardService.CreateCard();

            // Assert
            Assert.NotNull(card1);
            Assert.Equal("000000000000001", card1.Number);

            Assert.Single(repository.Cards);
            mockCardNumberServices.Verify(o => o.GenetateCardNumber(), Times.Once);
        }

        [Fact]
        public void CreateCard_ConflictResolveTests()
        {
            // Arrange
            var repository = new RapidPayRepository();
            repository.Cards.Add(new Card { Number = "000000000000001" });

            Queue<string> numbersQueue = new Queue<string>();
            numbersQueue.Enqueue("000000000000001");
            numbersQueue.Enqueue("000000000000002");

            var mockCardNumberServices = new Mock<ICardNumberServices>();
            mockCardNumberServices.Setup(o => o.GenetateCardNumber())
                .Returns(() => numbersQueue.Dequeue());

            var cardService = new CardServices(repository, mockCardNumberServices.Object);

            // Act
            var card1 = cardService.CreateCard();

            // Assert
            Assert.NotNull(card1);
            Assert.Equal("000000000000002", card1.Number);

            Assert.Equal(2, repository.Cards.Count);
            mockCardNumberServices.Verify(o => o.GenetateCardNumber(), Times.Exactly(2));
        }

        [Fact]
        public void GetCards()
        {
            // Arrange
            var repository = new RapidPayRepository();
            repository.Cards.Add(new Card { Number = "000000000000001" });
            repository.Cards.Add(new Card { Number = "000000000000002" });

            var mockCardNumberServices = new Mock<ICardNumberServices>();

            var cardService = new CardServices(repository, mockCardNumberServices.Object);

            // Act
            var cards = cardService.GetCards();

            // Assert
            Assert.NotNull(cards);
            Assert.Equal(2, cards.Count());
        }

        [Fact]
        public void GetCard_Existing()
        {
            // Arrange
            var repository = new RapidPayRepository();
            repository.Cards.Add(new Card { Number = "000000000000001" });
            repository.Cards.Add(new Card { Number = "000000000000002" });
            repository.Cards.Add(new Card { Number = "000000000000003" });

            var mockCardNumberServices = new Mock<ICardNumberServices>();

            var cardService = new CardServices(repository, mockCardNumberServices.Object);

            // Act
            var card = cardService.GetCard("000000000000002");

            // Assert
            Assert.NotNull(card);
            Assert.Equal("000000000000002", card.Number);
        }

        [Fact]
        public void GetCard_NonExisting()
        {
            // Arrange
            var repository = new RapidPayRepository();
            repository.Cards.Add(new Card { Number = "000000000000001" });
            repository.Cards.Add(new Card { Number = "000000000000002" });
            repository.Cards.Add(new Card { Number = "000000000000003" });

            var mockCardNumberServices = new Mock<ICardNumberServices>();

            var cardService = new CardServices(repository, mockCardNumberServices.Object);

            // Act
            var card = cardService.GetCard("000000000000004");

            // Assert
            Assert.Null(card);
        }
    }
}