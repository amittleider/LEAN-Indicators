namespace StreamingTALib.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    public class SimpleMovingAverageTests
    {
        [Fact]
        public void SimpleMovingAverage_Should_EqualLinq()
        {
            SimpleMovingAverage sma = new SimpleMovingAverageInitializer(3).InitalizeWithData(new List<decimal> { 10, 10, 10 });
            SimpleMovingAverageState state = null;

            // Call
            state = sma.IntegrateValue(10);
            state.ValueQueue.ToArray().Should().BeEquivalentTo(new decimal[] { 10m, 10m, 10m });
            state.Mean.Should().BeApproximately(state.ValueQueue.ToArray().Average(), 0.0000001m);

            // Call
            sma.IntegrateValue(10);
            state.ValueQueue.ToArray().Should().BeEquivalentTo(new decimal[] { 10m, 10m, 10m });
            state.Mean.Should().BeApproximately(state.ValueQueue.ToArray().Average(), 0.0000001m);

            // Call
            sma.IntegrateValue(10);
            state.ValueQueue.ToArray().Should().BeEquivalentTo(new decimal[] { 10m, 10m, 10m });
            state.Mean.Should().BeApproximately(state.ValueQueue.ToArray().Average(), 0.0000001m);
        }

        [Fact]
        public void SimpleMovingAverage_Should_EqualLinq2()
        {
            SimpleMovingAverage sma = new SimpleMovingAverageInitializer(4).InitalizeWithData(new List<decimal> { 10, 20, 30, 33 });
            SimpleMovingAverageState state = null;

            // Call
            state = sma.IntegrateValue(50);
            state.ValueQueue.ToArray().Should().BeEquivalentTo(new decimal[] { 20m, 30, 33, 50 });
            state.Mean.Should().BeApproximately(state.ValueQueue.ToArray().Average(), 0.0000001m);

            // Call
            sma.IntegrateValue(200);
            state.ValueQueue.ToArray().Should().BeEquivalentTo(new decimal[] { 30, 33, 50, 200 });
            state.Mean.Should().BeApproximately(state.ValueQueue.ToArray().Average(), 0.0000001m);

            // Call
            sma.IntegrateValue(0);
            state.ValueQueue.ToArray().Should().BeEquivalentTo(new decimal[] { 33, 50, 200, 0 });
            state.Mean.Should().BeApproximately(state.ValueQueue.ToArray().Average(), 0.0000001m);
        }

        [Fact]
        public void SimpleMovingAverageInitializer_Should_ThrowWhenNotEnoughData()
        {
            Assert.Throws<Exception>(() => new SimpleMovingAverageInitializer(4).InitalizeWithData(new List<decimal> { 10, 30, 33 }));
        }
    }
}
