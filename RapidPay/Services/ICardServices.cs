using RapidPay.Models;

namespace RapidPay.Services
{
    public interface ICardServices
    {
        Card CreateCard();

        Card? GetCard(string number);

        IEnumerable<Card> GetCards();
    }
}
