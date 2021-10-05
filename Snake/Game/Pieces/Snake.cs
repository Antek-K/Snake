using System.Collections.Generic;
using System.Linq;

namespace Game
{
    /// <summary>
    /// Determines location of the snake on game board.
    /// </summary>
    public class Snake
    {
        private readonly GameBoard gameBoard;
        private readonly Queue<CellLocation> snakeBodyQueue = new();

        public Snake(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        public Snake() { }

        public virtual CellLocation HeadLocation { get; set; }

        public virtual int Length => snakeBodyQueue.Count;

        public virtual bool IsLocationOnSnake(CellLocation cellLocation) => snakeBodyQueue.Contains(cellLocation);

        public virtual void Clear()
        {
            snakeBodyQueue.ToList().ForEach(cellLocation => gameBoard[cellLocation].Clear());
            snakeBodyQueue.Clear();
        }

        public virtual void MoveHead(CellLocation nextHeadLocation)
        {
            HeadLocation = nextHeadLocation;
            snakeBodyQueue.Enqueue(HeadLocation);
            gameBoard[HeadLocation].PlaceSnake();
        }

        public virtual void MoveTail()
        {
            var tail = snakeBodyQueue.Dequeue();
            gameBoard[tail].Clear();
        }
    }
}
