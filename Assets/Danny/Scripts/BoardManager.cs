using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/// <summary>
/// Script to manage the board items
/// </summary>
public class BoardManager : MonoBehaviour
{
    /*
     * Arrays to store each type of game element
     */
    private BoardTileScript[][] boardTiles;
    private PlayerTokenScript[] players;
    private RoomScript[] rooms;
    private StartTileScript[] startTiles;
    private ShortcutBoardTileScript[] shortcuts;
    private RoomEntryBoardTileScript[] roomEntries;
    private FreeRollBoardTileScript[] freeRollTiles;
    private FreeSuggestionTileScript[] freeSuggestionTiles;
    private WeaponTokenScript[] weaponTokens;

    [Header("Displaying Moveable Tiles")]
    [SerializeField] List<BoardTileScript> movableTile;

    public PlayerTokenScript[] Players { get => players; }
    public RoomScript[] Rooms { get => rooms; }
    public StartTileScript[] StartTiles { get => startTiles; }
    public ShortcutBoardTileScript[] Shortcuts { get => shortcuts; }
    public RoomEntryBoardTileScript[] RoomEntries { get => roomEntries; set => roomEntries = value; }
    public WeaponTokenScript[] WeaponTokens { get => weaponTokens; set => weaponTokens = value; }
    public FreeRollBoardTileScript[] FreeRollTiles { get => freeRollTiles; set => freeRollTiles = value; }
    public List<BoardTileScript> MovableTile { get => movableTile; }
    public FreeSuggestionTileScript[] FreeSuggestionTiles { get => freeSuggestionTiles; set => freeSuggestionTiles = value; }
    
    /// <summary>
    /// Return array of all neighbours of given tile
    /// </summary>
    /// <param name="tilescript">Tile to check from</param>
    /// <returns>Array of all neighbouring tiles</returns>
    public BoardTileScript[] GetTileNeighbours(BoardTileScript tilescript)
    {
        List<BoardTileScript> neighboursToReturn = new List<BoardTileScript>();
        int x = (int)tilescript.GridPosition.x;
        int y = (int)tilescript.GridPosition.y;
        BoardTileScript neighbour;
        /*
         * Check up 1 position
         */
        neighbour = GetTileFromGrid(x, y + 1);
        if (neighbour != null)
        {
            neighboursToReturn.Add(neighbour);
        }
        /*
         * Check down 1 position
         */
        neighbour = GetTileFromGrid(x, y - 1);
        if (neighbour != null)
        {
            neighboursToReturn.Add(neighbour);
        }
        /*
         * Check left 1 position
         */
        neighbour = GetTileFromGrid(x - 1, y);
        if (neighbour != null)
        {
            neighboursToReturn.Add(neighbour);
        }
        /*
         * Check right 1 position
         */
        neighbour = GetTileFromGrid(x + 1, y);
        if (neighbour != null)
        {
            neighboursToReturn.Add(neighbour);
        }

        return neighboursToReturn.ToArray();
    }

    /// <summary>
    /// Create 2d array of all board tiles
    /// </summary>
    /// <param name="tiles">All board tiles</param>
    /// <param name="boardHeight">Height of the board</param>
    /// <param name="boardWidth">Width of the board</param>
    public void CreateBoardArray(BoardTileScript[] tiles, int boardHeight, int boardWidth)
    {
        boardTiles = new BoardTileScript[boardHeight][];
        for (int row = 0; row < boardHeight; row++)
        {
            BoardTileScript[] rowArray = new BoardTileScript[boardWidth];
            boardTiles[row] = rowArray;
        }
        foreach (BoardTileScript tile in tiles)
        {
            boardTiles[(int)tile.GridPosition.y][(int)tile.GridPosition.x] = tile;
        }
        /*
         * For Testing
         */
        //PrintArray();
    }

    /// <summary>
    /// Get a tile from the array using a grid reference. 
    /// </summary>
    /// <param name="XPos">X position</param>
    /// <param name="YPos">Y position</param>
    /// <returns>Board tile at location, null if no tile exists at location.</returns>
    public BoardTileScript GetTileFromGrid(int XPos, int YPos)
    {
        if (XPos < boardTiles[0].Length && YPos < boardTiles.Length && XPos >= 0 && YPos >= 0)
        {
            return boardTiles[YPos][XPos];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Start coroutine to place all the weapons after a delay.
    /// </summary>
    internal void PlaceWeapons()
    {
        StartCoroutine(PlaceWeaponsDelay());
    }

    /// <summary>
    /// Coroutine to randomly place weapon tokens in rooms with no more than 1 per room
    /// </summary>
    IEnumerator PlaceWeaponsDelay()
    {
        yield return new WaitForSeconds(0.01f);
        //print("Placing Weapons");
        foreach (WeaponTokenScript weapon in weaponTokens)
        {
            while (weapon.CurrentRoom == null)
            {
                int rand = Random.Range(0, rooms.Length);
                RoomScript roomToTry = rooms[rand];
                if (roomToTry.Room != RoomEnum.None && roomToTry.Room != RoomEnum.Centre && roomToTry.WeaponSlotsEmpty())
                {
                    roomToTry.AddWeapon(weapon);
                }
            }
        }
    }

    /// <summary>
    /// Print Array for Testing
    /// </summary>
    private void PrintArray()
    {
        for (int row = 0; row < boardTiles.Length; row++)
        {
            for (int col = 0; col < boardTiles[row].Length; col++)
            {
                BoardTileScript tile = GetTileFromGrid(col, row);
                if (tile != null)
                {
                    BoardTileScript[] neighbours = GetTileNeighbours(tile);
                    print(tile + " neighbours = " + neighbours.Length);
                    foreach (BoardTileScript neighbour in neighbours)
                    {
                        print("\t" + tile + "==>> Neighbour ==>> " + neighbour);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Perform bfs search for the tiles
    /// </summary>
    /// <param name="currentTile">Tile to search from</param>
    /// <param name="range">Distance to search</param>
    /// <param name="queue"></param>
    /// <returns>List of board tiles in range</returns>
    public List<BoardTileScript> bfs(BoardTileScript currentTile, int range, List<BoardTileScript> queue = null)
    {
        if (queue == null)
        {
            queue = new List<BoardTileScript>();
            queue.Add(currentTile);
        }

        int pointer = 0;
        for (int j = 0; j < range; j++)
        {
            List<BoardTileScript> neighbours = new List<BoardTileScript>();
            //Debug.Log("In Loop: " + j);
            for (int i = pointer; i < queue.Count; i++)
            {
                pointer++;
                foreach (BoardTileScript b in GetTileNeighbours(queue[i]))
                {
                    //Debug.Log("Getting more neighbour");
                    if (!queue.Contains(b) && !neighbours.Contains(b) && b.IsEmpty())
                    {
                        neighbours.Add(b);
                    }
                    else
                    {
                        //print(b.GridPosition + " ignored: " + !queue.Contains(b) + ", " + !neighbours.Contains(b) + ", " + b.IsEmpty());
                    }
                }
            }
            queue.AddRange(neighbours);
        }
        if (currentTile is RoomEntryBoardTileScript)
        {

        }
        else
        {
            queue.Remove(currentTile);
        }
        return queue;
    }

    /// <summary>
    /// Mark and show which tile the player can move to
    /// </summary>
    /// <param name="currentTile">Tile to search from</param>
    /// <param name="range">Distance to search</param>
    /// <returns>true if successful, false if invalid tile</returns>
    public bool ShowMovable(BoardTileScript currentTile, int range)
    {
        if (currentTile == null)
        {
            return false;
        }

        print("Finding new tile");
        movableTile.AddRange(bfs(currentTile, range));

        foreach (BoardTileScript b in movableTile)
        {
            b.GlowTile(true);
        }
        return true;
    }
    /// <summary>
    /// Show movable tiles
    /// </summary>
    /// <param name="boardTileScripts">Array of tiles to search from</param>
    /// <param name="range">Distance to search</param>
    /// <returns>true if successful</returns>
    public bool ShowMovable(BoardTileScript[] boardTileScripts, int range)
    {
        ClearMovable();
        print("Show movable in room");
        foreach (BoardTileScript b in boardTileScripts)
        {
            ShowMovable(b, range);
            print("movableTiles: " + movableTile.Count);

        }
        print("finished room");
        return true;
    }
    /// <summary>
    /// Show movable tiles from room
    /// </summary>
    /// <param name="roomScript">Room to check from</param>
    /// <param name="range">Distance to search</param>
    /// <returns>true if successful, false if room is invalid</returns>
    public bool ShowMovable(RoomScript roomScript, int range)
    {
        if (roomScript == null)
        {
            return false;
        }

        ShowMovable(roomScript.GetEntryTiles(), range - 1);
        return true;
    }

    /// <summary>
    /// Clear all movable tiles
    /// </summary>
    public void ClearMovable()
    {
        foreach (BoardTileScript b in movableTile)
        {
            b.GlowTile(false);
        }

        movableTile = new List<BoardTileScript>();
        //print("Clear");
    }

    public bool CanMove(BoardTileScript currentTile)
    {
        return movableTile.Contains(currentTile);
    }

    /// <summary>
    /// Set all object arrays once board is built
    /// </summary>
    /// <param name="players">Array of player scripts</param>
    /// <param name="rooms">Array of room scripts</param>
    /// <param name="roomEntries">Array of room entry scripts</param>
    /// <param name="shortcuts">Array of shortcut scripts</param>
    /// <param name="startTiles">Array of start tile scripts</param>
    /// <param name="weaponTokens">Array of weapon token scripts</param>
    /// <param name="freeRollTiles">Array of free roll tile scripts</param>
    /// <param name="freeSuggestionTiles">Array of free suggestion tile scripts</param>
    public void SetObjectArrays(PlayerTokenScript[] players,
                                RoomScript[] rooms,
                                RoomEntryBoardTileScript[] roomEntries,
                                ShortcutBoardTileScript[] shortcuts,
                                StartTileScript[] startTiles,
                                WeaponTokenScript[] weaponTokens,
                                FreeRollBoardTileScript[] freeRollTiles,
                                FreeSuggestionTileScript[] freeSuggestionTiles)
    {
        this.players = players;
        this.rooms = rooms;
        this.roomEntries = roomEntries;
        this.shortcuts = shortcuts;
        this.startTiles = startTiles;
        this.weaponTokens = weaponTokens;
        this.freeRollTiles = freeRollTiles;
        this.freeSuggestionTiles = freeSuggestionTiles;
    }

    /// <summary>
    /// Gets a random list of entry tile from Movable list
    /// return null if none found
    /// </summary>
    /// <returns>List of random entry tiles</returns>
    public List<RoomEntryBoardTileScript> GetRandomEntryTileInMovable()
    {
        int offset = Random.Range(0, roomEntries.Length - 1);
        List<RoomEntryBoardTileScript> temp = new List<RoomEntryBoardTileScript>();
        for (int i = 0; i < roomEntries.Length; i++)
        {
            if (movableTile.Contains(roomEntries[(offset + i) % roomEntries.Length]))
            {
                temp.Add( roomEntries[(offset + i) % roomEntries.Length]);
            }
        }

        return temp;
    }

    /// <summary>
    /// Get a free roll tile from movable tiles
    /// </summary>
    /// <returns>free roll tile from Movable list, null if none found</returns>
    public FreeRollBoardTileScript GetFreeRollTileInMovable()
    {
        foreach (FreeRollBoardTileScript e in freeRollTiles)
        {
            if (movableTile.Contains(e))
            {
                return e;
            }
        }
        return null;
    }

    /// <summary>
    /// Gets an free suggestion tile from Movable list
    /// </summary>
    /// <returns>Free suggestion tile null if none found</returns>
    public FreeSuggestionTileScript GetFreeSuggesTileInMovable()
    {
        foreach (FreeSuggestionTileScript e in freeSuggestionTiles)
        {
            if (movableTile.Contains(e))
            {
                return e;
            }
        }
        return null;
    }
}
