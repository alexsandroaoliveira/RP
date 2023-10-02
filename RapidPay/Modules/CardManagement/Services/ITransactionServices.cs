using RapidPay.Modules.CardManagement.Models;

namespace RapidPay.Modules.CardManagement.Services
{
    /// <summary>
    /// Services to work with RapidPay Transaction 
    /// </summary>
    public interface ITransactionServices
    {
        /// <summary>
        /// Create a new transaction on RapidPay system
        /// </summary>
        /// <param name="card">Card Number</param>
        /// <param name="amount">Transaction amount</param>
        /// <returns>Created transactions</returns>
        Task<Transaction> CreateTransactionAsync(Card card, decimal amount);

        /// <summary>
        /// Get the card balance. Sum of transaction amount + fee from RapidPay transactions history.
        /// </summary>
        /// <param name="card">Card</param>
        /// <returns>Card transaction balance</returns>
        Task<decimal> GetBalanceAsync(Card card);
    }
}
