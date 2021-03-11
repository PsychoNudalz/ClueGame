using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntryBoardTileScript : BoardTileScript
{
    [SerializeField] private Room room;
    private RoomEntryPoint entryPoint;
    private RoomScript roomScript;

    public Room Room { get => room; set => room = value; }
    public RoomScript RoomScript { get => roomScript; set => roomScript = value; }
    public RoomEntryPoint EntryPoint { get => entryPoint; set => entryPoint = value; }

    private void Start()
    {
        foreach (RoomScript tempRoomScript in GameObject.FindObjectsOfType<RoomScript>())
        {
            if (Room.Equals(tempRoomScript.Room))
            {
                roomScript = tempRoomScript;
                break;
            }
        }

        entryPoint = GetEntryPoint();
        entryPoint.RoomScript = roomScript;
    }

    public RoomEntryPoint GetEntryPoint()
    {
        RoomEntryPoint closest = null;
        float minDist = Mathf.Infinity;
        foreach (RoomEntryPoint roomEntryPoint in roomScript.GetComponentsInChildren<RoomEntryPoint>())
        {
            float dist = Vector3.Distance(roomEntryPoint.transform.position, transform.position);
            if (dist < minDist)
            {
                closest = roomEntryPoint;
                minDist = dist;
            }
        }
        return closest;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Equals("Player") && other.transform.position == transform.position)
        {
            PlayerTokenScript player = other.GetComponent<PlayerTokenScript>();
            if (!player.IsInRoom)
            {
                //print("Entering " + room);
                player.CurrentTile = this;
                player.EnterRoom(entryPoint);
            }
        }
    }

    override
    public string ToString()
    {
        return $"{TileType} Tile ({room}) located at ({GridPosition.x} : {GridPosition.y})";
    }

    internal void exitRoom(PlayerTokenScript playerToRemove, BoardTileScript targetTile)
    {
        print(playerToRemove.Character + " exiting via " + this.transform);
        playerToRemove.transform.position = entryPoint.transform.position;
        playerToRemove.ExitRoom(this, targetTile);
    }
}
