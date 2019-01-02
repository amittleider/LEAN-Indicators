using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamingTALib
{
    public class DirectionalMovementAggregateInitializer
    {
        private int period;

        public DirectionalMovementAggregateInitializer(int period)
        {
            this.period = period;
        }

        public DirectionalMovementAggregate InitalizeWithData(List<decimal> values)
        {
            if (values.Count < SimpleMovingAverageInitializer.GetLookback(this.period) - 1)
            {
                throw new Exception("Cannot initialize the Simple Moving Average with this period. Not enough values.");
            }

            // TODO: This will fail if you try to initialize with more than the exact correct lookback value.
            // Need to run some iterations if there are more values given in the initializer
            DirectionalMovementAggregate wilderMovingAverage = new DirectionalMovementAggregate(this.period, values.Sum());
            return wilderMovingAverage;
        }
    }
}
