using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAttackKnightNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        PawnBehaviour target = GameManager.Instance.AITree.closestPawn(selfPawn, opponentPawns);
        if (target is ScoutBehaviour)
        {
            Vector2 movePos = (target.PawnRig.position - selfPawn.PawnRig.position).normalized * selfPawn.ClassScriptObj.MoveDistance * GameManager.Instance.GameDesignScriptObj.BackAttackMaxMove + selfPawn.PawnRig.position;
            Vector2 attackPos = Vector2.Distance(selfPawn.PawnRig.position, target.PawnRig.position) < selfPawn.ClassScriptObj.MoveDistance * GameManager.Instance.GameDesignScriptObj.BackAttackRangeMul ? selfPawn.PawnRig.position - target.PawnRig.position + selfPawn.PawnRig.position : target.PawnRig.position;
            bool defend = Vector2.Distance(selfPawn.PawnRig.position, target.PawnRig.position) >= selfPawn.ClassScriptObj.MoveDistance * GameManager.Instance.GameDesignScriptObj.BackAttackRangeMul;
            GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(movePos, attackPos, defend));
            //Debug.Log(selfPawn.name + " Runs BackAttackKnightNode for Target: " + target);
            return true;
        }
        return false;
    }
}
