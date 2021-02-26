using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionScript : MonoBehaviour
{
    [SerializeField] BoardTileScript currentTile;
    [SerializeField] int moveAmount;

    public int MoveAmount { get => moveAmount; set => moveAmount = value; }

    public void ClearCurrentTile()
    {
        currentTile.ClearTile();
    }

    public void SelectCurrentTile(BoardTileScript b)
    {
        if (currentTile != null)
        {
            ClearCurrentTile();
        }
        currentTile = b;
        currentTile.SelectTile();

    }


}
