using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestUIScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected PlayerMasterController playerMasterController;
    [SerializeField] protected BoardManager boardManager;
    [SerializeField] protected Dice dice;
    [SerializeField] protected TurnController turnController;
    [SerializeField] protected RoundManager roundManager;
    [SerializeField] protected CardManager cardManager;
    [SerializeField] protected UserController userController;


    [Header("Outputs")]
    [SerializeField] protected TextMeshProUGUI outputText;
    [SerializeField] protected string outputString;
    [SerializeField] protected TextMeshProUGUI expectedText;
    [SerializeField] protected TextMeshProUGUI currentStatusText;

    public virtual void UpdateOutputText(string s)
    {
        outputText.text = s;
    }



    public virtual void UpdateExpectedText(string s)
    {
        expectedText.text = s;
    }

    public virtual void UpdateStatusText(string s)
    {
        currentStatusText.text = s;
    }

    public virtual void UpdateBehaviour()
    {

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
        if (!turnController)
        {
            turnController = FindObjectOfType<TurnController>();
        }
        if (!cardManager)
        {
            cardManager = FindObjectOfType<CardManager>();
        }
        if (!roundManager)
        {
            roundManager = FindObjectOfType<RoundManager>();
        }
        if (!userController) 
        {
            userController = FindObjectOfType<UserController>();
        }
    }

}
