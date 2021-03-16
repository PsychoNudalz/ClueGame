using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomEntryExitTestScript : MonoBehaviour
{
    [SerializeField] PlayerMasterController[] players;
    RoomScript room;
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

    private void Start()
    {
        room = GameObject.FindObjectOfType<RoomScript>();
        foreach (PlayerMasterController player in players)
        {
            player.SetCharacter(player.GetCharacter());
        }
    }

    private void Update()
    {
        SetPlayersInRoomText();
        SetButtonsEnabled();
    }

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
        player.transform.position = entries[0].transform.position;
    }

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
        player.transform.position = entries[1].transform.position;
    }

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
