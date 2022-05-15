using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fleetio.ECS
{
    public class WorldRunner : MonoBehaviour
    {
        [SerializeField] private int _initialEntitiesAllocation = 32768;
        [SerializeField] private List<Context> _contexts;

        private World _world;

        private void Awake()
        {
            _world = new World(_initialEntitiesAllocation);
            foreach (var context in _contexts)
            {
                context.Install(_world);
            }
        }
        
        private void Update()
        {
            _world.Run();
        }
        
        private void OnDestroy()
        {
            _world.Dispose();
        }
    }
}