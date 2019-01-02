// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    /// <summary>
    /// The directional movement aggregate indicator state.
    /// It only depends on the Wilders Aggregate state (or the sum, if it's the first iteration).
    /// See Wilder's book on ADX (or the WildersWorksheetTests file) if it's confusing.
    /// </summary>
    public class DirectionalMovementAggregateState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionalMovementAggregateState"/> class.
        /// </summary>
        /// <param name="sum">The current sum of the values.</param>
        public DirectionalMovementAggregateState(decimal sum)
        {
            this.WildersAggregate = sum;
        }

        /// <summary>
        /// Gets or sets the WildersAggregate
        /// </summary>
        public decimal WildersAggregate
        {
            get;
            set;
        }
    }
}
