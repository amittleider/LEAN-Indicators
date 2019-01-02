// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    using System;

    /// <summary>
    /// The directional movement indicator is used in the ADX calculation.
    /// </summary>
    public class DirectionalMovement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionalMovement"/> class.
        /// </summary>
        internal DirectionalMovement()
        {
            this.DirectionalMovementState = new DirectionalMovementState();
        }

        /// <summary>
        /// Gets the directional movement state
        /// </summary>
        public DirectionalMovementState DirectionalMovementState
        {
            get;
            private set;
        }

        /// <summary>
        /// Integrate a new value into the directional movement
        /// </summary>
        /// <param name="high">The current high</param>
        /// <param name="low">The current low</param>
        /// <returns>The current state of this directional movement indicator</returns>
        public DirectionalMovementState IntegrateValue(decimal high, decimal low)
        {
            decimal potentialPlusDirectionalMovement = high - this.DirectionalMovementState.PreviousHigh;
            decimal potentialMinusDirectionalMovement = this.DirectionalMovementState.PreviousLow - low;

            potentialPlusDirectionalMovement = Math.Max(potentialPlusDirectionalMovement, 0);
            potentialMinusDirectionalMovement = Math.Max(potentialMinusDirectionalMovement, 0);

            if (potentialPlusDirectionalMovement > potentialMinusDirectionalMovement)
            {
                this.DirectionalMovementState.PlusDirectionalMovement = potentialPlusDirectionalMovement;
                this.DirectionalMovementState.MinusDirectionalMovement = 0m;
            }
            else if (potentialMinusDirectionalMovement > potentialPlusDirectionalMovement)
            {
                this.DirectionalMovementState.MinusDirectionalMovement = potentialMinusDirectionalMovement;
                this.DirectionalMovementState.PlusDirectionalMovement = 0m;
            }
            else
            {
                this.DirectionalMovementState.PlusDirectionalMovement = 0m;
                this.DirectionalMovementState.MinusDirectionalMovement = 0m;
            }

            this.DirectionalMovementState.PreviousHigh = high;
            this.DirectionalMovementState.PreviousLow = low;
            return this.DirectionalMovementState;
        }
    }
}
