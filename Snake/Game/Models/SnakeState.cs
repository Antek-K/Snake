using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game
{
    /// <summary>
    /// Determines if the snake is dead and the score in the game.
    /// </summary>
    public class SnakeState : INotifyPropertyChanged
    {
        private readonly int snakeInitialLength;
        private readonly int scoreFactor;

        private int snakeLength;
        private bool isDead;

        public event PropertyChangedEventHandler PropertyChanged;

        public SnakeState(int snakeInitialLength, int scoreFactor)
        {
            this.snakeInitialLength = snakeInitialLength;
            this.scoreFactor = scoreFactor;
            SnakeLength = snakeInitialLength;
            IsDead = true;
        }

        public SnakeState() { }

        public virtual int SnakeLength 
        { 
            get => snakeLength;
            set
            {
                snakeLength = value;
                NotifyPropertyChanged(nameof(Score));
            }
        }

        public object Score => (SnakeLength - snakeInitialLength) * scoreFactor;

        public bool IsDead
        {
            get => isDead;
            set
            {
                isDead = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
