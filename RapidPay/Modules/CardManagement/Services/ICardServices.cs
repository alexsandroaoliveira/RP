using RapidPay.Modules.CardManagement.Models;

namespace RapidPay.Modules.CardManagement.Services
{
    public interface ICardServices
    {
        Card CreateCard();

        Card? GetCard(string number);

        IEnumerable<Card> GetCards();
    }
}
