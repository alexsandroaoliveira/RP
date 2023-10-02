namespace RapidPay
{
    /// <summary>
    /// Static class to hold the System context.
    /// </summary>
    public static class RapidPayContext
    {
        private static readonly object LastFeeLocker = new();
        private static decimal _lastFee = 1;

        /// <summary>
        /// Tread-safe code to get and set the Last transaction Fee 
        /// </summary>
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
