using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// The central class in the game.
    /// Contects parts of the application.
    /// Contains main loop of the game.
    /// </summary>
    class GameLogic
    {
        private readonly SnakeMover snakeMover;
        private readonly Food food;
        private readonly DirectionBuffer directionBuffer;
        private readonly SnakeState snakeState;

        public GameLogic(GameBoard gameBoard, DirectionBuffer directionBuffer, SnakeState snakeState)
        {
            this.directionBuffer = directionBuffer;
            this.snakeState = snakeState;

            var snake = new Snake(gameBoard);
            snakeMover = new SnakeMover(snake, snakeState, gameBoard.RowCount, gameBoard.ColumnCount, Parameters.SnakeInitialRow, Parameters.SnakeInitialColumn, Parameters.SnakeInitialLength);
            food = new Food(gameBoard, snake);
        }

        public void Start()
        {
            food.Hide();

            directionBuffer.Initialize();
            snakeMover.Initialize(directionBuffer.GetNextDirection());
            food.PlaceFoodRandomlyNotAtSnake();

            snakeState.IsDead = false;
        }

        public void PerformMove()
        {
            snakeMover.PrepareNextMove(directionBuffer.GetNextDirection());
            snakeState.IsDead = snakeMover.IsSnakeDying();

            if (!snakeState.IsDead)
            {
                if (snakeMover.IsSnakeEating(food.FoodLocation))
                {
                    snakeMover.ExtendAndMove();
                    food.PlaceFoodRandomlyNotAtSnake();
                }
                else
                {
                    snakeMover.Move();
                }
            }
        }
    }
}

