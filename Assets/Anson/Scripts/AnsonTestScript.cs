using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnsonTestScript : MonoBehaviour
{
    [SerializeField] PlayerMasterController playerMasterController;
    [SerializeField] BoardManager boardManager;
    [SerializeField] Dice dice;
    bool diceRolled = false;

    private void Awake()
    {
        AssignAllComponents();
    }
    private void FixedUpdate()
    {
        if (dice.GetValue() > 0 && diceRolled)
        {
            diceRolled = false;
            playerMasterController.DisplayBoardMovableTiles(dice.GetValue());

        }
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
        dice.RollDice();
        //playerMasterController.PlayerSelectionScript.MoveAmount = dice.GetValue();
        print(this + " rolled: " + playerMasterController.PlayerSelectionScript.MoveAmount);
        diceRolled = true;
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
