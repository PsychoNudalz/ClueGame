using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private BoardTileScript[][] boardTileArray;

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

    public void CreateBoardArray(BoardTileScript[] tiles, int height, int width)
    {
        boardTileArray = new BoardTileScript[height][];
        for (int row = 0; row < height; row++)
        {
            BoardTileScript[] rowArray = new BoardTileScript[width];
            boardTileArray[row] = rowArray;
        }
        foreach (BoardTileScript tile in tiles)
        {
            boardTileArray[(int)tile.GridPosition.y][(int)tile.GridPosition.x] = tile;
        }
        /*
         * For Testing
         */
        //PrintArray();
    }

    public BoardTileScript GetTileFromGrid(int x, int y)
    {
        if (x < boardTileArray[0].Length && y < boardTileArray.Length && x >= 0 && y >= 0)
        {
            return boardTileArray[y][x];
        }
        else
        {
            return null;
        }
    }

    /*Print Array for Testing*/
    public void PrintArray()
    {
        for (int row = 0; row < boardTileArray.Length; row++)
        {
            for (int col = 0; col < boardTileArray[row].Length; col++)
            {
                BoardTileScript tile = GetTileFromGrid(col, row);
                if (tile != null)
                {
                    BoardTileScript[] neighbours = GetTileNeighbours(tile);
                    print(tile + " neighbours = " + neighbours.Length);
                    foreach(BoardTileScript neighbour in neighbours)
                    {
                        print("\t" + tile + "==>> Neighbour ==>> " + neighbour);
                    }
                }
            }
        }
    }

}
