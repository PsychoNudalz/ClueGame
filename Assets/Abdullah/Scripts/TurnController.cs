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

    public List<PlayerMasterController> GetRestOfPlayersInOrder() {
        List<PlayerMasterController> restOfPlayers = new List<PlayerMasterController>();
        List<PlayerMasterController> rOPinOrder = new List<PlayerMasterController>();
        restOfPlayers = currentPlayers;
        int orderIndex = currentPlayerIndex;
        restOfPlayers.RemoveAt(currentPlayerIndex);
        for (int i = 0; i < restOfPlayers.Count;i++) {
            if (rOPinOrder.Count < restOfPlayers.Count)
            {
                if ((orderIndex + i) > restOfPlayers.Count)
                {
                    orderIndex = 0;
                    i = 0;
                }
                rOPinOrder.Add(restOfPlayers[orderIndex + i]);
            }
            else {
                break;
            }
        }

        return rOPinOrder;
    }
    

    public void SetNumberofPlayers(int a) 
    {
        //set number of players playing the game, rest will be AI
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
