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
        private readonly Snake snake;
        private readonly Food food;
        private readonly DirectionBuffer directionBuffer;
        private readonly SnakeState snakeState;

        private readonly int speedMsPerMove;

        public GameLogic(GameBoard gameBoard, DirectionBuffer directionBuffer, SnakeState snakeState)
        {
            this.directionBuffer = directionBuffer;
            this.snakeState = snakeState;

            snake = new Snake(gameBoard, snakeState, Parameters.SnakeInitialRow, Parameters.SnakeInitialColumn, Parameters.SnakeInitialLength);
            food = new Food(gameBoard, snake);
            
            speedMsPerMove = Parameters.SpeedMsPerMove;

            snakeState.IsDead = true;

            Task.Run(() => KeepMoving());
        }

        public void Start()
        {
            food.Hide();

            directionBuffer.Initialize();
            snake.Initialize(directionBuffer.GetNextDirection());
            food.PlaceFoodRandomlyNotAtSnake();

            snakeState.IsDead = false;
        }

        private void KeepMoving()
        {
            while (true)
            {
                if (!snakeState.IsDead)
                {
                    snake.PrepareNextMove(directionBuffer.GetNextDirection());
                    snakeState.IsDead = snake.IsSnakeDying();

                    if (!snakeState.IsDead)
                    {
                        if (snake.IsSnakeEating(food.FoodLocation))
                        {
                            snake.ExtendAndMove();
                            food.PlaceFoodRandomlyNotAtSnake();
                        }
                        else
                        {
                            snake.Move();
                        }
                    }
                }
                Thread.Sleep(speedMsPerMove);
            }
        }
    }
}

