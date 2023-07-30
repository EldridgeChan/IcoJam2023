using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankKnightNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        PawnBehaviour target = GameManager.Instance.AITree.farthestPawn(selfPawn, opponentPawns);
        GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(target.PawnRig.position, target.PawnRig.position, true));
        //Debug.Log(selfPawn.name + " Runs TankKnightNode for Target: " + target);
        return true;
    }
}
