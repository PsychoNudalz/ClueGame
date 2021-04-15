using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_MovementScript : TestUIScript
{

    private void Awake()
    {
        Application.targetFrameRate = 120;
        AssignAllComponents();
    }

    private void FixedUpdate()
    {
        UpdateBehaviour();
    }

    public void GiveRoll(int r)
    {
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
        playerMasterController = turnController.GetCurrentPlayer();

        UpdateStatusText(string.Concat("Turn:"+playerMasterController+ToString()+"\n"+
            "Current Tile:"+playerMasterController.GetTile()+"\n"+
            "Current Room:"+playerMasterController.GetCurrentRoom()
            ));

    }


}
