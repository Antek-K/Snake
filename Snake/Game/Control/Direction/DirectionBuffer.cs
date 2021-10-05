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

        private Direction lastDirection;

        public DirectionBuffer(Direction initialDirection)
        {
            this.initialDirection = initialDirection;
        }

        public DirectionBuffer() { }

        public void AddDirectionToBuffer(Direction direction)
        {
            if(IsDirectionAcceptable(lastDirection, direction))
            {
                directionQueue.Enqueue(direction);
                lastDirection = direction;
            }
        }

        public Direction GetNextDirection()
        {
            if (directionQueue.Count > 0)
            {
                return directionQueue.Dequeue();
            }
            return lastDirection;
        }

        public virtual void Initialize()
        {
            directionQueue.Clear();
            lastDirection = initialDirection;
        }

        // Snake can turn only its left or right
        private static bool IsDirectionAcceptable(Direction previousDirection, Direction direction) =>
            (direction == Direction.Down || direction == Direction.Up) && (previousDirection == Direction.Left || previousDirection == Direction.Right) ||
            (direction == Direction.Left || direction == Direction.Right) && (previousDirection == Direction.Up || previousDirection == Direction.Down);
    }

}
