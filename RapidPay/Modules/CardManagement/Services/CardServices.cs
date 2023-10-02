using RapidPay.Modules.CardManagement.Data;
using RapidPay.Modules.CardManagement.Models;

namespace RapidPay.Modules.CardManagement.Services
{
    /// <summary>
    /// Services to work with card using RapidRayRepository.
    /// </summary>
    public class CardServices : ICardServices
    {

        private readonly ICardNumberServices _cardNumberServices;
        private readonly RapidPayRepository _repository;

        public CardServices(RapidPayRepository repository, ICardNumberServices cardNumberServices)
        {
            _repository = repository;
            _cardNumberServices = cardNumberServices;
        }

        /// <summary>
        /// Create a RapidPay Card with unique Card Number.
        /// </summary>
        /// <returns>RapidPay Card with unique Card Number</returns>
        public async Task<Card> CreateCardAsync()
        {
            var cardNumber = _cardNumberServices.GenetateCardNumber();

            // Check with the generated cardNumber already exist on the DataBase.
            if (_repository.Cards.Any(c => c.Number == cardNumber))
            {
                // Recursive call of CreateCardAsync, until generate a non-existing Card Number.
                return await CreateCardAsync();
            }

            var newCard = new Card { Number = cardNumber };

            // Add generated card to database before return it.
            _repository.Cards.Add(newCard);

            return newCard;
        }

        /// <summary>
        /// Get Card using Card Number 
        /// </summary>
        /// <param name="number"></param>
        /// <returns>RapidPay Card, return null if the card not exist</returns>
        public Task<Card?> GetCardAsync(string number)
        {
            // Check if the number is valid before quering
            if (!_cardNumberServices.IsValid(number))
                return Task.FromResult((Card)null);

            // Using Linq to find the card on database using the number.
            return Task.FromResult(_repository.Cards.SingleOrDefault(c => c.Number == number));
        }

        /// <summary>
        /// Get all RapidPay created cards
        /// </summary>
        /// <returns>List of all RapidPay created cards</returns>
        public Task<IEnumerable<Card>> GetCardsAsync() =>
            Task.FromResult((IEnumerable<Card>)_repository.Cards);
    }
}
