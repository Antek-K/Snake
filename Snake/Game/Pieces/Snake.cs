using System.Collections.Generic;
using System.Linq;

namespace Game
{
    /// <summary>
    /// Determines location of the snake on game board.
    /// Provides methods to move it.
    /// </summary>
    public class Snake
    {
        private readonly GameBoard gameBoard;
        private readonly SnakeState snakeState;

        private readonly int headInitialRow;
        private readonly int headInitialColumn;
        private readonly int snakeInitialLength;

        private readonly Queue<CellLocation> snakeBodyQueue = new();
        private CellLocation headLocation;
        private CellLocation nextHeadLocation;

        public Snake(GameBoard gameBoard, SnakeState snakeState, int headInitialRow, int headInitialColumn, int snakeInitialLength)
        {
            this.gameBoard = gameBoard;
            this.snakeState = snakeState;

            this.headInitialRow = headInitialRow;
            this.headInitialColumn = headInitialColumn;
            this.snakeInitialLength = snakeInitialLength;
        }

        public void Initialize(Direction direction)
        {
            Clear();

            nextHeadLocation = new CellLocation(headInitialRow, headInitialColumn);
            ExtendAndMove();
            for (int i = 1; i < snakeInitialLength; i++)
            {
                PrepareNextMove(direction);
                ExtendAndMove();
            }
        }

        public void PrepareNextMove(Direction direction)
        {
            var row = headLocation.Row;
            var column = headLocation.Column;

            switch (direction)
            {
                case Direction.Up:
                    row = SumModulo(row, -1, gameBoard.RowCount);
                    break;
                case Direction.Down:
                    row = SumModulo(row, 1, gameBoard.RowCount);
                    break;
                case Direction.Left:
                    column = SumModulo(column, -1, gameBoard.ColumnCount);
                    break;
                case Direction.Right:
                    column = SumModulo(column, 1, gameBoard.ColumnCount);
                    break;
            }
            nextHeadLocation = new CellLocation(row, column);
        }

        public bool IsSnakeDying() => snakeBodyQueue.Contains(nextHeadLocation);

        public bool IsSnakeEating(CellLocation food) => food.Equals(nextHeadLocation);

        public bool IsLocationOnSnake(CellLocation cellLocation) => snakeBodyQueue.Contains(cellLocation);

        public void Move()
        {
            MoveHead(nextHeadLocation);
            MoveTail();
        }

        public void ExtendAndMove()
        {
            MoveHead(nextHeadLocation);
            snakeState.SnakeLength = snakeBodyQueue.Count;
        }

        private static int SumModulo(int a, int b, int modulo) => (a + b + modulo) % modulo;

        private void Clear()
        {
            snakeBodyQueue.ToList().ForEach(cell => gameBoard[cell].Clear());
            snakeBodyQueue.Clear();
        }

        private void MoveHead(CellLocation nextHeadLocation)
        {
            headLocation = nextHeadLocation;
            snakeBodyQueue.Enqueue(headLocation);
            gameBoard[headLocation].PlaceSnake();
        }

        private void MoveTail()
        {
            var tail = snakeBodyQueue.Dequeue();
            gameBoard[tail].Clear();
        }
    }
}
