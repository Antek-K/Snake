namespace Game
{
    static class Parameters
    {
        public static int ColumnCount { get; } = 20;
        public static int RowCount { get; } = 20;

        public static int SnakeInitialX { get; } = 5;
        public static int SnakeInitialY { get; } = 10;
        public static int SnakeInitialLength { get; } = 10;
        public static Direction InitialDirection { get; } = Direction.Right;

        public static int SpeedMsPerMove { get; } = 100;

        public static int ScoreFactor { get; } = 1000;
    }
}
