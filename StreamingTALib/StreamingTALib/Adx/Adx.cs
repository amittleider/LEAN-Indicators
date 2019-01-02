// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    using System;

    /// <summary>
    /// The ADX indicator
    /// </summary>
    public class Adx
    {
        private readonly int period;
        private readonly DirectionalMovement dm;
        private readonly TrueRange trueRange;
        private readonly DirectionalMovementAggregate actualPlusDm14Wma;
        private readonly DirectionalMovementAggregate actualMinusDm14Wma;
        private readonly DirectionalMovementAggregate actualTrueRange14Wma;
        private readonly WildersMovingAverage adxSma;

        /// <summary>
        /// Initializes a new instance of the <see cref="Adx"/> class.
        /// Use the AdxInitializer to instantiate an instance of this class.
        /// </summary>
        /// <param name="period">The period of the indicator</param>
        /// <param name="directionalMovement">The directional movement indicator</param>
        /// <param name="trueRange">The true range indiciator</param>
        /// <param name="actualPlusDm14Wma">The plus directional movement indicator sent through a Wilder's Moving Average indicator</param>
        /// <param name="actualMinusDm14Wma">The minus directional movement indicator sent through a Wilder's Moving Average indicator</param>
        /// <param name="actualTrueRange14Wma">The true range indiciator sent through a Wilder's Moving Average indiciator</param>
        /// <param name="adxSma">The Wilder's moving average indicator that smooths the ADX values</param>
        internal Adx(int period, DirectionalMovement directionalMovement, TrueRange trueRange, DirectionalMovementAggregate actualPlusDm14Wma, DirectionalMovementAggregate actualMinusDm14Wma, DirectionalMovementAggregate actualTrueRange14Wma, WildersMovingAverage adxSma)
        {
            this.period = period;
            this.dm = directionalMovement;
            this.trueRange = trueRange;
            this.actualPlusDm14Wma = actualPlusDm14Wma;
            this.actualMinusDm14Wma = actualMinusDm14Wma;
            this.actualTrueRange14Wma = actualTrueRange14Wma;
            this.adxSma = adxSma;
            this.AdxState = new AdxState();
        }

        /// <summary>
        /// Gets the ADX State
        /// </summary>
        public AdxState AdxState
        {
            get;
        }

        /// <summary>
        /// Computes the next value of the ADX given the current high, low, and close
        /// </summary>
        /// <param name="high">The current high</param>
        /// <param name="low">The current low</param>
        /// <param name="close">The current close</param>
        /// <returns>The ADX state representing the current ADX value</returns>
        public AdxState ComputeNextValue(decimal high, decimal low, decimal close)
        {
            DirectionalMovementState directionalMovementState = this.dm.IntegrateValue(high, low);
            DirectionalMovementAggregateState actualPlusDm14SmaState = this.actualPlusDm14Wma.IntegrateValue(directionalMovementState.PlusDirectionalMovement);
            DirectionalMovementAggregateState actualMinusDm14SmaState = this.actualMinusDm14Wma.IntegrateValue(directionalMovementState.MinusDirectionalMovement);

            TrueRangeState trueRangeState = this.trueRange.ComputeTrueRange(high, low, close);
            DirectionalMovementAggregateState actualTrueRange14State = this.actualTrueRange14Wma.IntegrateValue(trueRangeState.TrueRange);

            // Calculate the DIs by dividing the directional movement by the true range
            decimal actualPlusDi = actualPlusDm14SmaState.WildersAggregate / actualTrueRange14State.WildersAggregate * 100.0m;
            decimal actualMinusDi = actualMinusDm14SmaState.WildersAggregate / actualTrueRange14State.WildersAggregate * 100.0m;

            decimal diDiff = Math.Abs(actualPlusDi - actualMinusDi);
            decimal diSum = actualPlusDi + actualMinusDi;

            decimal di = diDiff / diSum * 100m;

            WildersMovingAverageState adxSmaState = this.adxSma.IntegrateValue(di);

            this.AdxState.AverageDirectionalIndex = adxSmaState.Mean;
            return this.AdxState;
        }
    }
}
