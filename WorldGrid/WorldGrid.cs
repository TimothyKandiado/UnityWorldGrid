using UnityEngine;
using Color = UnityEngine.Color;

namespace WorldGrid
{
    public class WorldGrid : MonoBehaviour
    {
        [SerializeField] private GridShape gridShape;
        [SerializeField] private LayerMask layerMask;
        //[SerializeField] private string tag = "Foilage";
        
        [SerializeField] private uint worldSize;
        [SerializeField] private uint gridDensity;

        private Grid _grid;
        private ChunkManager _chunkManager;

        private void Start()
        {
            _grid = new BoxGrid();
            var worldCenter = transform.position;
            _grid.Init(worldCenter, worldSize, gridDensity);

            _chunkManager = new ChunkManager(layerMask, _grid, worldCenter);
            _chunkManager.CreateChunks();
            
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var worldCenter = transform.position;
            //Draw World Bounds
            Gizmos.DrawWireCube(worldCenter, new Vector3(worldSize, worldSize, worldSize));

            if (gridDensity == 0 || gridDensity == 1) return;
            
            // Draw grids inside the world
            var cellSize = worldSize / (float) gridDensity;

            var firstCellXPosition = (worldCenter.x - worldSize / 2.0f) + 0.5f * cellSize;
            var firstCellZPosition = (worldCenter.z - worldSize / 2.0f) + 0.5f * cellSize;

            // take the bottom left cell as reference
            var refCellPosition = new Vector3(firstCellXPosition, worldCenter.y, firstCellZPosition);
            
            Gizmos.color = Color.blue;
            for (int row = 0; row < gridDensity; row++)
            {
                var cellPositionX = refCellPosition.x + cellSize * row;
                for (int column = 0; column < gridDensity; column++)
                {
                    var cellPositionZ = refCellPosition.z + cellSize * column;
                    var cellPosition = new Vector3(cellPositionX, refCellPosition.y, cellPositionZ);
                    Gizmos.DrawWireCube(cellPosition, new Vector3(cellSize, worldSize, cellSize));
                }
            }
        }
    }
}
