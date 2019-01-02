namespace StreamingTALib
{
    public class TrueRangeState
    {
        public TrueRangeState()
        {
        }

        public decimal PreviousLow { get; internal set; }
        public decimal PreviousClose { get; internal set; }
        public decimal TrueRange { get; internal set; }
    }
}