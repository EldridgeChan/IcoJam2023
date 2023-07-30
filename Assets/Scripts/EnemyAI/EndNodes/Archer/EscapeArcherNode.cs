using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeArcherNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        PawnBehaviour target = GameManager.Instance.AITree.closestPawn(selfPawn, opponentPawns);
        Vector2 targetDir = (target.PawnRig.position - selfPawn.PawnRig.position).normalized;
        Vector2 movePos;
        Vector2 attackPos = Vector2.zero;
        int turnDir = Random.Range(0, 2);
        targetDir = (turnDir > 0 ? -1.0f : 1.0f) * new Vector2(-targetDir.y, targetDir.x);
        movePos = targetDir * Random.Range(GameManager.Instance.GameDesignScriptObj.ArcherMinMove, 1.0f) * selfPawn.ClassScriptObj.MoveDistance + selfPawn.PawnRig.position;
        if ((selfPawn as ArcherBehaviour).ArrowLoaded)
        {
            attackPos = selfPawn.PawnRig.position + new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)).normalized * GameManager.Instance.GameDesignScriptObj.AttackRangeOffset;
        }
        GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(movePos, attackPos, false));
        //Debug.Log(selfPawn.name + " Runs EscapeArcherNode for Target: " + target);
        return true;
    }
}
