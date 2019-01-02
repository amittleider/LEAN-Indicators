// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib
{
    /// <summary>
    /// The simple moving average indicator
    /// TODO: This class implements a queue but it's unnecessary.  Instead, you can subtract a fraction of the current mean and get rid of the queue.
    /// </summary>
    public class SimpleMovingAverage
    {
        private int k;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleMovingAverage"/> class.
        /// </summary>
        /// <param name="period">The period of the simple moving average</param>
        public SimpleMovingAverage(int period)
        {
            this.k = 0;
            this.SimpleMovingAverageState = new SimpleMovingAverageState(period);

            for (int i = 0; i < period; i++)
            {
                this.AddValue(0m);
            }
        }

        /// <summary>
        /// Gets the simple moving average state
        /// </summary>
        public SimpleMovingAverageState SimpleMovingAverageState
        {
            get;
            private set;
        }

        /// <summary>
        /// Integrate the following value with the indicator
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>The current state of the simple moving average</returns>
        public SimpleMovingAverageState IntegrateValue(decimal value)
        {
            decimal oldValue = this.SimpleMovingAverageState.ValueQueue.Dequeue();
            this.RemoveValue(oldValue);
            this.AddValue(value);
            return this.SimpleMovingAverageState;
        }

        /// <summary>
        /// Add a value to the current average
        /// </summary>
        /// <param name="value">The value</param>
        private void AddValue(decimal value)
        {
            this.k += 1;
            decimal current_mean = this.SimpleMovingAverageState.Mean;
            decimal new_mean = this.SimpleMovingAverageState.Mean + (value - this.SimpleMovingAverageState.Mean) / this.k;
            this.SimpleMovingAverageState.Mean = new_mean;
            this.SimpleMovingAverageState.ValueQueue.Enqueue(value);
        }

        /// <summary>
        /// Remove a value from the current average
        /// </summary>
        /// <param name="value">The value to remove</param>
        private void RemoveValue(decimal value)
        {
            this.k -= 1;
            decimal new_mean = this.SimpleMovingAverageState.Mean - (value - this.SimpleMovingAverageState.Mean) / this.k;
            this.SimpleMovingAverageState.Mean = new_mean;
        }
    }
}
