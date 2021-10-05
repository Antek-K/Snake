
namespace Game
{
    /// <summary>
    /// The central class in the game.
    /// Contects parts of the application.
    /// Contains main loop of the game.
    /// </summary>
    public class GameLogic
    {
        private readonly Food food;
        private readonly DirectionBuffer directionBuffer;
        private readonly SnakeState snakeState;
        private readonly SnakeMover snakeMover;

        public GameLogic(GameBoard gameBoard, DirectionBuffer directionBuffer, SnakeState snakeState)
        {
            this.directionBuffer = directionBuffer;
            this.snakeState = snakeState;

            var snake = new Snake(gameBoard);
            snakeMover = new SnakeMover(snake, snakeState, gameBoard.RowCount, gameBoard.ColumnCount, Parameters.SnakeInitialRow, Parameters.SnakeInitialColumn, Parameters.SnakeInitialLength);
            food = new Food(gameBoard, snake);
        }

        public GameLogic(GameBoard gameBoard, DirectionBuffer directionBuffer, SnakeState snakeState, SnakeMover snakeMover, Food food)
        {
            this.directionBuffer = directionBuffer;
            this.snakeState = snakeState;
            this.snakeMover = snakeMover;
            this.food = food;
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

