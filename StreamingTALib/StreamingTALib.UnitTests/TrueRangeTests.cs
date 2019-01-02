namespace StreamingTALib.UnitTests
{
    using FluentAssertions;
    using System;
    using System.Linq;
    using Xunit;

    public class TrueRangeTests
    {
        [Fact]
        public void TrueRange_Should_ReturnTALibResult()
        {
            decimal[] highs = new decimal[] { 12.3332m, 10.2113m, 8.22281m, 11.20193m };
            decimal[] lows = new decimal[] { 12.3332m, 10.2113m, 8.22281m, 11.20193m };
            decimal[] closes = { 12.3332m, 10.2113m, 8.22281m, 11.20193m };

            double[] highsDouble = Array.ConvertAll(highs, x => (double)x);
            double[] lowsDouble = Array.ConvertAll(lows, x => (double)x);
            double[] closesDouble = Array.ConvertAll(closes, x => (double)x);

            double[] outReal = new double[highs.Length];
            TicTacTec.TA.Library.Core.TrueRange(0, 3, highsDouble, lowsDouble, closesDouble, out int begIdx, out int nbElement, outReal);

            TrueRangeInitializer trueRangeInitializer = new TrueRangeInitializer();
            TrueRange trueRange = trueRangeInitializer.InitializeWithData(highs.Take(1).ToList(), lows.Take(1).ToList(), closes.Take(1).ToList());

            TrueRangeState trueRangeState = null;
            trueRangeState = trueRange.ComputeTrueRange(highs[1], lows[1], closes[1]);
            trueRangeState.TrueRange.Should().BeApproximately((decimal)outReal[0], 0.0001m);
            trueRangeState = trueRange.ComputeTrueRange(highs[2], lows[2], closes[2]);
            trueRangeState.TrueRange.Should().BeApproximately((decimal)outReal[1], 0.0001m);
            trueRangeState = trueRange.ComputeTrueRange(highs[3], lows[3], closes[3]);
            trueRangeState.TrueRange.Should().BeApproximately((decimal)outReal[2], 0.0001m);
        }
    }
}
