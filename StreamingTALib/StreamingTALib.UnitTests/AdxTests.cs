using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace StreamingTALib.UnitTests
{
    public class AdxTests
    {
        [Fact]
        public void Adx_Should_Equal_TaLib()
        {
            var data = AdxTests.GetPerformanceDemoData();
            var highs = data.Item1;
            var lows = data.Item2;
            var closes = data.Item3;

            double[] outReal = new double[91];
            TicTacTec.TA.Library.Core.Adx(0, 99, highs.Take(100).ToArray(), lows.Take(100).ToArray(), closes.Take(100).ToArray(), 5, out int outBegIdx, out int nbElement, outReal);

            var decimalData = AdxTests.GetPerformanceDemoDataDecimal();
            var highs2 = decimalData.Item1;
            var lows2 = decimalData.Item2;
            var closes2 = decimalData.Item3;

            AdxInitializer adxInitializer = new AdxInitializer(5);
            Adx adx = adxInitializer.InitializeWithData(highs2, lows2, closes2);

            adx.AdxState.AverageDirectionalIndex.Should().BeApproximately(10m, 0.0001m);
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
