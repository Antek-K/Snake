using Xunit;
using FluentAssertions;
using Moq;

namespace Game.UnitTests.Control
{
    public class SnakeMoverTests
    {
        [Fact]
        public void Initialize_OldSnakeIsClearedAndNewSnakeIsExtendedProperNumberOfTimes()
        {
            // Arrange
            var cellLocationMock = new Mock<CellLocation>();
            cellLocationMock.SetupGet(cl => cl.Row).Returns(1);
            cellLocationMock.SetupGet(cl => cl.Column).Returns(1);
            
            var snakeMock = new Mock<Snake>();
            snakeMock.SetupGet(s => s.HeadLocation).Returns(cellLocationMock.Object);
            snakeMock.Setup(s => s.Clear());
            snakeMock.Setup(s => s.MoveHead(It.IsAny<CellLocation>()));
            var snakeSateMock = new Mock<SnakeState>();
            int snakeInitialLength = 2;
            var snakeMover = new SnakeMover(snakeMock.Object, snakeSateMock.Object, 3, 3, 0, 0, snakeInitialLength);

            // Act
            snakeMover.Initialize(Direction.Right);

            // Assert
            snakeMock.Verify(s => s.Clear(), Times.Once);
            snakeMock.Verify(s => s.MoveHead(It.IsAny<CellLocation>()), Times.Exactly(snakeInitialLength));
        }

        [Theory]
        [InlineData(0, 0, 2, 3, Direction.Right, 0, 1)]
        [InlineData(0, 2, 2, 3, Direction.Right, 0, 0)]
        [InlineData(0, 0, 2, 3, Direction.Left, 0, 2)]
        [InlineData(0, 1, 2, 3, Direction.Left, 0, 0)]
        [InlineData(0, 0, 3, 2, Direction.Down, 1, 0)]
        [InlineData(3, 0, 4, 2, Direction.Down, 0, 0)]
        [InlineData(1, 0, 4, 2, Direction.Up, 0, 0)]
        [InlineData(0, 0, 3, 2, Direction.Up, 2, 0)]
        public void PrepareNextMove_ReturnsProperNextLocation(int snakeHeadLocationRow, int snakeHeadLocationColumn, int gameBoardRowCount, int gameBoardColumnCount, Direction direction, int ExpectedSnakeHeadLocationRow, int ExpectedSnakeHeadLocationColumn)
        {
            // Arrange
            var cellLocationMock = new Mock<CellLocation>();
            cellLocationMock.SetupGet(cl => cl.Row).Returns(snakeHeadLocationRow);
            cellLocationMock.SetupGet(cl => cl.Column).Returns(snakeHeadLocationColumn);

            var snakeMock = new Mock<Snake>();
            snakeMock.SetupGet(s => s.HeadLocation).Returns(cellLocationMock.Object);

            var snakeSateMock = new Mock<SnakeState>();
            var snakeMover = new SnakeMover(snakeMock.Object, snakeSateMock.Object, gameBoardRowCount, gameBoardColumnCount, 0, 0, 0);            

            // Act
            var nextHeadLocation = snakeMover.PrepareNextMove(direction);

            // Assert
            nextHeadLocation.Row.Should().Be(ExpectedSnakeHeadLocationRow);
            nextHeadLocation.Column.Should().Be(ExpectedSnakeHeadLocationColumn);
        }

        [Fact]
        public void IsSnakeDying_WhenNextHeadLocationIsLocatedOnSnake_CallsIsLocationOnSnakeAndReturnsTrue() 
        {
            // Arrange
            var snakeMock = new Mock<Snake>();
            snakeMock.Setup(s => s.IsLocationOnSnake(It.IsAny<CellLocation>())).Returns(true);
            var snakeSateMock = new Mock<SnakeState>();
            var snakeMover = new SnakeMover(snakeMock.Object, snakeSateMock.Object, 0, 0, 0, 0, 0);

            // Act
            var isSnakeDying = snakeMover.IsSnakeDying();

            // Assert
            snakeMock.Verify(s => s.IsLocationOnSnake(It.IsAny<CellLocation>()), Times.Once);
            isSnakeDying.Should().Be(true);
        }

        [Fact]
        public void IsSnakeDying_WhenNextHeadLocationIsNotLocatedOnSnake_CallsIsLocationOnSnakeAndReturnsFalse()
        {
            // Arrange
            var snakeMock = new Mock<Snake>();
            snakeMock.Setup(s => s.IsLocationOnSnake(It.IsAny<CellLocation>())).Returns(false);
            var snakeSateMock = new Mock<SnakeState>();
            var snakeMover = new SnakeMover(snakeMock.Object, snakeSateMock.Object, 0, 0, 0, 0, 0);

            // Act
            var isSnakeDying = snakeMover.IsSnakeDying();

            // Assert
            snakeMock.Verify(s => s.IsLocationOnSnake(It.IsAny<CellLocation>()), Times.Once);
            isSnakeDying.Should().Be(false);
        }

        [Fact]
        public void IsSnakeEating_WhenNextHeadLocationIsTheSameAsFood_CallsEqualsAndReturnsTrue()
        {
            // Arrange
            var snakeMock = new Mock<Snake>();
            var snakeSateMock = new Mock<SnakeState>();
            var snakeMover = new SnakeMover(snakeMock.Object, snakeSateMock.Object, 0, 0, 0, 0, 0);

            var CellLocationMock = new Mock<CellLocation>();
            CellLocationMock.Setup(cl => cl.Equals(It.IsAny<CellLocation>())).Returns(true);

            // Act
            var isSnakeEating = snakeMover.IsSnakeEating(CellLocationMock.Object);

            // Assert
            CellLocationMock.Verify(cl => cl.Equals(It.IsAny<CellLocation>()), Times.Once);
            isSnakeEating.Should().Be(true);
        }

        [Fact]
        public void IsSnakeEating_WhenNextHeadLocationIsNotTheSameAsFood_CallsEqualsAndReturnsFalse()
        {
            // Arrange
            var snakeMock = new Mock<Snake>();
            var snakeSateMock = new Mock<SnakeState>();
            var snakeMover = new SnakeMover(snakeMock.Object, snakeSateMock.Object, 0, 0, 0, 0, 0);

            var CellLocationMock = new Mock<CellLocation>();
            CellLocationMock.Setup(cl => cl.Equals(It.IsAny<CellLocation>())).Returns(false);

            // Act
            var isSnakeEating = snakeMover.IsSnakeEating(CellLocationMock.Object);

            // Assert
            CellLocationMock.Verify(cl => cl.Equals(It.IsAny<CellLocation>()), Times.Once);
            isSnakeEating.Should().Be(false);
        }

        [Fact]
        public void Move_CallsMoveHeadAndMoveTail()
        {
            // Arrange
            var snakeMock = new Mock<Snake>();
            snakeMock.Setup(s => s.MoveHead(It.IsAny<CellLocation>()));
            snakeMock.Setup(s => s.MoveTail());
            var snakeSateMock = new Mock<SnakeState>();
            var snakeMover = new SnakeMover(snakeMock.Object, snakeSateMock.Object, 0, 0, 0, 0, 0);

            // Act
            snakeMover.Move();

            // Assert
            snakeMock.Verify(s => s.MoveHead(It.IsAny<CellLocation>()), Times.Once);
            snakeMock.Verify(s => s.MoveTail(), Times.Once);
        }

        [Fact]
        public void ExtendAndMove_CallsMoveHeadAndUpdatesSnakeLength()
        {
            // Arrange
            var snakeMock = new Mock<Snake>();
            snakeMock.Setup(s => s.MoveHead(It.IsAny<CellLocation>()));
            snakeMock.SetupGet(s => s.Length).Returns(1);
            var snakeSateMock = new Mock<SnakeState>();
            snakeSateMock.SetupSet(ss => ss.SnakeLength = 1);
            var snakeMover = new SnakeMover(snakeMock.Object, snakeSateMock.Object, 0, 0, 0, 0, 0);

            // Act
            snakeMover.ExtendAndMove();

            // Assert
            snakeMock.Verify(s => s.MoveHead(It.IsAny<CellLocation>()), Times.Once);
            snakeSateMock.VerifySet(ss => ss.SnakeLength = 1);
        }
    }
}
