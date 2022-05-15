using Fleetio.ECS;
using Fleetio.Movement;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fleetio.Input
{
    public class RightClickMoveSystem : ISystem
    {
        private readonly Camera _camera;
        private readonly ComponentsList<Selection> _selectionFilter;

        private ComponentsList<MoveComponent> _movingFilter;
        
        public RightClickMoveSystem(Camera camera, ComponentsList<Selection> selectionFilter,
            ComponentsList<MoveComponent> movingFilter)
        {
            _camera = camera;
            _selectionFilter = selectionFilter;
            _movingFilter = movingFilter;
        }

        public void Run()
        {
            if (!Mouse.current.rightButton.isPressed)
            {
                return;
            }
            var mousePosition = Mouse.current.position.ReadValue();
            if (Physics.Raycast(_camera.ScreenPointToRay(mousePosition), out var hit))
            {
                var filter = new Filter<Selection, MoveComponent>(_selectionFilter.Map, _movingFilter.Map,
                    Allocator.TempJob);
                foreach (var entity in filter.GetArray())
                {
                    _movingFilter.Set(entity.Id, new MoveComponent
                    {
                        Target = hit.point, Speed = entity.Second.Speed, RotationSpeed = entity.Second.RotationSpeed
                    });    
                }
                    
                filter.Dispose();
            }


        }
    }
}