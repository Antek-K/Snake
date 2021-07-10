using Xunit;
using FluentAssertions;

namespace Game.UnitTests
{
    public class MultiplicationConverterTests
    {
       [Fact]
        public void Convert_WhenValueIsNotInt_Returns1()
        {
            // Arrange
            object value = 0.5;
            object parameter = 0;
            int expResult = 1;
            var multiplicationConverter = new MultiplicationConverter();

            // Act
            var result = multiplicationConverter.Convert(value, typeof(int), parameter, null);

            // Assert
            result.Should().Be(expResult);
        }

        [Fact]
        public void Convert_WhenParameterIsNotInt_ReturnsValue()
        {
            // Arrange
            object value = 2;
            object parameter = "";
            var multiplicationConverter = new MultiplicationConverter();

            // Act
            var result = multiplicationConverter.Convert(value, typeof(int), parameter, null);

            // Assert
            result.Should().Be(value);
        }

        [Fact]
        public void Convert_WhenArgumentsAreInt_ResultMultiplicationResult()
        {
            // Arrange
            object value = 2;
            object parameter = 2;
            int expResult = 4;
            var multiplicationConverter = new MultiplicationConverter();

            // Act
            var result = multiplicationConverter.Convert(value, typeof(int), parameter, null);

            // Assert
            result.Should().Be(expResult);
        }
    }
}
