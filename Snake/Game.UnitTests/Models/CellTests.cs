using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game.UnitTests
{
    [Apartment(ApartmentState.STA)]
    class CellTests
    {
        [Test]
        public void Cell_AfterConstructorCalled_ShapeIsNull()
        {
            var cell = new Cell();

            Assert.IsNull(cell.Shape);
        }

        [Test]
        public void PlaceSnake_AfterCalled_ShapeIsBlackRectangle()
        {
            var cell = new Cell();

            cell.PlaceSnake();

            Assert.IsInstanceOf<Rectangle>(cell.Shape);
            Assert.AreEqual(Brushes.Black, cell.Shape.Fill);
        }

        [Test]
        public void PlaceFeed_AfterCalled_ShapeIsRedEllipse()
        {
            var cell = new Cell();
            cell.PlaceSnake();

            cell.PlaceFood();

            Assert.IsInstanceOf<Ellipse>(cell.Shape);
            Assert.AreEqual(Brushes.Red, cell.Shape.Fill);
        }

        [Test]
        public void Clear_AfterCalled_ShapeIsNull()
        {
            var cell = new Cell();
            cell.PlaceFood();

            cell.Clear();

            Assert.IsNull(cell.Shape);
        }

        [Test]
        public void PlaceSnake_WhenCalled_EventIsRaised()
        {
            var cell = new Cell();
            object eventSender = null;
            PropertyChangedEventArgs eventArgs = null;
            Cell.Dispatcher = new DispatcherMock();
            cell.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { eventSender = sender; eventArgs = e; };

            cell.PlaceSnake();

            Assert.AreSame(cell, eventSender);
            Assert.AreEqual(nameof(cell.Shape), eventArgs?.PropertyName);
        }

        [Test]
        public void PlaceFeed_WhenCalled_EventIsRaised()
        {
            var cell = new Cell();
            object eventSender = null;
            PropertyChangedEventArgs eventArgs = null;
            Cell.Dispatcher = new DispatcherMock();
            cell.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { eventSender = sender; eventArgs = e; };

            cell.PlaceFood();

            Assert.AreSame(cell, eventSender);
            Assert.AreEqual(nameof(cell.Shape), eventArgs?.PropertyName);
        }

        [Test]
        public void Clear_WhenCalled_EventIsRaised()
        {
            var cell = new Cell();
            object eventSender = null;
            PropertyChangedEventArgs eventArgs = null;
            Cell.Dispatcher = new DispatcherMock();
            cell.PropertyChanged += (object sender, PropertyChangedEventArgs e) => { eventSender = sender; eventArgs = e; };

            cell.Clear();

            Assert.AreSame(cell, eventSender);
            Assert.AreEqual(nameof(cell.Shape), eventArgs?.PropertyName);
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
