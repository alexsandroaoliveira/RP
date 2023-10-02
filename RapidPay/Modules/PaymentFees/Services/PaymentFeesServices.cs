using UFE;

namespace RapidPay.Modules.PaymentFees.Services
{
    /// <summary>
    /// Services to work with Payment Fees. Using UFE integration.
    /// </summary>
    public class PaymentFeesServices : IPaymentFeesServices
    {
        private readonly IUFEClient _uFEClient;

        public PaymentFeesServices(IUFEClient uFEClient)
        {
            _uFEClient = uFEClient;
        }

        /// <summary>
        /// Get the current Payment Fee
        /// </summary>
        /// <returns>Payment fee</returns>
        public decimal GetPaymentFee(decimal lastFee)
        {
            // Call UFE to get current fee;
            var ufeFee = _uFEClient.GetCurrentFee();

            // Req: "The new fee price is the last fee amount multiplied by the recent random decimal"
            var fee = lastFee * ufeFee;

            return fee;
        }
    }
}
