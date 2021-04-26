using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// class for test the movement of the player
/// </summary>
public class Testing_MovementScript : TestUIScript
{

    private void Awake()
    {
        //FindObjectsOfType<GameManagerScript>().
        Application.targetFrameRate = 120;
        AssignAllComponents();
    }

    private void FixedUpdate()
    {
        UpdateBehaviour();
    }

    /// <summary>
    /// give a select value for the player roll
    /// </summary>
    /// <param name="r">range the player can move</param>
    public void GiveRoll(int r)
    {
        boardManager.ClearMovable();
        playerMasterController = turnController.GetCurrentPlayer();

        if (!boardManager.ShowMovable(playerMasterController.GetTile(), r))
        {
            if (!boardManager.ShowMovable(playerMasterController.GetCurrentRoom(), r))
            {
                Debug.LogError("failed to show tiles");
                UpdateOutputText("failed to show tiles");
                return;
            }
        }

        UpdateOutputText(playerMasterController.ToString()+" given roll: "+r);
    }

    public override void UpdateBehaviour()
    {
        try
        {
        playerMasterController = turnController.GetCurrentPlayer();
        }catch (System.ArgumentOutOfRangeException e)
        {
            return;
        }

        UpdateStatusText(string.Concat("Turn:"+playerMasterController+ToString()+"\n"+
            "Current Tile:"+playerMasterController.GetTile()+"\n"+
            "Current Room:"+playerMasterController.GetCurrentRoom()
            ));

    }


}
