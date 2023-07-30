using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    private PawnBehaviour weaponParent;

    private List<Transform> hitedTransforms = new List<Transform>();
    private bool getBlocked = false;
    private bool attacking = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hitedTransforms.Add(collision.transform);
        if (attacking && collision.CompareTag("Shield"))
        {
            getBlocked = true;
        }
        if (attacking && !getBlocked && (collision.CompareTag("PlayerPawn") || collision.CompareTag("EnemyPawn")) && collision.transform != weaponParent.transform)
        {
            attacked(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hitedTransforms.Remove(collision.transform);
    }

    public void strikeStart()
    {
        attacking = true;
        List<Transform> hittedPawn = new List<Transform>();
        foreach (Transform trans in hitedTransforms)
        {
            if (trans)
            {
                if (trans.CompareTag("Shield"))
                {
                    getBlocked = true;
                }
                else if (trans.CompareTag("PlayerPawn") || trans.CompareTag("EnemyPawn"))
                {
                    hittedPawn.Add(trans);
                }
            }
        }
        if (!getBlocked)
        {
            foreach (Transform pawn in hittedPawn)
            {
                if (pawn != weaponParent.transform)
                {
                    attacked(pawn);
                }
            }
        }
    }

    private void attacked(Transform pawnTrans)
    {
        PawnBehaviour target = pawnTrans.GetComponent<PawnBehaviour>();
        target.attacked(weaponParent.attackPower(weaponParent, target));
    }

    public void strikeEnd()
    {
        attacking = false;
        getBlocked = false;
    }
}
