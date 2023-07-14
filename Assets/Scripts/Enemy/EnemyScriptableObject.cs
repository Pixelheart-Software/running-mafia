using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
    public class EnemyScriptableObject : PositionableScriptableObject
    {
        [SerializeField]
        private float elevation;

        public override float Elevation => elevation;

        public override bool IsFloor => false;
    }
}