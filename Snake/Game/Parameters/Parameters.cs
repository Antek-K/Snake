namespace Game
{
    /// <summary>
    /// Provides parameters of game board, snake initial state, snake speed and score factor.
    /// </summary>
    static class Parameters
    {
        public static int ColumnCount { get; } = 20;
        public static int RowCount { get; } = 20;

        public static int SnakeInitialColumn { get; } = 5;
        public static int SnakeInitialRow { get; } = 10;
        public static int SnakeInitialLength { get; } = 10;
        public static Direction InitialDirection { get; } = Direction.Right;

        public static int SpeedMsPerMove { get; } = 100;

        public static int ScoreFactor { get; } = 1000;
    }
}
