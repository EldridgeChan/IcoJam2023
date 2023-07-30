using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSwingKnightNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        PawnBehaviour target = GameManager.Instance.AITree.closestPawn(selfPawn, opponentPawns);
        Vector2 movePos = selfPawn.PawnRig.position + (target.PawnRig.position - selfPawn.PawnRig.position) * Random.Range(0.0f, GameManager.Instance.GameDesignScriptObj.KnightMaxMove);
        Vector2 AttackPos = AttackPos = target.PawnRig.position;
        GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(movePos, AttackPos, Vector2.Distance(selfPawn.PawnRig.position, target.PawnRig.position) > GameManager.Instance.GameDesignScriptObj.AttackDefendRangeMul * (selfPawn.ClassScriptObj.MoveDistance + GameManager.Instance.GameDesignScriptObj.AttackRangeOffset)));
        //Debug.Log(selfPawn.name + " Runs StepSwingKnightNode for Target: " + target);
        return true;
    }
}
