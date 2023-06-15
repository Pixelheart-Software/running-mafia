using DefaultNamespace;
using UnityEngine;

namespace Obstacle
{
    [CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObjects/Obstacle", order = 1)]
    public class ObstacleScriptableObject : PositionableScriptableObject
    {
        public ObstacleType type;
    
        public override float Elevation => type == ObstacleType.HIGH ? 2.50F : 1.25F;
    }

    public enum ObstacleType
    {
        HIGH, LOW
    }
}