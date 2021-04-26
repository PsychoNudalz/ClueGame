using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/// <summary>
/// Script for the room Gameobject
/// </summary>
public class RoomScript : MonoBehaviour
{
    [SerializeField] private RoomEnum room;
    private RoomEntryBoardTileScript[] entryTiles;
    private RoomPlayerSlot[] playerSlots;
    private RoomWeaponSlot[] weaponSlots;
    private ShortcutBoardTileScript shortcutTile;
    /// <summary>
    /// Getter and setter for Room Type
    /// </summary>
    public RoomEnum Room { get => room; set => room = value; }
    /// <summary>
    /// Getter for PlayerSlots
    /// </summary>
    public RoomPlayerSlot[] PlayerSlots { get => playerSlots;}
    /// <summary>
    /// Getter for Shortcut tile
    /// </summary>
    public ShortcutBoardTileScript ShortcutTile { get => shortcutTile;}
    /// <summary>
    /// Getter for weaponSlots 
    /// </summary>
    public RoomWeaponSlot[] WeaponSlots { get => weaponSlots;}
    /// <summary>
    /// Getter for entrytiles
    /// </summary>
    public RoomEntryBoardTileScript[] EntryTiles { get => entryTiles;}


    /// <summary>
    /// Set required variables
    /// </summary>
    void Start()
    {
        GetEntryTiles();
        playerSlots = GetComponentsInChildren<RoomPlayerSlot>();
        weaponSlots = GetComponentsInChildren<RoomWeaponSlot>();
        GetShortcut();
    }

    /// <summary>
    /// Set shortcut tile of script
    /// </summary>
    private void GetShortcut()
    {
        foreach (ShortcutBoardTileScript shortcutTile in GameObject.FindObjectsOfType<ShortcutBoardTileScript>())
        {
            if (shortcutTile.ShortcutFrom.Equals(room))
            {
                this.shortcutTile = shortcutTile;
            }
        }
    }

    /// <summary>
    /// Gets all entry tiles
    /// </summary>
    /// <returns>An array of EntryTiles</returns>
    public RoomEntryBoardTileScript[] GetEntryTiles()
    {
        List<RoomEntryBoardTileScript> entryTileList = new List<RoomEntryBoardTileScript>();
        foreach (RoomEntryBoardTileScript entryTile in GameObject.FindObjectsOfType<RoomEntryBoardTileScript>())
        {
            if (entryTile.Room.Equals(Room))
            {
                entryTileList.Add(entryTile);
            }
        }
        entryTiles = entryTileList.ToArray();
        return entryTiles;
    }

    /// <summary>
    /// Get Room Enum from string
    /// </summary>
    /// <param name="roomString">String of room to return</param>
    /// <returns>Room Enum represented by string entered - throws exception if room not found</returns>
    public static RoomEnum GetRoomFromString(string roomString)
    {
        switch (roomString)
        {
            case "Ballroom":
                return RoomEnum.Ballroom;
            case "Billiard Room":
                return RoomEnum.BilliardRoom;
            case "Centre":
                return RoomEnum.Centre;
            case "Conservatory":
                return RoomEnum.Conservatory;
            case "Dining Room":
                return RoomEnum.DiningRoom;
            case "Hall":
                return RoomEnum.Hall;
            case "Kitchen":
                return RoomEnum.Kitchen;
            case "Study":
                return RoomEnum.Study;
            case "Library":
                return RoomEnum.Library;
            case "Lounge":
                return RoomEnum.Lounge;
            default:
                throw new Exception("Room enum not found");
        }
    }

    /// <summary>
    /// Add Player to Room
    /// </summary>
    /// <param name="PlayerController">Player to add</param>
    internal void AddPlayer(PlayerMasterController PlayerController)
    {
        foreach(RoomPlayerSlot slot in playerSlots)
        {
            if (!slot.SlotOccupied())
            {
                PlayerController.SetPosition(slot.transform.position);
                slot.AddPlayerToSlot(PlayerController);
                PlayerController.SetCurrentRoom(this);
                //print(playerTokenScript.Character + " added in " + slot.transform.ToString() + " in the " + room);
                break;
            }
        }
    }

    /// <summary>
    /// Remove a Player from room and move to tile
    /// </summary>
    /// <param name="player">Player to remove</param>
    /// <param name="targetTile">Tile to move player to on exit</param>
    internal void RemovePlayerFromRoom(PlayerMasterController player, BoardTileScript targetTile)
    {
        PlayerMasterController playerToRemove = null;
        foreach(RoomPlayerSlot slot in playerSlots)
        {
            if (slot.GetCharacterInSlot() != null && slot.GetCharacterInSlot().Equals(player))
            {
                playerToRemove = slot.RemovePlayerFromSlot();
                break;
            }

        }
        if(playerToRemove != null)
        {
            RoomEntryBoardTileScript exitPoint =  FindClosestEntryTile(targetTile);
            exitPoint.ExitRoom(playerToRemove, targetTile);
            playerToRemove.SetCurrentRoom(null);
        }
        else
        {
            Debug.LogError(player.GetCharacter() + " not found in " + Room);
        }
    }

    /// <summary>
    /// Remove a Player from room
    /// </summary>
    /// <param name="player">Player to remove</param>
    internal void RemovePlayerFromRoom(PlayerMasterController player)
    {
        PlayerMasterController playerToRemove;
        foreach (RoomPlayerSlot slot in playerSlots)
        {
            if (slot.GetCharacterInSlot() != null && slot.GetCharacterInSlot().Equals(player))
            {
                playerToRemove = slot.RemovePlayerFromSlot();
                break;
            }
            else
            {
                Debug.LogWarning(player.GetCharacter() + " not found in " + Room);
            }
        }
    }
    /// <summary>
    /// Remove player from room via shortcut
    /// </summary>
    /// <param name="player">Player to remove</param>
    internal void RemovePlayerFromRoomViaShortcut(PlayerMasterController player)
    {
        PlayerMasterController playerToRemove = null;
        foreach (RoomPlayerSlot slot in playerSlots)
        {
            if (slot.GetCharacterInSlot() != null && slot.GetCharacterInSlot().Equals(player))
            {
                playerToRemove = slot.RemovePlayerFromSlot();
                break;
            }

        }
        if(playerToRemove != null)
        {
            playerToRemove.SetCurrentRoom(null);
        }
    }

    /// <summary>
    /// Add weapon token to room
    /// </summary>
    /// <param name="weaponTokenScript">Weapon to add</param>
    internal void AddWeapon(WeaponTokenScript weaponTokenScript)
    {
        if(weaponTokenScript.CurrentRoom != null)
        {
            weaponTokenScript.CurrentRoom.RemoveWeaponFromRoom(weaponTokenScript);
            weaponTokenScript.CurrentRoom = null;
        }
        foreach (RoomWeaponSlot slot in weaponSlots)
        {
            if (!slot.SlotOccupied())
            {
                //print(weaponTokenScript.WeaponType + " added in " + slot.transform.ToString() + " in the " + room);
                slot.AddWeaponToSlot(weaponTokenScript);
                weaponTokenScript.CurrentRoom = this;
                weaponTokenScript.MoveToken(slot.transform.position);
                break;
            }
        }
    }

    /// <summary>
    /// Remove weapon from room
    /// </summary>
    /// <param name="weapon">Weapon to remove</param>
    /// <returns></returns>
    internal WeaponTokenScript RemoveWeaponFromRoom(WeaponTokenScript weapon)
    {
        WeaponTokenScript weaponToRemove = null;
        foreach (RoomWeaponSlot slot in weaponSlots)
        {
            if (slot.GetWeaponInSlot() != null && slot.GetWeaponInSlot().Equals(weapon))
            {
                weaponToRemove = slot.RemoveWeaponFromSlot();
                break;
            }
        }
        return weaponToRemove;
    }

    /// <summary>
    /// Are weapon slots empty
    /// </summary>
    /// <returns>true if slots empty, false if not</returns>
    public bool WeaponSlotsEmpty()
    {
        bool result = true;
        foreach(RoomWeaponSlot slot in weaponSlots)
        {
            if (slot.SlotOccupied())
            {
                result = false;
            }
        }
        return result;
    }

    /// <summary>
    /// Returns the closest entry tile to target exit tile
    /// </summary>
    /// <param name="targetTile">Tile to check distance from</param>
    /// <returns>Entry tile closest to target tile</returns>
    private RoomEntryBoardTileScript FindClosestEntryTile(BoardTileScript targetTile)
    {
        RoomEntryBoardTileScript closest = null;
        float minDist = Mathf.Infinity;
        foreach (RoomEntryBoardTileScript tileScript in entryTiles)
        {
            float dist = Vector3.Distance(tileScript.transform.position, targetTile.transform.position);
            if (dist < minDist)
            {
                closest = tileScript;
                minDist = dist;
            }
        }
        return closest;
    }

    /// <summary>
    /// Does the room have a shortcut tile
    /// </summary>
    /// <returns>true if room has shortcut, false if not</returns>
    public bool HasShortcut()
    {
        return (shortcutTile != null);
    }

    /// <summary>
    /// Move a weapon to the room
    /// </summary>
    /// <param name="weaponEnum">Weapon to move</param>
    public void MoveWeaponToRoom(WeaponEnum weaponEnum)
    {
        WeaponTokenScript[] weapons = FindObjectsOfType<WeaponTokenScript>();
        WeaponTokenScript weaponToMove = null;
        foreach(WeaponTokenScript weapon in weapons)
        {
            if (weapon.WeaponType.Equals(weaponEnum))
            {
                weaponToMove = weapon;
                break;
            }
        }
        if(weaponToMove != null && weaponToMove.CurrentRoom.room != room)
        {
            AddWeapon(weaponToMove);
        }
    }

    /// <summary>
    /// Move a player to the room
    /// </summary>
    /// <param name="characterEnum">Player to move</param>
    public void MovePlayerToRoom(CharacterEnum characterEnum)
    {
        PlayerMasterController[] players = FindObjectsOfType<PlayerMasterController>();
        PlayerMasterController playerToMove = null;
        foreach(PlayerMasterController player in players)
        {
            if (player.GetCharacter().Equals(characterEnum))
            {
                playerToMove = player;
                break;
            }
        }
        if (playerToMove != null)
        {
            if (playerToMove.IsInRoom())
            {
                playerToMove.GetCurrentRoom().RemovePlayerFromRoom(playerToMove);
            }
            playerToMove.ClearTokenTile();
            AddPlayer(playerToMove);
        }
    }
}
