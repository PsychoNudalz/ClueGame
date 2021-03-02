using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    TurnController player;
    Dice dice;
    PlayerMasterController playerController;
    void RollDice() 
    {
        dice.RollDice();
    }

    void MovePlayer() 
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
