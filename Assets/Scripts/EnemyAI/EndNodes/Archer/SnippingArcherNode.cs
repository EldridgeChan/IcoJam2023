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
        PawnBehaviour target = GameManager.Instance.AITree.farthestPawn(selfPawn, targetList);

        return false;
    }
}
