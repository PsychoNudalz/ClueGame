using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSelectionScript : MonoBehaviour
{
    [SerializeField] BoardTileScript currentTile;


    public void ClearCurrentTile()
    {
        if (currentTile != null)
        {
            currentTile.ClearTile();
            currentTile = null;
        }
    }

    public void SelectCurrentTile(BoardTileScript b)
    {
        if (!b.Equals(currentTile))
        {
            ClearCurrentTile();

        }

        if (b.CanMove())
        {
            currentTile = b;
            currentTile.SelectTile();
        }
    }

    public BoardTileScript GetCurrentTile()
    {
        return currentTile;
    }

}
