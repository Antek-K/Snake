using Xunit;
using FluentAssertions;

namespace Game.UnitTests
{

    public class DirectionBufferTests
    {
        [Theory]
        [InlineData(Direction.Left)] 
        [InlineData(Direction.Up)] 
        [InlineData(Direction.Right)] 
        [InlineData(Direction.Down)] 
        public void Initialize_AfterCalled_NextDirectionIsInitialDirections(Direction initialDirection)
        {
            // Arrange
            var directionBuffer = new DirectionBuffer(initialDirection);

            // Act
            directionBuffer.Initialize();

            // Assert
            directionBuffer.GetNextDirection().Should().Be(initialDirection);
        }

        [Theory]
        [InlineData(Direction.Left)]
        [InlineData(Direction.Up)]
        [InlineData(Direction.Right)]
        [InlineData(Direction.Down)]
        public void Initialize_AfterCalledOnNotEmptyBuffer_NextDirectionIsInitialDirections(Direction initialDirection)
        {
            // Arrange
            var directionBuffer = new DirectionBuffer(initialDirection);
            directionBuffer.Initialize();
            directionBuffer.AddDirectionToBuffer(Direction.Left);
            directionBuffer.AddDirectionToBuffer(Direction.Up);

            // Act
            directionBuffer.Initialize();

            // Assert
            directionBuffer.GetNextDirection().Should().Be(initialDirection);
        }

        [Fact]
        public void GetNextDirection_WhenQueueIsEmpty_KeepsReturningLastDirection()
        {
            // Arrange
            Direction initialDirection = Direction.Left;
            Direction nextDirection1 = Direction.Up;
            Direction nextDirection2 = Direction.Right;
            Direction nextDirection3 = Direction.Up;

            var directionBuffer = new DirectionBuffer(initialDirection);
            directionBuffer.Initialize();
            directionBuffer.AddDirectionToBuffer(nextDirection1);
            directionBuffer.AddDirectionToBuffer(nextDirection2);
            directionBuffer.AddDirectionToBuffer(nextDirection3);

            // Act&Assert
            directionBuffer.GetNextDirection().Should().Be(nextDirection1);
            directionBuffer.GetNextDirection().Should().Be(nextDirection2);
            directionBuffer.GetNextDirection().Should().Be(nextDirection3);
        }

        [Fact]
        public void GetNextDirection_WhenQueueIsNotEmpty_KeepsReturningDataFromIt()
        {
            // Arrange
            Direction initialDirection = Direction.Up;
            Direction nextDirection = Direction.Right;

            var directionBuffer = new DirectionBuffer(initialDirection);
            directionBuffer.Initialize();
            directionBuffer.AddDirectionToBuffer(nextDirection);
            directionBuffer.GetNextDirection();

            // Act&Assert
            directionBuffer.GetNextDirection().Should().Be(nextDirection);
            directionBuffer.GetNextDirection().Should().Be(nextDirection);
            directionBuffer.GetNextDirection().Should().Be(nextDirection);
        }

        [Theory]
        [InlineData(Direction.Left, Direction.Up)]
        [InlineData(Direction.Left, Direction.Down)]
        [InlineData(Direction.Right, Direction.Up)]
        [InlineData(Direction.Right, Direction.Down)]
        [InlineData(Direction.Up, Direction.Left)]
        [InlineData(Direction.Up, Direction.Right)]
        [InlineData(Direction.Down, Direction.Left)]
        [InlineData(Direction.Down, Direction.Right)]
        public void AddDirectionToBuffer_WhenDirectionIsAcceptable_AddsItToQueue(Direction initialDirection, Direction nextDirection)
        {
            // Arrange
            var directionBuffer = new DirectionBuffer(initialDirection);
            directionBuffer.Initialize();

            // Act
            directionBuffer.AddDirectionToBuffer(nextDirection);

            //Assert
            directionBuffer.GetNextDirection().Should().Be(nextDirection);
        }

        [Theory]
        [InlineData(Direction.Left, Direction.Left, Direction.Up)]
        [InlineData(Direction.Left, Direction.Right, Direction.Up)]
        [InlineData(Direction.Right, Direction.Right, Direction.Up)]
        [InlineData(Direction.Right, Direction.Left, Direction.Up)]
        [InlineData(Direction.Up, Direction.Up, Direction.Left)]
        [InlineData(Direction.Up, Direction.Down, Direction.Left)]
        [InlineData(Direction.Down, Direction.Down, Direction.Left)]
        [InlineData(Direction.Down, Direction.Up, Direction.Left)]
        public void AddDirectionToBuffer_WhenDirectionIsNotAcceptable_DiscardsIt(Direction initialDirection, Direction nextDirection, Direction finalDirection)
        {
            // Arrange
            var directionBuffer = new DirectionBuffer(initialDirection);
            directionBuffer.Initialize();

            // Act
            directionBuffer.AddDirectionToBuffer(nextDirection);
            directionBuffer.AddDirectionToBuffer(finalDirection);

            // Assert
            directionBuffer.GetNextDirection().Should().Be(finalDirection);
        }

    }
}
