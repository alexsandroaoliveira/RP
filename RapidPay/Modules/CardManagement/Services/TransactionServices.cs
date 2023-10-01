using RapidPay.Modules.CardManagement.Data;
using RapidPay.Modules.CardManagement.Models;
using RapidPay.Modules.PaymentFees.Services;

namespace RapidPay.Modules.CardManagement.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly RapidPayRepository _repository;
        private readonly IPaymentFeesServices _paymentFeesServices;

        public TransactionServices(RapidPayRepository repository, IPaymentFeesServices paymentFeesServices)
        {
            _repository = repository;
            _paymentFeesServices = paymentFeesServices;
        }

        public  Task<Transaction> CreateTransactionAsync(Card card, decimal amount)
        {
            var newTransaction = new Transaction
            {
                Card = card,
                Amount = amount,
                Fee = _paymentFeesServices.GetPaymentFee()
            };

            _repository.Transactions.Add(newTransaction);

            return Task.FromResult(newTransaction);
        }

        public  Task<decimal> GetBalanceAsync(Card card) =>
            Task.FromResult(
                _repository.Transactions
                    .Where(o => o.Card == card)
                    .Sum(o => o.Amount + o.Fee));
    }
}
