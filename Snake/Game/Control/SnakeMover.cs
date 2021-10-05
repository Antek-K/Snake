namespace Game
{
    /// <summary>
    /// Allows to control snake on the game board.
    /// </summary>
    public class SnakeMover
    {
        private readonly SnakeState snakeState;
        private readonly Snake snake;

        private readonly int gameBoardRowCount;
        private readonly int gameBoardColumnCount;
        private readonly int headInitialRow;
        private readonly int headInitialColumn;
        private readonly int snakeInitialLength;

        private CellLocation nextHeadLocation;

        public SnakeMover(Snake snake, SnakeState snakeState, int gameBoardRowCount, int gameBoardColumnCount, int headInitialRow, int headInitialColumn, int snakeInitialLength)
        {
            this.snake = snake;
            this.snakeState = snakeState;

            this.gameBoardRowCount = gameBoardRowCount;
            this.gameBoardColumnCount = gameBoardColumnCount;
            this.headInitialRow = headInitialRow;
            this.headInitialColumn = headInitialColumn;
            this.snakeInitialLength = snakeInitialLength;
        }

        public void Initialize(Direction direction)
        {
            snake.Clear();

            nextHeadLocation = new CellLocation(headInitialRow, headInitialColumn);
            ExtendAndMove();
            for (int i = 1; i < snakeInitialLength; i++)
            {
                PrepareNextMove(direction);
                ExtendAndMove();
            }
        }

        public CellLocation PrepareNextMove(Direction direction)
        {
            var row = snake.HeadLocation.Row;
            var column = snake.HeadLocation.Column;

            switch (direction)
            {
                case Direction.Up:
                    row = SumModulo(row, -1, gameBoardRowCount);
                    break;
                case Direction.Down:
                    row = SumModulo(row, 1, gameBoardRowCount);
                    break;
                case Direction.Left:
                    column = SumModulo(column, -1, gameBoardColumnCount);
                    break;
                case Direction.Right:
                    column = SumModulo(column, 1, gameBoardColumnCount);
                    break;
            }
            return nextHeadLocation = new CellLocation(row, column);
        }

        public bool IsSnakeDying() => snake.IsLocationOnSnake(nextHeadLocation);

        public bool IsSnakeEating(CellLocation food) => food.Equals(nextHeadLocation);

        public void Move()
        {
            snake.MoveHead(nextHeadLocation);
            snake.MoveTail();
        }

        public void ExtendAndMove()
        {
            snake.MoveHead(nextHeadLocation);
            snakeState.SnakeLength = snake.Length;
        }

        private static int SumModulo(int a, int b, int modulo) => (a + b + modulo) % modulo;
    }
}
