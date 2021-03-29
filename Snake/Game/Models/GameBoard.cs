using System.Collections.Generic;

namespace Game
{
    public class GameBoard
    {
        private readonly Cell[][] board;

        public GameBoard(int x, int y)
        {
            ColumnCount = x;
            RowCount = y;

            board = new Cell[ColumnCount][];
            for (int i = 0; i < ColumnCount; i++)
            {
                board[i] = new Cell[RowCount];
                for (int j = 0; j < RowCount; j++)
                {
                    board[i][j] = new Cell();
                }
            }
        }

        public int RowCount { get; }
        public int ColumnCount { get; }

        public Cell this[CellLocation cellLocation] => board[cellLocation.X][cellLocation.Y];

        public Cell[] FlatBoard()
        {
            var flatBoard = new List<Cell>();

            for (int j = 0; j < RowCount; j++)
            {
                for (int i = 0; i < ColumnCount; i++)
                {
                    flatBoard.Add(board[i][j]);
                }
            }
            return flatBoard.ToArray();
        }
    }
}
