using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AITestScript : TestUIScript
{
    [SerializeField] AIControllerScript aIController;
    private void Awake()
    {
        AssignAllComponents();
    }
    public void EndTurn(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            FindObjectOfType<UIHandler>().EndTurn();

        }
    }

    public void HaveAIMove(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            aIController.SetAIMode(AIMode.Move);
        }
    }
}
