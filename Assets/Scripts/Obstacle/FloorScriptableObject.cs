using DefaultNamespace;
using UnityEngine;

namespace Obstacle
{
    [CreateAssetMenu(fileName = "Floor", menuName = "ScriptableObjects/Floor", order = 2)]
    public class FloorScriptableObject : PositionableScriptableObject
    {
        public override float Elevation => 0.0F;

        public override bool IsFloor => true;
    }
}