// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    /// <summary>
    /// The directional movement state
    /// </summary>
    public class DirectionalMovementState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionalMovementState"/> class.
        /// </summary>
        public DirectionalMovementState()
        {
        }

        /// <summary>
        /// Gets the previous high
        /// </summary>
        public decimal PreviousHigh
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the previous low
        /// </summary>
        public decimal PreviousLow
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the current value of the plus DM
        /// </summary>
        public decimal PlusDirectionalMovement
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the current value of the minus DM
        /// </summary>
        public decimal MinusDirectionalMovement
        {
            get;
            internal set;
        }
    }
}