using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Game
{
    class MainWindowViewModel
    {
        private readonly DirectionBuffer directionBuffer;
        private readonly GameLogic gameLogic;

        public MainWindowViewModel()
        {
            var gameBoard = new GameBoard(Parameters.ColumnCount, Parameters.RowCount);
            directionBuffer = new DirectionBuffer(Parameters.InitialDirection);
            SnakeState = new SnakeState(Parameters.SnakeInitialLength, Parameters.ScoreFactor);
            gameLogic = new GameLogic(gameBoard, directionBuffer, SnakeState);

            FlatBoard = new ObservableCollection<Cell>(gameBoard.FlatBoard());
            ColumnCount = gameBoard.ColumnCount;
            RowCount = gameBoard.RowCount;
        }
        
        public SnakeState SnakeState { get; }
        public ObservableCollection<Cell> FlatBoard { get; }
        public int ColumnCount { get; }
        public int RowCount { get; }

        public ICommand StartCommand => new RelayCommand(gameLogic.Start, () => SnakeState.IsDead);
        public ICommand GoLeftCommand => new RelayCommand(() => directionBuffer.AddDirectionToBuffer(Direction.Left));
        public ICommand GoDownCommand => new RelayCommand(() => directionBuffer.AddDirectionToBuffer(Direction.Down));
        public ICommand GoRightCommand => new RelayCommand(() => directionBuffer.AddDirectionToBuffer(Direction.Right));
        public ICommand GoUpCommand => new RelayCommand(() => directionBuffer.AddDirectionToBuffer(Direction.Up));
    }
}
