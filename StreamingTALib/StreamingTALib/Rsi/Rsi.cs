// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    /// <summary>
    /// Performs the RSI calculations
    /// </summary>
    public class Rsi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rsi"/> class.
        /// </summary>
        /// <param name="period">The period</param>
        internal Rsi(int period)
        {
            this.RsiState = new RsiState();
            this.Period = period;
        }

        /// <summary>
        /// Gets the period
        /// </summary>
        public int Period
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the rsi state
        /// </summary>
        public RsiState RsiState
        {
            get;
            private set;
        }

        /// <summary>
        /// Calcluates the rsi value given a single new price and the current state of the RSI
        /// </summary>
        /// <param name="newPrice">The incoming price</param>
        /// <returns>The RSI</returns>
        public RsiState CalculateRsiWithNewPrice(double newPrice)
        {
            double priceDifference = newPrice - this.RsiState.PreviousPrice;

            double upCandle = 0;
            if (priceDifference > 0)
            {
                upCandle = priceDifference;
            }

            double downCandle = 0;
            if (priceDifference < 0)
            {
                downCandle = -priceDifference;
            }

            this.RsiState.SmoothUpCandle = (1.0 / this.Period * upCandle) + ((1 - (1.0 / this.Period)) * this.RsiState.SmoothUpCandle);
            this.RsiState.SmoothDownCandle = (1.0 / this.Period * downCandle) + ((1 - (1.0 / this.Period)) * this.RsiState.SmoothDownCandle);

            this.RsiState.RsiValue = this.RsiState.SmoothUpCandle / (this.RsiState.SmoothDownCandle + this.RsiState.SmoothUpCandle) * 100.0;

            this.RsiState.PreviousPrice = newPrice;
            return this.RsiState;
        }
    }
}
