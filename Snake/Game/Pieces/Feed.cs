using System;

namespace Game
{
    public class Feed : CellLocation
    {
        private readonly GameBoard gameBoard;
        private readonly Random random = new();

        public Feed(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
            SetFeedLocationRandomly();
        }

        public void ShowFeed() => gameBoard[this].PlaceFeed();

        public void Clear() => gameBoard[this].Clear();

        public void SetFeedLocationRandomly()
        {
            X = random.Next(0, gameBoard.ColumnCount);
            Y = random.Next(0, gameBoard.RowCount);
        }
    }
}
