namespace Game
{
    public class GameBoard
    {
        /// <summary>
        /// Specifies the size of the game board and provide access to all its cells.
        /// </summary>
        protected readonly Cell[,] board;

        public GameBoard() { }

        public GameBoard(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;

            board = new Cell[RowCount, ColumnCount];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    board[i, j] = new Cell();
                }
            }
        }

        public virtual int RowCount { get; }
        public virtual int ColumnCount { get; }


        public virtual Cell this[CellLocation cellLocation] => board[cellLocation.Row, cellLocation.Column];

        public Cell[] FlatBoard()
        {
            var flatBoard = new Cell[RowCount * ColumnCount];

            int i = 0;
            foreach (var cell in board)
            {
                flatBoard[i++] = cell;
            }

            return flatBoard;
        }
    }
}
