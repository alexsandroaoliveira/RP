using RapidPay.Modules.CardManagement.Models;

namespace RapidPay.Modules.CardManagement.Services
{
    public interface ICardServices
    {
        Task<Card> CreateCardAsync();

        Task<Card?> GetCardAsync(string number);

        Task<IEnumerable<Card>> GetCardsAsync();
    }
}
