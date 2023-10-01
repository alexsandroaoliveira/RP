namespace RapidPay
{
    public static class RapidPayContext
    {
        private static readonly object LastFeeLocker = new();
        private static decimal _lastFee = 1;

        public static decimal LastFee
        {
            get
            {
                lock (LastFeeLocker)
                {
                    return _lastFee;
                }
            }
            set
            {
                lock (LastFeeLocker)
                {
                    _lastFee = value;
                }
            }
        }
    }
}
