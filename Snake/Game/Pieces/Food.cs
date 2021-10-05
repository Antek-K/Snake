using System;

namespace Game
{
    /// <summary>
    /// Determines location of the food on the game board.
    /// Provides methods to change location randomly, show food or hide it.
    /// </summary>
    public class Food
    {
        private readonly GameBoard gameBoard;
        private readonly Snake snake;
        private readonly Random random = new();

        public Food(GameBoard gameBoard, Snake snake)
        {
            this.gameBoard = gameBoard;
            this.snake = snake;
            PlaceFoodRandomlyNotAtSnake();
        }

        public Food() { }

        public CellLocation FoodLocation { get; } = new CellLocation();

        public void Show() => gameBoard[FoodLocation].PlaceFood();

        public virtual void Hide() => gameBoard[FoodLocation].Clear();

        public virtual void PlaceFoodRandomlyNotAtSnake()
        {
            do
            {
                SetFoodLocationRandomly();
            } while (snake.IsLocationOnSnake(FoodLocation));

            Show();
        }

        private void SetFoodLocationRandomly()
        {
            FoodLocation.Row = random.Next(0, gameBoard.RowCount);
            FoodLocation.Column = random.Next(0, gameBoard.ColumnCount);
        }
    }
}
