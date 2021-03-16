using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    private BoardTileScript[][] boardTiles;
    private PlayerTokenScript[] players;
    private RoomScript[] rooms;
    private StartTileScript[] startTiles;
    private ShortcutBoardTileScript[] shortcuts;
    private RoomEntryBoardTileScript[] roomEntries;
    private WeaponTokenScript[] weaponTokens;

    [Header("Displaying Moveable Tiles")]
    [SerializeField] List<BoardTileScript> movableTile;

    public PlayerTokenScript[] Players { get => players; }
    public RoomScript[] Rooms { get => rooms; }
    public StartTileScript[] StartTiles { get => startTiles; }
    public ShortcutBoardTileScript[] Shortcuts { get => shortcuts; }
    public RoomEntryBoardTileScript[] RoomEntries { get => roomEntries; set => roomEntries = value; }
    public WeaponTokenScript[] WeaponTokens { get => weaponTokens; set => weaponTokens = value; }

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

    internal void PlaceWeapons()
    {
        StartCoroutine(PlaceWeaponsDelay());
    }

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
                if (roomToTry.Room != Room.None && roomToTry.Room != Room.Centre && roomToTry.WeaponSlotsEmpty())
                {
                    roomToTry.AddWeapon(weapon);
                }
            }
        }
    }

    /*Print Array for Testing*/
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
            Debug.Log("In Loop: " + j);
            for (int i = pointer; i < queue.Count; i++)
            {
                pointer++;
                foreach (BoardTileScript b in GetTileNeighbours(queue[i]))
                {
                    Debug.Log("Getting more neighbour");
                    if (!queue.Contains(b) && !neighbours.Contains(b))
                    {
                        neighbours.Add(b);
                    }
                }
            }
            queue.AddRange(neighbours);
        }

        return queue;
    }

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
    public bool ShowMovable(RoomScript roomScript, int range)
    {
        if (roomScript == null)
        {
            return false;
        }
        ShowMovable(roomScript.GetEntryTiles(), range);
        return true;
    }


    public void ClearMovable()
    {
        foreach (BoardTileScript b in movableTile)
        {
            b.GlowTile(false);
        }

        movableTile = new List<BoardTileScript>();
        print("Clear");
    }

    public bool CanMove(BoardTileScript currentTile)
    {
        return movableTile.Contains(currentTile);
    }

    public void SetObjectArrays(PlayerTokenScript[] players, RoomScript[] rooms, RoomEntryBoardTileScript[] roomEntries, ShortcutBoardTileScript[] shortcuts, StartTileScript[] startTiles, WeaponTokenScript[] weaponTokens)
    {
        this.players = players;
        this.rooms = rooms;
        this.roomEntries = roomEntries;
        this.shortcuts = shortcuts;
        this.startTiles = startTiles;
        this.weaponTokens = weaponTokens;
    }
}
