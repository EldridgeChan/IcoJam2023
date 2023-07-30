using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAggressiveNode : AITreeNode
{
    public ArcherAggressiveNode()
    {
        nextNodes = new List<AITreeNode>();
        nextNodes.Add(new ReloadArcherNode());
        nextNodes.Add(new SnippingArcherNode());
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
