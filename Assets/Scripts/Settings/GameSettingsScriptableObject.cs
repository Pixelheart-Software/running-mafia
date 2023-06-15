using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 2)]
public class GameSettingsScriptableObject : ScriptableObject
{
    public float highObstacleElevation = 2.50F;
    public float lowObstacleElevation = 1.25F;
    
}
