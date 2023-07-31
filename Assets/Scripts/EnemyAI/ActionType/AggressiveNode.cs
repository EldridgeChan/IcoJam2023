using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveNode : AITreeNode
{
    public AggressiveNode()
    {
        nextNodes = new List<AITreeNode>();
        nextNodes.Add(new ArcherAggressiveNode());
        nextNodes.Add(new KnightAggressiveNode());
        nextNodes.Add(new ScoutAggressiveNode());
    }

    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        if (selfPawn is ArcherBehaviour)
        {
            return nextNodes[0].runAITree(selfPawn, selfPawns, opponentPawns);
        }
        else if (selfPawn is KnightBehaviour)
        {
            return nextNodes[1].runAITree(selfPawn, selfPawns, opponentPawns);
        }
        else if (selfPawn is ScoutBehaviour)
        {
            return nextNodes[2].runAITree(selfPawn, selfPawns, opponentPawns);
        }
        else
        {
            Debug.Log("ERROR: Unreconized Class of Pawn: " + selfPawn.name);
            return false;
        }
    }
}
