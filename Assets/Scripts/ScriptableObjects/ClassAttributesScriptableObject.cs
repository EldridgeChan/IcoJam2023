using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClassAttributesScriptableObject", menuName = "ScriptableObjects/ClassAttributes")]
public class ClassAttributesScriptableObject : ScriptableObject
{
    public int MaxHealth = 0;
    public int AttackPower = 0;
    public float MoveDistance = 0.0f;
}
