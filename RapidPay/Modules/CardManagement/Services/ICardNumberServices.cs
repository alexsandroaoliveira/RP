namespace RapidPay.Modules.CardManagement.Services
{
    /// <summary>
    /// Services to work with Card Numbers. Generation and validation
    /// </summary>
    public interface ICardNumberServices
    {
        /// <summary>
        /// Genenerate a 15 digits random number
        /// </summary>
        /// <returns>15 digits randon number as String</returns>
        string GenetateCardNumber();

        /// <summary>
        /// Validate the Card Number. Should be a string with a 15 digits number.
        /// </summary>
        /// <param name="number">Card Number</param>
        /// <returns>True if valid</returns>
        bool IsValid(string number);
    }
}
