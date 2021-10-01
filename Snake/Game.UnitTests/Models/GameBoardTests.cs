using Xunit;
using Moq;
using FluentAssertions;

namespace Game.UnitTests
{
    public class GameBoardTests
    {
        [StaFact]
        public void GameBoard_AfterCalled_HasProperRowAndColumnCountAndAllCellsInitialized()
        {
            // Arrange
            int rowCount = 3;
            int columnCount = 2;

            // Act
            var gameBoard = new GameBoard(rowCount, columnCount);

            // Assert
            gameBoard.RowCount.Should().Be(rowCount);
            gameBoard.ColumnCount.Should().Be(columnCount);
            for (int i = 0; i < gameBoard.RowCount; i++)
            {
                for (int j = 0; j < gameBoard.ColumnCount; j++)
                {
                    gameBoard[new CellLocation(i, j)].Should().NotBeNull().And.BeOfType<Cell>();
                }
            }
        }

        [StaFact]
        public void FlatBoard_WhenCalled_ReturnsCellsInProperOrderAndNumber()
        {
            // Arrange
            int rowCount = 2;
            int columnCount = 3;
            var gameBoard = new GameBoardWithMockedCells(rowCount, columnCount);

            // Act
            Cell[] flatBoard = gameBoard.FlatBoard();

            // Assert
            flatBoard.Length.Should().Be(6);

            int k = 0;
            for (int i = 0; i < gameBoard.RowCount; i++)
            {
                for (int j = 0; j < gameBoard.ColumnCount; j++)
                {
                    flatBoard[k++].Should().BeSameAs(gameBoard[new CellLocation(i, j)], "i: " + i + " j: " + j + " k: " + k);
                }
            }
        }

        private class GameBoardWithMockedCells : GameBoard
        {
            public GameBoardWithMockedCells(int rowCount, int columnCount) : base(rowCount, columnCount)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        board[i, j] = new Mock<Cell>().Object;
                    }
                }
            }
        }
    }
}
