﻿using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Game
{
    class MainWindowViewModel
    {
        private readonly DirectionManager directionManager;

        public MainWindowViewModel()
        {
            var gameBoard = new GameBoard(Parameters.ColumnCount, Parameters.RowCount);
            var snake = new Snake(gameBoard, Parameters.SnakeInitialX, Parameters.SnakeInitialY, Parameters.SnakeInitialLength);
            var feed = new Feed(gameBoard);
            directionManager = new DirectionManager(Parameters.InitialDirection);
            GameLogic = new GameLogic(snake, feed, directionManager, Parameters.SpeedMsPerMove, Parameters.ScoreFactor);

            FlatBoard = new ObservableCollection<Cell>(gameBoard.FlatBoard());
            ColumnCount = gameBoard.ColumnCount;
            RowCount = gameBoard.RowCount;
        }
        public GameLogic GameLogic { get; }
        public ObservableCollection<Cell> FlatBoard { get; }
        public int ColumnCount { get; }
        public int RowCount { get; }

        public ICommand StartCommand => new RelayCommand(GameLogic.Start, () => GameLogic.IsDead);
        public ICommand GoLeftCommand => new RelayCommand(() => directionManager.Enqueue(Direction.Left));
        public ICommand GoDownCommand => new RelayCommand(() => directionManager.Enqueue(Direction.Down));
        public ICommand GoRightCommand => new RelayCommand(() => directionManager.Enqueue(Direction.Right));
        public ICommand GoUpCommand => new RelayCommand(() => directionManager.Enqueue(Direction.Up));
    }
}