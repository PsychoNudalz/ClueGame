using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This handles the turn between the players
/// </summary>
public class TurnController : MonoBehaviour
{
   [SerializeField] List<PlayerMasterController> currentPlayers;
   [SerializeField] List<PlayerMasterController> initialisePlayers;
    public int currentPlayerIndex;

    public List<PlayerMasterController> CurrentPlayers { get => currentPlayers; set => currentPlayers = value; }

    void Awake()
    {
        //run the StartGame() method in order to 
        //StartGame();
    }

    public void StartGame() 
    {

        InitialisePlayers();
        //pointer to first player
        currentPlayerIndex = 0;
    }

    /// <summary>
    /// Initialises the list with all the players from the gameobject, adding them in order
    /// </summary>
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
                    //print(initialisePlayers[j].GetCharacter().ToString());
                }

            }
        }

    }


    /// <summary>
    /// Creates a list of the players in order without the current player
    /// </summary>
    /// <returns>A list of the players after the current player in order</returns>
    public List<PlayerMasterController> GetRestOfPlayersInOrder() {
        //copy current players into a pseudo list
        List<PlayerMasterController> restOfPlayers = new List<PlayerMasterController>(currentPlayers);
        //create a new list that will have the players in order, exluding the current player
        List<PlayerMasterController> rOPinOrder = new List<PlayerMasterController>();
        //create a new index that has the same value as the currentplayerindex
        int orderIndex = currentPlayerIndex;
        // remove the current player from the pseudo list
        restOfPlayers.RemoveAt(currentPlayerIndex % restOfPlayers.Count);
        // Iterate through the pseudo currentPlayers list, remove the current player from the pseduo list, and start adding the players in order into a new list where index 0 is the next player
        for (int i = 0; i < restOfPlayers.Count;i++) {
            //Anson: changed this painful sphageghtii
            /*
        if (rOPinOrder.Count < restOfPlayers.Count)
        {
            if ((orderIndex + i) > restOfPlayers.Count)
            {
                //if the index reached the end of the list, set index to 0
                orderIndex = 0;
                i = 0;
            }
            rOPinOrder.Add(restOfPlayers[(orderIndex + i)%restOfPlayers.Count]);
        }
        else {
            break;
        }*/

            rOPinOrder.Add(restOfPlayers[(orderIndex + i) % restOfPlayers.Count]);

        }
        // return the rest of the players in order, excluding the current player
        return rOPinOrder;
    }
    

    public PlayerMasterController GetNextPlayer() 
    {
        //if index is at the end of list, get the start of the list
        if (currentPlayerIndex == currentPlayers.Count) 
        {
            return currentPlayers[currentPlayerIndex = 0];
        }
        else
        {
            //get the next player
            return currentPlayers[(currentPlayerIndex + 1) % currentPlayers.Count];
        }
    }

    
    public bool SetCurrentPlayerToNext() 
    {
        int loopFlag = 0;
        do
        {
            loopFlag++;
            if (currentPlayerIndex == currentPlayers.Count)
            {
                //change index to 0 if the last player ends turn
                currentPlayerIndex = 0;
            }
            else
            {
                //incriment the player index to advance to the next player
                currentPlayerIndex++;
                currentPlayerIndex = currentPlayerIndex % currentPlayers.Count;
            }
        } while (currentPlayers[currentPlayerIndex].IsEliminated()&&loopFlag<=currentPlayers.Count*2);
        if(loopFlag >=currentPlayers.Count)
        {
            Debug.LogWarning("Loop detected, game over");
            return false;
        }

        return true;
    }

    public PlayerMasterController GetCurrentPlayer() 
    {
        //return current player
        return currentPlayers[currentPlayerIndex];
    }


    /// <summary>
    /// remove current player from the turn list
    /// </summary>
    public void RemovePlayer() 
    {
        //remove current player
        currentPlayers.RemoveAt(currentPlayerIndex);
    }

}
