using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    class GameLogic
    {
        private readonly Snake snake;
        private readonly Feed feed;
        private readonly DirectionManager directionManager;
        private readonly SnakeState snakeState;

        private readonly int speedMsPerMove;

        public GameLogic(Snake snake, Feed feed, DirectionManager directionManager, SnakeState snakeState, int speedMsPerMove)
        {
            this.snake = snake;
            this.feed = feed;
            this.directionManager = directionManager;
            this.snakeState = snakeState;

            this.speedMsPerMove = speedMsPerMove;

            snakeState.IsDead = true;

            Task.Run(() => KeepMoving());
        }

        public void Start()
        {
            feed.Clear();

            directionManager.Initialize();
            snake.Initialize(directionManager.Dequeue());
            snakeState.ScoreValueChanged();
            PlaceFeed();

            snakeState.IsDead = false;
        }

        private void KeepMoving()
        {
            while (true)
            {
                if (!snakeState.IsDead)
                {
                    var nextHeadLocation = snake.NextHeadLocation(directionManager.Dequeue());

                    if (snake.Contains(nextHeadLocation))
                    {
                        // Snake is dead.
                        snakeState.IsDead = true;
                    }

                    snake.Enqueue(nextHeadLocation);

                    if (feed.Equals(nextHeadLocation))
                    {
                        // Snake ate feed.
                        PlaceFeed();
                        snakeState.ScoreValueChanged();
                    }
                    else
                    {
                        // Snake didn't eat feed.
                        snake.Dequeue();
                    }
                }
                Thread.Sleep(speedMsPerMove);
            }
        }

        private void PlaceFeed()
        {
            do
            {
                feed.SetFeedLocationRandomly();
            } while (snake.Contains(feed));

            feed.ShowFeed();
        }
    }
}

