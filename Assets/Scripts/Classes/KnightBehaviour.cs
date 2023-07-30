using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBehaviour : PawnBehaviour
{
    [SerializeField]
    private MeleeWeaponBehaviour meleeWeapon;

    private void Start()
    {
        init();
    }

    public override void attack()
    {
        if (HasTurn)
        {
            Debug.Log("Has Turn");
            PawnAnmt.SetTrigger("Block");
            HasTurn = false;
        }
        else if (HasAttack)
        {
            Invoke("strikeStart", GameManager.Instance.GameDesignScriptObj.KnightStrikeStartTimeOffset);
            Invoke("strikeEnd", GameManager.Instance.GameDesignScriptObj.KnightStrikeEndTimeOffset);
        }
        base.attack();
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
