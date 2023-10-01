using RapidPay.Models;
using System.Collections.Concurrent;

namespace RapidPay.Data
{
    public class RapidPayRepository
    {
        public ConcurrentBag<Card> Cards { get; set; } = new();
        public ConcurrentBag<Transaction> Transactions { get; set; } = new();
    }
}
