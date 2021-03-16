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
    BoardTileScript exitTarget;


    private void Start()
    {
        base.Init();
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
    /*
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Equals("Player") && other.transform.position == transform.position)
        {
            PlayerTokenScript playerToken = other.GetComponent<PlayerTokenScript>();
            PlayerMasterController playerController = playerToken.GetController();
            if (!playerController.IsInRoom())
            {
                StartCoroutine(EnterRoom(playerController));
            }
            else
            {
                playerController.SetCurrentTile(this);
                playerController.MovePlayer(exitTarget);
                exitTarget = null;
                
            }
        }
    }*/

    internal void EnterRoom(PlayerMasterController player)
    {
        StartCoroutine(EnterRoomAnimation(player));
    }

    IEnumerator EnterRoomAnimation(PlayerMasterController player)
    {
        door.OpenDoor();
        yield return new WaitForSeconds(0.8f);
        player.SetCurrentTile(this);
        player.EnterRoom(entryPoint);
        yield return new WaitForSeconds(2f);
        door.CloseDoor();

    }

    override
    public string ToString()
    {
        return $"{TileType} Tile ({room}) located at ({GridPosition.x} : {GridPosition.y})";
    }

    internal void ExitRoom(PlayerMasterController playerToRemove, BoardTileScript targetTile)
    {
        StartCoroutine(ExitRoomDelay(playerToRemove, targetTile));
    }

    public IEnumerator ExitRoomDelay(PlayerMasterController playerToRemove, BoardTileScript targetTile)
    {
        //print(playerToRemove.Character + " exiting via " + this.transform);
        //playerToRemove.CurrentRoom = null;
        playerToRemove.SetPosition(entryPoint.transform.position);
        playerToRemove.SetCurrentTile(this);
        door.OpenDoor();
        yield return new WaitForSeconds(1f);
        playerToRemove.ExitRoom(this, targetTile);
        yield return new WaitForSeconds(1.5f);
        door.CloseDoor();
        exitTarget = targetTile;
        yield return new WaitForSeconds(0.01f);
        playerToRemove.SetCurrentRoom(null);
    }
}
