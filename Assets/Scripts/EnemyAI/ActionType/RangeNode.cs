using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : AITreeNode
{
    public RangeNode()
    {
        nextNodes = new List<AITreeNode>();
        nextNodes.Add(new ArcherRangeNode());
        nextNodes.Add(new KnightRangeNode());
        nextNodes.Add(new ScoutRangeNode());
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
