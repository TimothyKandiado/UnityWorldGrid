using UnityEngine;
using System.Collections.Generic;

namespace WorldGrid
{
    public class BoxGrid : Grid
    {
        private Vector3 _worldCenter;
        private uint _worldSize;
        private uint _gridDensity;

        private readonly List<Cell> _cells;
        private readonly Dictionary<Cell_ID, Vector3> _cellDictionary;
        private float _cellSize;

        public BoxGrid()
        {
            _cells = new List<Cell>();
            _cellDictionary = new Dictionary<Cell_ID, Vector3>();
        }

        public override void Init(Vector3 origin, uint worldSize, uint gridDensity)
        {
            _worldCenter = origin;
            _worldSize = worldSize;
            _gridDensity = gridDensity;
            
            _cellSize = _worldSize / (float) _gridDensity;
            
            CreateGrid();
        }

        public override Cell[] GetCells()
        {
            return _cells.ToArray();
        }

        public override Cell GetCellWithPoint(Vector3 point)
        {
            var cellId = CreateCellIdFromPosition(point);

            var cellPosition = _cellDictionary[cellId];

            var cell = new Cell(cellId, cellPosition);
  
            return cell;
        }

        private Cell_ID CreateCellIdFromPosition(Vector3 point)
        {
           // calculate column the point is in 
           uint column = 0; // assume point is in the firstColumn
           var leftBound = _worldCenter.x - _worldSize / 2.0f;
           while (column < _gridDensity)
           {
               if((leftBound + _cellSize * (column + 1)) > point.x) break;
               column++;
           }

           uint row = 0; // assume point is in first row
           var bottomBound = _worldCenter.z - _worldSize / 2.0f;

           while (row < _gridDensity)
           {
               if((bottomBound + _cellSize * (row + 1)) > point.z) break;
               row++;
           }

           return new Cell_ID(column, row);
        }

        private bool CellHasPoint(Cell cell, Vector3 point)
        {
            Bounds bounds = new Bounds(cell.Position, new Vector3(_cellSize, _worldSize, _cellSize));
            return bounds.Contains(point);
        }

        private void CreateGrid()
        {
            // Draw grids inside the world

            var firstCellXPosition = (_worldCenter.x - _worldSize / 2.0f) + 0.5f * _cellSize;
            var firstCellZPosition = (_worldCenter.z - _worldSize / 2.0f) + 0.5f * _cellSize;

            // take the bottom left cell as reference
            var refCellPosition = new Vector3(firstCellXPosition, _worldCenter.y, firstCellZPosition);
            
            Gizmos.color = Color.blue;
            for (uint column = 0; column < _gridDensity; column++)
            {
                var cellPositionZ = refCellPosition.x + _cellSize * column;
                for (uint row = 0; row < _gridDensity; row++)
                {
                    var cellPositionX = refCellPosition.z + _cellSize * row;
                    var cellPosition = new Vector3(cellPositionX, refCellPosition.y, cellPositionZ);

                    var cellId = new Cell_ID(row, column);
                    var cell = new Cell(cellId, cellPosition);
                    
                    _cellDictionary.Add(cellId, cellPosition);
                    _cells.Add(cell);
                }
            }
        }
    }
}