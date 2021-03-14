using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    class GameLogic : INotifyPropertyChanged
    {
        private readonly Snake snake;
        private readonly Feed feed;
        private readonly DirectionManager directionManager;

        private readonly int speedMsPerMove;
        private readonly int scoreFactor;

        private bool isDead;

        public event PropertyChangedEventHandler PropertyChanged;

        public GameLogic(Snake snake, Feed feed, DirectionManager directionManager, int speedMsPerMove, int scoreFactor)
        {
            this.snake = snake;
            this.feed = feed;
            this.directionManager = directionManager;

            this.speedMsPerMove = speedMsPerMove;
            this.scoreFactor = scoreFactor;

            IsDead = true;

            Task.Run(() => KeepMoving());
        }

        public object Score => (snake.Count - snake.InitialLength) * scoreFactor;

        public bool IsDead
        {
            get => isDead;
            private set
            {
                isDead = value;
                NotifyPropertyChanged();
            }
        }

        public void Start()
        {
            feed.Clear();

            directionManager.Initialize();
            snake.Initialize(directionManager.Dequeue());
            NotifyPropertyChanged(nameof(Score));
            PlaceFeed();

            IsDead = false;
        }

        private void KeepMoving()
        {
            while (true)
            {
                if (!IsDead)
                {
                    var nextHeadLocation = snake.NextHeadLocation(directionManager.Dequeue());

                    if (snake.Contains(nextHeadLocation))
                    {
                        // Snake is dead.
                        IsDead = true;
                    }

                    snake.Enqueue(nextHeadLocation);

                    if (feed.Equals(nextHeadLocation))
                    {
                        // Snake ate feed.
                        PlaceFeed();
                        NotifyPropertyChanged(nameof(Score));
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

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

