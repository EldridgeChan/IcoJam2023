using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretKnightNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        PawnBehaviour target = GameManager.Instance.AITree.closestPawn(selfPawn, opponentPawns);
        GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(selfPawn.PawnRig.position, target.PawnRig.position, true));
        //Debug.Log(selfPawn.name + " Runs TurretKnightNode for Target: " + target);
        return true;
    }
}
