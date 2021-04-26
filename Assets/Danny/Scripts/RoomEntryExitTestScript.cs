using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class for player control in the room test scene
/// </summary>
public class RoomEntryExitTestScript : MonoBehaviour
{
    [SerializeField] PlayerMasterController[] players;
    private PlayerTokenScript[] playerTokens; 
    private RoomScript room;
    [SerializeField] RoomEntryBoardTileScript[] entries;
    [SerializeField] BoardTileScript[] targetTiles;
    [SerializeField] Text playersInRoom;

    [SerializeField] Button[] missScarlettEntry;
    [SerializeField] Button[] missScarlettExit;
    [SerializeField] Button[] profPlumEntry;
    [SerializeField] Button[] profPlumExit;
    [SerializeField] Button[] colMustardEntry;
    [SerializeField] Button[] colMustardExit;
    [SerializeField] Button[] mrsPeacockEntry;
    [SerializeField] Button[] mrsPeacockExit;
    [SerializeField] Button[] revGreenEntry;
    [SerializeField] Button[] revGreenExit;
    [SerializeField] Button[] mrsWhiteEntry;
    [SerializeField] Button[] mrsWhiteExit;

    /// <summary>
    /// Set required variables
    /// </summary>
    private void Start()
    {
        room = GameObject.FindObjectOfType<RoomScript>();
        playerTokens = FindObjectsOfType<PlayerTokenScript>();
        foreach(PlayerTokenScript playerToken in playerTokens)
        {
            playerToken.SetCharacter(playerToken.Character);
        }
    }
    /// <summary>
    /// Update players in room text and buttons interacable
    /// </summary>
    private void Update()
    {
        SetPlayersInRoomText();
        SetButtonsEnabled();
    }
    /// <summary>
    /// Set buttons interacable
    /// </summary>
    private void SetButtonsEnabled()
    {
        foreach(Button button in missScarlettEntry)
        {
            button.interactable = !players[0].IsInRoom();
        }
        foreach (Button button in missScarlettExit)
        {
            button.interactable = players[0].IsInRoom();
        }
        foreach (Button button in profPlumEntry)
        {
            button.interactable = !players[1].IsInRoom();
        }
        foreach (Button button in profPlumExit)
        {
            button.interactable = players[1].IsInRoom();
        }
        foreach (Button button in colMustardEntry)
        {
            button.interactable = !players[2].IsInRoom();
        }
        foreach (Button button in colMustardExit)
        {
            button.interactable = players[2].IsInRoom();
        }
        foreach (Button button in mrsPeacockEntry)
        {
            button.interactable = !players[3].IsInRoom();
        }
        foreach (Button button in mrsPeacockExit)
        {
            button.interactable = players[3].IsInRoom();
        }
        foreach (Button button in revGreenEntry)
        {
            button.interactable = !players[4].IsInRoom();
        }
        foreach (Button button in revGreenExit)
        {
            button.interactable = players[4].IsInRoom();
        }
        foreach (Button button in mrsWhiteEntry)
        {
            button.interactable = !players[5].IsInRoom();
        }
        foreach (Button button in mrsWhiteExit)
        {
            button.interactable = players[5].IsInRoom();
        }
    }
    /// <summary>
    /// Set players in room text
    /// </summary>
    private void SetPlayersInRoomText()
    {
        string text = "Characters in room\n";
        for(int i = 0; i < room.PlayerSlots.Length; i++)
        {
            PlayerMasterController player = room.PlayerSlots[i].GetCharacterInSlot();

            if(player != null)
            {
                text += String.Format("Slot {0} : {1}\n", i + 1, player.GetCharacter()); 
            }
            else
            {
                text += String.Format("Slot {0} : Empty\n", i + 1);
            }
        }
        playersInRoom.text = text;
    }
    /// <summary>
    /// Player enter room at entry point 1
    /// </summary>
    /// <param name="character">Player to enter</param>
    public void EnterRoom1(string character)
    {
        PlayerMasterController player = null;
        foreach(PlayerMasterController playerController in GameObject.FindObjectsOfType<PlayerMasterController>())
        {
            if (playerController.GetCharacter().ToString().Equals(character))
            {
                player = playerController;
                break;
            }
        }
        player.SetPosition(entries[0].transform.position);
        entries[0].EnterRoom(player);
    }

    /// <summary>
    /// Player enter room at entry point 2
    /// </summary>
    /// <param name="character">Player to enter</param>
    public void EnterRoom2(string character)
    {
        PlayerMasterController player = null;
        foreach (PlayerMasterController playerController in GameObject.FindObjectsOfType<PlayerMasterController>())
        {
            if (playerController.GetCharacter().ToString().Equals(character))
            {
                player = playerController;
                break;
            }
        }
        player.SetPosition(entries[1].transform.position);
        entries[1].EnterRoom(player);
    }
    /// <summary>
    /// Player exit room at entry point 1
    /// </summary>
    /// <param name="character">Player to exit</param>
    public void ExitRoom1(string character)
    {
        PlayerMasterController player = null;
        foreach (PlayerMasterController playerController in GameObject.FindObjectsOfType<PlayerMasterController>())
        {
            if (playerController.GetCharacter().ToString().Equals(character))
            {
                player = playerController;
                break;
            }
        }
        room.RemovePlayerFromRoom(player, targetTiles[0]);
    }
    /// <summary>
    /// Player exit room at entry point 2
    /// </summary>
    /// <param name="character">Player to exit</param>
    public void ExitRoom2(string character)
    {
        PlayerMasterController player = null;
        foreach (PlayerMasterController playerTokenScript in GameObject.FindObjectsOfType<PlayerMasterController>())
        {
            if (playerTokenScript.GetCharacter().ToString().Equals(character))
            {
                player = playerTokenScript;
                break;
            }
        }
        room.RemovePlayerFromRoom(player, targetTiles[1]);
    }
}
