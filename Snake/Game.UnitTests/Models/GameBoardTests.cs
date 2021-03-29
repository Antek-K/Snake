using NUnit.Framework;
using System.Threading;

namespace Game.UnitTests
{
    [Apartment(ApartmentState.STA)]
    class GameBoardTests
    {
        [Test]
        public void GameBoard_AfterCalled_HasProperRowAndColumnCountAndAllCellsInitialized()
        {
            var gameBoard = new GameBoard(3, 5);

            Assert.AreEqual(3, gameBoard.ColumnCount);
            Assert.AreEqual(5, gameBoard.RowCount);
            for(int i = 0; i < gameBoard.ColumnCount; i++)
            {
                for (int j = 0; j < gameBoard.RowCount; j++)
                {
                    Assert.IsNotNull(gameBoard[new CellLocation(i, j)]);
                }
            }
        }

        [Test]
        public void FlatBoard_WhenCalled_ReturnsCellsInProperOrderAndNumber()
        {
            var gameBoard = new GameBoard(4, 2);

            Cell[] flatBoard = gameBoard.FlatBoard();

            Assert.AreEqual(gameBoard.ColumnCount * gameBoard.RowCount, flatBoard.Length);

            int k = 0;
            for (int i = 0; i < gameBoard.RowCount; i++)
            {
                for (int j = 0; j < gameBoard.ColumnCount; j++)
                {
                    Assert.AreSame(gameBoard[new CellLocation(j, i)], flatBoard[k++],"i: " + i + " j: " + j + " k: " + k);
                }
            }
        }
    }
}
