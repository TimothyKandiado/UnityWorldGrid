using System;

namespace WorldGrid
{
    public struct Cell_ID
    {
        public uint Row { get; }
        public uint Column { get; }

        public Cell_ID(uint row, uint column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return $"Cell {Row},{Column}";
        }
    }
}