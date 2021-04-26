using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for the room entry board tile GameObject
/// </summary>
public class RoomEntryBoardTileScript : BoardTileScript
{
    [SerializeField] private RoomEnum room;
    private RoomEntryPoint entryPoint;
    private RoomScript roomScript;
    private DoorScript door;

    public RoomEnum Room { get => room; set => room = value; }
    public RoomScript RoomScript { get => roomScript; set => roomScript = value; }
    public RoomEntryPoint EntryPoint { get => entryPoint; set => entryPoint = value; }
    BoardTileScript exitTarget;

    /// <summary>
    /// Set required Variables
    /// </summary>
    private void Start()
    {
        base.Init();
        GetRoomScript();
        GetDoor();
        GetEntryPoint();
        entryPoint.RoomScript = roomScript;
    }
    /// <summary>
    /// Set the reference to the closest door on the board
    /// </summary>
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
    /// <summary>
    /// Set the reference to the entry tilse roomScript
    /// </summary>
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
    /// <summary>
    /// Set reference to the closest room entry point on the board
    /// </summary>
    private void GetEntryPoint()
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
    /// <summary>
    /// Start entering player to room
    /// </summary>
    /// <param name="player">Player to enter room</param>
    internal void EnterRoom(PlayerMasterController player)
    {
        StartCoroutine(EnterRoomAnimation(player));
    }
    /// <summary>
    /// Open door, pause start player entry, pause then close door
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    IEnumerator EnterRoomAnimation(PlayerMasterController player)
    {
        door.OpenDoor();
        yield return new WaitForSeconds(0.8f);
        player.SetCurrentTile(this);
        player.EnterRoom(entryPoint);
        yield return new WaitForSeconds(2f);
        door.CloseDoor();

    }
    /// <summary>
    /// Overidden ToString Method
    /// </summary>
    /// <returns>Description of the tile</returns>
    override
    public string ToString()
    {
        return $"{TileType} Tile ({room}) located at ({GridPosition.x} : {GridPosition.y})";
    }
    /// <summary>
    /// Start player exit from room
    /// </summary>
    /// <param name="playerToRemove">Player to exit</param>
    /// <param name="targetTile">target tile to move player to</param>
    internal void ExitRoom(PlayerMasterController playerToRemove, BoardTileScript targetTile)
    {
        StartCoroutine(ExitRoomDelay(playerToRemove, targetTile));
    }
    /// <summary>
    /// Move player to exit point, open door, exit player, close door, set player moving to target tile.
    /// </summary>
    /// <param name="playerToRemove">Player to exit</param>
    /// <param name="targetTile">Tile to move player to on exit</param>
    public IEnumerator ExitRoomDelay(PlayerMasterController playerToRemove, BoardTileScript targetTile)
    {
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
