// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    /// <summary>
    /// Test the RSI calcuations
    /// </summary>
    public class RsiTests
    {
        /// <summary>
        /// Test that an RSI25 is properly initialized and streaming values are computed properly
        /// </summary>
        [Fact]
        public void Rsi25_Should_ProduceCorrectResult()
        {
            ////TicTacTec.TA.Library.Core.Rsi
            List<double> performanceData = GetPerformanceDemoData();

            int period = 25;
            int lookback = RsiInitializer.GetLookback(period);

            List<double> rsiCalculationData = performanceData.TakeLast(lookback + 4).ToList();
            List<double> rsiInitializerData = rsiCalculationData.Take(lookback).ToList();
            List<double> streamingData = rsiCalculationData.Skip(lookback).ToList();

            RsiInitializer rsiInitializer = new RsiInitializer(period);
            Rsi rsi = rsiInitializer.InitalizeWithData(rsiInitializerData);

            rsi.CalculateRsiWithNewPrice(streamingData[0]).RsiValue.Should().BeApproximately(51.6190, 0.0001);
            rsi.CalculateRsiWithNewPrice(streamingData[1]).RsiValue.Should().BeApproximately(52.8735, 0.0001);
            rsi.CalculateRsiWithNewPrice(streamingData[2]).RsiValue.Should().BeApproximately(49.8797, 0.0001);
            rsi.CalculateRsiWithNewPrice(streamingData[3]).RsiValue.Should().BeApproximately(51.3154, 0.0001);
        }

        private static List<double> GetPerformanceDemoData()
        {
            string csv_location = @"IB-NQ-Future3M-20-Globex.csv";
            List<double> closes = new List<double>();
            List<DateTime> dateTimes = new List<DateTime>();

            using (var reader = new StreamReader(csv_location))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    double close = double.Parse(values[4]);
                    closes.Add(close);
                }
            }

            return closes;
        }
    }
}
