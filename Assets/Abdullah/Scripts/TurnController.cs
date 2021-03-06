﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    PlayerMasterController addPlayer;
   [SerializeField] List<PlayerMasterController> currentPlayers;
   [SerializeField] List<PlayerMasterController> initialisePlayers;
    public int currentPlayerIndex;
    int numberOfPlayers;

    
    void Awake()
    {
        StartGame();
    }

    void StartGame() 
    {
        InitialisePlayers();
        //pointer to first player
        currentPlayerIndex = 0;
    }
    void InitialisePlayers() 
    {
        currentPlayers = new List<PlayerMasterController>();
        initialisePlayers = new List<PlayerMasterController>(FindObjectsOfType<PlayerMasterController>());
        for (int i = 0; i < 6; i++) 
        {
            for (int j = 0; j < initialisePlayers.Count; j++)
            {

                if ((int)initialisePlayers[j].GetCharacter() == i)
                {
                    currentPlayers.Add(initialisePlayers[j]);
                    print(initialisePlayers[j].GetCharacter().ToString());
                }

            }
        }

    }

    public void SetNumberofPlayers(int a) 
    {
        //set number of players playing the game
        numberOfPlayers = a;
    }


    public PlayerMasterController GetNextPlayer() 
    {
        if (currentPlayerIndex == currentPlayers.Count) 
        {
            return currentPlayers[currentPlayerIndex = 0];
        }
        else
        {
            return currentPlayers[currentPlayerIndex + 1];
        }
    }

    public void SetCurrentPlayerToNext() 
    {
        if (currentPlayerIndex == currentPlayers.Count)
        {
            currentPlayerIndex = 0;
        }
        else
        {
            currentPlayerIndex++;
        }
    }

    public PlayerMasterController GetCurrentPlayer() 
    {
        return currentPlayers[currentPlayerIndex];
    }

    public void RemovePlayer() 
    {
        currentPlayers.RemoveAt(currentPlayerIndex);
    }

    void GetSuggestion() 
    {
        
    }

    public void Accusation() 
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
