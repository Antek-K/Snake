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

        public CellLocation HeadLocation { get; set; }

        public int Length => snakeBodyQueue.Count;

        public virtual bool IsLocationOnSnake(CellLocation cellLocation) => snakeBodyQueue.Contains(cellLocation);

        public void Clear()
        {
            snakeBodyQueue.ToList().ForEach(cell => gameBoard[cell].Clear());
            snakeBodyQueue.Clear();
        }

        public void MoveHead(CellLocation nextHeadLocation)
        {
            HeadLocation = nextHeadLocation;
            snakeBodyQueue.Enqueue(HeadLocation);
            gameBoard[HeadLocation].PlaceSnake();
        }

        public void MoveTail()
        {
            var tail = snakeBodyQueue.Dequeue();
            gameBoard[tail].Clear();
        }
    }
}
