using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager : MonoBehaviour
{
    private BoardTileScript[][] boardTiles;
    private PlayerTokenScript[] players;
    private RoomScript[] rooms;
    private StartTileScript[] startTiles;
    private ShortcutBoardTileScript[] shortcuts;
    private RoomEntryBoardTileScript[] roomEntries;

    [Header("Displaying Moveable Tiles")]
    [SerializeField] List<BoardTileScript> movableTile;

    public PlayerTokenScript[] Players { get => players; }
    public RoomScript[] Rooms { get => rooms; }
    public StartTileScript[] StartTiles { get => startTiles; }
    public ShortcutBoardTileScript[] Shortcuts { get => shortcuts; }
    public RoomEntryBoardTileScript[] RoomEntries { get => roomEntries; set => roomEntries = value; }

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
                    if (!queue.Contains(b)&&!neighbours.Contains(b))
                    {
                        neighbours.Add(b);
                    }
                }
            }
            queue.AddRange(neighbours);
        }

        return queue;
    }

    public void ShowMovable(BoardTileScript currentTile, int range)
    {
        ClearMovable();
        print("Finding new tile");
        movableTile = bfs(currentTile, range);
        foreach (BoardTileScript b in movableTile)
        {
            b.GlowTile(true);
        }
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

    public void SetObjectArrays(PlayerTokenScript[] players, RoomScript[] rooms, RoomEntryBoardTileScript[] roomEntries, ShortcutBoardTileScript[] shortcuts, StartTileScript[] startTiles)
    {
        this.players = players;
        this.rooms = rooms;
        this.roomEntries = roomEntries;
        this.shortcuts = shortcuts;
        this.startTiles = startTiles;
    }
}
