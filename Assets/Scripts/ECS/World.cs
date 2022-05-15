using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.Collections;

namespace Fleetio.ECS
{

#if DEBUG
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
#endif
    public class World : IDisposable
    {

        private readonly Dictionary<Type, object> _componentsLists = new(64);

        public NativeHashSet<int> Entities;

        private Stack<int> _freeEntities;

        private List<ISystem> _systems = new(32);

        public int EntitiesCount => Entities.Count();

        private int _currentEntitiesCapacity = 0;

        public World(int initialCapacity)
        {
            Entities = new NativeHashSet<int>(initialCapacity, Allocator.Persistent);
            _freeEntities = new Stack<int>(initialCapacity);
            AllocateFreeEntities(initialCapacity);
        }

        public void RegisterSystem(ISystem system)
        {
            _systems.Add(system);
        }

        public void Run()
        {
            foreach (var system in _systems)
            {
                system.Run();
            }
        }
        
        private void AllocateFreeEntities(int to)
        {
            for (int i = to - 1; i >= _currentEntitiesCapacity; i--)
            {
                _freeEntities.Push(i);
            }

            _currentEntitiesCapacity = to;
        }

        public int CreateEntity()
        {
            if (_freeEntities.Count == 0)
            {
                AllocateFreeEntities(_currentEntitiesCapacity * 2);
            }
            var id = _freeEntities.Pop();
            Entities.Add(id);
            
            return id;
        }

        public void DestroyEntity(int entityId)
        {
            foreach (var componentsList in _componentsLists.Values)
            {
                ((IComponentsList)componentsList).RemoveAt(entityId);
            }
            _freeEntities.Push(entityId);
            Entities.Remove(entityId);
        }

        public ComponentsList<T> GetRepo<T>() where T : unmanaged
        {
            var type = typeof(T);
            if (_componentsLists.TryGetValue(type, out var repo))
            {
                return (ComponentsList<T>)repo;
            }

            var repoT = new ComponentsList<T>(_currentEntitiesCapacity, Allocator.Persistent);
            _componentsLists.Add(type, repoT);
            return repoT;
        }

        public void Dispose()
        {
            foreach (var d in _componentsLists.Values)
            {
                (d as IDisposable).Dispose();
            }

            Entities.Dispose();
        }
    }

}