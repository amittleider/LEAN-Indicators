namespace StreamingTALib
{
    public class SimpleMovingAverage
    {
        private int k;

        internal SimpleMovingAverageState SimpleMovingAverageState { get; }

        public SimpleMovingAverage(int period)
        {
            this.k = 0;
            this.SimpleMovingAverageState = new SimpleMovingAverageState(period);

            for (int i = 0; i < period; i++)
            {
                this.AddValue(0m);
            }
        }

        public SimpleMovingAverageState IntegrateValue(decimal value)
        {
            decimal oldValue = this.SimpleMovingAverageState.ValueQueue.Dequeue();
            this.RemoveValue(oldValue);
            this.AddValue(value);
            return this.SimpleMovingAverageState;
        }

        private void AddValue(decimal value)
        {
            this.k += 1;
            decimal current_mean = this.SimpleMovingAverageState.Mean;
            decimal new_mean = this.SimpleMovingAverageState.Mean + (value - this.SimpleMovingAverageState.Mean) / this.k;
            this.SimpleMovingAverageState.Mean = new_mean;
            this.SimpleMovingAverageState.ValueQueue.Enqueue(value);
        }

        private void RemoveValue(decimal value)
        {
            this.k -= 1;
            decimal new_mean = this.SimpleMovingAverageState.Mean - (value - this.SimpleMovingAverageState.Mean) / this.k;
            this.SimpleMovingAverageState.Mean = new_mean;
        }
    }
}
