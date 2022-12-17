using System;

namespace WorldGrid
{
    public struct Cell_ID
    {
        public uint Row { get; }
        public uint Column { get; }
        
        // Incase the actual cell is outside bounds
        public bool IsValidCell { get; }

        public Cell_ID(uint row, uint column, bool valid = true)
        {
            Row = row;
            Column = column;
            IsValidCell = valid;
        }

        public override string ToString()
        {
            return $"Cell {Row},{Column}";
        }
    }
}