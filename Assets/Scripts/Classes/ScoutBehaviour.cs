using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutBehaviour : PawnBehaviour
{
    [SerializeField]
    private MeleeWeaponBehaviour meleeWeapon;

    void Start()
    {
        init();
    }

    public override void attack()
    {
        base.attack();
        Invoke("strikeStart", GameManager.Instance.GameDesignScriptObj.ScoutStrikeStartTimeOffset);
        Invoke("strikeEnd", GameManager.Instance.GameDesignScriptObj.ScoutStrikeEndTimeOffset);
    }

    public override int attackPower(PawnBehaviour attacker = null, PawnBehaviour target = null)
    {
        Vector2 targetBack = -new Vector2(Mathf.Cos(target.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(target.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
        Vector2 attackerDir = attacker.transform.position - target.transform.position;
        if (Vector2.Angle(targetBack, attackerDir) <= GameManager.Instance.GameDesignScriptObj.ScoutCritAngleTolerant)
        {
            return base.attackPower() * GameManager.Instance.GameDesignScriptObj.ScoutCritDamageMultiplier;
        }
        else
        {
            return base.attackPower();
        }
    }

    public void strikeStart()
    {
        meleeWeapon.strikeStart();
    }

    public void strikeEnd()
    {
        meleeWeapon.strikeEnd();
    }
}
