using Fleetio.ECS;
using Fleetio.Movement;
using Fleetio.Presentation;
using UnityEngine;

namespace Fleetio.Initialization
{
    public class SystemsInitialization : Context
    {
        [SerializeField]
        private Mesh _mesh;

        [SerializeField]
        private Material _material;
        
        public override void Install(World world)
        {
            var matrices = world.GetRepo<PositionData>();
            var moveSystem = new MoveSystem(matrices, world.GetRepo<MoveComponent>());

            var drawSystem = new DrawMeshesSystem(matrices, _material, _mesh, moveSystem, world);
            
            world.RegisterSystem(moveSystem);
            world.RegisterSystem(drawSystem);
            
        }
    }
    
}