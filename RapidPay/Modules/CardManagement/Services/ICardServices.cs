using RapidPay.Modules.CardManagement.Models;

namespace RapidPay.Modules.CardManagement.Services
{
    /// <summary>
    /// Services to work with card using RapidRayRepository.
    /// </summary>
    public interface ICardServices
    {
        /// <summary>
        /// Create a RapidPay Card with unique Card Number.
        /// </summary>
        /// <returns>RapidPay Card with unique Card Number</returns>
        Task<Card> CreateCardAsync();

        /// <summary>
        /// Get Card using Card Number 
        /// </summary>
        /// <param name="number">Card Number</param>
        /// <returns>RapidPay Card, return null if the card not exist</returns>
        Task<Card?> GetCardAsync(string number);

        /// <summary>
        /// Get all RapidPay created cards
        /// </summary>
        /// <returns>List of all RapidPay created cards</returns>
        Task<IEnumerable<Card>> GetCardsAsync();
    }
}
