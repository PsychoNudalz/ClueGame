using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    PlayerMasterController addPlayer;
   [SerializeField] List<PlayerMasterController> currentPlayers;
   [SerializeField] List<PlayerMasterController> initialisePlayers;
    public int currentPlayerIndex;
    int numberOfPlayers;

    public List<PlayerMasterController> CurrentPlayers { get => currentPlayers; set => currentPlayers = value; }

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
        //set number of players playing the game, rest will be AI
        numberOfPlayers = a;
    }


    public PlayerMasterController GetNextPlayer() 
    {
        //Anson: this is causing index out of range error, changed it to mod to fix it
        /*
        if (currentPlayerIndex == currentPlayers.Count) 
        {
            return currentPlayers[currentPlayerIndex = 0];
        }
        else
        {
            return currentPlayers[currentPlayerIndex + 1];
        }
        */
     
        return currentPlayers[(currentPlayerIndex + 1) % currentPlayers.Count];
    }

    public void SetCurrentPlayerToNext() 
    {
        //Anson: this is causing index out of range error, changed it to mod to fix it
        /*
        if (currentPlayerIndex == currentPlayers.Count)
        {
            currentPlayerIndex = 0;
        }
        else
        {
            currentPlayerIndex++;
        }
        */
        currentPlayerIndex++;
        currentPlayerIndex =  currentPlayerIndex % currentPlayers.Count;
    }

    public PlayerMasterController GetCurrentPlayer() 
    {
        //Anson: this is causing index out of range error, changed it to mod to fix it

        return currentPlayers[currentPlayerIndex];
    }

    public void RemovePlayer() 
    {
        currentPlayers.RemoveAt(currentPlayerIndex);
    }

    public void GetSuggestion() 
    {
        
    }

    public void Accusation() 
    { 
        
    }

    public void ShowCard() 
    {
    
    }

    public void CallNextTurn() 
    {
        
    }

    public void Win() 
    {
        //
        print("You Win!");
    }
}
