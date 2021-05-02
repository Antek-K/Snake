namespace Game
{
    public class GameBoard
    {
        private readonly Cell[,] board;

        public GameBoard(int columnCount, int rowCount)
        {
            ColumnCount = columnCount;
            RowCount = rowCount;

            board = new Cell[RowCount, ColumnCount];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    board[i, j] = new Cell();
                }
            }
        }

        public int RowCount { get; }
        public int ColumnCount { get; }

        public Cell this[CellLocation cellLocation] => board[cellLocation.Column, cellLocation.Row];

        public Cell[] FlatBoard()
        {
            var flatBoard = new Cell[RowCount* ColumnCount];
            
            int i = 0;
            foreach(var cell in board)
            {
                flatBoard[i++] = cell;
            }

            return flatBoard;
        }
    }
}
