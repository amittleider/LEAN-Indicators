﻿/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

namespace QuantConnect.Indicators
{
    /// <summary>
    /// An indicator that delays its input for a certain period
    /// </summary>
    public class Delay : WindowIndicator<IndicatorDataPoint>
    {
        /// <summary>
        /// Creates a new Delay indicator that delays its input by the specified period
        /// </summary>
        /// <param name="period">The period to delay input, must be greater than zero</param>
        public Delay(int period)
            : this("DELAY" + period, period)
        {
            
        }

        /// <summary>
        /// Creates a new Delay indicator that delays its input by the specified period
        /// </summary>
        /// <param name="name">Name of the delay window indicator</param>
        /// <param name="period">The period to delay input, must be greater than zero</param>
        public Delay(string name, int period) 
            : base(name, period)
        {
        }

        /// <summary>
        ///     Gets a flag indicating when this indicator is ready and fully initialized
        /// </summary>
        public override bool IsReady
        {
            get { return Samples > Period; }
        }

        /// <summary>
        ///     Computes the next value for this indicator from the given state.
        /// </summary>
        /// <param name="window">The window of data held in this indicator</param>
        /// <param name="input">The input value to this indicator on this time step</param>
        /// <returns>A new value for this indicator</returns>
        protected override decimal ComputeNextValue(IReadOnlyWindow<IndicatorDataPoint> window, IndicatorDataPoint input)
        {
            if (!IsReady)
            {
                // grab the initial value until we're ready
                return window[window.Count - 1];
            }

            return window.MostRecentlyRemoved;
        }
    }
}
