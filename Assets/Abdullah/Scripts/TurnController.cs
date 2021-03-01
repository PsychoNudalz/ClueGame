using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    List<PlayerMasterController> currentPlayers;
    List<PlayerMasterController> initialisePlayers;
    public int currentPlayerIndex;



    void Awake()
    {
        currentPlayers = initialisePlayers;
        currentPlayerIndex = 0;
    }

    PlayerMasterController GetNextPlayer() 
    {
        return currentPlayers[currentPlayerIndex + 1];
    }

    void SetCurrentPlayerToNext() 
    {
        currentPlayerIndex = currentPlayerIndex + 1;
    }

    PlayerMasterController GetCurrentPlayer() 
    {
        return currentPlayers[currentPlayerIndex];
    }

    void RemovePlayer() 
    {
        currentPlayers.RemoveAt(currentPlayerIndex);
    }

    void GetSuggestion() 
    {
        
    }

    void ShowCard() 
    {
    
    }

    void CallNextTurn() 
    {
    
    }

    void Win() 
    {
    
    }
}
