
using RapidPay.Models;

namespace RapidPay.Services
{
    public interface ITransactionServices
    {
        Transaction CreateTransaction(Card card, decimal amount);

        decimal GetBalance(Card card);
    }
}
