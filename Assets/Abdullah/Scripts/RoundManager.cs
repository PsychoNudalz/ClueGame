using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    TurnController player;
    Dice dice;
    TurnController turnController;
    PlayerMasterController playerController;
    BoardManager boardManager;
    bool secondRollavailable = false;
    bool canRoll = true;

    private void Awake()
    {
       
        turnController = FindObjectOfType<TurnController>();
        playerController = turnController.GetCurrentPlayer();
        boardManager = FindObjectOfType<BoardManager>();
    }

    void RollDice() 
    {
        if (!secondRollavailable && canRoll)
        {
            dice.RollDice();
            secondRollavailable = true;
        }
        if (secondRollavailable)
        {
            dice.RollDice();
            secondRollavailable = false;
            canRoll = true;
        }
    }

    public void MovePlayer(BoardTileScript b) 
    {
        if (playerController == null)
        {
            playerController = turnController.GetCurrentPlayer();

        }
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
    void ShowCard() 
    {
        /*
        if getnextPlayer has card show Card
        else if getnextPlayer + 1 has 1 card show card
        else if getnextPlayer + 2 has 1 card show card
        else if getnextPlayer + 3 has 1 card show card
        else if getnextPlayer + 4 has 1 card show card
        else return no card found
         */
    }

    void MakeSuggestion()
    {
    /*
      Player enters a room
      Player makes a weapon and player suggestion
      if other player has card -> show card
      if no players have the card -> player can choose to make accusation or end turn
     */

    }

    void MakeAccusation() 
    {
       /*Get player 3 chosen cards
        Check the players card against the 3 cards set at the start of the game
       if they match -> player wins
       if they dont match -> player asked to make a second accusation
       if second accusation doesnt not match -> remove player from queue
       */
    }

    void EndTurn()
    {
        player.SetCurrentPlayerToNext();
        canRoll = true;
        secondRollavailable = false;
    }
}
