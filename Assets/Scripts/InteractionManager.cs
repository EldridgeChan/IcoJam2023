using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private ActionPhases currPhase = ActionPhases.NonePhase;
    public ActionPhases CurrPhase { get { return currPhase; } }
    private List<PawnBehaviour> playerPawns = new List<PawnBehaviour>();
    public List<PawnBehaviour> PlayerPawns { get { return playerPawns; } }
    private List<PawnBehaviour> enemyPawns = new List<PawnBehaviour>();
    private List<PawnBehaviour> DiedPawns = new List<PawnBehaviour>();
    private List<AITreeHead.PawnAction> aiActions = new List<AITreeHead.PawnAction>();
    public List<AITreeHead.PawnAction> AIActions { get { return aiActions; } }
    private int finishCounter = 0;
    private int turnNumber = 0;

    private PawnBehaviour selectedPawn = null;
    public PawnBehaviour SelectedPawn { get { return selectedPawn; } set { selectedPawn = value; } }
    private bool inAttackControl = false;
    public bool InAttackControl { get { return inAttackControl; } set { inAttackControl = value; } }
    private bool inTurnControl = false;
    public bool InTurnControl { get { return inTurnControl; } set { inTurnControl = value; } }
    private bool inMoveControl = false;
    public bool InMoveControl { get { return inMoveControl; } set { inMoveControl = value; } }
    private bool inPawnManagment = false;
    public bool InPawnManagment { get { return inPawnManagment; } set { inPawnManagment = value; } }

    /*[SerializeField]
    private PawnBehaviour[] testPlayerPawns;*/
    /*[SerializeField]
    private PawnBehaviour[] testEnemyPawns;*/

    private void Start()
    {
        /*foreach (PawnBehaviour pawn in testPlayerPawns)
        {
            playerPawns.Add(pawn);
        }*/
        /*
        foreach (PawnBehaviour pawn in testEnemyPawns)
        {
            enemyPawns.Add(pawn);
        }*/
    }

    public void addPawn(PawnBehaviour pawn, bool isPlayerPawn)
    {
        if (pawn)
        {
            (isPlayerPawn ? playerPawns : enemyPawns).Add(pawn);
            GameManager.Instance.CanvasCon.setPawnNum();
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
            GameManager.Instance.CanvasCon.setPawnNum();
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
        currPhase = ActionPhases.EndGamePhase;
        GameManager.Instance.CanvasCon.playerLose();

    }

    public void playerWin()
    {
        currPhase = ActionPhases.EndGamePhase;
        GameManager.Instance.CanvasCon.playerWin();
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
                selectPawn(null);
                InAttackControl = false;
                InMoveControl = false;
                InTurnControl = false;
                disabloPawnControlGUI();
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
                if (currPhase != ActionPhases.EndGamePhase)
                {
                    currPhase = ActionPhases.NonePhase;
                    startActionPhase();
                }
                /*Debug.Log("Turn Ended");
                currPhase = ActionPhases.NonePhase;*/
            }
            GameManager.Instance.CanvasCon.setPhase(currPhase);
            //Debug.Log(currPhase + "Started");
            if (currPhase != ActionPhases.EndGamePhase && currPhase != ActionPhases.ControlPhase)
            {

                phaseActionStart();
            }
        }
    }
    
    public void startActionPhase()
    {
        turnNumber++;
        GameManager.Instance.CanvasCon.setTurnNumber(turnNumber);
        selectPawn(null);
        inAttackControl = false;
        inTurnControl = false;
        inMoveControl = false;

        performAIActions(true);
        //performAIActions(false);

        if (currPhase == ActionPhases.NonePhase)
        {
            currPhase = ActionPhases.ControlPhase;
            GameManager.Instance.CanvasCon.startTimer();
            phaseActionStart();
        }
        else
        {
            Debug.Log("ERROR: Unexpected Phase to start action");
        }
    }

    private void disabloPawnControlGUI()
    {
        foreach (PawnBehaviour pawn in playerPawns)
        {
            pawn.disableControlGUI();
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
            inAttackControl = isOn;
            inTurnControl = false;
            inMoveControl = false;
            selectedPawn.PawnHubCon.openOptions(!isOn);
            if (isOn)
            {
                selectedPawn.PawnHubCon.setAttactIndicatorActive(true);
                selectedPawn.PawnHubCon.setFakePawnActive(true);
                if (!selectedPawn.HasMove)
                {
                    selectedPawn.PawnHubCon.moveFakePawn(selectedPawn.transform.position, selectedPawn.faceDir());
                }
            }
        }
    }

    public void setTurnControl(bool isOn)
    {
        if (selectedPawn)
        {
            inTurnControl = isOn;
            inAttackControl = false;
            inMoveControl = false;
            selectedPawn.PawnHubCon.openOptions(!isOn);
            if (isOn)
            {
                selectedPawn.PawnHubCon.setAttactIndicatorActive(false);
                if (!selectedPawn.HasMove)
                {
                    selectedPawn.PawnHubCon.moveFakePawn(selectedPawn.transform.position, selectedPawn.faceDir());
                }
            }
        }
    }

    public void setMoveControl(bool isOn)
    {
        if (selectedPawn)
        {
            inMoveControl = isOn;
            inAttackControl = false;
            inTurnControl = false;
            selectedPawn.PawnHubCon.openOptions(!isOn);
            if (isOn)
            {
                selectedPawn.PawnHubCon.setFakePawnActive(true);
                selectedPawn.PawnHubCon.setAttactIndicatorActive(false);
            } 
            else
            {
                selectedPawn.PawnHubCon.setFakePawnActive(selectedPawn.HasMove);
                selectedPawn.PawnHubCon.moveFakePawn();
            }
        }
    }

    public void moveFakePawn(Vector3 mousePos)
    {
        if (selectedPawn 
            && Mathf.Abs(mousePos.y) < GameManager.Instance.GameDesignScriptObj.EnemySpawnY + 0.5f
            && mousePos.x < -0.5f
            && mousePos.x > -GameManager.Instance.GameDesignScriptObj.EnemySpawnMaxX - 0.5f)
        {
            selectedPawn.transform.position = new Vector2((int)(mousePos.x - 0.5f), (int)(mousePos.y + (mousePos.y == 0 ? 0 : mousePos.y < 0 ? -0.5f : 0.5f)));
        }
        else
        {
            selectedPawn.transform.position = new Vector2(mousePos.x, mousePos.y);
        }
    }

    public void placeFakePawn(Vector3 mousePos)
    {
        if (Mathf.Abs(mousePos.y) < GameManager.Instance.GameDesignScriptObj.EnemySpawnY + 0.5f
            && mousePos.x < -0.5f
            && mousePos.x > -GameManager.Instance.GameDesignScriptObj.EnemySpawnMaxX - 0.5f
            && playerPawns.Count < GameManager.Instance.GameDesignScriptObj.MaxPawn)
        {
            if (!hasPawnAlready())
            {
                addPawn(SelectedPawn, true);
                selectedPawn = null;
            }
        }
    }

    public bool hasPawnAlready()
    {
        foreach (PawnBehaviour pawn in playerPawns)
        {
            if (pawn.transform.position == selectedPawn.transform.position)
            {
                return true;
            }
        }
        return false;
    }

    public void pickUpFakePawn(PawnBehaviour pawn)
    {
        removePawn(pawn);
        selectedPawn = pawn;
    }

    public void trashFakePawn()
    {
        if (selectedPawn)
        {
            Destroy(selectedPawn.gameObject);
            selectedPawn = null;
        }
    }
}
