using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;

namespace Game
{
    class Snake
    {
        private readonly GameBoard gameBoard;

        private Queue<CellLocation> snakeBody;
        private CellLocation feed;
        private CellLocation head;

        private Direction direction;
        private int speedMsPerMove;

        private bool paused = false;

        private Thread keepMovingThread;

        private event Action SnakeLengthChanged;
        private event Action SnakeDied;

        public Snake(GameBoard gameBoard, int snakeInitialX, int snakeInitialY, int snakeInitialLength, Direction initialDirection, int speedMsPerMove, Action onSnakeLengthChanged, Action onSnakeDied)
        {
            this.gameBoard = gameBoard;
            this.speedMsPerMove = speedMsPerMove;

            direction = initialDirection;
            InitializeSnake(snakeInitialX, snakeInitialY, snakeInitialLength);
            PlaceFeed();
            
            SnakeLengthChanged += onSnakeLengthChanged;
            SnakeDied += onSnakeDied;

            keepMovingThread = new Thread(new ThreadStart(KeepMoving)) { IsBackground = true };
        }

        public Direction Direction
        {
            set
            {
                // Snake can turn only its left or right
                if ((value == Direction.Down || value == Direction.Up) && (direction == Direction.Left || direction == Direction.Right) ||
                    (value == Direction.Left || value == Direction.Right) && (direction == Direction.Up || direction == Direction.Down))
                {
                    direction = value;
                }
            }
        }

        public int Length => snakeBody.Count;
        public void Start() => keepMovingThread.Start();

        public void Restart(int snakeInitialX, int snakeInitialY, int snakeInitialLength, Direction initialDirection)
        {
            snakeBody.ToList().ForEach(cell => gameBoard[cell].Remove());
            gameBoard[feed].Remove();

            direction = initialDirection;
            InitializeSnake(snakeInitialX, snakeInitialY, snakeInitialLength);
            PlaceFeed();

            paused = false;
        }

        private void KeepMoving()
        {
            while (true)
            {
                if (!paused)
                {
                    var newHeadLocation = gameBoard.GetNeigborLocation(head, direction);
                    if (snakeBody.Contains(newHeadLocation))
                    {
                        // Snake is dead.
                        SnakeDied?.Invoke();
                        paused = true;
                    }

                    MoveHead(newHeadLocation);

                    if (feed.Equals(newHeadLocation))
                    {
                        // Snake ate feed.
                        PlaceFeed();
                        SnakeLengthChanged?.Invoke();
                    }
                    else
                    {
                        // Snake didn't eat feed.
                        RemoveTail();
                    }
                }
                Thread.Sleep(speedMsPerMove);
            }
        }

        private void InitializeSnake(int snakeInitialX, int snakeInitialY, int snakeLength)
        {
            snakeBody = new Queue<CellLocation>();
            var cell = new CellLocation(snakeInitialX, snakeInitialY);

            for (int i = 0; i < snakeLength - 1; i++)
            {
                snakeBody.Enqueue(cell);
                cell = gameBoard.GetNeigborLocation(cell, direction);
            }

            head = cell;
            snakeBody.Enqueue(head);
            SnakeLengthChanged?.Invoke();

            // Show snake on board game.
            snakeBody.ToList().ForEach(cell => gameBoard[cell].PlaceSnake());
        }

        private void MoveHead(CellLocation newHeadLocation)
        {
            head = newHeadLocation;
            snakeBody.Enqueue(head);
            gameBoard[head].PlaceSnake();
        }

        private void RemoveTail()
        {
            var tail = snakeBody.Dequeue();
            gameBoard[tail].Remove();
        }

        private void PlaceFeed()
        {
            do
            {
                feed = gameBoard.GetCellLocationRandomly();
            } while (snakeBody.Contains(feed));

            gameBoard[feed].PlaceFeed();
        }
    }
}

