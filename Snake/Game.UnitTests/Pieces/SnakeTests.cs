using Xunit;
using FluentAssertions;
using Moq;

namespace Game.UnitTests
{
    public class SnakeTests
    {
        [StaFact]
        public void IsLocationOnSnake_WhenContainsCellLoactaion_ReturnsTrue()
        {
            // Arrange
            var cellMock1 = new Mock<Cell>();
            var cellMock2 = new Mock<Cell>();

            var cellLocationMock1 = new Mock<CellLocation>();
            cellLocationMock1.Setup(cl => cl.Equals(It.IsAny<CellLocation>())).Returns(false);
            cellLocationMock1.Setup(cl => cl.Equals(cellLocationMock1.Object)).Returns(true);
            var cellLocationMock2 = new Mock<CellLocation>();
            cellLocationMock2.Setup(cl => cl.Equals(It.IsAny<CellLocation>())).Returns(false);
            cellLocationMock2.Setup(cl => cl.Equals(cellLocationMock2.Object)).Returns(true);

            var gameBoardMock = new Mock<GameBoard>();
            gameBoardMock.Setup(gb => gb[cellLocationMock1.Object]).Returns(cellMock1.Object);
            gameBoardMock.Setup(gb => gb[cellLocationMock2.Object]).Returns(cellMock2.Object);

            var snake = new Snake(gameBoardMock.Object);
            snake.MoveHead(cellLocationMock1.Object);
            snake.MoveHead(cellLocationMock2.Object);

            // Act
            var isLocationOnSnake = snake.IsLocationOnSnake(cellLocationMock2.Object);

            // Assert
            isLocationOnSnake.Should().BeTrue();
        }

        [StaFact]
        public void IsLocationOnSnake_WhenDoesNotContainCellLoactaion_ReturnsFalse()
        {
            // Arrange
            var cellMock1 = new Mock<Cell>();

            var cellLocationMock1 = new Mock<CellLocation>();
            cellLocationMock1.Setup(cl => cl.Equals(It.IsAny<CellLocation>())).Returns(false);
            cellLocationMock1.Setup(cl => cl.Equals(cellLocationMock1.Object)).Returns(true);

            var cellLocationMock2 = new Mock<CellLocation>();
            cellLocationMock2.Setup(cl => cl.Equals(It.IsAny<CellLocation>())).Returns(false);
            cellLocationMock2.Setup(cl => cl.Equals(cellLocationMock2.Object)).Returns(true);

            var gameBoardMock = new Mock<GameBoard>();
            gameBoardMock.Setup(gb => gb[cellLocationMock1.Object]).Returns(cellMock1.Object);

            var snake = new Snake(gameBoardMock.Object);
            snake.MoveHead(cellLocationMock1.Object);

            // Act
            var isLocationOnSnake = snake.IsLocationOnSnake(cellLocationMock2.Object);

            // Assert
            isLocationOnSnake.Should().BeFalse();
        }

        [StaFact]
        public void Clear_ClearsAllCellsInQueueAndQueueItself()
        {
            // Arrange
            var cellMock1 = new Mock<Cell>();
            cellMock1.Setup(c => c.Clear());
            var cellMock2 = new Mock<Cell>();
            cellMock2.Setup(c => c.Clear());

            var cellLocationMock1 = new Mock<CellLocation>();
            var cellLocationMock2 = new Mock<CellLocation>();

            var gameBoardMock = new Mock<GameBoard>();
            gameBoardMock.Setup(gb => gb[cellLocationMock1.Object]).Returns(cellMock1.Object);
            gameBoardMock.Setup(gb => gb[cellLocationMock2.Object]).Returns(cellMock2.Object);

            var snake = new Snake(gameBoardMock.Object);
            snake.MoveHead(cellLocationMock1.Object);
            snake.MoveHead(cellLocationMock2.Object);

            // Act
            snake.Clear();

            // Assert
            snake.Length.Should().Be(0);
            cellMock1.Verify(c => c.Clear(), Times.Once);
            cellMock2.Verify(c => c.Clear(), Times.Once);
        }

        [StaFact]
        public void MoveHead_ChnagesHeadLocationExtendSnakeAndPlaceSnakeOnNewHeadLocation()
        {
            // Arrange
            var cellMock1 = new Mock<Cell>();
            var cellMock2 = new Mock<Cell>();
            cellMock2.Setup(c => c.PlaceSnake());

            var cellLocationMock1 = new Mock<CellLocation>();
            var cellLocationMock2 = new Mock<CellLocation>();

            var gameBoardMock = new Mock<GameBoard>();
            gameBoardMock.Setup(gb => gb[cellLocationMock1.Object]).Returns(cellMock1.Object);
            gameBoardMock.Setup(gb => gb[cellLocationMock2.Object]).Returns(cellMock2.Object);

            var snake = new Snake(gameBoardMock.Object);
            snake.MoveHead(cellLocationMock1.Object);

            // Act
            snake.MoveHead(cellLocationMock2.Object);

            // Assert
            snake.HeadLocation.Should().BeSameAs(cellLocationMock2.Object);
            snake.Length.Should().Be(2);
            cellMock2.Verify(c => c.PlaceSnake(), Times.Once);
        }

        [StaFact]
        public void MoveTail_ShortensSnakeAndClearsTail()
        {
            // Arrange
            var cellMock1 = new Mock<Cell>();
            cellMock1.Setup(c => c.Clear());
            var cellMock2 = new Mock<Cell>();

            var cellLocationMock1 = new Mock<CellLocation>();
            var cellLocationMock2 = new Mock<CellLocation>();

            var gameBoardMock = new Mock<GameBoard>();
            gameBoardMock.Setup(gb => gb[cellLocationMock1.Object]).Returns(cellMock1.Object);
            gameBoardMock.Setup(gb => gb[cellLocationMock2.Object]).Returns(cellMock2.Object);

            var snake = new Snake(gameBoardMock.Object);
            snake.MoveHead(cellLocationMock1.Object);
            snake.MoveHead(cellLocationMock2.Object);

            // Act
            snake.MoveTail();

            // Assert
            snake.Length.Should().Be(1);
            cellMock1.Verify(c => c.Clear(), Times.Once);
        }
    }
}
