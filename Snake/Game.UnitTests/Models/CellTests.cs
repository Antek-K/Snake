using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Shapes;
using Xunit;
using FluentAssertions;

namespace Game.UnitTests
{
    public class CellTests
    {

        [StaFact]
        public void PlaceSnake_AfterCalled_ShapeIsBlackRectangle()
        {
            // Arrange
            var cell = new Cell();

            // Act
            cell.PlaceSnake();

            // Assert
            cell.Shape.Should().BeOfType<Rectangle>();
            cell.Shape.Fill.Should().Be(Brushes.Black);
        }

        [StaFact]
        public void PlaceFeed_AfterCalled_ShapeIsRedEllipse()
        {
            // Arrange
            var cell = new Cell();
            cell.PlaceSnake();

            // Act
            cell.PlaceFood();

            // Assert
            cell.Shape.Should().BeOfType<Ellipse>();
            cell.Shape.Fill.Should().Be(Brushes.Red);
        }

        [StaFact]
        public void Clear_AfterCalled_ShapeIsNull()
        {
            // Arrange
            var cell = new Cell();
            cell.PlaceFood();

            // Act
            cell.Clear();

            // Assert
            cell.Shape.Should().BeNull();
        }

        [StaFact]
        public void PlaceSnake_WhenCalled_EventIsRaised()
        {
            // Arrange
            var cell = new Cell();
            object eventSender = null;
            PropertyChangedEventArgs eventArgs = null;
            Cell.Dispatcher = new DispatcherMock();
            cell.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { eventSender = sender; eventArgs = e; };

            // Act
            cell.PlaceSnake();

            // Assert
            eventSender.Should().BeSameAs(cell);
            eventArgs?.PropertyName.Should().Be("Shape");
        }

        [StaFact]
        public void PlaceFeed_WhenCalled_EventIsRaised()
        {
            // Arrange
            var cell = new Cell();
            object eventSender = null;
            PropertyChangedEventArgs eventArgs = null;
            Cell.Dispatcher = new DispatcherMock();
            cell.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { eventSender = sender; eventArgs = e; };

            // Act
            cell.PlaceFood();

            // Assert
            eventSender.Should().BeSameAs(cell);
            eventArgs?.PropertyName.Should().Be("Shape");
        }

        [StaFact]
        public void Clear_WhenCalled_EventIsRaised()
        {
            // Arrange
            var cell = new Cell();
            object eventSender = null;
            PropertyChangedEventArgs eventArgs = null;
            Cell.Dispatcher = new DispatcherMock();
            cell.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { eventSender = sender; eventArgs = e; };

            // Act
            cell.Clear();

            // Assert
            eventSender.Should().BeSameAs(cell);
            eventArgs?.PropertyName.Should().Be("Shape");
        }

        class DispatcherMock : IDispatcher
        {
            public object Invoke(Delegate method, object caller, PropertyChangedEventArgs propertyChangedEventArgs)
            {
                var localMethod = method;
                if (localMethod == null)
                {
                    return null;
                }
                return localMethod.DynamicInvoke(caller, propertyChangedEventArgs);
            }
        }
    }
}
