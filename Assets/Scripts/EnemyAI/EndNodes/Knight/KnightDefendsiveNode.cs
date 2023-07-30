using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDefendsiveNode : AITreeNode
{
    public KnightDefendsiveNode()
    {
        nextNodes = new List<AITreeNode>();
        nextNodes.Add(new BackAttackKnightNode());
        nextNodes.Add(new TurretKnightNode());
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
