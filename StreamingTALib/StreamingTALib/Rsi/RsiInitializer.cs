// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This class initializes an RSI object and RSI state object properly.
    /// </summary>
    public class RsiInitializer
    {
        private int period;

        /// <summary>
        /// Initializes a new instance of the <see cref="RsiInitializer"/> class.
        /// </summary>
        /// <param name="period">The RSI period</param>
        public RsiInitializer(int period)
        {
            this.period = period;
        }

        /// <summary>
        /// Gets the lookback value. The lookback is the minimum number of data points needed to initialize an RSI.
        /// The rsi lookback `l = 7x + 200` is proven to be stable at all points.
        /// An RSI with a lower lookback will be unstable across different periods of the same data set.
        /// </summary>
        /// <param name="period">The RSI period</param>
        /// <returns>The lookback</returns>
        public static int GetLookback(int period)
        {
            int lookback = (7 * period) + 200;
            return lookback;
        }

        /// <summary>
        /// Initialize the RSI and RSI state objects with a data array.
        /// This computation time is the same as running a TALib.Rsi(prices, out rsi)
        /// </summary>
        /// <param name="prices">The prices</param>
        /// <returns>An RSI object</returns>
        public Rsi InitalizeWithData(List<double> prices)
        {
            if (prices.Count < RsiInitializer.GetLookback(this.period))
            {
                throw new Exception("Cannot initialize the RSI with this period. Not enough values.");
            }

            RsiState rsiState = null;
            Rsi rsi = new Rsi(this.period);
            rsi.RsiState.PreviousPrice = prices.First();
            for (int i = 1; i < prices.Count; i++)
            {
                rsiState = rsi.CalculateRsiWithNewPrice(prices[i]);
            }

            return rsi;
        }
    }
}