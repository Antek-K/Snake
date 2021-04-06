using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game.UnitTests
{
    [Apartment(ApartmentState.STA)]
    class SnakeTests
    {
        [Test]
        public void Snake_AfterCalled_InitialLengthHasProperValue()
        {
            var gameBoard = new GameBoard(10, 10);

            var snake = new Snake(gameBoard, 0, 0, 7);

            Assert.AreEqual(7, snake.InitialLength);
        }

        [Test]
        public void Initialize_AfterCalled_SnakeIsPlaced()
        {
            var gameBoard = new GameBoard(10, 10);
            var snake = new Snake(gameBoard, 2, 3, 5);

            snake.Initialize(Direction.Down);

            Assert.AreEqual(5, snake.Count);
            var snakeList = snake.ToList();
            Assert.AreEqual(2, snakeList[0].X);
            Assert.AreEqual(3, snakeList[0].Y);
            Assert.AreEqual(2, snakeList[1].X);
            Assert.AreEqual(4, snakeList[1].Y);
            Assert.AreEqual(2, snakeList[2].X);
            Assert.AreEqual(5, snakeList[2].Y);
            Assert.AreEqual(2, snakeList[3].X);
            Assert.AreEqual(6, snakeList[3].Y);
            Assert.AreEqual(2, snakeList[4].X);
            Assert.AreEqual(7, snakeList[4].Y);
            int PiecesOnBoardNumber = 0;
            gameBoard.FlatBoard().ToList().ForEach(cell => { if (cell.Shape != null) PiecesOnBoardNumber++; });
            Assert.AreEqual(5, PiecesOnBoardNumber);
            snakeList.ForEach(cellLocation =>
            {
                Assert.IsInstanceOf(typeof(Rectangle), gameBoard[cellLocation].Shape);
                Assert.AreEqual(Brushes.Black, gameBoard[cellLocation].Shape.Fill);
            });
        }

        [Test]
        public void Initialize_AfterCalled_OnlyCurrentSnakeIsPlaced()
        {
            var gameBoard = new GameBoard(10, 10);
            var snake = new Snake(gameBoard, 5, 5, 3);
            snake.Initialize(Direction.Up);
            snake.Enqueue(snake.NextHeadLocation(Direction.Left));
            snake.Enqueue(snake.NextHeadLocation(Direction.Left));
            snake.Enqueue(snake.NextHeadLocation(Direction.Down));

            snake.Initialize(Direction.Right);

            Assert.AreEqual(3, snake.Count);
            var snakeList = snake.ToList();
            Assert.AreEqual(5, snakeList[0].X);
            Assert.AreEqual(5, snakeList[0].Y);
            Assert.AreEqual(6, snakeList[1].X);
            Assert.AreEqual(5, snakeList[1].Y);
            Assert.AreEqual(7, snakeList[2].X);
            Assert.AreEqual(5, snakeList[2].Y);
            int PiecesOnBoardNumber = 0;
            gameBoard.FlatBoard().ToList().ForEach(cell => { if (cell.Shape != null) PiecesOnBoardNumber++; });
            Assert.AreEqual(3, PiecesOnBoardNumber);
        }

            [Test]
        public void NextHeadLocation_WhenDirectionLeft_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(3, 3);
            var snake = new Snake(gameBoard, 1, 1, 1);
            snake.Initialize(Direction.Up);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Left);

            Assert.AreEqual(0, cellLocation.X);
            Assert.AreEqual(1, cellLocation.Y);
        }

        [Test]
        public void NextHeadLocation_WhenDirectionUp_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(3, 3);
            var snake = new Snake(gameBoard, 1, 1, 1);
            snake.Initialize(Direction.Up);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Up);

            Assert.AreEqual(1, cellLocation.X);
            Assert.AreEqual(0, cellLocation.Y);
        }

        [Test]
        public void NextHeadLocation_WhenDirectionRight_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(3, 3);
            var snake = new Snake(gameBoard, 1, 1, 1);
            snake.Initialize(Direction.Up);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Right);

            Assert.AreEqual(2, cellLocation.X);
            Assert.AreEqual(1, cellLocation.Y);
        }

        [Test]
        public void NextHeadLocation_WhenDirectionDown_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(3, 3);
            var snake = new Snake(gameBoard, 1, 1, 1);
            snake.Initialize(Direction.Up);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Down);

            Assert.AreEqual(1, cellLocation.X);
            Assert.AreEqual(2, cellLocation.Y);
        }

        [Test]
        public void NextHeadLocation_WhenDirectionLeftAndFirstCorner_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(7, 3);
            var snake = new Snake(gameBoard, 0, 0, 1);
            snake.Initialize(Direction.Up);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Left);

            Assert.AreEqual(6, cellLocation.X);
            Assert.AreEqual(0, cellLocation.Y);
        }

        [Test]
        public void NextHeadLocation_WhenDirectionUpAndFirstCorner_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(6, 5);
            var snake = new Snake(gameBoard, 0, 0, 1);
            snake.Initialize(Direction.Up);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Up);

            Assert.AreEqual(0, cellLocation.X);
            Assert.AreEqual(4, cellLocation.Y);
        }

        [Test]
        public void NextHeadLocation_WhenDirectionRightAndFirstCorner_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(6, 4);
            var snake = new Snake(gameBoard, 5, 0, 1);
            snake.Initialize(Direction.Up);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Right);

            Assert.AreEqual(0, cellLocation.X);
            Assert.AreEqual(0, cellLocation.Y);
        }

        [Test]
        public void NextHeadLocation_WhenDirectionDownAndFirstCorner_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(3, 5);
            var snake = new Snake(gameBoard, 0, 4, 1);
            snake.Initialize(Direction.Up);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Down);

            Assert.AreEqual(0, cellLocation.X);
            Assert.AreEqual(0, cellLocation.Y);
        }

        [Test]
        public void NextHeadLocation_WhenDirectionLeftAndSecondCorner_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(5, 5);
            var snake = new Snake(gameBoard, 0, 4, 1);
            snake.Initialize(Direction.Right);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Left);

            Assert.AreEqual(4, cellLocation.X);
            Assert.AreEqual(4, cellLocation.Y);
        }

        [Test]
        public void NextHeadLocation_WhenDirectionUpAndSecondCorner_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(5, 6);
            var snake = new Snake(gameBoard, 4, 0, 1);
            snake.Initialize(Direction.Right);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Up);

            Assert.AreEqual(4, cellLocation.X);
            Assert.AreEqual(5, cellLocation.Y);
        }

        [Test]
        public void NextHeadLocation_WhenDirectionRightAndSecondCorner_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(10, 2);
            var snake = new Snake(gameBoard, 9, 1, 1);
            snake.Initialize(Direction.Left);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Right);

            Assert.AreEqual(0, cellLocation.X);
            Assert.AreEqual(1, cellLocation.Y);
        }

        [Test]
        public void NextHeadLocation_WhenDirectionDownAndSecondCorner_ReturnsProperCellLocation()
        {
            var gameBoard = new GameBoard(11, 2);
            var snake = new Snake(gameBoard, 10, 1, 1);
            snake.Initialize(Direction.Left);

            CellLocation cellLocation = snake.NextHeadLocation(Direction.Down);

            Assert.AreEqual(10, cellLocation.X);
            Assert.AreEqual(0, cellLocation.Y);
        }

        [Test]
        public void Enqueue_AfterCalled_NextPieceIsEnqueued()
        {
            var gameBoard = new GameBoard(10, 10);
            var snake = new Snake(gameBoard, 3, 3, 2);
            snake.Initialize(Direction.Right);

            snake.Enqueue(snake.NextHeadLocation(Direction.Down));

            Assert.AreEqual(3, snake.Count);
            var snakeList = snake.ToList();
            Assert.AreEqual(3, snakeList[0].X);
            Assert.AreEqual(3, snakeList[0].Y);
            Assert.AreEqual(4, snakeList[1].X);
            Assert.AreEqual(3, snakeList[1].Y);
            Assert.AreEqual(4, snakeList[2].X);
            Assert.AreEqual(4, snakeList[2].Y);
            int PiecesOnBoardNumber = 0;
            gameBoard.FlatBoard().ToList().ForEach(cell => { if (cell.Shape != null) PiecesOnBoardNumber++; });
            Assert.AreEqual(3, PiecesOnBoardNumber); 
            Assert.IsInstanceOf(typeof(Rectangle), gameBoard[snakeList[2]].Shape);
            Assert.AreEqual(Brushes.Black, gameBoard[snakeList[2]].Shape.Fill);
        }

        [Test]
        public void Dequeue_AfterCalled_ProperPieceIsRemoved()
        {
            var gameBoard = new GameBoard(12, 12);
            var snake = new Snake(gameBoard, 4, 4, 2);
            snake.Initialize(Direction.Down);
            snake.Enqueue(snake.NextHeadLocation(Direction.Left));
            snake.Enqueue(snake.NextHeadLocation(Direction.Left));
            snake.Enqueue(snake.NextHeadLocation(Direction.Up));

            snake.Dequeue();
            snake.Dequeue();
            snake.Dequeue();
            snake.Dequeue();

            Assert.AreEqual(1, snake.Count);
            var snakeList = snake.ToList();
            Assert.AreEqual(2, snakeList[0].X);
            Assert.AreEqual(4, snakeList[0].Y);

            int PiecesOnBoardNumber = 0;
            gameBoard.FlatBoard().ToList().ForEach(cell => { if (cell.Shape != null) PiecesOnBoardNumber++; });
            Assert.AreEqual(1, PiecesOnBoardNumber);
            Assert.IsInstanceOf(typeof(Rectangle), gameBoard[snakeList[0]].Shape);
            Assert.AreEqual(Brushes.Black, gameBoard[snakeList[0]].Shape.Fill);
        }

        [Test]
        public void Clear_AfterCalled_QueueAndGameBoardAreEmpty()
        {
            var gameBoard = new GameBoard(20, 20);
            var snake = new Snake(gameBoard, 18, 18, 10);
            snake.Initialize(Direction.Up);
            snake.Enqueue(snake.NextHeadLocation(Direction.Left));
            snake.Enqueue(snake.NextHeadLocation(Direction.Left));
            snake.Enqueue(snake.NextHeadLocation(Direction.Up));
            snake.Enqueue(snake.NextHeadLocation(Direction.Right));
            snake.Dequeue();
            snake.Dequeue();

            snake.Clear();

            Assert.AreEqual(0, snake.Count);
            int PiecesOnBoardNumber = 0;
            gameBoard.FlatBoard().ToList().ForEach(cell => { if (cell.Shape != null) PiecesOnBoardNumber++; });
            Assert.AreEqual(0, PiecesOnBoardNumber);
        }
    }
}
