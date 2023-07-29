using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBehaviour : MonoBehaviour
{
    [SerializeField]
    private ClassAttributesScriptableObject classScriptObj;
    [SerializeField]
    private Rigidbody2D pawnRig;
    [SerializeField]
    private Animator pawnAnmt;
    public Animator PawnAnmt { get { return pawnAnmt; } }

    private bool hasAttack = false;
    public bool HasAttack { get { return hasAttack; } }
    private bool hasMove = false;
    public bool HasMove { get { return hasMove; }  set { hasMove = value; } }
    private bool hasTurn = false;
    public bool HasTurn { get { return hasTurn; } set { hasTurn = value; } }

    private int currHealth;
    private float moveT = 0.0f;
    private ActionPhases currPhase;
    private bool isInAction = false;
    private Vector2 orgPos = Vector2.zero;
    private Vector2 movePos = Vector2.zero;
    private Vector2 orgDir = Vector2.zero;
    private Vector2 attackDir = Vector2.zero;

    private void Awake()
    {
        if (!pawnAnmt) { pawnAnmt = GetComponent<Animator>(); }
        if (!pawnRig) { pawnRig = GetComponent<Rigidbody2D>(); }
    }

    private void Start()
    {
        init();
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
                if (hasMove)
                {
                    turnPawn(moveT);
                }
                break;
            case ActionPhases.MovingPhase:
                if (hasMove)
                {
                    movePawn(moveT);
                }
                break;
            case ActionPhases.AttackTurnPhase:
                if (hasAttack || hasTurn)
                {
                    turnPawn(moveT, false);
                }
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
                movePos = pawnRig.position;
                hasMove = false;
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

        if (phase == ActionPhases.MovingPhase && hasMove)
        {
            pawnAnmt.SetBool("Walking", true);
        }
    }

    public virtual void attack()
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

    public virtual void movePawn(float t)
    {
        pawnRig.position = Vector2.Lerp(orgPos, movePos, t);
    }

    public virtual void turnPawn(float t, bool isMove = true)
    {
        float startRotation = (orgDir.y < 0.0f ? -1.0f : 1.0f) * Mathf.Acos(orgDir.x) * Mathf.Rad2Deg;
        float endRotation = isMove ? ((movePos - orgPos).y < 0.0f ? -1.0f : 1.0f) * Mathf.Acos((movePos - orgPos).normalized.x) * Mathf.Rad2Deg : (attackDir.y < 0.0f ? -1.0f : 1.0f) * Mathf.Acos(attackDir.x) * Mathf.Rad2Deg;
        pawnRig.rotation = Mathf.Lerp(startRotation, endRotation, t);
    }

    public virtual void setMovePos(Vector2 mousePos)
    {
        hasMove = true;
        movePos = Vector2.Distance(pawnRig.position, mousePos) > classScriptObj.MoveDistance ? ((mousePos - pawnRig.position).normalized * classScriptObj.MoveDistance) + pawnRig.position : mousePos;
        GameManager.Instance.InterMan.InMoveControl = false;
        Debug.Log(gameObject.name + " Set Move Position at " + movePos);
    }

    public virtual void setAttackDir(Vector2 mousePos, bool justTurn = false)
    {
        hasAttack = !justTurn;
        hasTurn = justTurn;
        attackDir = (mousePos - movePos).normalized;
        GameManager.Instance.InterMan.InAttackControl = false;
        GameManager.Instance.InterMan.InTurnControl = false;
        Debug.Log(gameObject.name + " Set attack Direction at " + attackDir);
    }

    public void attacked(int damage)
    {
        currHealth -= damage;
        Debug.Log(name + " Taked " + damage + " Damage. Current Health: " + currHealth);
        if (currHealth <= 0)
        {
            die();
        }
    }

    public virtual int attackPower(PawnBehaviour attacker = null, PawnBehaviour target = null)
    {
        return classScriptObj.AttackPower;
    }

    public void die()
    {
        Debug.Log(name + "Has Died.");
        GameManager.Instance.InterMan.removePawn(this);
        //Destroy(gameObject);
    }

    public void init()
    {
        currHealth = classScriptObj.MaxHealth;
        orgPos = pawnRig.position;
        movePos = orgPos;
        orgDir = new Vector2(Mathf.Cos(pawnRig.rotation * Mathf.Deg2Rad), Mathf.Sin(pawnRig.rotation * Mathf.Deg2Rad));
    }
}
