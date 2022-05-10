using UnityEngine;

namespace Fleetio.ECS
{
    public abstract class Context : MonoBehaviour
    {
        public abstract void Install(World world);
    }
}