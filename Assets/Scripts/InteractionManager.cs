using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    ActionPhases currPhase = ActionPhases.NonePhase;
    private List<PawnBehaviour> playerPawns = new List<PawnBehaviour>();
    private List<PawnBehaviour> enemyPawns = new List<PawnBehaviour>();
    private int finishCounter = 0;

    private PawnBehaviour selectedPawn = null;
    public PawnBehaviour SelectedPawn { get { return selectedPawn; } }
    private bool inAttackControl = false;
    public bool InAttackControl { get { return inAttackControl; } set { inAttackControl = value; } } 

    [SerializeField]
    private PawnBehaviour[] testPlayerPawns;

    private void Start()
    {
        foreach (PawnBehaviour pawn in testPlayerPawns)
        {
            playerPawns.Add(pawn);
        }
    }

    public void actionFinishCallback()
    {
        finishCounter++;
        Debug.Log(currPhase + " Action Finish Callback: " + finishCounter + "/" + (int)(playerPawns.Count + enemyPawns.Count));
        if (finishCounter >= playerPawns.Count + enemyPawns.Count)
        {
            finishCounter = 0;
            Debug.Log(currPhase + " Ended");
            currPhase = currPhase + 1;
            if (currPhase == ActionPhases.EndTurnPhase)
            {
                Debug.Log("All phase Ended");
                currPhase = ActionPhases.NonePhase;
            }
            else
            {
                Debug.Log(currPhase + "Started");
                phaseActionStart();
            }
        }
    }

    public void startActionPhase()
    {
        if (currPhase == ActionPhases.NonePhase)
        {
            currPhase = ActionPhases.MoveTurnPhase;
            phaseActionStart();
        }
        else
        {
            Debug.Log("ERROR: Unexpected Phase to start action");
        }
    }

    private void phaseActionStart()
    {
        foreach (PawnBehaviour pawn in playerPawns)
        {
            pawn.phaseActionStart(currPhase);
        }
        foreach (PawnBehaviour pawn in enemyPawns)
        {
            pawn.phaseActionStart(currPhase);
        }
    }

    public void selectPawn(PawnBehaviour pawn)
    {
        selectedPawn = pawn;
        if (pawn)
        {
            Debug.Log(pawn.name + " Has been selected");
        }
        else
        {
            Debug.Log("Pawn has been unselected");
        }
    }
}
