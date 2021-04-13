﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] Dice dice;
    [SerializeField] TurnController turnController;
    [SerializeField] PlayerMasterController playerController;
    [SerializeField] BoardManager boardManager;
    bool diceRolled = false;
    bool secondRollavailable = false;
    bool canRoll = true;


    private void Awake()
    {
        dice = FindObjectOfType<Dice>();
        turnController = FindObjectOfType<TurnController>();
        playerController = turnController.GetCurrentPlayer();
        boardManager = FindObjectOfType<BoardManager>();
        dice = boardManager.GetComponentInChildren<Dice>();
    }

    private void FixedUpdate()
    {
        DiceBehaviour();

    }

    void DiceBehaviour()
    {
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

    public void RollDice()
    {
        diceRolled = true;
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
    public void ShowCard()
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

    public void MakeSuggestion()
    {
    /*
      Player enters a room
      Player makes a weapon and player suggestion
      if other player has card -> show card
      if no players have the card -> player can choose to make accusation or end turn
     */

    }

    public void MakeAccusation()
    {
       /*Get player 3 chosen cards
        Check the players card against the 3 cards set at the start of the game
       if they match -> player wins
       if they dont match -> player asked to make a second accusation
       if second accusation doesnt not match -> remove player from queue
       */
    }

    public void EndTurn()
    {
        turnController.SetCurrentPlayerToNext();
        canRoll = true;
        secondRollavailable = false;
    }


}
