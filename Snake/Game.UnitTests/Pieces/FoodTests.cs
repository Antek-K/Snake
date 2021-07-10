using System.Linq;
using System.Threading;
using System.Windows.Media;
using System.Windows.Shapes;
using Xunit;
using FluentAssertions;
using Moq;

namespace Game.UnitTests
{
    public class FoodTests
    {
        [StaFact]
        public void PlaceFoodRandomlyNotAtSnake_CallsPlaceFoodOnce()
        {
            var cellMock = new Mock<Cell>();
            var row = 2;
            var column = 1;
            var gameBoardMock = new Mock<GameBoard>();
            gameBoardMock.SetupGet(gb => gb.RowCount).Returns(row);
            gameBoardMock.SetupGet(gb => gb.ColumnCount).Returns(column);
            gameBoardMock.Setup(gb => gb[It.IsAny<CellLocation>()]).Returns(cellMock.Object);
            var snakeMock = new Mock<Snake>();
            var food = new Food(gameBoardMock.Object, snakeMock.Object);
            gameBoardMock.Invocations.Clear();
            cellMock.Invocations.Clear();

            food.PlaceFoodRandomlyNotAtSnake();

            gameBoardMock.Verify(gb => gb[It.Is<CellLocation>(cl => cl == food.FoodLocation)], Times.Once);
            cellMock.Verify(c => c.PlaceFood(), Times.Once);
        }

        [StaFact]
        public void PlaceFoodRandomlyNotAtSnake_AfterCalledFiniteNumberOfTimes_HasValuesInRangeButOtherThenPrevious()
        {
            var cellMock = new Mock<Cell>();
            var row = 2;
            var column = 1;
            var gameBoardMock = new Mock<GameBoard>();
            gameBoardMock.SetupGet(gb => gb.RowCount).Returns(row);
            gameBoardMock.SetupGet(gb => gb.ColumnCount).Returns(column);
            gameBoardMock.Setup(gb => gb[It.IsAny<CellLocation>()]).Returns(cellMock.Object);

            var snakeMock = new Mock<Snake>();
            snakeMock.Setup(s => s.IsLocationOnSnake(It.IsAny<CellLocation>())).Returns(false);
            var food = new Food(gameBoardMock.Object, snakeMock.Object);
            int oldX = food.FoodLocation.Row;
            int oldY = food.FoodLocation.Column;

            do
            {
                food.PlaceFoodRandomlyNotAtSnake();
            } while (oldX == food.FoodLocation.Row && oldY == food.FoodLocation.Column);

            food.FoodLocation.Row.Should().BeGreaterOrEqualTo(0).And.BeLessThan(row);
            food.FoodLocation.Column.Should().BeGreaterOrEqualTo(0).And.BeLessThan(column);
        }

        [StaFact]
        public void Show_WhenCalled_FoodIsPlacedInProperCell()
        {
            var cellMock = new Mock<Cell>();
            var gameBoardMock = new Mock<GameBoard>();
            gameBoardMock.Setup(gb => gb[It.IsAny<CellLocation>()]).Returns(cellMock.Object);
            var snakeMock = new Mock<Snake>();
            var feed = new Food(gameBoardMock.Object, snakeMock.Object);
            gameBoardMock.Invocations.Clear();
            cellMock.Invocations.Clear();

            feed.Show();

            gameBoardMock.Verify(gb => gb[It.Is<CellLocation>(cl => cl == feed.FoodLocation)], Times.Once);
            gameBoardMock.Verify(gb => gb[It.Is<CellLocation>(cl => cl != feed.FoodLocation)], Times.Never);
            cellMock.Verify(c => c.PlaceFood(), Times.Once);
        }

        [StaFact]
        public void Hide_WhenCalled_ProperCellIsCleared()
        {
            var cellMock = new Mock<Cell>();
            var gameBoardMock = new Mock<GameBoard>();
            gameBoardMock.Setup(gb => gb[It.IsAny<CellLocation>()]).Returns(cellMock.Object);
            var snakeMock = new Mock<Snake>();
            var food = new Food(gameBoardMock.Object, snakeMock.Object);
            gameBoardMock.Invocations.Clear();

            food.Hide();

            gameBoardMock.Verify(gb => gb[It.Is<CellLocation>(cl => cl == food.FoodLocation)], Times.Once);
            gameBoardMock.Verify(gb => gb[It.Is<CellLocation>(cl => cl != food.FoodLocation)], Times.Never);
            cellMock.Verify(c => c.Clear(), Times.Once);
        }

    }
}
