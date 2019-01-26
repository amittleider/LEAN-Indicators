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
    /// This indicator computes the Rate Of Change Ratio (ROCR). 
    /// The Rate Of Change Ratio is calculated with the following formula:
    /// ROCR = price / prevPrice
    /// </summary>
    public class RateOfChangeRatio : WindowIndicator<IndicatorDataPoint>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RateOfChangeRatio"/> class using the specified name and period.
        /// </summary> 
        /// <param name="name">The name of this indicator</param>
        /// <param name="period">The period of the ROCR</param>
        public RateOfChangeRatio(string name, int period)
            : base(name, period)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RateOfChangeRatio"/> class using the specified period.
        /// </summary> 
        /// <param name="period">The period of the ROCR</param>
        public RateOfChangeRatio(int period)
            : base("ROCR" + period, period)
        {
        }

        /// <summary>
        /// Computes the next value for this indicator from the given state.
        /// </summary>
        /// <param name="window">The window of data held in this indicator</param>
        /// <param name="input">The input value to this indicator on this time step</param>
        /// <returns>A new value for this indicator</returns>
        protected override decimal ComputeNextValue(IReadOnlyWindow<IndicatorDataPoint> window, IndicatorDataPoint input)
        {
            var denominator = window.Samples > window.Size ? window.MostRecentlyRemoved : window[window.Count - 1];

            return denominator != 0 ? input / denominator : 0m;
        }
    }
}
