using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAggressiveNode : AITreeNode
{
    public KnightAggressiveNode()
    {
        nextNodes = new List<AITreeNode>();
        nextNodes.Add(new StepSwingKnightNode());
    }

    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        foreach (AITreeNode node in nextNodes)
        {
            if (node.runAITree(selfPawn, selfPawns, opponentPawns))
            {
                return true;
            }
        }
        return false;
    }
}
