using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace StreamingTALib.UnitTests
{
    public class WilderMovingAverageTests
    {
        [Fact]
        public void Test_WilderMovingAverage()
        {
            var data = WilderMovingAverageTests.GetPerformanceDemoData();
            var closes = data.Item3;

            double[] outReal = new double[100];
            TicTacTec.TA.Library.Core.Wma(0, 99, closes.Take(100).ToArray(), 5, out int outBegIdx, out int outNbElement, outReal);

            var data2 = WilderMovingAverageTests.GetPerformanceDemoDataDecimal();
            var closesDecimal = data2.Item3;

            DirectionalMovementAggregateInitializer wilderMovingAverageInitializer = new DirectionalMovementAggregateInitializer(5);
            DirectionalMovementAggregate wilderMovingAverage = wilderMovingAverageInitializer.InitalizeWithData(closesDecimal.Take(5).ToList());

            var streamingTestData = closesDecimal.Skip(5).Take(100).ToList();
            DirectionalMovementAggregateState state = null;
            foreach (var value in streamingTestData)
            {
                state = wilderMovingAverage.IntegrateValue(value);
            }
        }

        private static Tuple<List<double>, List<double>, List<double>> GetPerformanceDemoData()
        {
            string csv_location = @"IB-NQ-Future3M-20-Globex.csv";
            List<double> closes = new List<double>();
            List<double> highs = new List<double>();
            List<double> lows = new List<double>();
            List<DateTime> dateTimes = new List<DateTime>();

            using (var reader = new StreamReader(csv_location))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    double high = double.Parse(values[2]);
                    double low = double.Parse(values[3]);
                    double close = double.Parse(values[4]);

                    highs.Add(high);
                    lows.Add(low);
                    closes.Add(close);
                }
            }

            return Tuple.Create(highs, lows, closes);
        }

        private static Tuple<List<decimal>, List<decimal>, List<decimal>> GetPerformanceDemoDataDecimal()
        {
            string csv_location = @"IB-NQ-Future3M-20-Globex.csv";
            List<decimal> closes = new List<decimal>();
            List<decimal> highs = new List<decimal>();
            List<decimal> lows = new List<decimal>();
            List<DateTime> dateTimes = new List<DateTime>();

            using (var reader = new StreamReader(csv_location))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    decimal high = decimal.Parse(values[2]);
                    decimal low = decimal.Parse(values[3]);
                    decimal close = decimal.Parse(values[4]);

                    highs.Add(high);
                    lows.Add(low);
                    closes.Add(close);
                }
            }

            return Tuple.Create(highs, lows, closes);
        }
    }
}
