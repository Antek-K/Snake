﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game
{
    public class Cell : INotifyPropertyChanged
    {
        private readonly Shape feed = new Ellipse() { Fill = Brushes.Red };
        private readonly Shape snake = new Rectangle() { Fill = Brushes.Black };

        private Shape shape;

        public event PropertyChangedEventHandler PropertyChanged;
        public static IDispatcher Dispatcher { get; set; } = new Dispatcher();

        public Shape Shape
        {
            get => shape;
            private set
            {
                shape = value;
                NotifyPropertyChanged();
            }
        }

        public void PlaceSnake() => Shape = snake;
        public void PlaceFeed() => Shape = feed;
        public void Clear() => Shape = null;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                Dispatcher?.Invoke(PropertyChanged, this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}