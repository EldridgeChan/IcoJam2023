using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseArcherScoutNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        PawnBehaviour target = null;
        List<PawnBehaviour> enemyInRange = GameManager.Instance.AITree.allInRange(selfPawn, opponentPawns);
        if (enemyInRange.Count <= 0)
        {
            return false;
        }
        enemyInRange = GameManager.Instance.AITree.allArcher(enemyInRange);
        if (enemyInRange.Count <= 0)
        {
            return false;
        }
        foreach (PawnBehaviour archer in enemyInRange)
        {
            if (!(archer as ArcherBehaviour).ArrowLoaded)
            {
                target = archer;
            }
        }
        if (!target)
        {
            return false;
        }
        
        Vector2 movePos = Random.Range(0.0f, 1.0f) < GameManager.Instance.GameDesignScriptObj.AimBackProbability ? -target.faceDir() * GameManager.Instance.GameDesignScriptObj.AttackRangeOffset + target.PawnRig.position : target.PawnRig.position + target.faceDir() * GameManager.Instance.GameDesignScriptObj.AttackRangeOffset;
        GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(movePos, target.PawnRig.position, false));
        //Debug.Log(selfPawn.name + " Runs ChaseArcherScoutNode for Target: " + target);
        return true;
    }
}
