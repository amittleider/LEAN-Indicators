// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    /// <summary>
    /// The ADX state
    /// </summary>
    public class AdxState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdxState"/> class.
        /// </summary>
        public AdxState()
        {
            this.PreviousClose = 0.0m;
            this.PreviousHigh = 0.0m;
            this.PreviousLow = 0.0m;
            this.AverageDirectionalIndex = 0.0m;
        }

        /// <summary>
        /// Gets or sets the previous low
        /// </summary>
        public decimal PreviousLow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the average directional index
        /// </summary>
        public decimal AverageDirectionalIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the previous high
        /// </summary>
        public decimal PreviousHigh
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the previous close
        /// </summary>
        public decimal PreviousClose
        {
            get;
            set;
        }
    }
}
