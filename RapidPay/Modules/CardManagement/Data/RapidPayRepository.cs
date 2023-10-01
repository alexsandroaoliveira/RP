using RapidPay.Modules.CardManagement.Models;
using System.Collections.Concurrent;

namespace RapidPay.Modules.CardManagement.Data
{
    public class RapidPayRepository
    {
        public ConcurrentBag<Card> Cards { get; set; } = new();
        public ConcurrentBag<Transaction> Transactions { get; set; } = new();
    }
}
