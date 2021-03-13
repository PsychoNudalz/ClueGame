using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntryBoardTileScript : BoardTileScript
{
    [SerializeField] private Room room;
    private RoomEntryPoint entryPoint;
    private RoomScript roomScript;
    DoorScript door;

    public Room Room { get => room; set => room = value; }
    public RoomScript RoomScript { get => roomScript; set => roomScript = value; }
    public RoomEntryPoint EntryPoint { get => entryPoint; set => entryPoint = value; }

    private void Start()
    {
        GetRoomScript();
        GetDoor();
        GetEntryPoint();
        entryPoint.RoomScript = roomScript;
    }

    private void GetDoor()
    {
        DoorScript closest = null;
        float minDist = Mathf.Infinity;
        foreach (DoorScript door in GameObject.FindObjectsOfType<DoorScript>())
        {
            float dist = Vector3.Distance(door.transform.position, transform.position);
            if (dist < minDist)
            {
                closest = door;
                minDist = dist;
            }
        }
        door = closest;
    }

    private void GetRoomScript()
    {
        foreach (RoomScript tempRoomScript in GameObject.FindObjectsOfType<RoomScript>())
        {
            if (Room.Equals(tempRoomScript.Room))
            {
                roomScript = tempRoomScript;
                break;
            }
        }
    }

    public void GetEntryPoint()
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
        entryPoint = closest;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Equals("Player") && other.transform.position == transform.position)
        {
            PlayerTokenScript player = other.GetComponent<PlayerTokenScript>();
            if (!player.IsInRoom())
            {
                StartCoroutine(EnterRoom(player));
            }
        }
    }

    IEnumerator EnterRoom(PlayerTokenScript player)
    {
        door.OpenDoor();
        yield return new WaitForSeconds(0.8f);
        player.CurrentTile = this;
        player.EnterRoom(entryPoint);
        yield return new WaitForSeconds(2f);
        door.CloseDoor();

    }

    override
    public string ToString()
    {
        return $"{TileType} Tile ({room}) located at ({GridPosition.x} : {GridPosition.y})";
    }

    internal void exitRoom()
    {
        
    }

    public IEnumerator ExitRoom(PlayerTokenScript playerToRemove, BoardTileScript targetTile)
    {
        //print(playerToRemove.Character + " exiting via " + this.transform);
        playerToRemove.transform.position = entryPoint.transform.position;
        door.OpenDoor();
        yield return new WaitForSeconds(1f);
        playerToRemove.ExitRoom(this, targetTile);
        yield return new WaitForSeconds(1.5f);
        door.CloseDoor();

    }
}
