using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// for setting the tile effect the cursor is hovering on
/// </summary>
public class UserSelectionScript : MonoBehaviour
{
    [SerializeField] BoardTileScript currentTile;

    /// <summary>
    /// reset the effects on the current tile
    /// </summary>
    public void ClearCurrentTile()
    {
        if (currentTile != null)
        {
            currentTile.ClearTile();
            currentTile = null;
        }
    }

    /// <summary>
    /// Setting the current tile
    /// clears the previous tile and set the effect for the new tile
    /// </summary>
    /// <param name="b">tile to be set</param>
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
