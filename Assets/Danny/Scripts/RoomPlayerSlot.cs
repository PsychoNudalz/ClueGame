using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerSlot : MonoBehaviour
{
    PlayerTokenScript playerInSlot;

    public void AddPlayerToSlot(PlayerTokenScript player)
    {
        if (!SlotOccupied())
        {
            playerInSlot = player;
        }
    }

    public PlayerTokenScript RemovePlayerFromSlot()
    {
        PlayerTokenScript playerToReturn = playerInSlot;
        playerInSlot = null;
        return playerToReturn;
    }

    public bool SlotOccupied()
    {
        return (playerInSlot != null);
    }

    public PlayerTokenScript GetCharacterInSlot()
    {
        return playerInSlot;
    }
}
