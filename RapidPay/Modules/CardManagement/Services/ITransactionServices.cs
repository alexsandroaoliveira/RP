using RapidPay.Modules.CardManagement.Models;

namespace RapidPay.Modules.CardManagement.Services
{
    public interface ITransactionServices
    {
        Task<Transaction> CreateTransactionAsync(Card card, decimal amount);

        Task<decimal> GetBalanceAsync(Card card);
    }
}
