// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    public class WildersWorksheetTests
    {
        /// <summary>
        /// Welles Wilder's book "New Concepts in Trading Technologies" has a worksheet example where he calculates the values of his indicators.
        /// This test details the worksheet example in code, validating each indicator (Directional Movement, True Range, Directional Index, Wilder's Moving Average, and Average Directional Index).
        /// </summary>
        [Fact]
        public void DirectionalIndicators_True_range_Adx_WildersMovingAverage_Should_Match_WildersWorksheetExamples()
        {
            decimal[] highs = new decimal[] { 274m, 273.25m, 272m, 270.75m, 270m, 270.5m, 268.5m, 265.5m, 262.5m, 263.5m, 269.5m, 267.25m, 267.5m, 269.75m, 268.25m, 264m, 268m, 266m, 274m, 277.5m, 277m, 272m, 267.75m, 269.25m, 266m, 265m, 264.75m, 261m, 257.5m, 259m, 259.75m, 257.25m, 250m, 254.25m, 254m, 253.25m, 253.25m, 251.75m, 253m, 251.5m, 246.25m, 244.25m };
            decimal[] lows = new decimal[] { 272m, 270.25m, 269.75m, 268m, 269m, 268m, 266.5m, 263m, 259m, 260m, 263m, 265m, 265.5m, 266m, 263.25m, 261.5m, 266.25m, 264.25m, 267m, 273.5m, 272.5m, 269.58m, 264m, 263m, 263.5m, 262m, 261.5m, 255.5m, 253m, 254m, 257.5m, 250m, 247m, 252.75m, 250.5m, 250.25m, 251m, 250.5m, 249.50m, 245.25m, 240m, 244.25m };
            decimal[] closes = new decimal[] { 272.75m, 270.75m, 270m, 269.25m, 269.75m, 270m, 266.5m, 263.25m, 260.25m, 263m, 266.5m, 267m, 265.75m, 268.5m, 264.25m, 264m, 266.5m, 265.25m, 273m, 276.75m, 273m, 270.25m, 266.75m, 263m, 265.5m, 262.25m, 262.75m, 255.5m, 253m, 257.5m, 257.5m, 250m, 249.75m, 253.75m, 251.25m, 250.5m, 253m, 251.5m, 250m, 245.75m, 242.75m, 243.5m };

            decimal[] expectedPlusDM = { 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 1m, 6m, 0m, 0.25m, 2.25m, 0m, 0m, 4m, 0m, 8m, 3.5m, 0m, 0m, 0m, 1.5m, 0m, 0m, 0m, 0m, 0m, 1.5m, .75m, 0m, 0m, 4.25m, 0m, 0m, 0m, 01.25m, 0m, 0m, 0m };
            decimal[] expectedMinusDM = { 1.75m, .5m, 1.75m, 0m, 1m, 1.5m, 3.5m, 4.0m, 0m, 0m, 0m, 0m, 0m, 2.75m, 1.75m, 0m, 2m, 0m, 0m, 1m, 3m, 5.5m, 0m, 0m, 1.5m, .5m, 6m, 2.5m, 0m, 0m, 7.5m, 3m, 0m, 2.25m, .25m, 0m, .5m, 0m, 4.25m, 5.25m, 0m };
            decimal[] expectedTrueRange = { 3m, 2.25m, 2.75m, 1m, 2.5m, 3.5m, 3.5m, 4.25m, 3.5m, 6.5m, 2.25m, 2.0m, 4.0m, 5.25m, 2.75m, 4m, 2.25m, 8.75m, 4.5m, 4.5m, 3.5m, 6.25m, 6.25m, 3m, 3.5m, 3.25m, 7.25m, 4.5m, 6m, 2.25m, 7.5m, 3m, 4.5m, 3.5m, 3m, 2.75m, 2.5m, 3.5m, 6.25m, 6.25m, 3m };

            decimal[] expectedTrueRange14 = { 43.32m, 42.98m, 43.91m, 43.02m, 48.7m, 49.72m, 50.67m, 50.55m, 53.19m, 55.64m, 54.67m, 54.26m, 53.63m, 57.05m, 57.47m, 59.36m, 57.37m, 60.77m, 59.43m, 59.68m, 58.92m, 57.71m, 56.34m, 54.82m, 54.40m, 56.76m, 58.96m, 57.75m };
            decimal[] expectedPlusDm14 = { 8.82m, 8.19m, 11.60m, 10.77m, 18.0m, 20.21m, 18.77m, 17.43m, 16.18m, 16.52m, 15.34m, 14.24m, 13.22m, 12.28m, 11.40m, 12.09m, 11.98m, 11.12m, 10.33m, 13.84m, 13.85m, 11.93m, 11.08m, 10.29m, 10.80m, 10.05m, 9.31m, 8.64m };
            decimal[] expectedMinusDm14 = { 15.75m, 16.37m, 15.20m, 16.11m, 14.96m, 13.89m, 13.90m, 15.91m, 20.27m, 18.82m, 17.48m, 17.73m, 16.96m, 21.75m, 22.7m, 21.08m, 19.57m, 25.67m, 26.84m, 24.92m, 25.39m, 23.83m, 22.13m, 21.05m, 19.55m, 22.40m, 26.05m, 24.19m };
            decimal[] expectedPlusDi = { 20, 19, 26, 25, 37, 41, 37, 34, 30, 30, 28, 26, 25, 22, 20, 20, 21, 18, 17, 23, 22, 21, 20, 19, 20, 18, 16, 15 };
            decimal[] expectedMinusDi = { 36, 38, 35, 37, 31, 28, 27, 31, 38, 34, 32, 33, 32, 38, 39, 36, 34, 41, 45, 42, 43, 41, 39, 38, 36, 39, 44, 42 };
            decimal[] expectedDiDiff = { 16, 19, 9, 12, 6, 13, 10, 4, 8, 4, 4, 7, 7, 16, 19, 16, 13, 24, 28, 19, 21, 20, 19, 19, 16, 21, 28, 27 };

            decimal[] expectedDiSum = { 56, 57, 61, 62, 68, 69, 64, 66, 68, 64, 60, 59, 57, 60, 59, 56, 55, 60, 62, 65, 65, 62, 59, 57, 56, 57, 60, 57 };
            decimal[] expectedDi = { 29, 33, 15, 19, 9, 19, 16, 6, 12, 6, 7, 12, 12, 27, 32, 29, 24, 40, 45, 29, 32, 32, 32, 33, 28, 37, 47, 47 };
            decimal[] expectedAdx = { 17, 18, 18, 20, 22, 22, 23, 23, 24, 25, 25, 26, 27, 29 };

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

            DirectionalMovementAggregateInitializer wmaInitializer = new DirectionalMovementAggregateInitializer(14);
            DirectionalMovementAggregate actualPlusDm14Wma = wmaInitializer.InitalizeWithData(actualPlusDm);
            DirectionalMovementAggregate actualMinusDm14Wma = wmaInitializer.InitalizeWithData(actualMinusDm);
            DirectionalMovementAggregate actualTrueRange14Wma = wmaInitializer.InitalizeWithData(actualTrueRange);

            List<decimal> actualDirectionalIndexValues = new List<decimal>();
            for (int i = 14; i < 14 + 14; i++)
            {
                DirectionalMovementState directionalMovementState = dm.IntegrateValue(highs[i], lows[i]);
                DirectionalMovementAggregateState actualPlusDm14SmaState = actualPlusDm14Wma.IntegrateValue(directionalMovementState.PlusDirectionalMovement);
                DirectionalMovementAggregateState actualMinusDm14SmaState = actualMinusDm14Wma.IntegrateValue(directionalMovementState.MinusDirectionalMovement);

                TrueRangeState trueRangeState = trueRange.ComputeTrueRange(highs[i], lows[i], closes[i]);
                DirectionalMovementAggregateState actualTrueRange14State = actualTrueRange14Wma.IntegrateValue(trueRangeState.TrueRange);

                actualPlusDm14SmaState.WildersAggregate.Should().BeApproximately(expectedPlusDm14[i - 14], 0.1m); // Wilke's does a 2-decimal round here in his book. This is also tested against TAlib, which rounds out to a further decimal.
                actualMinusDm14SmaState.WildersAggregate.Should().BeApproximately(expectedMinusDm14[i - 14], 0.1m);
                actualTrueRange14State.WildersAggregate.Should().BeApproximately(expectedTrueRange14[i - 14], 0.1m);

                // Calculate the DIs by dividing the directional movement by the true range
                decimal actualPlusDi = actualPlusDm14SmaState.WildersAggregate / actualTrueRange14State.WildersAggregate * 100.0m;
                decimal actualMinusDi = actualMinusDm14SmaState.WildersAggregate / actualTrueRange14State.WildersAggregate * 100.0m;
                actualPlusDi.Should().BeApproximately(expectedPlusDi[i - 14], 1m);
                actualMinusDi.Should().BeApproximately(expectedMinusDi[i - 14], 1m); // Wilkes was doing some rounding shit here too, but this time rounding to the nearest integer. I don't do the rounding here because it's a bit unnecessary with the current state of technology.

                decimal diDiff = Math.Abs(actualPlusDi - actualMinusDi);
                diDiff.Should().BeApproximately(expectedDiDiff[i - 14], 1m);
                decimal diSum = actualPlusDi + actualMinusDi;
                diSum.Should().BeApproximately(expectedDiSum[i - 14], 1m);

                decimal di = diDiff / diSum * 100m;
                di.Should().BeApproximately(expectedDi[i - 14], 2m);

                actualDirectionalIndexValues.Add(di);
            }

            WildersMovingAverageInitializer smaInitializer = new WildersMovingAverageInitializer(14);
            WildersMovingAverage adxSma = smaInitializer.InitalizeWithData(actualDirectionalIndexValues);

            WildersMovingAverageState adxSmaState = null; // The final result goes here

            // Put a little extra in here -- Test that TA-Lib also matches our output
            // This is important because Wilder does some rounding that we don't have to do, which makes his calculations significantly (+- 2) wrong.
            // However, TA-Lib doesn't implement the rounding by default, so we can do some extra validation against that.
            double[] highsDouble = Array.ConvertAll(highs, x => (double)x);
            double[] lowsDouble = Array.ConvertAll(lows, x => (double)x);
            double[] closesDouble = Array.ConvertAll(closes, x => (double)x);

            double[] outReal = new double[highsDouble.Length];
            TicTacTec.TA.Library.Core.Adx(0, highsDouble.Length - 1, highsDouble, lowsDouble, closesDouble, 14, out int outBegIdx, out int nbElement, outReal);

            for (int i = 28; i < 28 + 14; i++)
            {
                DirectionalMovementState directionalMovementState = dm.IntegrateValue(highs[i], lows[i]);
                DirectionalMovementAggregateState actualPlusDm14SmaState = actualPlusDm14Wma.IntegrateValue(directionalMovementState.PlusDirectionalMovement);
                DirectionalMovementAggregateState actualMinusDm14SmaState = actualMinusDm14Wma.IntegrateValue(directionalMovementState.MinusDirectionalMovement);

                TrueRangeState trueRangeState = trueRange.ComputeTrueRange(highs[i], lows[i], closes[i]);
                DirectionalMovementAggregateState actualTrueRange14State = actualTrueRange14Wma.IntegrateValue(trueRangeState.TrueRange);

                actualPlusDm14SmaState.WildersAggregate.Should().BeApproximately(expectedPlusDm14[i - 14], 1m); // Wilke's does a 2-decimal round here in his book. This is also tested against TAlib, which rounds out to a further decimal.
                actualMinusDm14SmaState.WildersAggregate.Should().BeApproximately(expectedMinusDm14[i - 14], 1m);
                actualTrueRange14State.WildersAggregate.Should().BeApproximately(expectedTrueRange14[i - 14], 2m);

                // Calculate the DIs by dividing the directional movement by the true range
                decimal actualPlusDi = actualPlusDm14SmaState.WildersAggregate / actualTrueRange14State.WildersAggregate * 100.0m;
                decimal actualMinusDi = actualMinusDm14SmaState.WildersAggregate / actualTrueRange14State.WildersAggregate * 100.0m;
                actualPlusDi.Should().BeApproximately(expectedPlusDi[i - 14], 2m);
                actualMinusDi.Should().BeApproximately(expectedMinusDi[i - 14], 2m); // Wilkes was doing some rounding shit here too, but this time rounding to the nearest integer. I don't do the rounding here because it's a bit unnecessary with the current state of technology.

                decimal diDiff = Math.Abs(actualPlusDi - actualMinusDi);
                diDiff.Should().BeApproximately(expectedDiDiff[i - 14], 2m);
                decimal diSum = actualPlusDi + actualMinusDi;
                diSum.Should().BeApproximately(expectedDiSum[i - 14], 2m);

                decimal di = diDiff / diSum * 100m;
                di.Should().BeApproximately(expectedDi[i - 14], 2m);

                adxSmaState = adxSma.IntegrateValue(di);
                adxSmaState.Mean.Should().BeApproximately(expectedAdx[i - 28], 2m);
                adxSmaState.Mean.Should().BeApproximately((decimal)outReal[i - 27], 0.0001m);
            }
        }
    }
}
