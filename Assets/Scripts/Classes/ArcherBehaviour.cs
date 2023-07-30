using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBehaviour : PawnBehaviour
{
    [SerializeField]
    private LineRenderer archerLineRend;
    [SerializeField]
    private Transform[] cordPointTrans;
    [SerializeField]
    private Transform fakeArrowTrans;

    private bool arrowLoaded = true;
    public bool ArrowLoaded { get { return arrowLoaded; } }

    private void Start()
    {
        if (!archerLineRend) { archerLineRend = GetComponent<LineRenderer>(); }
        init();
    }

    private void Update()
    {
        setCordLine();
    }

    public override void attack()
    {

        PawnAnmt.SetBool("Loaded", arrowLoaded);
        if (HasAttack)
        {
            if (arrowLoaded)
            {
                Invoke("spawnArrow", GameManager.Instance.GameDesignScriptObj.ArrowShotTimeOffset);
            }
            arrowLoaded = !arrowLoaded;
        }
        base.attack();
    }

    private void spawnArrow()
    {
        Instantiate(GameManager.Instance.GameDesignScriptObj.ArrowPrefab, fakeArrowTrans.position, fakeArrowTrans.rotation).GetComponent<ArrowBehaviour>().init(attackPower());
    }

    public override void movePawn(float t)
    {
        if (arrowLoaded || !HasAttack)
        {
            base.movePawn(t);
        }
    }

    public override void turnPawn(float t, bool isMove = true)
    {
        if (arrowLoaded || !HasAttack)
        {
            base.turnPawn(t, isMove);
        }
    }

    public override void setAttackDir(Vector2 mousePos, bool justTurn = false)
    {
        base.setAttackDir(mousePos, justTurn);
        if (!justTurn && !arrowLoaded)
        {
            HasMove = false;
        }
    }

    private void setCordLine()
    {
        Vector3[] points = new Vector3[3];
        for (int i = 0; i < cordPointTrans.Length; i++)
        {
            points[i] = cordPointTrans[i].position;
        }
        archerLineRend.SetPositions(points);
    }
}
