using Xunit;
using FluentAssertions;

namespace Game.UnitTests
{
    public class CellLocationTests
    {
        [Fact]
        public void CellLocation_AfterCalled_XandYHaveProperValues()
        {
            // Arrange
            var row = 0;
            var column = 1;

            // Act
            var cellLocation = new CellLocation(row, column);

            // Assert
            cellLocation.Row.Should().Be(row);
            cellLocation.Column.Should().Be(column);
        }

        [Fact]
        public void Equals_WhenTheSame_ReturnsTrue()
        {
            // Arrange
            var row = 0;
            var column = 1;
            var cellLocation1 = new CellLocation(row, column);
            var cellLocation2 = new CellLocation(row, column);

            // Act
            bool result = cellLocation1.Equals(cellLocation2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_WhenDiffersOnlyByX_ReturnsFalse()
        {
            // Arrange
            var row1 = 0;
            var row2 = 1;
            var column = 1;
            var cellLocation1 = new CellLocation(row1, column);
            var cellLocation2 = new CellLocation(row2, column);

            // Act
            bool result = cellLocation1.Equals(cellLocation2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_WhenDiffersOnlyByY_ReturnsFalse()
        {
            // Arrange
            var row = 0;
            var column1 = 0;
            var column2 = 1;
            var cellLocation1 = new CellLocation(row, column1);
            var cellLocation2 = new CellLocation(row, column2);

            // Act
            bool result = cellLocation1.Equals(cellLocation2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_WhenDiffersByXandY_ReturnsFalse()
        {
            // Arrange
            var cellLocation1 = new CellLocation(0, 1);
            var cellLocation2 = new CellLocation(1, 0);

            // Act
            bool result = cellLocation1.Equals(cellLocation2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_WhenArgumentIsObjectAndNotDiffers_ReturnsTrue()
        {
            // Arrange
            var row = 0;
            var column = 0;
            object cellLocation1 = new CellLocation(row, column);
            object cellLocation2 = new CellLocation(row, column);

            // Act
            bool result = cellLocation1.Equals(cellLocation2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_WhenArgumentIsObjectAndDiffers_ReturnsFalse()
        {
            // Arrange
            object cellLocation1 = new CellLocation(0, 1);
            object cellLocation2 = new CellLocation(1, 2);

            // Act
            bool result = cellLocation1.Equals(cellLocation2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_WhenObjectsEqual_HashCodesEqual()
        {
            // Arrange
            var cellLocation1 = new CellLocation(0, 3);
            var cellLocation2 = new CellLocation(0, 3);

            // Act & Assert
            cellLocation1.GetHashCode().Should().Be(cellLocation2.GetHashCode());
        }
    }
}
