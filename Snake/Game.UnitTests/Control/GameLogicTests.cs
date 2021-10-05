using Xunit;
using Moq;

namespace Game.UnitTests
{
    public class GameLogicTests
    {
        [StaFact]
        public void Start_InitializesProperly()
        {
            // Arrange
            var gameBoardMock = new Mock<GameBoard>();

            var directionBufferMock = new Mock<DirectionBuffer>();
            directionBufferMock.Setup(db => db.Initialize());

            var snakeSateMock = new Mock<SnakeState>();
            snakeSateMock.SetupSet(ss => ss.IsDead = false);

            var snakeMoverMock = new Mock<SnakeMover>();
            snakeMoverMock.Setup(sm => sm.Initialize(It.IsAny<Direction>()));

            var foodMock = new Mock<Food>();
            foodMock.Setup(f => f.Hide());
            foodMock.Setup(f => f.PlaceFoodRandomlyNotAtSnake());

            var gameLogic = new GameLogic(gameBoardMock.Object, directionBufferMock.Object, snakeSateMock.Object, snakeMoverMock.Object, foodMock.Object);

            // Act
            gameLogic.Start();

            // Assert
            foodMock.Verify(f => f.Hide(), Times.Once);
            directionBufferMock.Verify(db => db.Initialize(), Times.Once);
            snakeMoverMock.Verify(sm => sm.Initialize(It.IsAny<Direction>()), Times.Once);
            foodMock.Verify(f => f.PlaceFoodRandomlyNotAtSnake(), Times.Once);
            snakeSateMock.VerifySet(ss => ss.IsDead = false, Times.Once);
        }

        [StaFact]
        public void PerformMove_WhenSnakeIsDying_SnakeIsNotMoved()
        {
            // Arrange
            var gameBoardMock = new Mock<GameBoard>();
            var directionBufferMock = new Mock<DirectionBuffer>();
            var snakeSateMock = new Mock<SnakeState>();

            var snakeMoverMock = new Mock<SnakeMover>();
            snakeMoverMock.Setup(sm => sm.IsSnakeDying()).Returns(true);
            snakeMoverMock.Setup(sm => sm.ExtendAndMove());
            snakeMoverMock.Setup(sm => sm.Move());

            var foodMock = new Mock<Food>();

            var gameLogic = new GameLogic(gameBoardMock.Object, directionBufferMock.Object, snakeSateMock.Object, snakeMoverMock.Object, foodMock.Object);

            // Act
            gameLogic.PerformMove();

            // Assert
            snakeMoverMock.Verify(sm => sm.ExtendAndMove(), Times.Never);
            snakeMoverMock.Verify(sm => sm.Move(), Times.Never);
        }

        [StaFact]
        public void PerformMove_WhenSnakeIsEating_SnakeIsExtendedAndMoved()
        {
            // Arrange
            var gameBoardMock = new Mock<GameBoard>();
            var directionBufferMock = new Mock<DirectionBuffer>();
            var snakeSateMock = new Mock<SnakeState>();

            var snakeMoverMock = new Mock<SnakeMover>();
            snakeMoverMock.Setup(sm => sm.IsSnakeDying()).Returns(false);
            snakeMoverMock.Setup(sm => sm.IsSnakeEating(It.IsAny<CellLocation>())).Returns(true);
            snakeMoverMock.Setup(sm => sm.ExtendAndMove());

            var foodMock = new Mock<Food>();
            foodMock.Setup(f => f.PlaceFoodRandomlyNotAtSnake());

            var gameLogic = new GameLogic(gameBoardMock.Object, directionBufferMock.Object, snakeSateMock.Object, snakeMoverMock.Object, foodMock.Object);

            // Act
            gameLogic.PerformMove();

            // Assert
            snakeMoverMock.Verify(sm => sm.ExtendAndMove(), Times.Once);
            foodMock.Verify(f => f.PlaceFoodRandomlyNotAtSnake(), Times.Once);
        }

        [StaFact]
        public void PerformMove_WhenSnakeIsNotEating_SnakeIsMoved()
        {
            // Arrange
            var gameBoardMock = new Mock<GameBoard>();
            var directionBufferMock = new Mock<DirectionBuffer>();
            var snakeSateMock = new Mock<SnakeState>();

            var snakeMoverMock = new Mock<SnakeMover>();
            snakeMoverMock.Setup(sm => sm.IsSnakeDying()).Returns(false);
            snakeMoverMock.Setup(sm => sm.IsSnakeEating(It.IsAny<CellLocation>())).Returns(false);
            snakeMoverMock.Setup(sm => sm.Move());

            var foodMock = new Mock<Food>();

            var gameLogic = new GameLogic(gameBoardMock.Object, directionBufferMock.Object, snakeSateMock.Object, snakeMoverMock.Object, foodMock.Object);

            // Act
            gameLogic.PerformMove();

            // Assert
            snakeMoverMock.Verify(sm => sm.Move(), Times.Once);
        }
    }
}
