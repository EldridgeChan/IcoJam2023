using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 mousePos = Vector3.zero;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GameManager.Instance.InterMan.CurrPhase == ActionPhases.ControlPhase)
        {
            InputControl();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.InterMan.startActionPhase();
            Debug.Log("Action Phase Started");
        }
    }

    private void InputControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.Instance.InterMan.SelectedPawn)
            {
                trySelectPawn();
            }
            else
            {
                pawnControl();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (GameManager.Instance.InterMan.InAttackControl || GameManager.Instance.InterMan.InMoveControl || GameManager.Instance.InterMan.InTurnControl)
            {
                //Debug.Log((GameManager.Instance.InterMan.InAttackControl ? "Attack" : (GameManager.Instance.InterMan.InMoveControl ? "Move" : "Turn")) + " Control Deactivated");
                GameManager.Instance.InterMan.setAttackControl(false);
                GameManager.Instance.InterMan.setTurnControl(false);
                GameManager.Instance.InterMan.setMoveControl(false);
            }
            else
            {
                GameManager.Instance.InterMan.selectPawn(null);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (GameManager.Instance.InterMan.SelectedPawn)
            {
                GameManager.Instance.InterMan.setAttackControl(true);
                //Debug.Log("Attack Control Activeated");
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (GameManager.Instance.InterMan.SelectedPawn)
            {
                GameManager.Instance.InterMan.setTurnControl(true);
                //Debug.Log("Turn Control Activeated");
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (GameManager.Instance.InterMan.SelectedPawn)
            {
                GameManager.Instance.InterMan.setMoveControl(true);
                //Debug.Log("Move Control Activeated");
            }
        }
    }

    private void trySelectPawn()
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward);
        if (hit.collider != null && hit.collider.CompareTag("PlayerPawn"))
        {
            GameManager.Instance.InterMan.selectPawn(hit.collider.GetComponent<PawnBehaviour>());
        }
    }

    private void pawnControl()
    {
        if (GameManager.Instance.InterMan.InAttackControl)
        {
            GameManager.Instance.InterMan.SelectedPawn.setAttackDir(mousePos);
            
        }
        else if (GameManager.Instance.InterMan.InMoveControl)
        {
            GameManager.Instance.InterMan.SelectedPawn.setMovePos(mousePos);
        }
        else if (GameManager.Instance.InterMan.InTurnControl)
        {
            GameManager.Instance.InterMan.SelectedPawn.setAttackDir(mousePos, true);
        }
        else
        {
            trySelectPawn();
        }
    }
}
