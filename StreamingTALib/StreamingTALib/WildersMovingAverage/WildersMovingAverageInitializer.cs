// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Initializes a wilders moving average object
    /// </summary>
    public class WildersMovingAverageInitializer
    {
        private int period;

        /// <summary>
        /// Initializes a new instance of the <see cref="WildersMovingAverageInitializer"/> class.
        /// </summary>
        /// <param name="period"></param>
        public WildersMovingAverageInitializer(int period)
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
        /// Initialize the Wilders Moving Average
        /// </summary>
        /// <param name="values">The values to initialize with</param>
        /// <returns>The wilders moving average object</returns>
        public WildersMovingAverage InitalizeWithData(List<decimal> values)
        {
            var periodValues = values.Take(GetLookback(this.period));
            WildersMovingAverage wildersFuckedMovingAverage = new WildersMovingAverage(this.period, periodValues.Average());

            foreach (var value in values.Skip(this.period))
            {
                wildersFuckedMovingAverage.IntegrateValue(value);
            }

            return wildersFuckedMovingAverage;
        }
    }
}
