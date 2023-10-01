using RapidPay.Modules.CardManagement.Models;

namespace RapidPay.Modules.CardManagement.Services
{
    public interface ITransactionServices
    {
        Transaction CreateTransaction(Card card, decimal amount);

        decimal GetBalance(Card card);
    }
}
