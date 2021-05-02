using NUnit.Framework;

namespace Game.UnitTests
{
    class DirectionManagerTests
    {
        /*[Test]
        public void Initialize_WhenCalled_ShouldSetToOneElement()
        {
            var directionManager = new DirectionBuffer(Direction.Down);

            directionManager.Initialize();     

            Assert.AreEqual(1, directionManager.Count);
            Assert.AreEqual(Direction.Down, directionManager.Peek());
        }

        [Test]
        public void Initialize_WhenCalled_ShouldResetToOneElement()
        {
            var directionManager = new DirectionBuffer(Direction.Left);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Right);
            directionManager.Enqueue(Direction.Up);
            directionManager.Enqueue(Direction.Down);

            directionManager.Initialize();

            Assert.AreEqual(1, directionManager.Count);
            Assert.AreEqual(Direction.Left, directionManager.Peek());
        }

        [Test]
        public void Dequeue_WhenThereIsOnlyOneDirection_ShouldKeepReturningIt()
        {
            var directionManager = new DirectionBuffer(Direction.Right);
            directionManager.Initialize();

            var nextDirection1 = directionManager.Dequeue();
            var nextDirection2 = directionManager.Dequeue();
            
            Assert.AreEqual(Direction.Right, nextDirection1, "nextDirection1");
            Assert.AreEqual(Direction.Right, nextDirection2, "nextDirection2");
            Assert.AreEqual(Direction.Right, directionManager.Peek(), "Peek");
        }

        [Test]
        public void Dequeue_WhenSecondDirectionIsAcceptable_ShouldKeepReturningSecond()
        {
            var directionManager = new DirectionBuffer(Direction.Left);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Up);

            var nextDirection1 = directionManager.Dequeue();
            var nextDirection2 = directionManager.Dequeue();

            
            Assert.AreEqual(Direction.Up, nextDirection1, "nextDirection1");
            Assert.AreEqual(Direction.Up, nextDirection2, "nextDirection2");
            Assert.AreEqual(Direction.Up, directionManager.Peek(), "Peek");
        }

        [Test]
        public void Dequeue_WhenSecondDirectionIsNotAcceptable_ShouldKeepReturningFirst()
        {
            var directionManager = new DirectionBuffer(Direction.Left);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Right);

            var nextDirection1 = directionManager.Dequeue();
            var nextDirection2 = directionManager.Dequeue();


            Assert.AreEqual(Direction.Left, nextDirection1, "nextDirection1");
            Assert.AreEqual(Direction.Left, nextDirection2, "nextDirection2");
            Assert.AreEqual(Direction.Left, directionManager.Peek(), "Peek");
        }

        [Test]
        public void Dequeue_WhenThreeDirectionsAreAcceptable_ShouldReturnSecondAndThird()
        {
            var directionManager = new DirectionBuffer(Direction.Right);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Up);
            directionManager.Enqueue(Direction.Left);

            var nextDirection1 = directionManager.Dequeue();
            var nextDirection2 = directionManager.Dequeue();


            Assert.AreEqual(Direction.Up, nextDirection1, "nextDirection1");
            Assert.AreEqual(Direction.Left, nextDirection2, "nextDirection2");
            Assert.AreEqual(Direction.Left, directionManager.Peek(), "Peek");
        }

        [Test]
        public void Dequeue_WhenThirdDirectionIsAcceptable_ShouldReturnThird()
        {
            var directionManager = new DirectionBuffer(Direction.Right);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Right);
            directionManager.Enqueue(Direction.Up);

            var nextDirection1 = directionManager.Dequeue();
            var nextDirection2 = directionManager.Dequeue();


            Assert.AreEqual(Direction.Up, nextDirection1, "nextDirection1");
            Assert.AreEqual(Direction.Up, nextDirection2, "nextDirection2");
            Assert.AreEqual(Direction.Up, directionManager.Peek(), "Peek");
        }

        [Test]
        public void Dequeue_WhenThirdDirectionOutOfFourIsAcceptable_ShouldKeepReturningThird()
        {
            var directionManager = new DirectionBuffer(Direction.Up);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Down);
            directionManager.Enqueue(Direction.Left);
            directionManager.Enqueue(Direction.Right);

            var nextDirection1 = directionManager.Dequeue();
            var nextDirection2 = directionManager.Dequeue();


            Assert.AreEqual(Direction.Left, nextDirection1, "nextDirection1");
            Assert.AreEqual(Direction.Left, nextDirection2, "nextDirection2");
            Assert.AreEqual(Direction.Left, directionManager.Peek(), "Peek");
        }

        [Test]
        public void Dequeue_WhenGoingDown_CantTurnUpOrDownCanTurnRight()
        {
            var directionManager = new DirectionBuffer(Direction.Down);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Up);
            directionManager.Enqueue(Direction.Down);
            directionManager.Enqueue(Direction.Right);

            var nextDirection = directionManager.Dequeue();

            Assert.AreEqual(Direction.Right, nextDirection);
        }

        [Test]
        public void Dequeue_WhenGoingDown_CanTurnLeft()
        {
            var directionManager = new DirectionBuffer(Direction.Down);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Left);

            var nextDirection = directionManager.Dequeue();

            Assert.AreEqual(Direction.Left, nextDirection);
        }

        [Test]
        public void Dequeue_WhenGoingLeft_CantTurnRightOrLeftCanTurnUp()
        {
            var directionManager = new DirectionBuffer(Direction.Left);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Right);
            directionManager.Enqueue(Direction.Left);
            directionManager.Enqueue(Direction.Up);

            var nextDirection = directionManager.Dequeue();

            Assert.AreEqual(Direction.Up, nextDirection);
        }

        [Test]
        public void Dequeue_WhenGoingLeft_CanTurnDown()
        {
            var directionManager = new DirectionBuffer(Direction.Left);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Down);

            var nextDirection = directionManager.Dequeue();

            Assert.AreEqual(Direction.Down, nextDirection);
        }

        [Test]
        public void Dequeue_WhenGoingUp_CantTurnUpOrDownCanTurnRight()
        {
            var directionManager = new DirectionBuffer(Direction.Up);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Up);
            directionManager.Enqueue(Direction.Down);
            directionManager.Enqueue(Direction.Right);

            var nextDirection = directionManager.Dequeue();

            Assert.AreEqual(Direction.Right, nextDirection);
        }

        [Test]
        public void Dequeue_WhenGoingUp_CanTurnLeft()
        {
            var directionManager = new DirectionBuffer(Direction.Up);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Left);

            var nextDirection = directionManager.Dequeue();

            Assert.AreEqual(Direction.Left, nextDirection);
        }

        [Test]
        public void Dequeue_WhenGoingRight_CantTurnRightOrLeftCanTurnUp()
        {
            var directionManager = new DirectionBuffer(Direction.Right);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Right);
            directionManager.Enqueue(Direction.Left);
            directionManager.Enqueue(Direction.Up);

            var nextDirection = directionManager.Dequeue();

            Assert.AreEqual(Direction.Up, nextDirection);
        }

        [Test]
        public void Dequeue_WhenGoingRight_CanTurnDown()
        {
            var directionManager = new DirectionBuffer(Direction.Right);
            directionManager.Initialize();
            directionManager.Enqueue(Direction.Down);

            var nextDirection = directionManager.Dequeue();

            Assert.AreEqual(Direction.Down, nextDirection);
        }*/
    }
}
