using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadArcherNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        if (!(selfPawn as ArcherBehaviour).ArrowLoaded)
        {
            GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(selfPawn.PawnRig.position, selfPawn.PawnRig.position + Vector2.right, false));
        }
        return !(selfPawn as ArcherBehaviour).ArrowLoaded;
    }
}
