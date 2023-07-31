using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverlayCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject battleMessageParent;
    [SerializeField]
    private TMP_Text turnNumberText;
    [SerializeField]
    private TMP_Text phaseText;
    [SerializeField]
    private Animator timerAnmt;
    [SerializeField]
    private GameObject pawnManageParent;
    [SerializeField]
    private TMP_Text pawnNumText;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject loseScreen;

    public void setTurnNumber(int num)
    {
        turnNumberText.text = "Current Turn: " + num;
    }

    public void setPhase(ActionPhases phase)
    {
        phaseText.text = "Current Phase: " + phase;
    }

    public void startTimer()
    {
        timerAnmt.SetTrigger("CountDown");
    }

    public void setBattleMessage(bool active)
    {
        battleMessageParent.SetActive(active);
    }

    public void setPawnNum()
    {
        pawnNumText.text = "Pawn Number: " + GameManager.Instance.InterMan.PlayerPawns.Count + "/" + GameManager.Instance.GameDesignScriptObj.MaxPawn;
    }

    public void startGame()
    {
        GameManager.Instance.InterMan.trashFakePawn();
        setPawnManageActive(false);
        setBattleMessage(true);
        GameManager.Instance.InterMan.startActionPhase();
        GameManager.Instance.InterMan.InPawnManagment = false;
    }

    public void spawnArcher()
    {
        if (!GameManager.Instance.InterMan.SelectedPawn && GameManager.Instance.InterMan.PlayerPawns.Count < GameManager.Instance.GameDesignScriptObj.MaxPawn)
        {
            GameManager.Instance.InterMan.SelectedPawn = Instantiate(GameManager.Instance.GameDesignScriptObj.ArcherPrefab).GetComponent<PawnBehaviour>();
        }
    }

    public void spawnKnight()
    {
        if (!GameManager.Instance.InterMan.SelectedPawn && GameManager.Instance.InterMan.PlayerPawns.Count < GameManager.Instance.GameDesignScriptObj.MaxPawn)
        {
            GameManager.Instance.InterMan.SelectedPawn = Instantiate(GameManager.Instance.GameDesignScriptObj.KnightPrefab).GetComponent<PawnBehaviour>();
        }
    }

    public void spawnScout()
    {
        if (!GameManager.Instance.InterMan.SelectedPawn && GameManager.Instance.InterMan.PlayerPawns.Count < GameManager.Instance.GameDesignScriptObj.MaxPawn)
        {
            GameManager.Instance.InterMan.SelectedPawn = Instantiate(GameManager.Instance.GameDesignScriptObj.ScoutPrefab).GetComponent<PawnBehaviour>();
        }
    }

    public void setPawnManageActive(bool active)
    {
        pawnManageParent.SetActive(active);
    }

    public void playerWin()
    {
        winScreen.SetActive(true);
    }

    public void playerLose()
    {
        loseScreen.SetActive(true);
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
