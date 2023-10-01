namespace RapidPay.Models
{
    public class Transaction
    {
        public Card Card { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; } = 0;
    }
}
