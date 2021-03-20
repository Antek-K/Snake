﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game.UnitTests.GameBoard
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
        public void Place_AfterCalled_ShapeIsRedEllipse()
        {
            var cell = new Cell();
            cell.PlaceSnake();

            cell.PlaceFeed();

            Assert.IsInstanceOf<Ellipse>(cell.Shape);
            Assert.AreEqual(Brushes.Red, cell.Shape.Fill);
        }

        [Test]
        public void Clear_AfterCalled_ShapeIsNull()
        {
            var cell = new Cell();
            cell.PlaceFeed();

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

            cell.PlaceFeed();

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
