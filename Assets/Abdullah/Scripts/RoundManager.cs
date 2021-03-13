using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    TurnController player;
    Dice dice;
    TurnController turnController;
    PlayerMasterController playerController;

    private void Awake()
    {
        turnController = FindObjectOfType<TurnController>();
        playerController = turnController.GetCurrentPlayer();
    }

    void RollDice() 
    {
        dice.RollDice();
    }

    public void MovePlayer() 
    {
        playerController.MovePlayer();
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
