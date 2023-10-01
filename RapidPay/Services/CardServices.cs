using RapidPay.Data;
using RapidPay.Models;

namespace RapidPay.Services
{
    public class CardServices : ICardServices
    {

        private readonly ICardNumberServices _cardNumberServices;
        private readonly RapidPayRepository _repository;

        public CardServices(RapidPayRepository repository, ICardNumberServices cardNumberServices)
        {
            _repository = repository;
            _cardNumberServices = cardNumberServices;
        }

        public Card CreateCard()
        {
            var cardNumber = _cardNumberServices.GenetateCardNumber();

            if (_repository.Cards.Any(c => c.Number == cardNumber))
                return CreateCard();

            var newCard = new Card { Number = cardNumber };

            _repository.Cards.Add(newCard);

            return newCard;
        }

        public Card? GetCard(string number) => 
            _repository.Cards.FirstOrDefault(c => c.Number == number);

        public IEnumerable<Card> GetCards() => _repository.Cards;
    }
}
