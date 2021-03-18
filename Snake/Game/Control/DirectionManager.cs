using System.Collections.Generic;

namespace Game
{
    public class DirectionManager : Queue<Direction>
    {
        private readonly Direction initialDirection;

        public DirectionManager(Direction initialDirection)
        {
            this.initialDirection = initialDirection;
        }

        public new Direction Dequeue()
        {
            if (Count > 1)
            {
                Direction previousDirection = base.Dequeue();

                while (!IsDirectionAcceptable(previousDirection, Peek()))
                {
                    base.Dequeue();
                    if (Count == 0)
                    {
                        Enqueue(previousDirection);
                        break;
                    }
                }
            }
            return Peek();
        }

        public void Initialize()
        {
            Clear();
            Enqueue(initialDirection);
        }

        // Snake can turn only its left or right
        private bool IsDirectionAcceptable(Direction previousDirection, Direction direction) =>
            (direction == Direction.Down || direction == Direction.Up) && (previousDirection == Direction.Left || previousDirection == Direction.Right) ||
            (direction == Direction.Left || direction == Direction.Right) && (previousDirection == Direction.Up || previousDirection == Direction.Down);
    }

}
