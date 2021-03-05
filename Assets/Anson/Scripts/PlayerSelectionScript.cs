using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionScript : MonoBehaviour
{
    [SerializeField] BoardTileScript currentTile;
    [SerializeField] int moveAmount;

    [Header("Other Player Components")]
    [SerializeField] PlayerMasterController playerMasterController;
    [SerializeField] PlayerTokenScript playerTokenScript;


    public int MoveAmount { get => moveAmount; set => moveAmount = value; }
    public PlayerTokenScript PlayerTokenScript { get => playerTokenScript; set => playerTokenScript = value; }
    public PlayerMasterController PlayerMasterController { get => playerMasterController; set => playerMasterController = value; }


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

        if (playerMasterController.CanMove(b))
        {
            currentTile = b;
            currentTile.SelectTile();

        }


    }


    public void MovePlayer()
    {
        if (playerTokenScript == null)
        {
            playerTokenScript = playerMasterController.PlayerTokenScript;
        }
        if (currentTile != null)
        {
            playerTokenScript.MoveToken(currentTile);
        }
    }


}
