using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OverlayCanvasController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text turnNumberText;
    [SerializeField]
    private TMP_Text phaseText;
    [SerializeField]
    private Animator timerAnmt;
    [SerializeField]
    private GameObject battleMessageParent;

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
}
