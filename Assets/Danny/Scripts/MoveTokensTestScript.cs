using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class to run the move tokens test scene
/// </summary>
public class MoveTokensTestScript : MonoBehaviour
{
    private RoomScript currentRoom;
    [SerializeField] private Text currentRoomText;
    private RoomScript[] rooms;
    /// <summary>
    /// Assign required variables
    /// </summary>
    private void Start()
    {
        rooms = FindObjectsOfType<RoomScript>();
    }
    /// <summary>
    /// Set the current room to move tokens to
    /// </summary>
    /// <param name="roomString">Room to set</param>
    public void SetCurrentRoom(string roomString)
    {
        RoomEnum roomEnum = RoomScript.GetRoomFromString(roomString);
        foreach(RoomScript room in rooms)
        {
            if (room.Room.Equals(roomEnum))
            {
                currentRoom = room;
            }
        }
        currentRoomText.text = "Current Room:\n " + currentRoom.Room.ToString();
    }
    /// <summary>
    /// Move weapon token to set room
    /// </summary>
    /// <param name="weaponString">Weapon to move</param>
    public void MoveWeapon(string weaponString)
    {
        WeaponEnum weaponToMove = WeaponTokenScript.GetWeaponEnumFromString(weaponString);
        if(currentRoom != null)
        {
            currentRoom.MoveWeaponToRoom(weaponToMove);
        }
    }
    /// <summary>
    /// Move player token to set room
    /// </summary>
    /// <param name="characterString">Character to move</param>
    public void MovePlayer(string characterString)
    {
        CharacterEnum characterToMove = PlayerTokenScript.GetCharacterEnumFromString(characterString);
        if(currentRoom != null)
        {
            currentRoom.MovePlayerToRoom(characterToMove);
        }
    }
}
