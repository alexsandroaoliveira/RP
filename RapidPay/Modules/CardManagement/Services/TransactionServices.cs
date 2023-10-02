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
        public async Task<Transaction> CreateTransactionAsync(Card card, decimal amount)
        {
            var lastTransaction = (await GetCardTransactions(card))
                .OrderBy(x => x.Date) //Preformance could be improved using sorted list
                .LastOrDefault();

            var newTransaction = new Transaction
            {
                Card = card,
                Amount = amount,
                Fee = _paymentFeesServices.GetPaymentFee(lastTransaction?.Fee ?? 1)
            };

            // Adding transaction to the Database.
            _repository.Transactions.Add(newTransaction);

            return newTransaction;
        }

        /// <summary>
        /// Get the card balance. Sum of transaction amount + fee from RapidPay transactions history.
        /// </summary>
        /// <param name="card">Card</param>
        /// <returns>Card transaction balance</returns>
        public async Task<decimal> GetBalanceAsync(Card card)
        {
            var cardTransactions = await GetCardTransactions(card);

            // Sum Amount and Fee to result the balance
            return cardTransactions.Sum(o => o.Amount + o.Fee);
        }

        /// <summary>
        /// Get all card's transaction
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public Task<IEnumerable<Transaction>> GetCardTransactions(Card card) =>
            Task.FromResult(_repository.Transactions
                // Using Linq to filter the card's transactions
                .Where(o => o.Card == card));

    }
}
