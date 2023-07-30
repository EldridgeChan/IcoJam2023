using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnippingArcherNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        List<PawnBehaviour> targetList; 
        targetList = GameManager.Instance.AITree.allArcher(opponentPawns);
        if (targetList.Count <= 0)
        {
            targetList = GameManager.Instance.AITree.allScout(opponentPawns);
        }
        if (targetList.Count <= 0)
        {
            targetList = GameManager.Instance.AITree.allKnight(opponentPawns);
        }
        if (targetList.Count <= 0)
        {
            Debug.Log("ERROR: No Enemy Found");
            return false;
        }
        PawnBehaviour target = GameManager.Instance.AITree.farthestPawn(selfPawn, targetList);
        GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(selfPawn.PawnRig.position, target.PawnRig.position, false));
        //Debug.Log(selfPawn.name + " Runs SnippingArcherNode for Target: " + target);
        return true;
    }
}
