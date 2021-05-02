using System.Threading;
using System.Threading.Tasks;

namespace Game
{
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

            snake = new Snake(gameBoard, snakeState, Parameters.SnakeInitialX, Parameters.SnakeInitialY, Parameters.SnakeInitialLength);
            food = new Food(gameBoard, snake);
            
            speedMsPerMove = Parameters.SpeedMsPerMove;

            snakeState.IsDead = true;

            Task.Run(() => KeepMoving());
        }

        public void Start()
        {
            food.Clear();

            directionBuffer.Initialize();
            snake.Initialize(directionBuffer.GetNextDirection());
            food.PlaceFoodNotAtSnake();

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
                            food.PlaceFoodNotAtSnake();
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

