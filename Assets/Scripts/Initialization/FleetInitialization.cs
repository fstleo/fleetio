using Fleetio.Core;
using Fleetio.ECS;
using UnityEngine;

namespace Fleetio.Initialization
{
    public class FleetInitialization : Context
    {
        
        [SerializeField] private FleetSettingsObject _settingsObject;

        [SerializeField] private int _fleetSize = 8192; 
        
        public override void Install(World world)
        {
            var fleetFactory = new FleetFactory(_settingsObject.Settings, world);
            for (var i = 0; i < _fleetSize; i++)
            {
                fleetFactory.Create();
            }
        }
    }
}