using Fleetio.ECS;
using Unity.Collections;
using UnityEngine;

namespace Fleetio.Presentation
{
    public class DrawMeshesSystem<TComponent> : ISystem where TComponent : unmanaged
    {
        private readonly Mesh _mesh;
        private readonly IJobDependency _moveJob;
        private readonly ComponentsList<PositionData> _positions;
        private readonly ComponentsList<TComponent> _markers;

        private readonly RenderParams _renderParams;

        private ComponentsList<TComponent> _marker;

        public DrawMeshesSystem(ComponentsList<PositionData> positions, ComponentsList<TComponent> markers,
            Material material, Mesh mesh)
        {
            _positions = positions;
            _mesh = mesh;
            _renderParams = new RenderParams { material = material};
            _markers = markers;
        }
        
        public void Run()
        {
            var filter = new Filter<PositionData, TComponent>(_positions.Map, _markers.Map, Allocator.Temp);
            var array = filter.GetArray0();
            var length = array.Length;
            var batchesCount = length / 1023;
            var leftoversCount = length % 1023;
            
            for (int i = 0; i < batchesCount; i++)
            {
                Graphics.RenderMeshInstanced(_renderParams, _mesh, 0, array, 
                    1023, i * 1023);
            }

            if (leftoversCount > 0)
            {
                Graphics.RenderMeshInstanced(_renderParams, _mesh, 0, array,
                    leftoversCount, length - leftoversCount);
            }

            filter.Dispose();
        }
        
    }
}