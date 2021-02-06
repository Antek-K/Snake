using System;
using System.Diagnostics.CodeAnalysis;

namespace Game
{
    class CellLocation : IEquatable<CellLocation>
    {
        public CellLocation(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X
        {
            get;
        }
        public int Y
        {
            get;
        }

        public bool Equals([AllowNull] CellLocation other) =>  X == other?.X && Y == other?.Y;
    }
}
