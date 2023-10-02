using System.Text;
using System.Text.RegularExpressions;

namespace RapidPay.Modules.CardManagement.Services
{
    /// <summary>
    /// Services to work with Card Numbers. Generation and validation
    /// </summary>
    public class CardNumberServices : ICardNumberServices
    {
        private const int DIGITS_NUMBER = 15;

        /// <summary>
        /// Genenerate a 15 digits random number
        /// </summary>
        /// <returns>15 digits randon number as String</returns>
        public string GenetateCardNumber()
        {
            var sb = new StringBuilder();
            var random = new Random();

            for (var i = 0; i < DIGITS_NUMBER; i++)
                sb.Append(random.Next(0, 9));

            return sb.ToString();
        }

        /// <summary>
        /// Validate the Card Number. Should be a string with a 15 digits number.
        /// </summary>
        /// <param name="number">Card Number</param>
        /// <returns>True if valid</returns>
        public bool IsValid(string number)
        {
            var pattern = new Regex("^\\d{15}$");
            var match = pattern.Match(number);
            return match.Success;
        }
    }
}
