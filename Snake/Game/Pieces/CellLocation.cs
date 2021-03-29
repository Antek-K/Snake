using System;
using System.Diagnostics.CodeAnalysis;

namespace Game
{
    public class CellLocation : IEquatable<CellLocation>
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

        public virtual bool Equals([AllowNull] CellLocation other) =>  X == other?.X && Y == other?.Y;

        public override bool Equals(object obj) => Equals(obj as CellLocation);

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}
