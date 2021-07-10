using System.ComponentModel;
using Xunit;
using FluentAssertions;

namespace Game.UnitTests
{
    // [Apartment(ApartmentState.STA)]
    public class SnakeStateTests
    {
        [Fact]
        public void SnakeState_SetsSnakeLengthToSnakeInitialLength()
        {
            // Arrange
            int snakeInitialLength = 2;

            // Act
            var snakeState = new SnakeState(snakeInitialLength, 0);

            // Assert
            snakeState.SnakeLength.Should().Be(snakeInitialLength);
        }

        [Fact]
        public void SnakeLengthSetter_RaisePropertyChangedForScore()
        {
            // Arrange
            var snakeState = new SnakeState(0, 0);
            object eventSender = null;
            PropertyChangedEventArgs eventArgs = null;
            snakeState.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { eventSender = sender; eventArgs = e; };

            // Act
            snakeState.SnakeLength = 1;

            // Assert
            eventSender.Should().Be(snakeState);
            eventArgs.PropertyName.Should().Be("Score");

        }

        [Fact]
        public void IsDeadSetter_RaisePropertyChanged()
        {
            // Arrange
            var snakeState = new SnakeState(0, 0);
            object eventSender = null;
            PropertyChangedEventArgs eventArgs = null;
            snakeState.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { eventSender = sender; eventArgs = e; };

            // Act
            snakeState.IsDead = true;

            // Assert
            eventSender.Should().Be(snakeState);
            eventArgs.PropertyName.Should().Be("IsDead");
        }

        [Theory]
        [InlineData(1, 1, 1, 0)]
        [InlineData(1, 1, 2, 1)]
        [InlineData(1, 2, 2, 2)]
        [InlineData(0, 3, 1, 3)]
        public void ScoreGetter_ReturnsProperValue(int snakeInitialLength, int scoreFactor, int snakeLength, int expScore)
        {
            // Arrange
            var snakeState = new SnakeState(snakeInitialLength, scoreFactor)
            {
                SnakeLength = snakeLength
            };

            // Act
            var score = snakeState.Score;

            // Assert
            score.Should().Be(expScore);
        }
    }
}
