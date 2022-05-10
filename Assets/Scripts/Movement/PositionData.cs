using UnityEngine;

namespace Fleetio.ECS
{
    public struct PositionData
    {
        public Matrix4x4 objectToWorld;
        //these two fields should be -1 or Unity will be throwing internal exceptions every frame and allocate garbage
        public int prevObjectToWorld;
        public int renderingLayerMask;

        public PositionData(Matrix4x4 m)
        {
            objectToWorld = m;
            renderingLayerMask = -1;
            prevObjectToWorld = -1;
        }
    }
}