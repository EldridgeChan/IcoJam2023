using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawnHubController : MonoBehaviour
{
    [SerializeField]
    private Transform parentTrans;
    [SerializeField]
    private Canvas pawnHubCanvas;
    [SerializeField]
    private SpriteRenderer selectIndicatorRend;
    [SerializeField]
    private Animator canvasAnmt;
    [SerializeField]
    private Animator[] heartsAnmt;

    private void Start()
    {
        pawnHubCanvas.worldCamera = Camera.main;
    }
    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    public void selectedPawn(bool selected)
    {
        selectIndicatorRend.enabled = selected;
        openOptions(selected);
    }

    public void openOptions(bool open)
    {
        canvasAnmt.SetBool("Open", open);
    }

    public void setHearts(int number)
    {
        for (int i = 0; i < heartsAnmt.Length; i++)
        {
            heartsAnmt[i].SetBool("HaveHeart", i < number);
        }
    }

    public void activateAttackControl()
    {
        GameManager.Instance.InterMan.setAttackControl(true);
    }

    public void activateTurnControl()
    {
        GameManager.Instance.InterMan.setTurnControl(true);
    }

    public void activateMoveControl()
    {
        GameManager.Instance.InterMan.setMoveControl(true);
    }
}
