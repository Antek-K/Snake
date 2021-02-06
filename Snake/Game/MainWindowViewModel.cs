using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Game
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameBoard gameBoard;
        private readonly Snake snake;
        private Visibility gameOverSubwindowVisibility = Visibility.Visible;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            gameBoard = new GameBoard(Parameters.ColumnCount, Parameters.RowCount);
            snake = new Snake(gameBoard, Parameters.SnakeInitialX, Parameters.SnakeInitialY, Parameters.SnakeInitialLength, Parameters.InitialDirection, Parameters.SpeedMsPerMove, Snake_SnakeLengthChanged, Snake_SnakeDied);
        }

        public ObservableCollection<Cell> GameBoard => new ObservableCollection<Cell>(gameBoard.FlatBoard());

        public int ColumnCount => Parameters.ColumnCount;
        public int RowCount => Parameters.RowCount;
        public double GameBoardWidth => Parameters.ColumnCount * Parameters.CellSize;
        public double GameBoardHeight => Parameters.RowCount * Parameters.CellSize;

        public int Score => (snake.Length - Parameters.SnakeInitialLength) * Parameters.ScoreFactor;

        public Visibility GameOverSubwindowVisibility
        {
            get => gameOverSubwindowVisibility;
            set
            {
                gameOverSubwindowVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand StartCommand => new RelayCommand(Start);
        public ICommand ReplayCommand => new RelayCommand(Restart, () => GameOverSubwindowVisibility == Visibility.Visible);

        public ICommand GoLeftCommand => new RelayCommand(() => snake.Direction = Direction.Left);
        public ICommand GoDownCommand => new RelayCommand(() => snake.Direction = Direction.Down);
        public ICommand GoRightCommand => new RelayCommand(() => snake.Direction = Direction.Right);
        public ICommand GoUpCommand => new RelayCommand(() => snake.Direction = Direction.Up);

        private void Snake_SnakeLengthChanged() => NotifyPropertyChanged(nameof(Score));
        private void Snake_SnakeDied() => GameOverSubwindowVisibility = Visibility.Visible;

        private void Start()
        {
            snake.Start();
            GameOverSubwindowVisibility = Visibility.Hidden;
        }

        private void Restart()
        {
            snake.Restart(Parameters.SnakeInitialX, Parameters.SnakeInitialY, Parameters.SnakeInitialLength, Parameters.InitialDirection);
            GameOverSubwindowVisibility = Visibility.Hidden;
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
