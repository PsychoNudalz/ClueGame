using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AITestScript : TestUIScript
{
    [SerializeField] AIControllerScript aIController;
    [SerializeField] bool isSpeedUp;
    [SerializeField] float speedUp = 10f;
    private void Awake()
    {
        AssignAllComponents();
        Time.timeScale = 1;
        if (isSpeedUp)
        {
            Time.timeScale = speedUp;
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

    public void SpeedAI_Up(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Time.timeScale = speedUp;
        }
    }
    public void SpeedAI_Down(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Time.timeScale = 1f;

        }
    }
}
