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

        public Transaction CreateTransaction(Card card, decimal amount)
        {
            var newTransaction = new Transaction
            {
                Card = card,
                Amount = amount,
                Fee = _paymentFeesServices.GetPaymentFee()
            };

            _repository.Transactions.Add(newTransaction);

            return newTransaction;
        }

        public decimal GetBalance(Card card) =>
            _repository.Transactions
                .Where(o => o.Card == card)
                .Sum(o => o.Amount + o.Fee);
    }
}
