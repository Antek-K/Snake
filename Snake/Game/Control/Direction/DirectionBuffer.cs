using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// Provides buffer of directions inserted by user.
    /// Returns only directions that make sense in the game.
    /// </summary>
    public class DirectionBuffer 
    {
        private readonly Direction initialDirection;
        private readonly Queue<Direction> directionQueue = new();

        private Direction lastAddedDirection;

        public DirectionBuffer(Direction initialDirection)
        {
            this.initialDirection = initialDirection;
        }

        public void AddDirectionToBuffer(Direction direction)
        {
            if(IsDirectionAcceptable(lastAddedDirection, direction))
            {
                directionQueue.Enqueue(direction);
                lastAddedDirection = direction;
            }
        }

        public Direction GetNextDirection()
        {
            if (directionQueue.Count > 0)
            {
                return directionQueue.Dequeue();
            }
            return lastAddedDirection;
        }

        public void Initialize()
        {
            directionQueue.Clear();
            directionQueue.Enqueue(initialDirection);
            lastAddedDirection = initialDirection;
        }

        // Snake can turn only its left or right
        private static bool IsDirectionAcceptable(Direction previousDirection, Direction direction) =>
            (direction == Direction.Down || direction == Direction.Up) && (previousDirection == Direction.Left || previousDirection == Direction.Right) ||
            (direction == Direction.Left || direction == Direction.Right) && (previousDirection == Direction.Up || previousDirection == Direction.Down);
    }

}
