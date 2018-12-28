namespace StreamingTALib
{
    public class AdxState
    {
        /// <summary>
        /// Gets previous low
        /// </summary>
        public decimal PreviousLow;
        public decimal AverageDirectionalIndex;
        public decimal PreviousHigh;
        public decimal PreviousClose;

        public AdxState()
        {
            this.PreviousClose = 0.0m;
            this.PreviousHigh = 0.0m;
            this.PreviousLow = 0.0m;
            this.AverageDirectionalIndex = 0.0m;
        }

    }
}
