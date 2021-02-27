using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnsonTestScript : MonoBehaviour
{
    [SerializeField] PlayerMasterController playerMasterController;
    [SerializeField] BoardManager boardManager;
    [SerializeField] Dice dice;

    private void Awake()
    {
        AssignAllComponents();
    }

    public void RollDie(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            RollDie();
        }
    }
    public void RollDie()
    {
        //Need to be replaced with results of die
        //dice.RollDice();
        //playerMasterController.PlayerSelectionScript.MoveAmount = dice.GetValue();
        print(this + " rolled: " + playerMasterController.PlayerSelectionScript.MoveAmount);
        playerMasterController.DisplayBoardMovableTiles(5);

    }

    public void AssignAllComponents(InputAction.CallbackContext callbackContext)
    {
        AssignAllComponents();
    }


    public void AssignAllComponents()
    {
        if (playerMasterController == null)
        {

            playerMasterController = FindObjectOfType<PlayerMasterController>();
        }
        if (boardManager == null)
        {
            boardManager = FindObjectOfType<BoardManager>();
        }
        if (dice == null)
        {
            dice = FindObjectOfType<Dice>();
        }
    }
}
