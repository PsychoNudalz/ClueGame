using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnsonTestScript : MonoBehaviour
{
    [SerializeField] PlayerHanderScript playerHanderScript;
    [SerializeField] BoardManager boardManager;
    [SerializeField] Die die;

    public void RollDie(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            //Need to be replaced with results of die
            playerHanderScript.PlayerSelectionScript.MoveAmount = Random.Range(1, 6) + Random.Range(1, 6);
            print(this + " rolled: " + playerHanderScript.PlayerSelectionScript.MoveAmount);
        }
    }

    public void AssignAllComponents(InputAction.CallbackContext callbackContext)
    {
        if (playerHanderScript == null)
        {

        playerHanderScript = FindObjectOfType<PlayerHanderScript>();
        }
        if (boardManager == null)
        {
            boardManager = FindObjectOfType<BoardManager>();
        }
        if (die == null)
        {
            die = FindObjectOfType<Die>();
        }
    }
}
