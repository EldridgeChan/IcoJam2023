using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutDefendsiveNode : AITreeNode
{
    public ScoutDefendsiveNode()
    {
        nextNodes = new List<AITreeNode>();
        nextNodes.Add(new DodgeScoutNode());
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
