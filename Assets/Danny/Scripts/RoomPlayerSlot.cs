using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class for the Room player slot GameObject
/// </summary>
public class RoomPlayerSlot : MonoBehaviour
{
    private PlayerMasterController playerInSlot;
    /// <summary>
    /// Add Player to the slot if not occupied
    /// </summary>
    /// <param name="player">Player to add to slot</param>
    public void AddPlayerToSlot(PlayerMasterController player)
    {
        if (!SlotOccupied())
        {
            playerInSlot = player;
        }
    }
    /// <summary>
    /// Remove player from slot
    /// </summary>
    /// <returns>Player removed from slot</returns>
    public PlayerMasterController RemovePlayerFromSlot()
    {
        PlayerMasterController playerToReturn = playerInSlot;
        playerInSlot = null;
        return playerToReturn;
    }
    /// <summary>
    /// Is slot occupied
    /// </summary>
    /// <returns>true if slot occupied, false if not</returns>
    public bool SlotOccupied()
    {
        return (playerInSlot != null);
    }
    /// <summary>
    /// Returns the player in the slot
    /// </summary>
    /// <returns>Player in the slot</returns>
    public PlayerMasterController GetCharacterInSlot()
    {
        return playerInSlot;
    }
}
