﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Game
{
    /// <summary>
    /// Determines location, row and column, of specific cell on the game board.
    /// </summary>
    public class CellLocation : IEquatable<CellLocation>
    {
        public CellLocation(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public CellLocation() { }

        public virtual int Row { get; set; }
        public virtual int Column { get; set; }


        public virtual bool Equals([AllowNull] CellLocation other) => Row == other?.Row && Column == other?.Column;

        public override bool Equals(object obj) => Equals(obj as CellLocation);

        public override int GetHashCode() => HashCode.Combine(Row, Column);
    }
}
