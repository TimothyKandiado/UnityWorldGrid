using UnityEngine;

namespace WorldGrid
{
    public abstract class Grid
    {
        public abstract void Init(Vector3 origin, uint worldSize, uint gridDensity);
        public abstract Cell[] GetCells();

        public abstract Cell GetCellWithPoint(Vector3 point);
    }
}