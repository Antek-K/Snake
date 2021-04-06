using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game
{
    public class SnakeState : INotifyPropertyChanged
    {
        private readonly Snake snake;
        private readonly int scoreFactor;

        private bool isDead;

        public event PropertyChangedEventHandler PropertyChanged;

        public SnakeState(Snake snake, int scoreFactor)
        {
            this.snake = snake;
            this.scoreFactor = scoreFactor;
        }

        public object Score => (snake.Count - snake.InitialLength) * scoreFactor;

        public bool IsDead
        {
            get => isDead;
            set
            {
                isDead = value;
                NotifyPropertyChanged();
            }
        }

        public void ScoreValueChanged() => NotifyPropertyChanged(nameof(Score));
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
