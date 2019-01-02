using System;
using System.Collections.Generic;
using System.Linq;

namespace StreamingTALib
{
    public class TrueRangeInitializer
    {
        public TrueRangeInitializer()
        {
        }

        public static int GetLookback()
        {
            return 1;
        }

        public TrueRange InitializeWithData(List<decimal> highs, List<decimal> lows, List<decimal> closes)
        {
            if (highs.Count() < GetLookback() || lows.Count() < GetLookback() || closes.Count() < GetLookback())
            {
                throw new Exception("Not enough values to initialize the true range");
            }

            TrueRange trueRange = new TrueRange();
            trueRange.ComputeTrueRange(highs[0], lows[0], closes[0]);

            return trueRange;
        }

    }
}