using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackStabScoutNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        List<PawnBehaviour> enemyInRange = GameManager.Instance.AITree.allInRange(selfPawn, opponentPawns);
        if (enemyInRange.Count <= 0)
        {
            return false;
        }
        PawnBehaviour target = enemyInRange[Random.Range(0, enemyInRange.Count)];

        if (target is ScoutBehaviour)
        {
            return false;
        }
        Vector2 movePos = Random.Range(0.0f, 1.0f) < GameManager.Instance.GameDesignScriptObj.AimBackProbability ? -target.faceDir() * GameManager.Instance.GameDesignScriptObj.AttackRangeOffset + target.PawnRig.position : (target.PawnRig.position - selfPawn.PawnRig.position) + ((selfPawn.PawnRig.position - target.PawnRig.position).normalized * GameManager.Instance.GameDesignScriptObj.AttackRangeOffset);
        Vector2 AttackPos = target.PawnRig.position;
        GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(movePos, AttackPos, false));
        //Debug.Log(selfPawn.name + " Runs BackStabScoutNode for Target: " + target);
        return true;
    }
}
