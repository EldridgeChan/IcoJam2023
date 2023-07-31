using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawnHubController : MonoBehaviour
{
    [SerializeField]
    private PawnBehaviour parentPawn;
    [SerializeField]
    private Canvas pawnHubCanvas;
    [SerializeField]
    private SpriteRenderer selectIndicatorRend;
    [SerializeField]
    private Animator canvasAnmt;
    [SerializeField]
    private Animator[] heartsAnmt;
    [SerializeField]
    private SpriteRenderer fakePawnRend;
    [SerializeField]
    private GameObject attackIndicator;

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

    public void setFakePawnActive(bool active)
    {
        fakePawnRend.enabled = active;
    }

    public void moveFakePawn(Vector2 mousePos, Vector2 faceDir)
    {
        Vector2 movePos = Vector2.Distance(parentPawn.transform.position, mousePos) > parentPawn.ClassScriptObj.MoveDistance ? ((mousePos - (Vector2)parentPawn.transform.position).normalized * parentPawn.ClassScriptObj.MoveDistance) + (Vector2)parentPawn.transform.position : mousePos;
        movePos = new Vector2(Mathf.Clamp(movePos.x, -GameManager.Instance.GameDesignScriptObj.AreanaWidth, GameManager.Instance.GameDesignScriptObj.AreanaWidth), Mathf.Clamp(movePos.y, -GameManager.Instance.GameDesignScriptObj.AreanaHight, GameManager.Instance.GameDesignScriptObj.AreanaHight));
        fakePawnRend.transform.position = movePos;
        fakePawnRend.transform.rotation = Quaternion.Euler(0.0f, 0.0f, ((mousePos - (Vector2)parentPawn.transform.position).y < 0 ? -1.0f : 1.0f) * Vector2.Angle(Vector2.right, (mousePos - (Vector2)parentPawn.transform.position)));
    }

    public void moveFakePawn()
    {
        fakePawnRend.transform.position = parentPawn.MovePos;
        fakePawnRend.transform.rotation = Quaternion.Euler(0.0f, 0.0f, ((parentPawn.MovePos - (Vector2)parentPawn.transform.position).y < 0 ? -1.0f : 1.0f) * Vector2.Angle(Vector2.right, (parentPawn.MovePos - (Vector2)parentPawn.transform.position)));
    }

    public void turnFakePawn(Vector2 mousePos)
    {
        Vector2 faceDir = (mousePos - (Vector2)fakePawnRend.transform.position).normalized;
        fakePawnRend.transform.rotation = Quaternion.Euler(0.0f, 0.0f, (faceDir.y < 0 ? -1.0f : 1.0f) * Vector2.Angle(Vector2.right, faceDir));
    }

    public void setAttactIndicatorActive(bool active)
    {
        attackIndicator.SetActive(active);
    }
}
