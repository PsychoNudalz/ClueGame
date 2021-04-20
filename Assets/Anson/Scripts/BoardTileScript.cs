using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileTypeEnum { General, Start, RoomEntry, Shortcut, FreeRoll, FreeAccusation };
/// <summary>
/// Super class for all tiles
/// </summary>
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

    /// <summary>
    /// initialise and finding the needed componenets for the tile
    /// includes boardTileEffectHandler, boardManager
    /// 
    /// </summary>
    protected void Init()
    {
        boardTileEffectHandler = GetComponent<BoardTileEffectHandlerScript>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    /// <summary>
    /// disable the select effect on the tile
    /// </summary>
    public virtual void ClearTile()
    {
        //print(this + " cleared");
        if (boardTileEffectHandler != null)
        {
            boardTileEffectHandler.DeselectTile();
        }
    }
    /// <summary>
    /// enable the select effect on the tile
    /// </summary>
    public virtual void SelectTile()
    {
        //print(this + " select");
        if (boardTileEffectHandler != null)
        {
            boardTileEffectHandler.SelectTile();
        }
    }


    /// <summary>
    /// setting the glow on the tile
    /// </summary>
    /// <param name="b">if the tile should glow</param>
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
        catch (System.NullReferenceException)
        {
            Debug.LogError(this + " Missing boardTileEffect");
        }
    }
    /// <summary>
    /// setting the player token that is currently on this tile
    /// </summary>
    /// <param name="token"> player token on this tile</param>
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

    /// <summary>
    /// Get neighbouring tiles
    /// </summary>
    public void GetTileNeighbours()
    {
        GameObject.FindObjectOfType<BoardManager>().GetTileNeighbours(this.GetComponent<BoardTileScript>());
    }

    /// <summary>
    /// check if the player can move to this tile
    /// </summary>
    /// <returns>if the player can move to this tile</returns>
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
