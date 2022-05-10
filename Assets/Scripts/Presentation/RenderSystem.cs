using Fleetio.ECS;
using UnityEngine;

namespace Fleetio.Presentation
{
    public class DrawMeshesSystem : ISystem
    {
        private readonly Mesh _mesh;
        private readonly IJobDependency _moveJob;
        private readonly World _world;
        private ComponentsList<PositionData> _positions;

        private readonly RenderParams _renderParams;
        
        public DrawMeshesSystem(ComponentsList<PositionData> positions, Material material, Mesh mesh, IJobDependency moveJob, World world)
        {
            _positions = positions;
            _mesh = mesh;
            _moveJob = moveJob;
            _world = world;
            _renderParams = new RenderParams { material = material};
        }
        
        public void Run()
        {
            _moveJob.JobHandle.Complete();
            
            var batchesCount = _world.EntitiesCount / 1023;
            for (int i = 0; i < batchesCount; i++)
            {
                Graphics.RenderMeshInstanced(_renderParams, _mesh, 0, _positions.GetArray(), 
                    1023, i * 1023);
            }

            var leftover = _world.EntitiesCount % 1023;
            if (_world.EntitiesCount % 1023 > 0)
                Graphics.RenderMeshInstanced(_renderParams, _mesh, 0, _positions.GetArray(), 
                    leftover, _world.EntitiesCount - leftover);
        }
        
    }
}