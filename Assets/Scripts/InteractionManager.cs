using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private ActionPhases currPhase = ActionPhases.NonePhase;
    public ActionPhases CurrPhase { get { return currPhase; } }
    private List<PawnBehaviour> playerPawns = new List<PawnBehaviour>();
    private List<PawnBehaviour> enemyPawns = new List<PawnBehaviour>();
    private List<PawnBehaviour> DiedPawns = new List<PawnBehaviour>();
    private List<AITreeHead.PawnAction> aiActions = new List<AITreeHead.PawnAction>();
    private int finishCounter = 0;

    private PawnBehaviour selectedPawn = null;
    public PawnBehaviour SelectedPawn { get { return selectedPawn; } }
    private bool inAttackControl = false;
    public bool InAttackControl { get { return inAttackControl; } set { inAttackControl = value; } }
    private bool inTurnControl = false;
    public bool InTurnControl { get { return inTurnControl; } set { inTurnControl = value; } }
    private bool inMoveControl = false;
    public bool InMoveControl { get { return inMoveControl; } set { inMoveControl = value; } }

    [SerializeField]
    private PawnBehaviour[] testPlayerPawns;
    [SerializeField]
    private PawnBehaviour[] testEnemyPawns;

    private void Start()
    {
        foreach (PawnBehaviour pawn in testPlayerPawns)
        {
            playerPawns.Add(pawn);
        }
        foreach (PawnBehaviour pawn in testEnemyPawns)
        {
            enemyPawns.Add(pawn);
        }
    }

    public void addPawn(PawnBehaviour pawn, bool isPlayerPawn)
    {
        if (pawn)
        {
            (isPlayerPawn ? playerPawns : enemyPawns).Add(pawn);
        }
        else
        {
            Debug.Log("ERROR: Pawn to add is null");
        }
    }

    public void removePawn(PawnBehaviour pawn)
    {
        if (pawn)
        {
            (pawn.CompareTag("PlayerPawn") ? playerPawns : enemyPawns).Remove(pawn);
        }
        else
        {
            Debug.Log("ERROR: Pawn to remove is null");
        }
    }

    public void addDied(PawnBehaviour pawn)
    {
        DiedPawns.Add(pawn);
    }

    public void deleteDiedPawn()
    {
        if (DiedPawns.Count <= 0)
        {
            return;
        }
        for (int i = DiedPawns.Count - 1; i >= 0; i--)
        {
            Destroy(DiedPawns[i].gameObject);
        }
        DiedPawns.Clear();
        if (enemyPawns.Count <= 0)
        {
            playerWin();
        }
        else if (playerPawns.Count <= 0)
        {
            playerLost();
        }
    }

    public void playerLost()
    {

    }

    public void playerWin()
    {
        currPhase = ActionPhases.NonePhase;
    }

    public void actionFinishCallback()
    {
        finishCounter++;
        //Debug.Log(currPhase + " Action Finish Callback: " + finishCounter + "/" + (int)(playerPawns.Count + enemyPawns.Count));
        if (finishCounter >= playerPawns.Count + enemyPawns.Count)
        {
            finishCounter = 0;
            //Debug.Log(currPhase + " Ended");
            currPhase = currPhase + 1;
            if (currPhase == ActionPhases.MoveTurnPhase)
            {
                selectedPawn = null;
                InAttackControl = false;
                InMoveControl = false;
                InTurnControl = false;
            }
            if (currPhase == ActionPhases.DiePhase)
            {
                foreach (PawnBehaviour pawn in DiedPawns)
                {
                    pawn.PawnAnmt.SetTrigger("Die");
                }
            }
            if (currPhase == ActionPhases.EndTurnPhase)
            {
                deleteDiedPawn();
                if (currPhase != ActionPhases.NonePhase)
                {
                    currPhase = ActionPhases.ControlPhase;
                }
                /*Debug.Log("Turn Ended");
                currPhase = ActionPhases.NonePhase;*/
            }
            //Debug.Log(currPhase + "Started");
            if (currPhase != ActionPhases.NonePhase)
            {
                phaseActionStart();
            }
        }
    }

    public void startActionPhase()
    {
        selectPawn(null);
        inAttackControl = false;
        inTurnControl = false;
        inMoveControl = false;

        performAIActions(true);
        //performAIActions(false);

        if (currPhase == ActionPhases.NonePhase)
        {
            currPhase = ActionPhases.ControlPhase;
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
        if (selectedPawn)
        {
            selectedPawn.PawnHubCon.selectedPawn(false);
        }
        selectedPawn = pawn;
        if (selectedPawn)
        {
            selectedPawn.PawnHubCon.selectedPawn(true);
            //Debug.Log(pawn.name + " Has been selected");
        }
        /*else
        {
            Debug.Log("Pawn has been unselected");
        }*/
    }

    private void performAIActions(bool toEnemy = true)
    {
        GameManager.Instance.AITree.runAI(toEnemy ? enemyPawns : playerPawns, toEnemy ? playerPawns: enemyPawns);
        for (int i = 0; i < (toEnemy ? enemyPawns : playerPawns).Count; i++)
        {
            PawnBehaviour pawn = (toEnemy ? enemyPawns : playerPawns)[i];
            AITreeHead.PawnAction action = aiActions[i];

            if (action.movePos != pawn.PawnRig.position)
            {
                pawn.setMovePos(action.movePos);
            }
            if (action.attackDir != Vector2.zero)
            {
                pawn.setAttackDir(action.attackDir, action.justTurn);
            }
        }
        aiActions.Clear();
    }

    public void setAIAction(AITreeHead.PawnAction action)
    {
        aiActions.Add(action);
    }

    public void setAttackControl(bool isOn)
    {
        if (selectedPawn)
        {
            InAttackControl = isOn;
            selectedPawn.PawnHubCon.openOptions(!isOn);
        }
    }

    public void setTurnControl(bool isOn)
    {
        if (selectedPawn)
        {
            inTurnControl = isOn;
            selectedPawn.PawnHubCon.openOptions(!isOn);
        }
    }

    public void setMoveControl(bool isOn)
    {
        if (selectedPawn)
        {
            InMoveControl = isOn;
            selectedPawn.PawnHubCon.openOptions(!isOn);
        }
    }
}
