using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTALib
{
    public class AdxInitializer
    {
        private int period;

        public AdxInitializer(int period)
        {
            this.period = period;
        }

        public static int GetLookback(int period)
        {
            return period;
        }

        public Adx InitializeWithData(List<decimal> highs, List<decimal> lows, List<decimal> closes)
        {
            if (highs.Count < AdxInitializer.GetLookback(this.period))
            {
                throw new Exception("Cannot initialize the Adx. Not enough data.");
            }

            if (lows.Count < AdxInitializer.GetLookback(this.period))
            {
                throw new Exception("Cannot initialize the Adx. Not enough data.");
            }

            if (closes.Count < AdxInitializer.GetLookback(this.period))
            {
                throw new Exception("Cannot initialize the Adx. Not enough data.");
            }

            Adx adx = new Adx(this.period);

            for (int i = 0; i < highs.Count; i++)
            {
                adx.ComputeNextValue(highs[i], lows[i], closes[i]);
            }

            return adx;
        }
    }
}
