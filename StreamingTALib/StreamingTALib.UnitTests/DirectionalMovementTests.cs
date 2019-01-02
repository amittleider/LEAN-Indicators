// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace StreamingTALib.UnitTests
{
    using FluentAssertions;
    using System.Linq;
    using Xunit;

    public class DirectionalMovementTests
    {
        [Fact]
        public void DirectionalMovement_Should_MatchWildersBook()
        {
            decimal[] highs = new decimal[] { 274m, 273.25m, 272m, 270.75m, 270m, 270.5m, 268.5m, 265.5m, 262.5m, 263.5m, 269.5m, 267.25m, 267.5m, 269.75m };
            decimal[] lows = new decimal[] { 272m, 270.25m, 269.75m, 268m, 269m, 268m, 266.5m, 263m, 259m, 260m, 263m, 265m, 265.5m, 266m };
            decimal[] closes = new decimal[] { 272.75m, 270.75m, 270m, 269.25m, 269.75m, 270m, 266.5m, 263.25m, 260.25m, 263m, 266.5m, 267m, 265.75m, 268.5m };

            decimal[] expectedPlusDM = { 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 1m, 6m, 0m, 0.25m, 2.25m };
            decimal[] expectedMinusDM = { 1.75m, .5m, 1.75m, 0m, 1m, 1.5m, 3.5m, 4.0m, 0m, 0m, 0m, 0m, 0m };
            decimal[] expectedTrueRange = { 3m, 2.25m, 2.75m, 1m, 2.5m, 3.5m, 3.5m, 4.25m, 3.5m, 6.5m, 2.25m, 2.0m, 4.0m };

            DirectionalMovementState directionalMovementState = null;
            DirectionalMovementInitializer directionalMovementInitializer = new DirectionalMovementInitializer();
            DirectionalMovement plusDm = directionalMovementInitializer.InitalizeWithData(highs.Take(1).ToList(), lows.Take(1).ToList());

            directionalMovementState = plusDm.IntegrateValue(highs[1], lows[1]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[0], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[0], 0.0001m);

            directionalMovementState = plusDm.IntegrateValue(highs[2], lows[2]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[1], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[1], 0.0001m);

            directionalMovementState = plusDm.IntegrateValue(highs[3], lows[3]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[2], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[2], 0.0001m);

            directionalMovementState = plusDm.IntegrateValue(highs[4], lows[4]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[3], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[3], 0.0001m);

            directionalMovementState = plusDm.IntegrateValue(highs[5], lows[5]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[4], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[4], 0.0001m);

            directionalMovementState = plusDm.IntegrateValue(highs[6], lows[6]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[5], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[5], 0.0001m);

            directionalMovementState = plusDm.IntegrateValue(highs[7], lows[7]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[6], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[6], 0.0001m);

            directionalMovementState = plusDm.IntegrateValue(highs[8], lows[8]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[7], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[7], 0.0001m);

            directionalMovementState = plusDm.IntegrateValue(highs[9], lows[9]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[8], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[8], 0.0001m);

            directionalMovementState = plusDm.IntegrateValue(highs[10], lows[10]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[9], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[9], 0.0001m);

            directionalMovementState = plusDm.IntegrateValue(highs[11], lows[11]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[10], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[10], 0.0001m);

            directionalMovementState = plusDm.IntegrateValue(highs[12], lows[12]);
            directionalMovementState.PlusDirectionalMovement.Should().BeApproximately(expectedPlusDM[11], 0.0001m);
            directionalMovementState.MinusDirectionalMovement.Should().BeApproximately(expectedMinusDM[11], 0.0001m);
        }
    }
}
