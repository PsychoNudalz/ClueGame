using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Room {
    None,
    Ballroom,
    BilliardRoom,
    Centre,
    Conservatory,
    DiningRoom,
    Hall,
    Kitchen,
    Study,
    Library,
    Lounge
}
    
public class RoomScript : MonoBehaviour
{
    [SerializeField] Room room;
    private RoomEntryBoardTileScript[] entryTiles;
    private RoomPlayerSlot[] playerSlots;
    private RoomWeaponSlot[] weaponSlots;
    private ShortcutBoardTileScript shortcutTile;

    public Room Room { get => room; set => room = value; }
    public RoomPlayerSlot[] PlayerSlots { get => playerSlots;}
    public ShortcutBoardTileScript ShortcutTile { get => shortcutTile;}
    public RoomWeaponSlot[] WeaponSlots { get => weaponSlots;}
    public RoomEntryBoardTileScript[] EntryTiles { get => entryTiles;}


    // Start is called before the first frame update
    void Start()
    {
        GetEntryTiles();
        playerSlots = GetComponentsInChildren<RoomPlayerSlot>();
        weaponSlots = GetComponentsInChildren<RoomWeaponSlot>();
        GetShortcut();
    }

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

    public static Room GetRoomFromString(string roomString)
    {
        switch (roomString)
        {
            case "Ballroom":
                return Room.Ballroom;
            case "Billiard Room":
                return Room.BilliardRoom;
            case "Centre":
                return Room.Centre;
            case "Conservatory":
                return Room.Conservatory;
            case "Dining Room":
                return Room.DiningRoom;
            case "Hall":
                return Room.Hall;
            case "Kitchen":
                return Room.Kitchen;
            case "Study":
                return Room.Study;
            case "Library":
                return Room.Library;
            case "Lounge":
                return Room.Lounge;
            default:
                throw new Exception("Room enum not found");
        }
    }

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

    internal void RemovePlayerFromRoom(PlayerMasterController player)
    {
        PlayerMasterController playerToRemove = null;
        foreach (RoomPlayerSlot slot in playerSlots)
        {
            if (slot.GetCharacterInSlot() != null && slot.GetCharacterInSlot().Equals(player))
            {
                playerToRemove = slot.RemovePlayerFromSlot();
                break;
            }
            else
            {
                Debug.LogError(player.GetCharacter() + " not found in " + Room);
            }
        }
    }

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

    public bool HasShortcut()
    {
        return (shortcutTile != null);
    }

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
            AddPlayer(playerToMove);
        }
    }
}
