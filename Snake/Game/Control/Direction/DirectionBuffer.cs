using System.Collections.Generic;

namespace Game
{
    public class DirectionBuffer 
    {
        private readonly Direction initialDirection;
        private readonly Queue<Direction> directionQueue = new();

        public DirectionBuffer(Direction initialDirection)
        {
            this.initialDirection = initialDirection;
        }

        public void AddDirectionToBuffer(Direction direction) => directionQueue.Enqueue(direction);

        public Direction GetNextDirection()
        {
            if (directionQueue.Count > 1)
            {
                Direction previousDirection = directionQueue.Dequeue();

                while (!IsDirectionAcceptable(previousDirection, directionQueue.Peek()))
                {
                    directionQueue.Dequeue();
                    if (directionQueue.Count == 0)
                    {
                        directionQueue.Enqueue(previousDirection);
                        break;
                    }
                }
            }
            return directionQueue.Peek();
        }

        public void Initialize()
        {
            directionQueue.Clear();
            directionQueue.Enqueue(initialDirection);
        }

        // Snake can turn only its left or right
        private static bool IsDirectionAcceptable(Direction previousDirection, Direction direction) =>
            (direction == Direction.Down || direction == Direction.Up) && (previousDirection == Direction.Left || previousDirection == Direction.Right) ||
            (direction == Direction.Left || direction == Direction.Right) && (previousDirection == Direction.Up || previousDirection == Direction.Down);
    }

}
