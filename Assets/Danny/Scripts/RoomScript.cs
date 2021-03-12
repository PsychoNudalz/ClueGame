﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private ShortcutBoardTileScript shortcutTile;

    public Room Room { get => room; set => room = value; }
    public RoomPlayerSlot[] PlayerSlots { get => playerSlots;}
    public ShortcutBoardTileScript ShortcutTile { get => shortcutTile;}


    // Start is called before the first frame update
    void Start()
    {
        GetEntryTiles();
        playerSlots = GetComponentsInChildren<RoomPlayerSlot>();
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

    private void GetEntryTiles()
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
                return Room.Centre;
        }
    }

    internal void AddPlayer(PlayerTokenScript playerTokenScript)
    {
        foreach(RoomPlayerSlot slot in playerSlots)
        {
            if (!slot.SlotOccupied())
            {
                playerTokenScript.transform.position = slot.transform.position;
                slot.AddPlayerToSlot(playerTokenScript);
                playerTokenScript.CurrentRoom = this;
                print(playerTokenScript.Character + " added in " + slot.transform.ToString() + " in the " + room);
                break;
            }
        }
    }

    internal void RemovePlayerFromRoom(PlayerTokenScript player, BoardTileScript targetTile)
    {
        PlayerTokenScript playerToRemove = null;
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
            StartCoroutine(exitPoint.ExitRoom(playerToRemove, targetTile));
        }
        else
        {
            Debug.LogError(player.Character + " not found in " + Room);
        }
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
}
