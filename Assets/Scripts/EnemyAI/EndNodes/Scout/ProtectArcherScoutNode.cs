using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectArcherScoutNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        List<PawnBehaviour> chooseFrom = GameManager.Instance.AITree.allArcher(opponentPawns);
        if (chooseFrom.Count <= 0)
        {
            return false;
        }
        PawnBehaviour target = chooseFrom[Random.Range(0, chooseFrom.Count)];
        Vector2 targetDir = target.PawnRig.position - selfPawn.PawnRig.position;
        Vector2 movePos = ((Random.Range(0, 2) > 0 ? -1.0f : 1.0f) * new Vector2(-targetDir.y, targetDir.x) * Mathf.Tan(GameManager.Instance.GameDesignScriptObj.SneakAngle * Mathf.Deg2Rad) + targetDir).normalized * selfPawn.ClassScriptObj.MoveDistance;
        GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(movePos, target.PawnRig.position, Vector2.Distance(selfPawn.PawnRig.position, target.PawnRig.position) < selfPawn.ClassScriptObj.MoveDistance + GameManager.Instance.GameDesignScriptObj.AttackRangeOffset));
        //Debug.Log(selfPawn.name + " Runs ProtectArcherScoutNode for Target: " + target);
        return true;
    }
}
