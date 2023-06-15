using UnityEngine;

namespace DefaultNamespace
{
    public abstract class PositionableScriptableObject : ScriptableObject
    {
        public abstract float Elevation { get; }
    }
}