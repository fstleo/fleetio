using Fleetio.ECS;
using Fleetio.Input;
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
        
        [SerializeField] private Camera _camera;
        [SerializeField] private Mesh _selectionMesh;
        [SerializeField] private Material _selectionMaterial;
        
        
        public override void Install(World world)
        {
            var matrices = world.GetRepo<PositionData>();
            var moveSystem = new MoveSystem(matrices, world.GetRepo<MoveComponent>());

            var drawSystem = new DrawMeshesSystem<Fleet>(matrices, world.GetRepo<Fleet>(), _material, _mesh);
            
            world.RegisterSystem(moveSystem);
            world.RegisterSystem(new SelectionSystem(_camera,_selectionMesh, _selectionMaterial, matrices, world.GetRepo<Selection>()));
            world.RegisterSystem(new DrawMeshesSystem<Selection>(matrices, world.GetRepo<Selection>(), _selectionMaterial, _selectionMesh));
            world.RegisterSystem(drawSystem);
            world.RegisterSystem(new RightClickMoveSystem(_camera, world.GetRepo< Selection>(), world.GetRepo<MoveComponent>()));
            
        }
    }
    
}