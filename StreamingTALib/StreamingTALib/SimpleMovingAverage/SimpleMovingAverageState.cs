// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    using System.Collections.Generic;

    /// <summary>
    /// The simple moving average state
    /// </summary>
    public class SimpleMovingAverageState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleMovingAverageState"/> class.
        /// </summary>
        /// <param name="period">The current period</param>
        public SimpleMovingAverageState(int period)
        {
            this.ValueQueue = new Queue<decimal>(period);
            this.Mean = 0m;
        }

        /// <summary>
        /// Gets the queue of values used by the simple moving average.
        /// </summary>
        public Queue<decimal> ValueQueue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the current mean
        /// </summary>
        public decimal Mean
        {
            get;
            set;
        }
    }
}