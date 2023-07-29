using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "GameDesignScriptableObject", menuName = "ScriptableObjects/GameDesign")]
public class GameDesignScriptableObject : ScriptableObject
{
    public float ArrowShotTimeOffset = 1.0f;
    public float ArrowSpeed = 1.0f;
    public float ArrowMaxFlyTime = 8.0f;
    public GameObject ArrowPrefab;
    public float KnightStrikeStartTimeOffset = 1.0f;
    public float KnightStrikeEndTimeOffset = 3.0f;
    public float ScoutStrikeStartTimeOffset = 1.0f;
    public float ScoutStrikeEndTimeOffset = 2.0f;
    public float ScoutCritAngleTolerant = 45.0f;
    public int ScoutCritDamageMultiplier = 2;
}
