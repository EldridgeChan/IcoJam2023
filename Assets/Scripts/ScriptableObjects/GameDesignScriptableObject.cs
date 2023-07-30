using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "GameDesignScriptableObject", menuName = "ScriptableObjects/GameDesign")]
public class GameDesignScriptableObject : ScriptableObject
{
    [Header("Pawn Attributes")]
    [Header("Prefabs")]
    public GameObject ArcherPrefab;
    public GameObject KnightPrefab;
    public GameObject ScoutPrefab;
    [Header("Archer")]
    public float ArrowShotTimeOffset = 1.0f;
    public float ArrowSpeed = 1.0f;
    public float ArrowMaxFlyTime = 8.0f;
    public GameObject ArrowPrefab;
    [Header("Knight")]
    public float KnightStrikeStartTimeOffset = 1.0f;
    public float KnightStrikeEndTimeOffset = 3.0f;
    [Header("Scout")]
    public float ScoutStrikeStartTimeOffset = 1.0f;
    public float ScoutStrikeEndTimeOffset = 2.0f;
    public float ScoutCritAngleTolerant = 45.0f;
    public int ScoutCritDamageMultiplier = 2;

    [Header("Enemy Attributes")]
    [Header("Prefabs")]
    public GameObject ArcherEnemyPrefab;
    public GameObject KnightEnemyPrefab;
    public GameObject ScoutEnemyPrefab;
    [Header("AI")]
    public float attackRangeOffset = 1.0f;
}
