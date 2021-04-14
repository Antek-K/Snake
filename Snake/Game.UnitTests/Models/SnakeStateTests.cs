using NUnit.Framework;
using System.ComponentModel;
using System.Threading;

namespace Game.UnitTests
{
    [Apartment(ApartmentState.STA)]
    class SnakeStateTests
    {
        [Test]
        public void Score_AfterSnakeInitialize_ScoreIsZero()
        {
            var gameBoard = new GameBoard(10, 10);
            var snake = new Snake(gameBoard, 0, 0, 4);
            var snakeState = new SnakeState(snake, 1);
            snake.Initialize(Direction.Right);

            int score = (int)snakeState.Score;

            Assert.Zero(score);
        }

        [Test]
        public void Score_AfterEnqueueFivePieces_ScoreIsFive()
        {
            var gameBoard = new GameBoard(10, 10);
            var snake = new Snake(gameBoard, 0, 0, 4);
            var snakeState = new SnakeState(snake, 1);
            snake.Initialize(Direction.Right);
            snake.Enqueue(snake.NextHeadLocation(Direction.Right));
            snake.Enqueue(snake.NextHeadLocation(Direction.Right));
            snake.Enqueue(snake.NextHeadLocation(Direction.Right));
            snake.Enqueue(snake.NextHeadLocation(Direction.Down));
            snake.Enqueue(snake.NextHeadLocation(Direction.Down));

            int score = (int)snakeState.Score;

            Assert.AreEqual(5, score);
        }

        [Test]
        public void Score_WhenScoreFactorIs30And7PiecesEnqueued_ScoreIs210()
        {
            var gameBoard = new GameBoard(10, 10);
            var snake = new Snake(gameBoard, 0, 0, 1);
            var snakeState = new SnakeState(snake, 30);
            snake.Initialize(Direction.Right);
            snake.Enqueue(snake.NextHeadLocation(Direction.Right));
            snake.Enqueue(snake.NextHeadLocation(Direction.Right));
            snake.Enqueue(snake.NextHeadLocation(Direction.Right));
            snake.Enqueue(snake.NextHeadLocation(Direction.Down));
            snake.Enqueue(snake.NextHeadLocation(Direction.Down));
            snake.Enqueue(snake.NextHeadLocation(Direction.Left));
            snake.Enqueue(snake.NextHeadLocation(Direction.Down));

            int score = (int)snakeState.Score;

            Assert.AreEqual(210, score);
        }

        [Test]
        public void IsDead_WhenSetFalse_IsFalse()
        {
            var gameBoard = new GameBoard(10, 10);
            var snake = new Snake(gameBoard, 0, 0, 1);
            var snakeState = new SnakeState(snake, 1);

            snakeState.IsDead = false;

            Assert.IsFalse(snakeState.IsDead);
        }

        [Test]
        public void IsDead_WhenSetTrue_IsTrue()
        {
            var gameBoard = new GameBoard(10, 10);
            var snake = new Snake(gameBoard, 0, 0, 1);
            var snakeState = new SnakeState(snake, 1);

            snakeState.IsDead = true;

            Assert.IsTrue(snakeState.IsDead);
        }

        [Test]
        public void IsDead_WhenSetFalse_CallsEvent()
        {
            var gameBoard = new GameBoard(10, 10);
            var snake = new Snake(gameBoard, 0, 0, 1);
            var snakeState = new SnakeState(snake, 1);
            object eventSender = null;
            PropertyChangedEventArgs eventArgs = null;
            snakeState.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { eventSender = sender; eventArgs = e; };

            snakeState.IsDead = false;

            Assert.AreSame(snakeState, eventSender);
            Assert.AreEqual(nameof(snakeState.IsDead), eventArgs?.PropertyName);
        }

        [Test]
        public void IsDead_WhenSetTrue_CallsEvent()
        {
            var gameBoard = new GameBoard(10, 10);
            var snake = new Snake(gameBoard, 0, 0, 1);
            var snakeState = new SnakeState(snake, 1);
            object eventSender = null;
            PropertyChangedEventArgs eventArgs = null;
            snakeState.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { eventSender = sender; eventArgs = e; };

            snakeState.IsDead = true;

            Assert.AreSame(snakeState, eventSender);
            Assert.AreEqual(nameof(snakeState.IsDead), eventArgs?.PropertyName);
        }

        [Test]
        public void ScoreValueChanged_WhenCalled_EventIsRised()
        {
            var gameBoard = new GameBoard(10, 10);
            var snake = new Snake(gameBoard, 0, 0, 1);
            var snakeState = new SnakeState(snake, 1);
            object eventSender = null;
            PropertyChangedEventArgs eventArgs = null;
            snakeState.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { eventSender = sender; eventArgs = e; };

            snakeState.ScoreValueChanged();

            Assert.AreSame(snakeState, eventSender);
            Assert.AreEqual(nameof(snakeState.Score), eventArgs?.PropertyName);
        }
    }
}
