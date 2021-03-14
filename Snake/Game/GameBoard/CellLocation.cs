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

        protected CellLocation() { }

        public int X
        {
            get;
            protected set;
        }

        public int Y
        {
            get;
            protected set;
        }

        public bool Equals([AllowNull] CellLocation other) =>  X == other?.X && Y == other?.Y;
    }
}
