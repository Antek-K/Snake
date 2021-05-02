using System;

namespace Game
{
    public class Food
    {
        private readonly GameBoard gameBoard;
        private readonly Snake snake;
        private readonly Random random = new();

        public Food(GameBoard gameBoard, Snake snake)
        {
            this.gameBoard = gameBoard;
            this.snake = snake;
            PlaceFoodNotAtSnake();
        }

        public CellLocation FoodLocation { get; } = new CellLocation();

        public void ShowFood() => gameBoard[FoodLocation].PlaceFood();

        public void Clear() => gameBoard[FoodLocation].Clear();

        public void PlaceFoodNotAtSnake()
        {
            do
            {
                SetFoodLocationRandomly();
            } while (snake.Contains(FoodLocation));

            ShowFood();
        }

        private void SetFoodLocationRandomly()
        {
            FoodLocation.Row = random.Next(0, gameBoard.ColumnCount);
            FoodLocation.Column = random.Next(0, gameBoard.RowCount);
        }
    }
}
