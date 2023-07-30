using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeValueNode : AITreeNode
{
    private int attackValue = 0;
    private int defendValue = 0;
    private int rangeValue = 0;

    public RangeValueNode()
    {
        nextNodes = new List<AITreeNode>();
        nextNodes.Add(new AggressiveNode());
        nextNodes.Add(new DefendsiveNode());
        nextNodes.Add(new RangeNode());
        nextNodes.Add(new JustMoveNode());
    }

    public override bool runAITree(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        calculateValues(selfPawn, selfPawns, opponentPawns);

        int r = Random.Range(0, attackValue + defendValue + rangeValue);
        if (r < attackValue)
        {
            if (nextNodes[0].runAITree(selfPawn, selfPawns, opponentPawns))
            {
                return true;
            }
        }
        else if (r < attackValue + defendValue)
        {
            if (nextNodes[1].runAITree(selfPawn, selfPawns, opponentPawns))
            {
                return true;
            }
        }
        else if (r < attackValue + defendValue + rangeValue)
        {
            if (nextNodes[2].runAITree(selfPawn, selfPawns, opponentPawns))
            {
                return true;
            }
        }
        return nextNodes[3].runAITree(selfPawn, selfPawns, opponentPawns);
    }

    private void calculateValues(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        attackValue = calculateAttackValue(selfPawn, selfPawns, opponentPawns);
        defendValue = calculateDefendValue(selfPawn, selfPawns, opponentPawns);
        rangeValue = calculateRangeValue(selfPawn, selfPawns, opponentPawns);
    }

    private int calculateAttackValue(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        int result = 0;
        foreach (PawnBehaviour oppoPawn in opponentPawns)
        {
            if (Vector2.Distance(selfPawn.transform.position, oppoPawn.transform.position) < selfPawn.ClassScriptObj.MoveDistance + GameManager.Instance.GameDesignScriptObj.attackRangeOffset)
            {
                result++;
            }
        }
        return result;
    }

    private int calculateDefendValue(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        int result = 0;
        foreach (PawnBehaviour oppoPawn in opponentPawns)
        {
            if (Vector2.Distance(selfPawn.transform.position, oppoPawn.transform.position) < oppoPawn.ClassScriptObj.MoveDistance + GameManager.Instance.GameDesignScriptObj.attackRangeOffset)
            {
                result++;
            }
        }
        return result;
    }

    private int calculateRangeValue(PawnBehaviour selfPawn, List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        int result = 0;
        foreach (PawnBehaviour oppoPawn in opponentPawns)
        {
            if (oppoPawn is ArcherBehaviour)
            {
                result++;
            }
        }
        return result;
    }
}
