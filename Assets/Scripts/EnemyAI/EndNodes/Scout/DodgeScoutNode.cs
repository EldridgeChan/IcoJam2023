using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeScoutNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        PawnBehaviour target;
        List<PawnBehaviour> chooseFrom = GameManager.Instance.AITree.allInRange(selfPawn, opponentPawns);
        if (chooseFrom.Count < 0)
        {
            chooseFrom = opponentPawns;
        }
        target = chooseFrom[Random.Range(0, chooseFrom.Count)];
        Vector2 targetDir = (target.PawnRig.position - selfPawn.PawnRig.position).normalized;
        Vector2 movePos = ((Random.Range(0, 2) > 0 ? -1.0f : 1.0f) * new Vector2(-targetDir.y, targetDir.x) * Mathf.Tan(GameManager.Instance.GameDesignScriptObj.dodgeAngle * Mathf.Deg2Rad) + targetDir).normalized * selfPawn.ClassScriptObj.MoveDistance * Random.Range(GameManager.Instance.GameDesignScriptObj.ScoutMinMove, 1.0f);
        GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(movePos, target.PawnRig.position, true));
        //Debug.Log(selfPawn.name + " Runs DodgeScoutNode for Target: " + target);
        return true;
    }
}
