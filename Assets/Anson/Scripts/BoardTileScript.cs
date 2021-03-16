using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileTypeEnum { General, Start, RoomEntry, Shortcut };

public class BoardTileScript : MonoBehaviour
{
    [SerializeField] Vector2 gridPosition;
    private TileTypeEnum tileType;
    [SerializeField] BoardTileEffectHandlerScript boardTileEffectHandler;
    [SerializeField] GameObject playerToken;
    [SerializeField] BoardManager boardManager;

    public Vector2 GridPosition { get => gridPosition; set => gridPosition = value; }
    public TileTypeEnum TileType { get => tileType; set => tileType = value; }
    public GameObject PlayerToken { get => playerToken; set => playerToken = value; }
    public BoardManager BoardManager { get => boardManager; set => boardManager = value; }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected void Init()
    {
        boardTileEffectHandler = GetComponent<BoardTileEffectHandlerScript>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    public virtual void ClearTile()
    {
        //print(this + " cleared");
        if (boardTileEffectHandler != null)
        {
            boardTileEffectHandler.DeselectTile();
        }
    }

    public virtual void SelectTile()
    {
        //print(this + " select");
        if (boardTileEffectHandler != null)
        {
            boardTileEffectHandler.SelectTile();
        }
    }

    public virtual void GlowTile(bool b)
    {
        try
        {
            if (boardTileEffectHandler == null)
            {
                Debug.LogError(this + " Missing boardTileEffect");
                return;
            }
            if (b)
            {
                boardTileEffectHandler.ToggleEffect_On();
            }
            else
            {
                boardTileEffectHandler.ToggleEffect_Off();
            }
        }
        catch (System.NullReferenceException _)
        {
            Debug.LogError(this + " Missing boardTileEffect");
        }
    }

    public virtual void SetToken(GameObject token)
    {
        playerToken = token;
    }
    /// <summary>
    /// check if tile is empty
    /// </summary>
    /// <returns>return if tile is empty</returns>
    public virtual bool IsEmpty()
    {
        return playerToken == null;
    } 

    public void GetTileNeighbours()
    {
        GameObject.FindObjectOfType<BoardManager>().GetTileNeighbours(this.GetComponent<BoardTileScript>());
    }

    public bool CanMove()
    {
        if (boardManager)
        {
        return boardManager.CanMove(this);

        }
        return false;
    }

    override
    public string ToString()
    {
        return $"{TileType} Tile located at ({GridPosition.x} : {GridPosition.y})";
    }
}
