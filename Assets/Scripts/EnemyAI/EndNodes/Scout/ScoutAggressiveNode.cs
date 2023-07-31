using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutAggressiveNode : AITreeNode
{
    public ScoutAggressiveNode()
    {
        nextNodes = new List<AITreeNode>();
        nextNodes.Add(new ChaseArcherScoutNode());
        nextNodes.Add(new BackStabScoutNode());
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
