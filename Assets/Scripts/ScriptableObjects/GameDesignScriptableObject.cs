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
    public float AttackRangeOffset = 1.0f;
    public float ArcherMinMove = 0.8f;
    public float KnightMaxMove = 0.8f;
    public float AttackDefendRangeMul = 2.0f;
    public float BackAttackRangeMul = 1.5f;
    public float BackAttackMaxMove = 0.2f;
    public float AimBackProbability = 0.8f;
    public float dodgeAngle = 45.0f;
    public float ScoutMinMove = 0.8f;
    public float SneakAngle = 20.0f;

    [Header("Game Loop Attributes")]
    public float MoveTurnTimePeriod = 1.0f;
    public float MoveTimePeriod = 1.0f;
    public float AttackTurnTimePeriod = 1.0f;
    public float AttackTimePeriod = 4.5f;
    public float DieTimePeriod = 3.0f;
    public float ControlTimePeriod = 3.0f;
    public float AreanaWidth = 8.5f;
    public float AreanaHight = 4.5f;

    [Header("Spawn Attributes")]
    public int EnemySpawnMaxX = 8;
    public int EnemySpawnY = 4;
    public int MaxPawn = 6;
}
