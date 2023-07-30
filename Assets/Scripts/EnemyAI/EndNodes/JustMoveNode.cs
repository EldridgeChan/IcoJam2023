using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustMoveNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        if (selfPawn is ArcherBehaviour)
        {
            GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(selfPawn.PawnRig.position, (opponentPawns[Random.Range(0, opponentPawns.Count)].transform.position - selfPawn.transform.position).normalized, false));
        }
        else if (selfPawn is KnightBehaviour)
        {
            PawnBehaviour target = GameManager.Instance.AITree.closestPawn(selfPawn, opponentPawns);
            Vector2 movePos = target.faceDir() * GameManager.Instance.GameDesignScriptObj.attackRangeOffset + target.PawnRig.position;
            Vector2 attackDir = target.PawnRig.position;
            bool justTurn = !(Vector2.Distance(selfPawn.PawnRig.position, target.PawnRig.position) < selfPawn.ClassScriptObj.MoveDistance + GameManager.Instance.GameDesignScriptObj.attackRangeOffset);
            GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(movePos, attackDir, justTurn));
        }
        else if (selfPawn is ScoutBehaviour)
        {
            PawnBehaviour target = GameManager.Instance.AITree.closestPawn(selfPawn, opponentPawns);
            Vector2 movePos = -target.faceDir() * GameManager.Instance.GameDesignScriptObj.attackRangeOffset + target.PawnRig.position;
            Vector2 attackDir = target.PawnRig.position;
            bool justTurn = !(Vector2.Distance(selfPawn.PawnRig.position, target.PawnRig.position) < selfPawn.ClassScriptObj.MoveDistance + GameManager.Instance.GameDesignScriptObj.attackRangeOffset);
            GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(movePos, attackDir, justTurn));
        }
        else
        {
            Debug.Log("ERROR: Unreconized Class of Pawn: " + selfPawn.name);
            return false;
        }
        return true;
    }
}
