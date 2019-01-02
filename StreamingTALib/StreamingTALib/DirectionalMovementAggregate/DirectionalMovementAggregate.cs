// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    /// <summary>
    /// The directional movement aggregate indicator
    /// </summary>
    public class DirectionalMovementAggregate
    {
        private readonly int period;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionalMovementAggregate"/> class.
        /// </summary>
        /// <param name="period">The period</param>
        /// <param name="currentSum">The current sum</param>
        internal DirectionalMovementAggregate(int period, decimal currentSum)
        {
            this.period = period;
            this.DirectionalMovementAggretateState = new DirectionalMovementAggregateState(currentSum);
        }

        /// <summary>
        /// Gets the directional movement aggregate state
        /// </summary>
        public DirectionalMovementAggregateState DirectionalMovementAggretateState
        {
            get;
            private set;
        }

        /// <summary>
        /// Integrate a new value into the indicator
        /// </summary>
        /// <param name="value">The new value</param>
        /// <returns>The current state of the directional movement aggregate indicator</returns>
        public DirectionalMovementAggregateState IntegrateValue(decimal value)
        {
            decimal newWildersMovingAverage = this.DirectionalMovementAggretateState.WildersAggregate - (this.DirectionalMovementAggretateState.WildersAggregate / this.period) + value;
            this.DirectionalMovementAggretateState.WildersAggregate = newWildersMovingAverage;

            return this.DirectionalMovementAggretateState;
        }
    }
}
