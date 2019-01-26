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
using System.Collections.Generic;
using System.Linq;

namespace QuantConnect.Data
{
    /// <summary>
    /// Abstract base data class of QuantConnect. It is intended to be extended to define
    /// generic user customizable data types while at the same time implementing the basics of data where possible
    /// </summary>
    public abstract class BaseData : IBaseData
    {
        private DateTime _time;
        private decimal _value;
        private bool _isFillForward;

        /// <summary>
        /// True if this is a fill forward piece of data
        /// </summary>
        public bool IsFillForward
        {
            get { return _isFillForward; }
        }

        /// <summary>
        /// Current time marker of this data packet.
        /// </summary>
        /// <remarks>All data is timeseries based.</remarks>
        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }

        /// <summary>
        /// The end time of this data. Some data covers spans (trade bars) and as such we want
        /// to know the entire time span covered
        /// </summary>
        public virtual DateTime EndTime
        {
            get { return _time; }
            set { _time = value; }
        }
        
        /// <summary>
        /// Value representation of this data packet. All data requires a representative value for this moment in time.
        /// For streams of data this is the price now, for OHLC packets this is the closing price.
        /// </summary>
        public virtual decimal Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// As this is a backtesting platform we'll provide an alias of value as price.
        /// </summary>
        public decimal Price
        {
            get
            {
                return Value;
            }
        }

        /// <summary>
        /// Constructor for initialising the dase data class
        /// </summary>
        public BaseData()
        {
            //Empty constructor required for fast-reflection initialization
        }
        
        /// <summary>
        /// Updates this base data with a new trade
        /// </summary>
        /// <param name="lastTrade">The price of the last trade</param>
        /// <param name="tradeSize">The quantity traded</param>
        public void UpdateTrade(decimal lastTrade, decimal tradeSize)
        {
            Update(lastTrade, 0, 0, tradeSize, 0, 0);
        }

        /// <summary>
        /// Updates this base data with new quote information
        /// </summary>
        /// <param name="bidPrice">The current bid price</param>
        /// <param name="bidSize">The current bid size</param>
        /// <param name="askPrice">The current ask price</param>
        /// <param name="askSize">The current ask size</param>
        public void UpdateQuote(decimal bidPrice, decimal bidSize, decimal askPrice, decimal askSize)
        {
            Update(0, bidPrice, askPrice, 0, bidSize, askSize);
        }

        /// <summary>
        /// Updates this base data with the new quote bid information
        /// </summary>
        /// <param name="bidPrice">The current bid price</param>
        /// <param name="bidSize">The current bid size</param>
        public void UpdateBid(decimal bidPrice, decimal bidSize)
        {
            Update(0, bidPrice, 0, 0, bidSize, 0);
        }

        /// <summary>
        /// Updates this base data with the new quote ask information
        /// </summary>
        /// <param name="askPrice">The current ask price</param>
        /// <param name="askSize">The current ask size</param>
        public void UpdateAsk(decimal askPrice, decimal askSize)
        {
            Update(0, 0, askPrice, 0, 0, askSize);
        }

        /// <summary>
        /// Update routine to build a bar/tick from a data update.
        /// </summary>
        /// <param name="lastTrade">The last trade price</param>
        /// <param name="bidPrice">Current bid price</param>
        /// <param name="askPrice">Current asking price</param>
        /// <param name="volume">Volume of this trade</param>
        /// <param name="bidSize">The size of the current bid, if available</param>
        /// <param name="askSize">The size of the current ask, if available</param>
        public virtual void Update(decimal lastTrade, decimal bidPrice, decimal askPrice, decimal volume, decimal bidSize, decimal askSize)
        {
            Value = lastTrade;
        }
    }
}
