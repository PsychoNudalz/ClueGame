using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { General, Start, RoomEntry, Shortcut};

public class BoardTileScript : MonoBehaviour
{
    [SerializeField] Vector2 gridPosition;
    [SerializeField] TileType tileType;

    public Vector2 GridPosition { get => gridPosition; set => gridPosition = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    public TileType GetTileType()
    {
        return tileType;
    }

    public virtual void ClearTile()
    {
        print(this + " cleared");
    }

    public virtual void SelectTile()
    {
        print(this + " select");
    }
    
    public void GetTileNeighbours()
    {
       GameObject.FindObjectOfType<BoardManager>().GetTileNeighbours(this.GetComponent<BoardTileScript>());
    }

    override
    public string ToString()
    {
        return tileType + " Tile located at X:" + gridPosition.x + " Y:" + gridPosition.y;
    }
}
