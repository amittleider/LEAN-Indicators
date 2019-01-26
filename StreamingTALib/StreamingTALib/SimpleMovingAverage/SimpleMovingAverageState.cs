using System.Collections.Generic;

namespace StreamingTALib
{
    public class SimpleMovingAverageState
    {
        public Queue<decimal> ValueQueue
        {
            get;
            private set;
        }

        public decimal Mean
        {
            get;
            set;
        }

        public SimpleMovingAverageState(int period)
        {
            this.ValueQueue = new Queue<decimal>(period);
            this.Mean = 0m;
        }
    }
}