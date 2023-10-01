using System.Text;

namespace RapidPay.Modules.CardManagement.Services
{
    public class CardNumberServices : ICardNumberServices
    {
        private const int DIGITS_NUMBER = 15;

        public string GenetateCardNumber()
        {
            var sb = new StringBuilder();
            var random = new Random();

            for (var i = 0; i < DIGITS_NUMBER; i++)
                sb.Append(random.Next(0, 9));

            return sb.ToString();
        }
    }
}
