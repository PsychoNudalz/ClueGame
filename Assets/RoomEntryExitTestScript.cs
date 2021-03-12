using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomEntryExitTestScript : MonoBehaviour
{
    [SerializeField] PlayerTokenScript[] players;
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
        foreach(PlayerTokenScript player in players)
        {
            player.SetCharacter(player.Character);
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
        string text = "";
        for(int i = 0; i < room.PlayerSlots.Length; i++)
        {
            PlayerTokenScript player = room.PlayerSlots[i].GetCharacterInSlot();

            if(player != null)
            {
                text += String.Format("Slot {0} : {1}\n", i + 1, player.CharacterName); 
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
        PlayerTokenScript player = null;
        foreach(PlayerTokenScript playerTokenScript in GameObject.FindObjectsOfType<PlayerTokenScript>())
        {
            if (playerTokenScript.Character.ToString().Equals(character))
            {
                player = playerTokenScript;
                break;
            }
        }
        player.transform.position = entries[0].transform.position;
    }

    public void EnterRoom2(string character)
    {
        PlayerTokenScript player = null;
        foreach (PlayerTokenScript playerTokenScript in GameObject.FindObjectsOfType<PlayerTokenScript>())
        {
            if (playerTokenScript.Character.ToString().Equals(character))
            {
                player = playerTokenScript;
                break;
            }
        }
        player.transform.position = entries[1].transform.position;
    }

    public void ExitRoom1(string character)
    {
        PlayerTokenScript player = null;
        foreach (PlayerTokenScript playerTokenScript in GameObject.FindObjectsOfType<PlayerTokenScript>())
        {
            if (playerTokenScript.Character.ToString().Equals(character))
            {
                player = playerTokenScript;
                break;
            }
        }
        room.RemovePlayerFromRoom(player, targetTiles[0]);
    }

    public void ExitRoom2(string character)
    {
        PlayerTokenScript player = null;
        foreach (PlayerTokenScript playerTokenScript in GameObject.FindObjectsOfType<PlayerTokenScript>())
        {
            if (playerTokenScript.Character.ToString().Equals(character))
            {
                player = playerTokenScript;
                break;
            }
        }
        room.RemovePlayerFromRoom(player, targetTiles[1]);
    }
}
