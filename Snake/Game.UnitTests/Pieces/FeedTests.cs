using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game.UnitTests
{
    [Apartment(ApartmentState.STA)]
    class FeedTests
    {
        /*[Test]
        public void Feed_AfterCalled_XandYHaveValuesInRange()
        {
            var feed = new Food(new GameBoard(2,3));

            Assert.GreaterOrEqual(feed.Row, 0);
            Assert.Less(feed.Row, 2);
            Assert.GreaterOrEqual(feed.Column, 0);
            Assert.Less(feed.Column, 3);
        }

        [Test]
        public void SetFeedLocationRandomly_AfterCalled_XandYHaveValuesInRange()
        {
            var feed = new Food(new GameBoard(2, 2));

            feed.SetFoodLocationRandomly();

            Assert.GreaterOrEqual(feed.Row, 0);
            Assert.Less(feed.Row, 2);
            Assert.GreaterOrEqual(feed.Column, 0);
            Assert.Less(feed.Column, 2);
        }

        [Test]
        public void SetFeedLocationRandomly_AfterCalledFiniteNumberOfTimes_HasDifferentValues()
        {
            var food = new Food(new GameBoard(4, 5));
            int oldX = food.Row;
            int oldY = food.Column;

            do
            {
                food.SetFoodLocationRandomly();
            } while (oldX == food.Row && oldY == food.Column);

            Assert.Pass();
        }

        [Test]
        public void ShowFeed_WhenCalled_FeedIsShown()
        {
            var gameBoard = new GameBoard(2, 2);
            var feed = new Food(gameBoard);

            feed.ShowFood();

            Assert.IsInstanceOf(typeof(Ellipse), gameBoard[feed].Shape);
            Assert.AreEqual(Brushes.Red, gameBoard[feed].Shape.Fill);
            int PiecesOnBoardNumber = 0;
            gameBoard.FlatBoard().ToList().ForEach(cell => { if (cell.Shape != null) PiecesOnBoardNumber++; });
            Assert.AreEqual(1, PiecesOnBoardNumber);
        }

        [Test]
        public void Clear_WhenCalled_FeedIsNotShown()
        {
            var gameBoard = new GameBoard(2, 2);
            var feed = new Food(gameBoard);
            feed.ShowFood();

            feed.Clear();

            Assert.IsNull(gameBoard[feed].Shape);
            int PiecesOnBoardNumber = 0;
            gameBoard.FlatBoard().ToList().ForEach(cell => { if (cell.Shape != null) PiecesOnBoardNumber++; });
            Assert.AreEqual(0, PiecesOnBoardNumber);
        }


        [Test]
        public void Equals_WhenFeedAndCellLocationTheSame_ReturnsTrue()
        {
            var gameBoard = new GameBoard(2, 2);
            var feed = new Food(gameBoard);
            var cellLocation = new CellLocation(feed.Row, feed.Column);

            bool result1 = feed.Equals(cellLocation);
            bool result2 = cellLocation.Equals(feed);


            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void Equals_WhenFeedAndCellLocationDiffersByX_ReturnsFalse()
        {
            var gameBoard = new GameBoard(5, 2);
            var feed = new Food(gameBoard);
            var cellLocation = new CellLocation((feed.Row + 2) % 5, feed.Column);

            bool result1 = feed.Equals(cellLocation);
            bool result2 = cellLocation.Equals(feed);

            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [Test]
        public void Equals_WhenFeedAndCellLocationDiffersByY_ReturnsFalse()
        {
            var gameBoard = new GameBoard(2, 5);
            var feed = new Food(gameBoard);
            var cellLocation = new CellLocation(feed.Row, (feed.Column + 3) % 5);

            bool result1 = feed.Equals(cellLocation);
            bool result2 = cellLocation.Equals(feed);

            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [Test]
        public void Equals_WhenFeedAndCellLocationDiffersByXandY_ReturnsFalse()
        {
            var gameBoard = new GameBoard(6, 6);
            var feed = new Food(gameBoard);
            var cellLocation = new CellLocation((feed.Row + 3) % 6, (feed.Column + 3) % 6);

            bool result1 = feed.Equals(cellLocation);
            bool result2 = cellLocation.Equals(feed);

            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [Test]
        public void Equals_WhenTwoFeedsTheSame_ReturnsTrue()
        {
            var gameBoard = new GameBoard(1, 1);
            var feed1 = new Food(gameBoard);
            var feed2 = new Food(gameBoard);

            bool result = feed1.Equals(feed2);

            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_WhenTwoFeedsDiffer_ReturnsFalse()
        {
            var gameBoard = new GameBoard(10, 10);
            var feed1 = new Food(gameBoard);
            var feed2 = new Food(gameBoard);
            while (feed1.Row == feed2.Row && feed1.Column == feed2.Column)
            {
                feed2.SetFoodLocationRandomly();
            } 

            bool result = feed1.Equals(feed2);

            Assert.IsFalse(result);
        }*/
    }
}
