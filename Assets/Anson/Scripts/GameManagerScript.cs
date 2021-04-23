using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PlayerMasterController playerMasterController;
    [SerializeField] BoardManager boardManager;
    [SerializeField] Dice dice;
    [SerializeField] TurnController turnController;
    [SerializeField] RoundManager roundManager;
    [SerializeField] CardManager cardManager;
    [SerializeField] UserController userController;
    [SerializeField] UIHandler uIHandler;
    private void Start()
    {

        AssignAllComponents();
    }

    public void StartGame()
    {
        if (FindObjectOfType<GameSetUpScript>() != null)
        {
            FindObjectOfType<GameSetUpScript>().StartGame();
        }
        turnController.StartGame();
        cardManager.Initialise();
        foreach (PlayerMasterController p in FindObjectsOfType<PlayerMasterController>())
        {
            p.StartBehaviour();
        }
        uIHandler.StartBehaviour();
        roundManager.StartTurn();
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
        if (!uIHandler)
        {
            uIHandler = FindObjectOfType<UIHandler>();
        }
    }



}
