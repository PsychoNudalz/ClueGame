using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomEntryExitTestScript : MonoBehaviour
{
    Keyboard kb;
    PlayerTokenScript player;
    RoomScript room;
    BoardTileScript targetTile;

    private void Start()
    {
        kb = InputSystem.GetDevice<Keyboard>();
        player = GameObject.FindObjectOfType<PlayerTokenScript>();
        room = GameObject.FindObjectOfType<RoomScript>();
        targetTile = GameObject.FindObjectOfType<BoardTileScript>();
    }

    private void Update()
    {

        if (!player.IsInRoom)
        {
            if (kb.digit1Key.wasReleasedThisFrame)
            {
                player.transform.position = new Vector3(1, 0, 1);
            }
            if (kb.digit2Key.wasReleasedThisFrame)
            {
                player.transform.position = new Vector3(6, 0, -3);
            }
        }
        else
        {
            if (kb.digit3Key.wasReleasedThisFrame)
            {
                room.RemovePlayerFromRoom(player, targetTile);
            }
        }
    }
}
