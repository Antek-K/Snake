using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class Snake
    {
        private readonly Queue<CellLocation> snakeBodyQueue;
        private readonly GameBoard gameBoard;
        private readonly SnakeState snakeState;
        private readonly int headInitialRow;
        private readonly int headInitialColumn;
        private readonly int snakeInitialLength;
        private CellLocation headLocation;
        private CellLocation nextHeadLocation;

        public Snake(GameBoard gameBoard, SnakeState snakeState, int headInitialRow, int headInitialColumn, int snakeInitialLength)
        {
            snakeBodyQueue = new Queue<CellLocation>();
            this.gameBoard = gameBoard;
            this.snakeState = snakeState;
            this.headInitialRow = headInitialRow;
            this.headInitialColumn = headInitialColumn;
            this.snakeInitialLength = snakeInitialLength;
        }

        public void ExtendAndMove()
        {
            MoveHead(nextHeadLocation);
            snakeState.SnakeLength = snakeBodyQueue.Count;
        }

        public void Move()
        {
            MoveHead(nextHeadLocation);
            MoveTail();
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

        public bool IsSnakeDying() => snakeBodyQueue.Contains(nextHeadLocation);

        public bool Contains(CellLocation cellLocation) => snakeBodyQueue.Contains(cellLocation);

        public bool IsSnakeEating(CellLocation food) => food.Equals(nextHeadLocation);

        public void PrepareNextMove(Direction direction)
        {
            var x = headLocation.Row;
            var y = headLocation.Column;

            switch (direction)
            {
                case Direction.Left:
                    x = SumModulo(x, -1, gameBoard.ColumnCount);
                    break;
                case Direction.Right:
                    x = SumModulo(x, 1, gameBoard.ColumnCount);
                    break;
                case Direction.Up:
                    y = SumModulo(y, -1, gameBoard.RowCount);
                    break;
                case Direction.Down:
                    y = SumModulo(y, 1, gameBoard.RowCount);
                    break;
            }
            nextHeadLocation = new CellLocation(x, y);
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
