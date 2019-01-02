// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    using System;

    /// <summary>
    /// The true range indicator
    /// </summary>
    public class TrueRange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrueRange"/> class.
        /// </summary>
        internal TrueRange()
        {
            this.TrueRangeState = new TrueRangeState();
        }

        /// <summary>
        /// Gets the true range state
        /// </summary>
        public TrueRangeState TrueRangeState
        {
            get;
            private set;
        }

        /// <summary>
        /// Compute the true range with the following values
        /// </summary>
        /// <param name="high">The high</param>
        /// <param name="low">The low</param>
        /// <param name="close">The close</param>
        /// <returns>The true range</returns>
        public TrueRangeState ComputeTrueRange(decimal high, decimal low, decimal close)
        {
            var range1 = high - low;
            var range2 = Math.Abs(high - this.TrueRangeState.PreviousClose);
            var range3 = Math.Abs(low - this.TrueRangeState.PreviousClose);

            this.TrueRangeState.PreviousLow = low;
            this.TrueRangeState.PreviousClose = close;

            decimal trueRange = Math.Max(range1, Math.Max(range2, range3));
            this.TrueRangeState.TrueRange = trueRange;
            return this.TrueRangeState;
        }
    }
}
