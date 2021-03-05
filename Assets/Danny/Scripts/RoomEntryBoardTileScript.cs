using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntryBoardTileScript : BoardTileScript
{
    private Room room;

    public Room Room { get => room; set => room = value; }


    override
    public string ToString()
    {
        return $"{TileType} Tile ({room}) located at ({GridPosition.x} : {GridPosition.y})";
    }
}
