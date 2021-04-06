using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game.UnitTests.Pieces
{
    [Apartment(ApartmentState.STA)]
    class FeedTests
    {
        [Test]
        public void Feed_AfterCalled_XandYHaveValuesInRange()
        {
            var feed = new Feed(new GameBoard(2,3));

            Assert.GreaterOrEqual(feed.X, 0);
            Assert.Less(feed.X, 2);
            Assert.GreaterOrEqual(feed.Y, 0);
            Assert.Less(feed.Y, 3);
        }

        [Test]
        public void SetFeedLocationRandomly_AfterCalled_XandYHaveValuesInRange()
        {
            var feed = new Feed(new GameBoard(2, 2));

            feed.SetFeedLocationRandomly();

            Assert.GreaterOrEqual(feed.X, 0);
            Assert.Less(feed.X, 2);
            Assert.GreaterOrEqual(feed.Y, 0);
            Assert.Less(feed.Y, 2);
        }

        [Test]
        public void SetFeedLocationRandomly_AfterCalledFiniteNumberOfTimes_HasDifferentValues()
        {
            var feed = new Feed(new GameBoard(4, 5));
            int oldX = feed.X;
            int oldY = feed.Y;

            do
            {
                feed.SetFeedLocationRandomly();
            } while (oldX == feed.X && oldY == feed.Y);

            Assert.Pass();
        }

        [Test]
        public void ShowFeed_WhenCalled_FeedIsShown()
        {
            var gameBoard = new GameBoard(2, 2);
            var feed = new Feed(gameBoard);

            feed.ShowFeed();

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
            var feed = new Feed(gameBoard);
            feed.ShowFeed();

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
            var feed = new Feed(gameBoard);
            var cellLocation = new CellLocation(feed.X, feed.Y);

            bool result1 = feed.Equals(cellLocation);
            bool result2 = cellLocation.Equals(feed);


            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void Equals_WhenFeedAndCellLocationDiffersByX_ReturnsFalse()
        {
            var gameBoard = new GameBoard(5, 2);
            var feed = new Feed(gameBoard);
            var cellLocation = new CellLocation((feed.X + 2) % 5, feed.Y);

            bool result1 = feed.Equals(cellLocation);
            bool result2 = cellLocation.Equals(feed);

            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [Test]
        public void Equals_WhenFeedAndCellLocationDiffersByY_ReturnsFalse()
        {
            var gameBoard = new GameBoard(2, 5);
            var feed = new Feed(gameBoard);
            var cellLocation = new CellLocation(feed.X, (feed.Y + 3) % 5);

            bool result1 = feed.Equals(cellLocation);
            bool result2 = cellLocation.Equals(feed);

            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [Test]
        public void Equals_WhenFeedAndCellLocationDiffersByXandY_ReturnsFalse()
        {
            var gameBoard = new GameBoard(6, 6);
            var feed = new Feed(gameBoard);
            var cellLocation = new CellLocation((feed.X + 3) % 6, (feed.Y + 3) % 6);

            bool result1 = feed.Equals(cellLocation);
            bool result2 = cellLocation.Equals(feed);

            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [Test]
        public void Equals_WhenTwoFeedsTheSame_ReturnsTrue()
        {
            var gameBoard = new GameBoard(1, 1);
            var feed1 = new Feed(gameBoard);
            var feed2 = new Feed(gameBoard);

            bool result = feed1.Equals(feed2);

            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_WhenTwoFeedsDiffer_ReturnsFalse()
        {
            var gameBoard = new GameBoard(10, 10);
            var feed1 = new Feed(gameBoard);
            var feed2 = new Feed(gameBoard);
            while (feed1.X == feed2.X && feed1.Y == feed2.Y)
            {
                feed2.SetFeedLocationRandomly();
            } 

            bool result = feed1.Equals(feed2);

            Assert.IsFalse(result);
        }
    }
}
