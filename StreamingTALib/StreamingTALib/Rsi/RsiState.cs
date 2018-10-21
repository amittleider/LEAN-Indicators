// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    /// <summary>
    /// This class holds the state of an RSI during a calculation
    /// </summary>
    public class RsiState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RsiState"/> class.
        /// </summary>
        public RsiState()
        {
            this.SmoothUpCandle = 0;
            this.SmoothDownCandle = 0;
            this.PreviousPrice = 0;
        }

        /// <summary>
        /// Gets or sets the smooth up candle
        /// </summary>
        public double SmoothUpCandle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the smooth down candle
        /// </summary>
        public double SmoothDownCandle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the previous price
        /// </summary>
        public double PreviousPrice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the RSI value
        /// </summary>
        public double RsiValue
        {
            get;
            set;
        }
    }
}