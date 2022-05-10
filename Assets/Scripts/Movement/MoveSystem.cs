using Fleetio.ECS;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Burst;

namespace Fleetio.Movement
{
    public class MoveSystem : ISystem, IJobDependency
    {
        public JobHandle JobHandle { get; private set; }
        private readonly ComponentsList<PositionData> _positionsRepo;
        private readonly ComponentsList<MoveComponent> _movingFilter;

        public MoveSystem(ComponentsList<PositionData> positionsRepo, ComponentsList<MoveComponent> movingFilter)
        {
            _positionsRepo = positionsRepo;
            _movingFilter = movingFilter;
        }

        public void Run()
        {
            var job = new MoveJob
            {
                Positions = _positionsRepo,
                Moves = _movingFilter,
                DeltaTime = Time.deltaTime
            };
            
            JobHandle = job.Schedule(32768, 64);

        }
        
        [BurstCompile(CompileSynchronously = true)]
        private struct MoveJob : IJobParallelFor
        {
            public ComponentsList<PositionData> Positions; 
            
            [ReadOnly]
            public ComponentsList<MoveComponent> Moves; 
            [ReadOnly]
            public float DeltaTime;
        
            public void Execute(int index)
            {
                if (!Positions.TryGet(index, out var c) || !Moves.TryGet(index, out var moveComponent))
                {
                    return;    
                }
                
                var position = c.objectToWorld.GetPosition();
                var target = moveComponent.Target;
                var rotation = c.objectToWorld.rotation;
                var speed = moveComponent.Speed;
                var rotationSpeed = moveComponent.RotationSpeed;

                var angleDelta = Vector3.SignedAngle(rotation * Vector3.forward, target - position, Vector3.up) * DeltaTime;
            
                var rotSpeed = Mathf.Sign(angleDelta) * Mathf.Min(Mathf.Abs(angleDelta),rotationSpeed);
                rotation = Quaternion.Euler(0,  rotSpeed, 0) * rotation;
                position += rotation * (speed * Vector3.forward * DeltaTime);
                c.objectToWorld = Matrix4x4.TRS(position, rotation, Vector3.one);
                Positions.Set(index, c);
            }
        }

    }
}