using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { General, Start, RoomEntry, Shortcut };

public class BoardTileScript : MonoBehaviour
{
    [SerializeField] Vector2 gridPosition;
    [SerializeField] TileType tileType;
    [SerializeField] BoardTileEffectHandlerScript boardTileEffectHandler;

    public Vector2 GridPosition { get => gridPosition; set => gridPosition = value; }

    // Start is called before the first frame update
    void Start()
    {
        boardTileEffectHandler = GetComponent<BoardTileEffectHandlerScript>();
    }

    public TileType GetTileType()
    {
        return tileType;
    }

    public virtual void ClearTile()
    {
        print(this + " cleared");
        if (boardTileEffectHandler != null)
        {
            boardTileEffectHandler.DeselectTile();
        }
    }

    public virtual void SelectTile()
    {
        print(this + " select");
        if (boardTileEffectHandler != null)
        {
            boardTileEffectHandler.SelectTile();
        }
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
