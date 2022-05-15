using Fleetio.ECS;
using Fleetio.Initialization;
using Fleetio.Movement;
using Unity.Collections;
using UnityEngine;

namespace Fleetio.Core
{
    public class FleetFactory : IFactory<int>
    {
        private readonly FleetSettings _settings;
        private readonly World _world;

        private ComponentsList<PositionData> _positionsRepo;
        private ComponentsList<MoveComponent> _moveRepo;
        private ComponentsList<Fleet> _fleetMarkers;

        public FleetFactory(FleetSettings settings, World world)
        {
            _settings = settings;
            _world = world;
            _positionsRepo = world.GetRepo<PositionData>();
            _moveRepo = world.GetRepo<MoveComponent>();
            _fleetMarkers = world.GetRepo<Fleet>();
        }

        public int Create()
        {
            var id = _world.CreateEntity();
            var forward = Random.insideUnitCircle;
            var forwardV = new Vector3(forward.x, 0, forward.y);
            var position = Random.insideUnitCircle;
            _positionsRepo.Set(id, new PositionData(Matrix4x4.TRS(new Vector3(position.x, 0, position.y),
                Quaternion.Euler(0, Vector3.Angle(forwardV, Vector3.forward), 0), Vector3.one)));
            
            _moveRepo.Set(id, new MoveComponent
            {
                RotationSpeed = _settings.RotationSpeed,
                Speed = _settings.MovementSpeed,
                Target = new Vector3(4,0,4)
            });
            _fleetMarkers.Set(id, new Fleet());
            return id;
        }
    }

}