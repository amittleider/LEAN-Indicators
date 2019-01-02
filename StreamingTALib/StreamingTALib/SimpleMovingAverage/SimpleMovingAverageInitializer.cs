// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Initializes a simple moving average indicator
    /// </summary>
    public class SimpleMovingAverageInitializer
    {
        private int period;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleMovingAverageInitializer"/> class.
        /// </summary>
        /// <param name="period">The period of the simple moving average</param>
        public SimpleMovingAverageInitializer(int period)
        {
            this.period = period;
        }

        /// <summary>
        /// The numerically stable lookback value for this indicator
        /// </summary>
        /// <param name="period">The period of the indicator</param>
        /// <returns>The numerically stable lookback value for this indicator.</returns>
        public static int GetLookback(int period)
        {
            return period;
        }

        /// <summary>
        /// Initialize the indicator with the following values
        /// </summary>
        /// <param name="values">The values to initialize the indicator with</param>
        /// <returns>The simple moving average indicator</returns>
        public SimpleMovingAverage InitalizeWithData(List<decimal> values)
        {
            if (values.Count < GetLookback(this.period))
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
