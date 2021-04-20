using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTokensTestScript : MonoBehaviour
{
    RoomScript currentRoom;
    [SerializeField] Text currentRoomText;
    RoomScript[] rooms;

    private void Start()
    {
        rooms = FindObjectsOfType<RoomScript>();
    }

    public void SetCurrentRoom(string roomString)
    {
        Room roomEnum = RoomScript.GetRoomFromString(roomString);
        foreach(RoomScript room in rooms)
        {
            if (room.Room.Equals(roomEnum))
            {
                currentRoom = room;
            }
        }
        currentRoomText.text = "Current Room:\n " + currentRoom.Room.ToString();
    }

    public void MoveWeapon(string weaponString)
    {
        WeaponEnum weaponToMove = WeaponTokenScript.GetWeaponEnumFromString(weaponString);
        if(currentRoom != null)
        {
            currentRoom.MoveWeaponToRoom(weaponToMove);
        }
    }

    public void MovePlayer(string characterString)
    {
        CharacterEnum characterToMove = PlayerTokenScript.GetCharacterEnumFromString(characterString);
        if(currentRoom != null)
        {
            currentRoom.MovePlayerToRoom(characterToMove);
        }
    }
}
