using DefaultNamespace;
using UnityEngine;

namespace Backwall
{
    [CreateAssetMenu(fileName = "Backwall", menuName = "ScriptableObjects/Backwall", order = 2)]
    public class BackwallScriptableObject : PositionableScriptableObject
    {
        public override float Elevation => 0.0F;

        public override bool IsFloor => false;
    }
}