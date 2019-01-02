// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Initializes a directional movement indicator
    /// </summary>
    public class DirectionalMovementInitializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionalMovementInitializer"/> class.
        /// </summary>
        public DirectionalMovementInitializer()
        {
        }

        /// <summary>
        /// Gets the lookback of the directional movement indicator
        /// </summary>
        /// <returns>The lookback of the directional movement indicator.</returns>
        public static int GetLookback()
        {
            return 1;
        }

        /// <summary>
        /// Initializes the directional movement indicator with data.
        /// </summary>
        /// <param name="highs">The highs</param>
        /// <param name="lows">The lows</param>
        /// <returns>A new directional movement indicator</returns>
        public DirectionalMovement InitalizeWithData(List<decimal> highs, List<decimal> lows)
        {
            if (highs.Count < GetLookback() || lows.Count < GetLookback())
            {
                throw new Exception("Cannot initialize the directional movement with this period. Not enough values.");
            }

            DirectionalMovement directionalMovement = new DirectionalMovement();
            directionalMovement.IntegrateValue(highs[0], lows[0]);

            return directionalMovement;
        }
    }
}
