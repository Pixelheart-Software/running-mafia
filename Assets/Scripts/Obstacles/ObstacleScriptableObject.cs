using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObjects/Obstacle", order = 1)]
public class ObstacleScriptableObject : ScriptableObject
{
    public ObstacleType type;
}

public enum ObstacleType
{
    HIGH, LOW
}
