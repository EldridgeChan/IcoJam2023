using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBehaviour : MonoBehaviour
{
    [SerializeField]
    private ClassAttributesScriptableObject classScriptObj;
    [SerializeField]
    private Animator pawnAnmt;
    [SerializeField]
    private Rigidbody2D pawnRig;


    private int currHealth;
    private float moveT = 0.0f;
    private ActionPhases currPhase;
    private bool isInAction = false;
    private Vector2 orgPos = Vector2.zero;
    private Vector2 movePos = Vector2.zero;
    private Vector2 orgDir = Vector2.zero;
    private Vector2 attackDir = Vector2.zero;
    private bool hasAttack = false;

    private void Awake()
    {
        if (!pawnAnmt) { pawnAnmt = GetComponent<Animator>(); }
        if (!pawnRig) { pawnRig = GetComponent<Rigidbody2D>(); }
    }

    private void Start()
    {
        //meant to delete Just for testing purposes
        movePos = pawnRig.position;
        orgDir = new Vector2(Mathf.Cos(pawnRig.rotation * Mathf.Deg2Rad), Mathf.Sin(pawnRig.rotation * Mathf.Deg2Rad));
    }

    private void FixedUpdate()
    {
        if (isInAction)
        {
            phaseAction();
        }
    }

    private void phaseAction()
    {
        moveT = Mathf.Clamp01(moveT + Time.fixedDeltaTime);
        switch (currPhase)
        {
            case ActionPhases.MoveTurnPhase:
                turnPawn(moveT);
                break;
            case ActionPhases.MovingPhase:
                movePawn(moveT);
                break;
            case ActionPhases.AttackTurnPhase:
                turnPawn(moveT, false);
                break;
            case ActionPhases.AttackPhase:
                attack();
                moveT = 1.0f;
                break;
            default:
                Debug.Log("ERROR: Unexpected Phase: " + currPhase);
                break;
        }
        if (moveT >= 1.0f)
        {
            moveT = 0.0f;
            isInAction = false;
            if (currPhase == ActionPhases.MovingPhase)
            {
                pawnAnmt.SetBool("Walking", false);
            }

            if (currPhase != ActionPhases.AttackPhase)
            {
                GameManager.Instance.InterMan.actionFinishCallback();
            }
        }
    }

    public void phaseActionStart(ActionPhases phase)
    {
        orgPos = pawnRig.position;
        orgDir = new Vector2(Mathf.Cos(pawnRig.rotation * Mathf.Deg2Rad), Mathf.Sin(pawnRig.rotation * Mathf.Deg2Rad));
        currPhase = phase;
        isInAction = true;

        if (phase == ActionPhases.MovingPhase)
        {
            pawnAnmt.SetBool("Walking", true);
        }
    }

    virtual public void attack()
    {
        if (hasAttack)
        {
            pawnAnmt.SetTrigger("Attack");
            hasAttack = false;
        }
        Invoke("actionFinishCallback", 5.0f);
    }

    private void actionFinishCallback()
    {
        GameManager.Instance.InterMan.actionFinishCallback();
    }

    private void movePawn(float t)
    {
        pawnRig.position = Vector2.Lerp(orgPos, movePos, t);
    }

    private void turnPawn(float t, bool isMove = true)
    {
        float startRotation = (orgDir.y < 0.0f ? -1.0f : 1.0f) * Mathf.Acos(orgDir.x) * Mathf.Rad2Deg;
        float endRotation = isMove ? ((movePos - orgPos).y < 0.0f ? -1.0f : 1.0f) * Mathf.Acos((movePos - orgPos).normalized.x) * Mathf.Rad2Deg : (attackDir.y < 0.0f ? -1.0f : 1.0f) * Mathf.Acos(attackDir.x) * Mathf.Rad2Deg;
        Debug.Log(endRotation);
        pawnRig.rotation = Mathf.Lerp(startRotation, endRotation, t);
    }

    public void setMovePos(Vector2 mousePos)
    {
        movePos = mousePos;
        Debug.Log(gameObject.name + " Set Move Position at " + movePos);
    }

    public void setAttackDir(Vector2 mousePos)
    {
        hasAttack = true;
        attackDir = (mousePos - movePos).normalized;
        GameManager.Instance.InterMan.InAttackControl = false;
        Debug.Log(gameObject.name + " Set attack Direction at " + attackDir);
    }
}
