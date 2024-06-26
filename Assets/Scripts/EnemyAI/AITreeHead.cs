using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITreeHead : MonoBehaviour
{
    private AITreeNode head;

    private void Start()
    {
        head = new RangeValueNode();
    }

    public void runAI(List<PawnBehaviour> selfPawns, List<PawnBehaviour> opponentPawns)
    {
        foreach (PawnBehaviour selfPawn in selfPawns)
        {
            if (!head.runAITree(selfPawn, selfPawns , opponentPawns))
            {
                Debug.Log("ERROR: AI Tree Not Returning true");
            }
        }
    }

    public struct PawnAction
    {
        public Vector2 movePos;
        public Vector2 attackDir;
        public bool justTurn;

        public PawnAction(Vector2 movePos, Vector2 attackDir, bool justTurn)
        {
            this.movePos = movePos;
            this.attackDir = attackDir;
            this.justTurn = justTurn;
        }
    }

    public PawnBehaviour closestPawn(PawnBehaviour self, List<PawnBehaviour> opponentPawns)
    {
        PawnBehaviour result = null;
        foreach (PawnBehaviour opponent in opponentPawns)
        {
            if (!result || Vector2.Distance(self.PawnRig.position, opponent.PawnRig.position) < Vector2.Distance(self.PawnRig.position, result.PawnRig.position))
            {
                result = opponent;
            }
        }
        return result;
    }

    public PawnBehaviour farthestPawn(PawnBehaviour self, List<PawnBehaviour> opponentPawns)
    {
        PawnBehaviour result = null;
        foreach (PawnBehaviour opponent in opponentPawns)
        {
            if (!result || Vector2.Distance(self.PawnRig.position, opponent.PawnRig.position) > Vector2.Distance(self.PawnRig.position, result.PawnRig.position))
            {
                result = opponent;
            }
        }
        return result;
    }

    public List<PawnBehaviour> allArcher(List<PawnBehaviour> opponentPawn)
    {
        List<PawnBehaviour> result = new List<PawnBehaviour>();
        foreach (PawnBehaviour pawn in opponentPawn)
        {
            if (pawn is ArcherBehaviour)
            {
                result.Add(pawn);
            }
        }
        return result;
    }

    public List<PawnBehaviour> allKnight(List<PawnBehaviour> opponentPawn)
    {
        List<PawnBehaviour> result = new List<PawnBehaviour>();
        foreach (PawnBehaviour pawn in opponentPawn)
        {
            if (pawn is KnightBehaviour)
            {
                result.Add(pawn);
            }
        }
        return result;
    }

    public List<PawnBehaviour> allScout(List<PawnBehaviour> opponentPawn)
    {
        List<PawnBehaviour> result = new List<PawnBehaviour>();
        foreach (PawnBehaviour pawn in opponentPawn)
        {
            if (pawn is ScoutBehaviour)
            {
                result.Add(pawn);
            }
        }
        return result;
    }

    public List<PawnBehaviour> allInRange(PawnBehaviour selfPawn, List<PawnBehaviour> opponentPawn)
    {
        List<PawnBehaviour> result = new List<PawnBehaviour>();
        foreach (PawnBehaviour pawn in opponentPawn)
        {
            if (Vector2.Distance(selfPawn.PawnRig.position, pawn.PawnRig.position) < selfPawn.ClassScriptObj.MoveDistance)
            {
                result.Add(pawn);
            }
        }
        return result;
    }
}
