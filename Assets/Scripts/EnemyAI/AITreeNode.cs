using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AITreeNode
{
    public List<AITreeNode> nextNodes;

    public abstract bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns);
}
