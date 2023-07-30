using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretArcherNode : AITreeNode
{
    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        int selfIndex = selfPawns.IndexOf(selfPawn);
        List<PawnBehaviour> beforePawns = new List<PawnBehaviour>();
        for (int i = 0; i < selfIndex; i++)
        {
            beforePawns.Add(selfPawns[i]);
        }

        beforePawns = GameManager.Instance.AITree.allKnight(beforePawns);
        List<Vector2> knightMoves = friendIndex(beforePawns, selfPawns);
        int friend = 0;
        Vector2 movePos;
        Vector2 attackPos;
        if (knightMoves != null)
        {
            friend = closestFriend(selfPawn, knightMoves);
        }
        if (knightMoves != null && (Vector2.Distance(selfPawn.PawnRig.position, knightMoves[friend]) < selfPawn.ClassScriptObj.MoveDistance + GameManager.Instance.GameDesignScriptObj.AttackRangeOffset))
        {
            movePos = (knightMoves[friend] - selfPawn.PawnRig.position) + ((selfPawn.PawnRig.position - knightMoves[friend]).normalized * GameManager.Instance.GameDesignScriptObj.AttackRangeOffset) + selfPawn.PawnRig.position;
            attackPos = GameManager.Instance.AITree.farthestPawn(selfPawn, opponentPawns).PawnRig.position;
            GameManager.Instance.InterMan.setAIAction(new AITreeHead.PawnAction(movePos, attackPos, false));
            //Debug.Log(selfPawn.name + " Runs TurretArcherNode for Target: " + knightMoves[friend]);
            return true;
        }
        return false;
    }

    private Vector2 closerPos(PawnBehaviour selfPawn, Vector2 pos1, Vector2 pos2)
    {
        return Vector2.Distance(selfPawn.PawnRig.position, pos1) <= Vector2.Distance(selfPawn.PawnRig.position, pos2) ? pos1 : pos2;
    }

    private List<Vector2> friendIndex(List<PawnBehaviour> selectedList, List<PawnBehaviour> selfPawns)
    {
        if (selectedList.Count <= 0)
        {
            return null;
        }
        List<Vector2> result = new List<Vector2>();
        foreach (PawnBehaviour pawn in selectedList)
        {
            result.Add(GameManager.Instance.AITree.PawnAIAction[selfPawns.IndexOf(pawn)].movePos);
        }
        return result;
    }

    private int closestFriend(PawnBehaviour selfPawn, List<Vector2> moves)
    {
        float distance = float.MaxValue;
        int result = 0;
        for (int i = 0; i < moves.Count; i++)
        {
            if (Vector2.Distance(selfPawn.PawnRig.position, moves[i]) < distance)
            {
                distance = Vector2.Distance(selfPawn.PawnRig.position, moves[i]);
                result = i;
            }
        }
        return result;
    }
}
