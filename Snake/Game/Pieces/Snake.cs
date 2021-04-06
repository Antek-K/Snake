using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class Snake : Queue<CellLocation>
    {
        private readonly GameBoard gameBoard;
        private readonly int snakeInitialX;
        private readonly int snakeInitialY;
        private CellLocation head;

        public int InitialLength { get; }

        public Snake(GameBoard gameBoard, int snakeInitialX, int snakeInitialY, int snakeInitialLength)
        {
            this.gameBoard = gameBoard;
            this.snakeInitialX = snakeInitialX;
            this.snakeInitialY = snakeInitialY;
            InitialLength = snakeInitialLength;
        }

        public new void Clear()
        {
            this.ToList().ForEach(cell => gameBoard[cell].Clear());
            base.Clear();
        }

        public new void Enqueue(CellLocation newHeadLocation)
        {
            head = newHeadLocation;
            base.Enqueue(head);
            gameBoard[head].PlaceSnake();
        }

        public new CellLocation Dequeue()
        {
            var tail = base.Dequeue();
            gameBoard[tail].Clear();
            return tail;
        }

        public void Initialize(Direction direction)
        {
            Clear();

            Enqueue(new CellLocation(snakeInitialX, snakeInitialY));
            for (int i = 1; i < InitialLength; i++)
            {
                Enqueue(NextHeadLocation(direction));
            }
        }

        public CellLocation NextHeadLocation(Direction direction)
        {
            var x = head.X;
            var y = head.Y;

            switch (direction)
            {
                case Direction.Left:
                    x = SumModulo(x, -1, gameBoard.ColumnCount);
                    break;
                case Direction.Right:
                    x = SumModulo(x, 1, gameBoard.ColumnCount);
                    break;
                case Direction.Up:
                    y = SumModulo(y, -1, gameBoard.RowCount);
                    break;
                case Direction.Down:
                    y = SumModulo(y, 1, gameBoard.RowCount);
                    break;
            }
            return new CellLocation(x, y);
        }

        private int SumModulo(int a, int b, int modulo) => (a + b + modulo) % modulo;
    }
}
