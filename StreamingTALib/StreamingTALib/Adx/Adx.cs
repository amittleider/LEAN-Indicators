using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTALib.Adx
{
    public class Adx
    {
        public int Period { get; }

        public AdxState AdxState { get; private set; }

        public Adx(int period)
        {
            this.Period = period;
            this.AdxState = new AdxState();
        }

        /// <summary>
        /// Computes the positive directional movement.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private decimal ComputePositiveDirectionalMovement(decimal high, decimal low)
        {
            if (this.AdxState.PreviousHigh != 0 &&
                high> this.AdxState.PreviousHigh &&
                high - this.AdxState.PreviousHigh >= this.AdxState.PreviousLow - low)
            {
                return high - this.AdxState.PreviousHigh;
            }

            return 0m;
        }

        /// <summary>
        /// Computes the negative directional movement.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private decimal ComputeNegativeDirectionalMovement(decimal high, decimal low)
        {
            if (this.AdxState.PreviousLow != 0 &&
                this.AdxState.PreviousLow > low &&
                this.AdxState.PreviousLow - low > high - this.AdxState.PreviousHigh)
            {
                return this.AdxState.PreviousLow - low;
            }

            return 0m;
        }

        /// <summary>
        /// Computes the True Range value.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private decimal ComputeTrueRange(decimal high, decimal low)
        {
            if (this.AdxState.PreviousLow == 0)
            {
                return 0m;
            }

            var range1 = high - low;
            var range2 = Math.Abs(high - this.AdxState.PreviousClose);
            var range3 = Math.Abs(low - this.AdxState.PreviousClose);

            return Math.Max(range1, Math.Max(range2, range3));
        }

        /// <summary>
        /// Computes the next value of this indicator from the given state
        /// </summary>
        /// <param name="input">The input given to the indicator</param>
        /// <returns>A new value for this indicator</returns>
        protected AdxState ComputeNextValue(decimal high, decimal low, decimal close)
        {
            decimal trueRange = this.ComputeTrueRange(high, low);
            decimal directionalMovementPlus = this.ComputePositiveDirectionalMovement(high, low);
            decimal directionalMovementMinus = this.ComputeNegativeDirectionalMovement(high, low);
            decimal smoothedTrueRange = trueRange / this.Period;
            decimal smoothedDirectionalMovementPlus = directionalMovementPlus / this.Period;
            decimal smoothedDirectionalMovementMinus = directionalMovementMinus / this.Period;


            decimal positiveDirectionalIndex = 100m * smoothedDirectionalMovementPlus / smoothedTrueRange;
            decimal negativeDirectionalIndex = 100m * smoothedDirectionalMovementMinus / smoothedTrueRange;

            var diff = Math.Abs(positiveDirectionalIndex - negativeDirectionalIndex);
            var sum = positiveDirectionalIndex + negativeDirectionalIndex;

            if (sum == 0)
            {
                this.AdxState.AverageDirectionalIndex = 50m;
                return this.AdxState;
            }


            this.AdxState.AverageDirectionalIndex= 100m * diff / sum;
            return this.AdxState;
        }
    }
}
