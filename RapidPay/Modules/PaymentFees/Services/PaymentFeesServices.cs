using UFE;

namespace RapidPay.Modules.PaymentFees.Services
{
    public class PaymentFeesServices : IPaymentFeesServices
    {
        private readonly IUFEClient _uFEClient;

        public PaymentFeesServices(IUFEClient uFEClient)
        {
            _uFEClient = uFEClient;
        }

        public decimal GetPaymentFee()
        {
            var ufeFee = _uFEClient.GetCurrentFee();

            var fee = RapidPayContext.LastFee * ufeFee;

            RapidPayContext.LastFee = fee;

            return fee;
        }
    }
}
