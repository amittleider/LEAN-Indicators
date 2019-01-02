// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This class is used to initialize an ADX indicator
    /// </summary>
    public class AdxInitializer
    {
        private int period;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdxInitializer"/> class.
        /// </summary>
        /// <param name="period">The period of the ADX</param>
        public AdxInitializer(int period)
        {
            this.period = period;
        }

        /// <summary>
        /// Gets the lookback of the ADX.
        /// </summary>
        /// <param name="period">The period</param>
        /// <returns>
        /// The theoretically obtained numerically stable lookback period.
        /// The ADX has the same rules as the RSI as far as numeric stability. They both have the same lookback.
        /// </returns>
        public static int GetLookback(int period)
        {
            return (period * 2) - 1;
        }

        /// <summary>
        /// Initialize an ADX. The data must be of at least the length of the ADX lookback.
        /// </summary>
        /// <param name="highs">The highs</param>
        /// <param name="lows">The lows</param>
        /// <param name="closes">The closes</param>
        /// <returns>The ADX</returns>
        public Adx InitializeWithData(List<decimal> highs, List<decimal> lows, List<decimal> closes)
        {
            // First initliaze the directional movement indicator, it only takes 1 value to initialize
            DirectionalMovementInitializer directionalMovementInitializer = new DirectionalMovementInitializer();
            DirectionalMovement dm = directionalMovementInitializer.InitalizeWithData(highs.Take(1).ToList(), lows.Take(1).ToList());
            TrueRangeInitializer trueRangeInitializer = new TrueRangeInitializer();
            TrueRange trueRange = trueRangeInitializer.InitializeWithData(highs.Take(1).ToList(), lows.Take(1).ToList(), closes.Take(1).ToList());

            // Next initialize a simple moving average over the plus DM and minus DM
            List<decimal> actualPlusDm = new List<decimal>();
            List<decimal> actualMinusDm = new List<decimal>();
            List<decimal> actualTrueRange = new List<decimal>();
            for (int i = 1; i < 1 + 13; i++)
            {
                DirectionalMovementState directionalMovementState = dm.IntegrateValue(highs[i], lows[i]);
                actualPlusDm.Add(directionalMovementState.PlusDirectionalMovement);
                actualMinusDm.Add(directionalMovementState.MinusDirectionalMovement);

                TrueRangeState trueRangeState = trueRange.ComputeTrueRange(highs[i], lows[i], closes[i]);
                actualTrueRange.Add(trueRangeState.TrueRange);
            }

            DirectionalMovementAggregateInitializer wmaInitializer = new DirectionalMovementAggregateInitializer(this.period);
            DirectionalMovementAggregate plusDmAggregation = wmaInitializer.InitalizeWithData(actualPlusDm);
            DirectionalMovementAggregate minusDmAggregation = wmaInitializer.InitalizeWithData(actualMinusDm);
            DirectionalMovementAggregate trueRangeAggregation = wmaInitializer.InitalizeWithData(actualTrueRange);

            List<decimal> startingDirectionalIndexValues = new List<decimal>();
            for (int i = this.period; i < this.period * 2; i++)
            {
                DirectionalMovementState directionalMovementState = dm.IntegrateValue(highs[i], lows[i]);
                DirectionalMovementAggregateState plusDmAggregateState = plusDmAggregation.IntegrateValue(directionalMovementState.PlusDirectionalMovement);
                DirectionalMovementAggregateState minusDmAggregateState = minusDmAggregation.IntegrateValue(directionalMovementState.MinusDirectionalMovement);

                TrueRangeState trueRangeState = trueRange.ComputeTrueRange(highs[i], lows[i], closes[i]);
                DirectionalMovementAggregateState actualTrueRange14State = trueRangeAggregation.IntegrateValue(trueRangeState.TrueRange);

                // Calculate the DIs by dividing the directional movement by the true range
                decimal actualPlusDi = plusDmAggregateState.WildersAggregate / actualTrueRange14State.WildersAggregate * 100.0m;
                decimal actualMinusDi = minusDmAggregateState.WildersAggregate / actualTrueRange14State.WildersAggregate * 100.0m;

                decimal diDiff = Math.Abs(actualPlusDi - actualMinusDi);
                decimal diSum = actualPlusDi + actualMinusDi;

                decimal di = diDiff / diSum * 100m;

                startingDirectionalIndexValues.Add(di);
            }

            WildersMovingAverageInitializer smaInitializer = new WildersMovingAverageInitializer(this.period);
            WildersMovingAverage adxSma = smaInitializer.InitalizeWithData(startingDirectionalIndexValues);

            Adx adx = new Adx(this.period, dm, trueRange, plusDmAggregation, minusDmAggregation, trueRangeAggregation, adxSma);
            return adx;
        }
    }
}
