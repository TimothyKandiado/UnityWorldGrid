using UnityEngine;

namespace WorldGrid
{
    public struct Cell
    {
        public Cell_ID CellID { get; }
        public Vector3 Position { get; }

        public Cell(Cell_ID cellID, Vector3 position)
        {
            CellID = cellID;
            Position = position;
        }
    }
}