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
using System.Diagnostics;
using QuantConnect.Data;

namespace QuantConnect.Indicators
{
    /// <summary>
    /// Provides a base type for all indicators
    /// </summary>
    /// <typeparam name="T">The type of data input into this indicator</typeparam>
    [DebuggerDisplay("{ToDetailedString()}")]
    public abstract partial class IndicatorBase<T> : IIndicator<T>
        where T : IBaseData
    {
        /// <summary>the most recent input that was given to this indicator</summary>
        private T _previousInput;

        /// <summary>
        /// Event handler that fires after this indicator is updated
        /// </summary>
        public event IndicatorUpdatedHandler Updated;

        /// <summary>
        /// Initializes a new instance of the Indicator class using the specified name.
        /// </summary>
        /// <param name="name">The name of this indicator</param>
        protected IndicatorBase(string name)
        {
            Name = name;
            Current = new IndicatorDataPoint(DateTime.MinValue, 0m);
        }

        /// <summary>
        /// Gets a name for this indicator
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a flag indicating when this indicator is ready and fully initialized
        /// </summary>
        public abstract bool IsReady { get; }

        /// <summary>
        /// Gets the current state of this indicator. If the state has not been updated
        /// then the time on the value will equal DateTime.MinValue.
        /// </summary>
        public IndicatorDataPoint Current { get; protected set; }

        /// <summary>
        /// Gets the number of samples processed by this indicator
        /// </summary>
        public long Samples { get; private set; }

        /// <summary>
        /// Updates the state of this indicator with the given value and returns true
        /// if this indicator is ready, false otherwise
        /// </summary>
        /// <param name="input">The value to use to update this indicator</param>
        /// <returns>True if this indicator is ready, false otherwise</returns>
        public bool Update(IBaseData input)
        {
            if (_previousInput != null && input.Time < _previousInput.Time)
            {
                // if we receive a time in the past, throw
                throw new ArgumentException($"This is a forward only indicator: {Name} Input: {input.Time:u} Previous: {_previousInput.Time:u}");
            }
            if (!ReferenceEquals(input, _previousInput))
            {
                // compute a new value and update our previous time
                Samples++;

                if (!(input is T))
                {
                    throw new ArgumentException($"IndicatorBase.Update() 'input' expected to be of type {typeof(T)} but is of type {input.GetType()}");
                }
                _previousInput = (T)input;

                var nextResult = ValidateAndComputeNextValue((T)input);
                if (nextResult.Status == IndicatorStatus.Success)
                {
                    Current = new IndicatorDataPoint(input.Time, nextResult.Value);

                    // let others know we've produced a new data point
                    OnUpdated(Current);
                }
            }
            return IsReady;
        }

        /// <summary>
        /// Updates the state of this indicator with the given value and returns true
        /// if this indicator is ready, false otherwise
        /// </summary>
        /// <param name="time">The time associated with the value</param>
        /// <param name="value">The value to use to update this indicator</param>
        /// <returns>True if this indicator is ready, false otherwise</returns>
        public bool Update(DateTime time, decimal value)
        {
            if (typeof(T) == typeof(IndicatorDataPoint))
            {
                return Update((T)(object)new IndicatorDataPoint(time, value));
            }
            else
            {
                throw new NotSupportedException(string.Format("{0} does not support Update(DateTime, decimal) method overload. Use Update({1}) instead.", typeof(T).Name));
            }
        }

        /// <summary>
        /// Resets this indicator to its initial state
        /// </summary>
        public virtual void Reset()
        {
            Samples = 0;
            _previousInput = default(T);
            Current = new IndicatorDataPoint(DateTime.MinValue, default(decimal));
        }

        /// <summary>
        /// Provides a more detailed string of this indicator in the form of {Name} - {Value}
        /// </summary>
        /// <returns>A detailed string of this indicator's current state</returns>
        public string ToDetailedString()
        {
            return string.Format("{0} - {1}", Name, this);
        }

        /// <summary>
        /// Computes the next value of this indicator from the given state
        /// </summary>
        /// <param name="input">The input given to the indicator</param>
        /// <returns>A new value for this indicator</returns>
        protected abstract decimal ComputeNextValue(T input);

        /// <summary>
        /// Computes the next value of this indicator from the given state
        /// and returns an instance of the <see cref="IndicatorResult"/> class
        /// </summary>
        /// <param name="input">The input given to the indicator</param>
        /// <returns>An IndicatorResult object including the status of the indicator</returns>
        protected virtual IndicatorResult ValidateAndComputeNextValue(T input)
        {
            // default implementation always returns IndicatorStatus.Success
            return new IndicatorResult(ComputeNextValue(input));
        }

        /// <summary>
        /// Event invocator for the Updated event
        /// </summary>
        /// <param name="consolidated">This is the new piece of data produced by this indicator</param>
        protected virtual void OnUpdated(IndicatorDataPoint consolidated)
        {
            var handler = Updated;
            if (handler != null) handler(this, consolidated);
        }
    }
}