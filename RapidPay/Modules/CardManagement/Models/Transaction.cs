namespace RapidPay.Modules.CardManagement.Models
{
    /// <summary>
    /// RapidPay Transaction
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Card
        /// </summary>
        public Card Card { get; set; }

        /// <summary>
        /// Transaction Amount
        /// </summary>
        public decimal Amount { get; set; }


        /// <summary>
        /// Transaction Fee Amount
        /// </summary>
        public decimal Fee { get; set; } = 0;
    }
}
