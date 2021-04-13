using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] Dice dice;
    [SerializeField] TurnController turnController;
    [SerializeField] PlayerMasterController playerController;
    [SerializeField] BoardManager boardManager;
    bool diceRolled = false;


    private void Awake()
    {
        turnController = FindObjectOfType<TurnController>();
        playerController = turnController.GetCurrentPlayer();
        boardManager = FindObjectOfType<BoardManager>();
        dice = boardManager.GetComponentInChildren<Dice>();
    }

    private void FixedUpdate()
    {
        DiceBehaviour();
    }

    void DiceBehaviour()
    {
        PlayerMasterController playerMasterController = turnController.GetCurrentPlayer();
        if (dice.GetValue() > 0 && diceRolled)
        {
            diceRolled = false;
            if (!boardManager.ShowMovable(playerMasterController.GetTile(), dice.GetValue()))
            {
                if (!boardManager.ShowMovable(playerMasterController.GetCurrentRoom(), dice.GetValue()))
                {
                    Debug.LogError("Failed to show boardManager movable");
                }
            }
            //playerMasterController.DisplayBoardMovableTiles(dice.GetValue());
            dice.ResetDice();
        }
    }
    IEnumerator DelayResetDice(float t)
    {
        yield return new WaitForSeconds(t);
        dice.ResetDice();
    }

    public void RollDice()
    {
        diceRolled = true;
        dice.RollDice();
        StartCoroutine(DelayResetDice(5f));
    }

    public void MovePlayer(BoardTileScript b)
    {
        playerController = turnController.GetCurrentPlayer();
        if (playerController.PlayerTokenScript.IsInRoom())
        {
            playerController.GetCurrentRoom().RemovePlayerFromRoom(playerController, b);
        }
        else
        {
            playerController.MovePlayer(b);
        }
        if (boardManager == null)
        {
            boardManager = FindObjectOfType<BoardManager>();

        }
        boardManager.ClearMovable();

    }
    public void ShowCard()
    {

    }

    public void MakeSuggestion()
    {

    }

    public void MakeAccusation()
    {

    }

    public void EndTurn()
    {

        turnController.SetCurrentPlayerToNext();
    }


}
