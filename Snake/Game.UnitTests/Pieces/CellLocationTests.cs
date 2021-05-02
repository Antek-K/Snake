using NUnit.Framework;

namespace Game.UnitTests
{
    class CellLocationTests
    {
        [Test]
        public void CellLocation_AfterCalled_XandYHaveProperValues()
        {
            var cellLocation = new CellLocation(2, 7);

            Assert.AreEqual(2, cellLocation.Row);
            Assert.AreEqual(7, cellLocation.Column);
        }

        [Test]
        public void Equals_WhenTheSame_ReturnsTrue()
        {
            var cellLocation1 = new CellLocation(5, 1);
            var cellLocation2 = new CellLocation(5, 1);

            bool result = cellLocation1.Equals(cellLocation2);

            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_WhenDiffersByX_ReturnsFalse()
        {
            var cellLocation1 = new CellLocation(2, 3);
            var cellLocation2 = new CellLocation(3, 3);

            bool result = cellLocation1.Equals(cellLocation2);

            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_WhenDiffersByY_ReturnsFalse()
        {
            var cellLocation1 = new CellLocation(0, 11);
            var cellLocation2 = new CellLocation(0, 1);

            bool result = cellLocation1.Equals(cellLocation2);

            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_WhenDiffersByXandY_ReturnsFalse()
        {
            var cellLocation1 = new CellLocation(4, 5);
            var cellLocation2 = new CellLocation(5, 4);

            bool result = cellLocation1.Equals(cellLocation2);

            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_WhenArgumentIsObjectAndNotDiffers_ReturnsTrue()
        {
            var cellLocation1 = new CellLocation(2, 2);
            object cellLocation2 = new CellLocation(2, 2);

            bool result = cellLocation1.Equals(cellLocation2);

            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_WhenArgumentIsObjectAndDiffers_ReturnsFalse()
        {
            var cellLocation1 = new CellLocation(3, 4);
            object cellLocation2 = new CellLocation(5, 3);

            bool result = cellLocation1.Equals(cellLocation2);

            Assert.IsFalse(result);
        }

        [Test]
        public void GetHashCode_WhenObjectsEqual_HashCodesEqual()
        {
            var cellLocation1 = new CellLocation(0, 3);
            var cellLocation2 = new CellLocation(0, 3);

            Assert.AreEqual(cellLocation1.GetHashCode(), cellLocation2.GetHashCode());
        }
    }
}
