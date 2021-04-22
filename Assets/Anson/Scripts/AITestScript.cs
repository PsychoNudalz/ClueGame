using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AITestScript : TestUIScript
{
    [SerializeField] AIControllerScript aIController;
    [SerializeField] bool isSpeedUp;
    private void Awake()
    {
        AssignAllComponents();
        Time.timeScale = 1;
        if (isSpeedUp)
        {
            Time.timeScale = 10f;
        }
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
