using RapidPay.Modules.CardManagement.Data;
using RapidPay.Modules.CardManagement.Models;

namespace RapidPay.Modules.CardManagement.Services
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

        public async Task<Card> CreateCardAsync()
        {
            var cardNumber = _cardNumberServices.GenetateCardNumber();

            if (_repository.Cards.Any(c => c.Number == cardNumber))
                return await CreateCardAsync();

            var newCard = new Card { Number = cardNumber };

            _repository.Cards.Add(newCard);

            return newCard;
        }

        public Task<Card?> GetCardAsync(string number) =>
            Task.FromResult(_repository.Cards.FirstOrDefault(c => c.Number == number));

        public Task<IEnumerable<Card>> GetCardsAsync() =>
            Task.FromResult((IEnumerable<Card>)_repository.Cards);
    }
}
