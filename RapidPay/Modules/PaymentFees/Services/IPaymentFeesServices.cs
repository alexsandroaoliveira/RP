namespace RapidPay.Modules.PaymentFees.Services
{
    /// <summary>
    /// Services to work with Payment Fees
    /// </summary>
    public interface IPaymentFeesServices
    {
        /// <summary>
        /// Get the current Payment Fee
        /// </summary>
        /// <returns>Payment fee</returns>
        decimal GetPaymentFee();
    }
}
