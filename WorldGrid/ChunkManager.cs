using System.Collections.Generic;
using UnityEngine;

namespace WorldGrid
{
    public class ChunkManager
    {
        private readonly LayerMask _layerMask;
        private readonly Grid _grid;

        private readonly Transform _chunkManagerTransform;

        private readonly Dictionary<Cell_ID, Chunk> _chunkDictionary;

        public ChunkManager(LayerMask layerMask, Grid grid)
        {
            _layerMask = layerMask;
            _grid = grid;
            
            _chunkDictionary = new Dictionary<Cell_ID, Chunk>();

            var gameObject = new GameObject("Chunk Manager");
            _chunkManagerTransform = gameObject.transform;
            _chunkManagerTransform.position = Vector3.zero;
        }

        public void CreateChunks()
        {
            var gridCells = _grid.GetCells();
            if (gridCells.Length == 0) return;

            CreateChunkObjects(gridCells);
            AssignMeshesToChunks();
            FinalizeChunks();
        }

        private void CreateChunkObjects(Cell[] gridCells)
        {
            foreach (var gridCell in gridCells)
            {
                var chunk = new Chunk(gridCell.Position, gridCell.CellID.ToString());
                chunk.SetParentTransform(_chunkManagerTransform);
                _chunkDictionary.Add(gridCell.CellID, chunk);
            }
        }

        private void AssignMeshesToChunks()
        {
            var meshFilters = GameObject.FindObjectsOfType<MeshFilter>();

            foreach (var meshFilter in meshFilters)
            {
                var meshGameObject = meshFilter.gameObject;
                if (_layerMask != (_layerMask | (1 << meshGameObject.layer))) continue;

                var cellWithMesh = _grid.GetCellWithPoint(meshGameObject.transform.position);
                _chunkDictionary[cellWithMesh.CellID].AddMeshFilter(meshFilter);
            }
        }

        private void FinalizeChunks()
        {
            foreach (var chunk in _chunkDictionary)
            {
                chunk.Value.MergeMeshes();
            }
        }
    }
}