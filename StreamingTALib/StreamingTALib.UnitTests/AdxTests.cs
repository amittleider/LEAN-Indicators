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
        public void Adx_Should_MatchWildersBook()
        {
            decimal[] highs = new decimal[] { 274m, 273.25m, 272m, 270.75m, 270m, 270.5m, 268.5m, 265.5m, 262.5m, 263.5m, 269.5m, 267.25m, 267.5m, 269.75m, 268.25m, 264m, 268m, 266m, 274m, 277.5m, 277m, 272m, 267.75m, 269.25m, 266m, 265m, 264.75m, 261m, 257.5m, 259m, 259.75m, 257.25m, 250m, 254.25m, 254m, 253.25m, 253.25m, 251.75m, 253m, 251.5m, 246.25m, 244.25m };
            decimal[] lows = new decimal[] { 272m, 270.25m, 269.75m, 268m, 269m, 268m, 266.5m, 263m, 259m, 260m, 263m, 265m, 265.5m, 266m, 263.25m, 261.5m, 266.25m, 264.25m, 267m, 273.5m, 272.5m, 269.58m, 264m, 263m, 263.5m, 262m, 261.5m, 255.5m, 253m, 254m, 257.5m, 250m, 247m, 252.75m, 250.5m, 250.25m, 251m, 250.5m, 249.50m, 245.25m, 240m, 244.25m };
            decimal[] closes = new decimal[] { 272.75m, 270.75m, 270m, 269.25m, 269.75m, 270m, 266.5m, 263.25m, 260.25m, 263m, 266.5m, 267m, 265.75m, 268.5m, 264.25m, 264m, 266.5m, 265.25m, 273m, 276.75m, 273m, 270.25m, 266.75m, 263m, 265.5m, 262.25m, 262.75m, 255.5m, 253m, 257.5m, 257.5m, 250m, 249.75m, 253.75m, 251.25m, 250.5m, 253m, 251.5m, 250m, 245.75m, 242.75m, 243.5m };

            decimal[] expectedAdx = { 17, 18, 18, 20, 22, 22, 23, 23, 24, 25, 25, 26, 27, 29 };

            AdxInitializer adxInitializer = new AdxInitializer(14);
            int lookback = AdxInitializer.GetLookback(14);
            Adx adx = adxInitializer.InitializeWithData(highs.Take(28).ToList(), lows.Take(28).ToList(), closes.Take(28).ToList());

            for (int i = 28; i < highs.Length; i++)
            {
                AdxState adxState = adx.ComputeNextValue(highs[i], lows[i], closes[i]);
                adxState.AverageDirectionalIndex.Should().BeApproximately(expectedAdx[i - 28], 2m);
            }
        }

        [Fact]
        public void Adx_Should_MatchTALib()
        {
            // Need to fix this test, use the performance demo data
            ////decimal[] highs = new decimal[] { 274m, 273.25m, 272m, 270.75m, 270m, 270.5m, 268.5m, 265.5m, 262.5m, 263.5m, 269.5m, 267.25m, 267.5m, 269.75m, 268.25m, 264m, 268m, 266m, 274m, 277.5m, 277m, 272m, 267.75m, 269.25m, 266m, 265m, 264.75m, 261m, 257.5m, 259m, 259.75m, 257.25m, 250m, 254.25m, 254m, 253.25m, 253.25m, 251.75m, 253m, 251.5m, 246.25m, 244.25m };
            ////decimal[] lows = new decimal[] { 272m, 270.25m, 269.75m, 268m, 269m, 268m, 266.5m, 263m, 259m, 260m, 263m, 265m, 265.5m, 266m, 263.25m, 261.5m, 266.25m, 264.25m, 267m, 273.5m, 272.5m, 269.58m, 264m, 263m, 263.5m, 262m, 261.5m, 255.5m, 253m, 254m, 257.5m, 250m, 247m, 252.75m, 250.5m, 250.25m, 251m, 250.5m, 249.50m, 245.25m, 240m, 244.25m };
            ////decimal[] closes = new decimal[] { 272.75m, 270.75m, 270m, 269.25m, 269.75m, 270m, 266.5m, 263.25m, 260.25m, 263m, 266.5m, 267m, 265.75m, 268.5m, 264.25m, 264m, 266.5m, 265.25m, 273m, 276.75m, 273m, 270.25m, 266.75m, 263m, 265.5m, 262.25m, 262.75m, 255.5m, 253m, 257.5m, 257.5m, 250m, 249.75m, 253.75m, 251.25m, 250.5m, 253m, 251.5m, 250m, 245.75m, 242.75m, 243.5m };

            ////decimal[] expectedAdx = { 17, 18, 18, 20, 22, 22, 23, 23, 24, 25, 25, 26, 27, 29 };

            ////AdxInitializer adxInitializer = new AdxInitializer(14);
            ////int lookback = AdxInitializer.GetLookback(14);
            ////Adx adx = adxInitializer.InitializeWithData(highs.Take(28).ToList(), lows.Take(28).ToList(), closes.Take(28).ToList());

            ////for (int i = 28; i < highs.Length; i++)
            ////{
            ////    AdxState adxState = adx.ComputeNextValue(highs[i], lows[i], closes[i]);
            ////    adxState.AverageDirectionalIndex.Should().BeApproximately(expectedAdx[i - 28], 2m);
            ////}
        }

        private static Tuple<List<decimal>, List<decimal>, List<decimal>> GetPerformanceDemoData()
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
