using System.Collections.Generic;
using UnityEngine;

namespace WorldGrid
{
    public class MeshCombiner
    {
        private readonly List<CombineInstance> _combineInstances;

        public MeshCombiner()
        {
            _combineInstances = new List<CombineInstance>();
        }

        public void AddMesh(Mesh mesh, Matrix4x4 transform)
        {
            var combineInstance = new CombineInstance()
            {
                mesh = mesh,
                transform = transform
            };
            
            _combineInstances.Add(combineInstance);
        }

        public Mesh GetCombinedMesh()
        {
            var mesh = new Mesh();
            mesh.CombineMeshes(_combineInstances.ToArray());

            return mesh;
        }
    }
}