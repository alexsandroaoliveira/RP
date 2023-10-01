using RapidPay.Data;
using RapidPay.Models;

namespace RapidPay.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly RapidPayRepository _repository;

        public TransactionServices(RapidPayRepository repository)
        {
            _repository = repository;
        }

        public Transaction CreateTransaction(Card card, decimal amount)
        {
            var newTransaction = new Transaction
            {
                Card = card,
                Amount = amount,
            };

            _repository.Transactions.Add(newTransaction);

            return newTransaction;
        }

        public decimal GetBalance(Card card) =>
            _repository.Transactions
                .Where(o => o.Card == card)
                .Sum(o => o.Amount);
    }
}
