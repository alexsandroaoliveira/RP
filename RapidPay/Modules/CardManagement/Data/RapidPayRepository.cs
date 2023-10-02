using RapidPay.Modules.CardManagement.Models;
using System.Collections.Concurrent;

namespace RapidPay.Modules.CardManagement.Data
{
    /// <summary>
    /// Data Repository for RapidPay system. Storigin data in-memory using Concurrent Collections.
    /// </summary>
    public class RapidPayRepository
    {
        /// <summary>
        /// RapidPay Created Cards
        /// </summary>
        public ConcurrentBag<Card> Cards { get; set; } = new();
        
        /// <summary>
        /// RapidPay transactions history
        /// </summary>
        public ConcurrentBag<Transaction> Transactions { get; set; } = new();
    }
}
