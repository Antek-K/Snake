using System;
using System.Collections.Generic;

namespace Game
{
    class GameBoard
    {
        private readonly Cell[][] board;
        private readonly Random random = new Random();
        private readonly int columnCount;
        private readonly int rowCount;

        public GameBoard(int x, int y)
        {
            columnCount = x;
            rowCount = y;

            board = new Cell[x][];
            for (int i = 0; i < x; i++)
            {
                board[i] = new Cell[y];
                for (int j = 0; j < y; j++)
                {
                    board[i][j] = new Cell();
                }
            }
        }

        public Cell this[CellLocation cellLocation] => board[cellLocation.X][cellLocation.Y];

        public Cell[] FlatBoard()
        {
            var flatBoard = new List<Cell>();

            for (int j = 0; j < rowCount; j++)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    flatBoard.Add(board[i][j]);
                }
            }
            return flatBoard.ToArray();
        }

        public CellLocation GetCellLocationRandomly() => new CellLocation(random.Next(0, columnCount), random.Next(0, rowCount));

        public CellLocation GetNeigborLocation(CellLocation originalLocation, Direction direction)
        {
            var x = originalLocation.X;
            var y = originalLocation.Y;

            switch (direction)
            {
                case Direction.Left:
                    x = SumModulo(x, -1, columnCount);
                    break;
                case Direction.Right:
                    x = SumModulo(x, 1, columnCount);
                    break;
                case Direction.Up:
                    y = SumModulo(y, -1, rowCount);
                    break;
                case Direction.Down:
                    y = SumModulo(y, 1, rowCount);
                    break;
            }
            return new CellLocation(x, y);
        }

        private int SumModulo(int a, int b, int modulo) => (a + b + modulo) % modulo;
    }
}
