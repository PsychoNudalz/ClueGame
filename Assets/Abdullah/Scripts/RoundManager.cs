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

    private void Awake()
    {
        turnController = FindObjectOfType<TurnController>();
        playerController = turnController.GetCurrentPlayer();
        boardManager = FindObjectOfType<BoardManager>();
    }

    void RollDice() 
    {
        dice.RollDice();
    }

    public void MovePlayer(BoardTileScript b) 
    {
        if (playerController == null)
        {
            playerController = turnController.GetCurrentPlayer();

        }
        playerController.MovePlayer(b);
        if (boardManager == null)
        {
            boardManager = FindObjectOfType<BoardManager>();

        }
        boardManager.ClearMovable();
        
    }
    void ShowCard() 
    {
      
    }

    void MakeSuggestion()
    {
    
    }

    void MakeAccusation() 
    {
        
    }

    void EndTurn()
    {
       
        player.SetCurrentPlayerToNext();
    }
}
