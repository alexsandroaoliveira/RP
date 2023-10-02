using RapidPay.Modules.CardManagement.Data;
using RapidPay.Modules.CardManagement.Models;
using RapidPay.Modules.PaymentFees.Services;

namespace RapidPay.Modules.CardManagement.Services
{
    /// <summary>
    /// Services to work with RapidPay Transaction 
    /// </summary>
    public class TransactionServices : ITransactionServices
    {
        private readonly RapidPayRepository _repository;
        private readonly IPaymentFeesServices _paymentFeesServices;

        public TransactionServices(RapidPayRepository repository, IPaymentFeesServices paymentFeesServices)
        {
            _repository = repository;
            _paymentFeesServices = paymentFeesServices;
        }

        /// <summary>
        /// Create a new transaction on RapidPay system. Get transaction fee from PaymentFee Module.
        /// </summary>
        /// <param name="card">Card Number</param>
        /// <param name="amount">Transaction amount</param>
        /// <returns>Created transactions</returns>
        public Task<Transaction> CreateTransactionAsync(Card card, decimal amount)
        {
            var newTransaction = new Transaction
            {
                Card = card,
                Amount = amount,
                Fee = _paymentFeesServices.GetPaymentFee()
            };

            // Adding transaction to the Database.
            _repository.Transactions.Add(newTransaction);

            return Task.FromResult(newTransaction);
        }

        /// <summary>
        /// Get the card balance. Sum of transaction amount + fee from RapidPay transactions history.
        /// </summary>
        /// <param name="card">Card</param>
        /// <returns>Card transaction balance</returns>
        public Task<decimal> GetBalanceAsync(Card card) =>
            Task.FromResult(
                _repository.Transactions
                    // Using Linq to filter the card's transactions
                    .Where(o => o.Card == card)
                    // Sum Amount and Fee to result the balance
                    .Sum(o => o.Amount + o.Fee));
    }
}
