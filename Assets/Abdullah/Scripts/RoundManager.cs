using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] Dice dice;
    [SerializeField] TurnController turnController;
    [SerializeField] PlayerMasterController playerController;
    [SerializeField] BoardManager boardManager;
    [SerializeField] CardManager gameGenerator;
    [SerializeField] CameraCloseUp cameraCloseUp;
    bool diceRolled = false;
    bool secondRollavailable = false;
    bool secondAccusationavailable = false;
    bool canRoll = true;
    bool canSug = true;
    bool canAcc = true;

    public bool CanRoll { get => canRoll;}
    public bool CanSug { get => canSug;}
    public bool CanAcc { get => canAcc;}

    private void Awake()
    {
        dice = FindObjectOfType<Dice>();
        turnController = FindObjectOfType<TurnController>();
        playerController = turnController.GetCurrentPlayer();
        boardManager = FindObjectOfType<BoardManager>();
        try
        {

        dice = boardManager.GetComponentInChildren<Dice>();
        }catch(System.Exception e) { 
        }
        gameGenerator = FindObjectOfType<CardManager>();
        cameraCloseUp = FindObjectOfType<CameraCloseUp>();
    }


    private void FixedUpdate()
    {
        DiceBehaviour();

    }

    void DiceBehaviour()
    {
        if (dice == null)
        {
            return;
        }
        PlayerMasterController playerMasterController = turnController.GetCurrentPlayer();
        if (dice.GetValue() > 0 && diceRolled)
        {
            diceRolled = false;
            if (!boardManager.ShowMovable(playerMasterController.GetTile(), dice.GetValue()))
            {
                if (!boardManager.ShowMovable(playerMasterController.GetCurrentRoom(), dice.GetValue()))
                {
                    Debug.LogError("Failed to show boardManager movable");
                }
            }
            //playerMasterController.DisplayBoardMovableTiles(dice.GetValue());
            dice.ResetDice();
        }
    }
    IEnumerator DelayResetDice(float t)
    {
        yield return new WaitForSeconds(t);
        dice.ResetDice();
    }

    /// <summary>
    /// Call to roll the dice, with a bool to check if you can roll again in your turn
    /// </summary>
    public void RollDice()
    {
        diceRolled = true;
        if (!secondRollavailable && canRoll)
        {
            dice.RollDice();
            secondRollavailable = true;
            canRoll = false;
        }
        if (secondRollavailable)
        {
            dice.RollDice();
            secondRollavailable = false;
            canRoll = false;
        }
        StartCoroutine(DelayResetDice(5f));
    }



    public void MovePlayer(BoardTileScript b)
    {
        playerController = turnController.GetCurrentPlayer();
        if (playerController.PlayerTokenScript.IsInRoom())
        {
            playerController.GetCurrentRoom().RemovePlayerFromRoom(playerController, b);
        }
        else
        {
            playerController.MovePlayer(b);
        }
        if (boardManager == null)
        {
            boardManager = FindObjectOfType<BoardManager>();

        }
        boardManager.ClearMovable();

    }



    public void MakeSuggestion(List<Card> sug)
    { /*
          Player enters a room
          Player makes a weapon and player suggestion
          if other player has card -> show card
          if no players have the card -> player can choose to make accusation or end turn
         */
        bool playerWithCardFound= false;
        List<PlayerMasterController> RestOfPlayers = turnController.GetRestOfPlayersInOrder();
        //Iterate through the players in order and check if a player has a card
        for (int i = 0; i < RestOfPlayers.Count;i++) {
            if (RestOfPlayers[i].FindCard(sug) != null) {
                Tuple<PlayerMasterController, List<Card>> foundPlayer = RestOfPlayers[i % RestOfPlayers.Count].FindCard(sug);
                Debug.Log(foundPlayer.Item1.ToString()+" Has cards:");
                foreach(Card c in foundPlayer.Item2)
                {
                    Debug.Log(c.gameObject.name);
                }
                playerWithCardFound = true;
            }
            
        }
        if (!playerWithCardFound) {
            print("No Player With Card Found");
            playerWithCardFound = false;
        }

    }

    public void MakeAccusation(List<Card> cards)
    {
        /*Get player 3 chosen cards
         Check the players card against the 3 cards set at the start of the game
        if they match -> player wins
        if they dont match -> player asked to make a second accusation
        */
        if (gameGenerator.IsMatchAnswer(cards))
        {
            //code for wining
            turnController.Win();
        }
    }

    public void EndTurn()
    {
        // go to next player, set booleans for the second roll to be ready
        turnController.SetCurrentPlayerToNext();
        canRoll = true;
        secondRollavailable = false;

        //Anson: reset camera
        cameraCloseUp.ClearCloseUp();

        //Anson: start the turn to update the current player
        StartTurn();
    }
    /// <summary>
    /// Method for starting the turn
    /// </summary>
    public void StartTurn()
    {
        playerController = turnController.GetCurrentPlayer();
        uIHandler.DisplayDeck(playerController.GetDeck());
        canRoll = true;
        canSug = true;
        canAcc = true;
    }

    /// <summary>
    /// Gets the player controller for the current player
    /// </summary>
    /// <returns>player controller for the current player</returns>
    public PlayerMasterController GetCurrentPlayer()
    {
        return turnController.GetCurrentPlayer();
    }
}
