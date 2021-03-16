using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerSlot : MonoBehaviour
{
    PlayerMasterController playerInSlot;

    public void AddPlayerToSlot(PlayerMasterController player)
    {
        if (!SlotOccupied())
        {
            playerInSlot = player;
        }
    }

    public PlayerMasterController RemovePlayerFromSlot()
    {
        PlayerMasterController playerToReturn = playerInSlot;
        playerInSlot = null;
        return playerToReturn;
    }

    public bool SlotOccupied()
    {
        return (playerInSlot != null);
    }

    public PlayerMasterController GetCharacterInSlot()
    {
        return playerInSlot;
    }
}
