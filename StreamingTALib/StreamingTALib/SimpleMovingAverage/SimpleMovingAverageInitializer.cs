using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTALib
{
    public class SimpleMovingAverageInitializer
    {
        private int period;

        public SimpleMovingAverageInitializer(int period)
        {
            this.period = period;
        }

        public static int GetLookback(int period)
        {
            return period;
        }

        public SimpleMovingAverage InitalizeWithData(List<decimal> values)
        {
            if (values.Count < SimpleMovingAverageInitializer.GetLookback(this.period))
            {
                throw new Exception("Cannot initialize the Simple Moving Average with this period. Not enough values.");
            }

            SimpleMovingAverage simpleMovingAverage = new SimpleMovingAverage(this.period);
            for (int i = 0; i < values.Count; i++)
            {
                simpleMovingAverage.IntegrateValue(values[i]);
            }

            return simpleMovingAverage;
        }
    }
}
