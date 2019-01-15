/*
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

using System;

namespace QuantConnect.Data
{
    /// <summary>
    /// Base Data Class: Type, Timestamp, Key -- Base Features.
    /// </summary>
    public interface IBaseData
    {
               /// <summary>
        /// Time keeper of data -- all data is timeseries based.
        /// </summary>
        DateTime Time
        {
            get;
            set;
        }

        DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// All timeseries data is a time-value pair:
        /// </summary>
        decimal Value
        {
            get;
            set;
        }


        /// <summary>
        /// Alias of Value.
        /// </summary>
        decimal Price
        {
            get;
        }
    } // End Base Data Class

} // End QC Namespace
