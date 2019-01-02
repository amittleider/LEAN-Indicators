using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTALib
{
    public class WildersMovingAverage
    {
        private int period;

        public WildersMovingAverageState WildersFuckedMovingAverageState { get; private set; }

        public WildersMovingAverage(int period, decimal average)
        {
            this.period = period;
            this.WildersFuckedMovingAverageState = new WildersMovingAverageState();
            this.WildersFuckedMovingAverageState.Mean = average;
        }

        public WildersMovingAverageState IntegrateValue(decimal newPrice)
        {
            decimal newAverage = ((this.WildersFuckedMovingAverageState.Mean * (this.period - 1)) + newPrice) / this.period;
            this.WildersFuckedMovingAverageState.Mean = newAverage;

            return this.WildersFuckedMovingAverageState;
        }
    }
}
