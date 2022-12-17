using System.Collections.Generic;
using UnityEngine;

namespace WorldGrid
{
    public class Chunk
    {
        public Vector3 Position { get; }
        private readonly List<MeshFilter> _meshFilters;
        private readonly GameObject _gameObject;
        private readonly GameObject _chunkHandle;
        public Chunk(Vector3 position, string id = "chunk")
        {
            _meshFilters = new List<MeshFilter>();
            Position = position;
            _gameObject = new GameObject(id)
            {
                transform =
                {
                    position = position
                }
            };

            _chunkHandle = new GameObject("Chunk Handle");

            _chunkHandle.transform.parent = _gameObject.transform;
            _chunkHandle.transform.position = Vector3.zero;

            _chunkHandle.AddComponent<MeshFilter>();
            _chunkHandle.AddComponent<MeshRenderer>();
        }

        public void AddMeshFilter(MeshFilter meshFilter)
        {
            _meshFilters.Add(meshFilter);
            meshFilter.GetComponent<MeshRenderer>().enabled = false;
            meshFilter.transform.SetParent(_chunkHandle.transform);
        }

        public void MergeMeshes()
        {
            MeshCombiner meshCombiner = new MeshCombiner();
            foreach (var meshFilter in _meshFilters)
            {
                var mesh = meshFilter.sharedMesh;
                var meshTransform = meshFilter.transform.localToWorldMatrix;
                
                meshCombiner.AddMesh(mesh, meshTransform);
            }

            // take the first material in the array
            _chunkHandle.GetComponent<MeshRenderer>().material = _meshFilters[0].GetComponent<MeshRenderer>().material;

            var superMesh = meshCombiner.GetCombinedMesh();
            _chunkHandle.GetComponent<MeshFilter>().sharedMesh = superMesh;
        }

        public void SetParentTransform(Transform parent)
        {
            _gameObject.transform.parent = parent;
        }
    }
}