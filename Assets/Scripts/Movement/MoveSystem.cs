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

        private ComponentsList<PositionData> _positionsRepo;

        private readonly ComponentsList<MoveComponent> _movingFilter;
        
        private NativeHashMap<int, PositionData>.ParallelWriter PositionsWriter;
        
        public MoveSystem(ComponentsList<PositionData> positionsRepo,
            ComponentsList<MoveComponent> movingFilter)
        {
            _positionsRepo = positionsRepo;
            _movingFilter = movingFilter;
            PositionsWriter = _positionsRepo.Map.AsParallelWriter();
        }

        public void Run()
        {
            var filter = new Filter<PositionData, MoveComponent>(_positionsRepo.Map, _movingFilter.Map, Allocator.TempJob);
            for (int i = 0; i < filter.Length; i++)
            {
                _positionsRepo.RemoveAt(i);
            }
            var job = new MoveJob
            {
                Positions = filter,
                PositionsWriter = PositionsWriter,
                DeltaTime = Time.deltaTime
            };
            
            JobHandle = job.Schedule(filter.Length, 64);
            
            JobHandle.Complete();
            filter.Dispose();
        }
        
        [BurstCompile(CompileSynchronously = true)]
        private struct MoveJob : IJobParallelFor
        {
            [ReadOnly]
            public Filter<PositionData, MoveComponent> Positions; 
            
            public NativeHashMap<int, PositionData>.ParallelWriter PositionsWriter; 

            [ReadOnly]
            public float DeltaTime;
        
            public void Execute(int index)
            {
                
                var movingData = Positions[index];
                var positionData = movingData.First;
                var move = movingData.Second;
                var position = positionData.objectToWorld.GetPosition();
                var target = move.Target;
                var rotation = positionData.objectToWorld.rotation;
                var speed = move.Speed;
                var rotationSpeed = move.RotationSpeed;

                var angleDelta = Vector3.SignedAngle(rotation * Vector3.forward, target - position, Vector3.up) * DeltaTime;
            
                var rotSpeed = Mathf.Sign(angleDelta) * Mathf.Min(Mathf.Abs(angleDelta),rotationSpeed);
                rotation = Quaternion.Euler(0,  rotSpeed, 0) * rotation;
                position += rotation * (speed * Vector3.forward * DeltaTime);
                positionData.objectToWorld = Matrix4x4.TRS(position, rotation, Vector3.one);
                PositionsWriter.TryAdd(movingData.Id,positionData);
            }
        }
    }
}