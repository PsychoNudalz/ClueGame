using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] Dice dice;
    [SerializeField] TurnController turnController;
    [SerializeField] PlayerMasterController playerController;
    [SerializeField] BoardManager boardManager;
    [SerializeField] CardManager gameGenerator;
    [SerializeField] CameraCloseUp cameraCloseUp;
    bool diceRolled = false;
    bool secondRollavailable = false;
    bool secondAccusationavailable = false;
    bool canRoll = true;
    


    private void Awake()
    {
        dice = FindObjectOfType<Dice>();
        turnController = FindObjectOfType<TurnController>();
        playerController = turnController.GetCurrentPlayer();
        boardManager = FindObjectOfType<BoardManager>();
        dice = boardManager.GetComponentInChildren<Dice>();
        gameGenerator = FindObjectOfType<CardManager>();
        cameraCloseUp = FindObjectOfType<CameraCloseUp>();
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
        if (!secondRollavailable && canRoll)
        {
            dice.RollDice();
            secondRollavailable = true;
        }
        if (secondRollavailable)
        {
            dice.RollDice();
            secondRollavailable = false;
            canRoll = true;
        }
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
        /*
        if getnextPlayer has card show Card
        else if getnextPlayer + 1 has 1 card show card
        else if getnextPlayer + 2 has 1 card show card
        else if getnextPlayer + 3 has 1 card show card
        else if getnextPlayer + 4 has 1 card show card
        else return no card found
         */
    }

    public void MakeSuggestion(List<Card> sug)
    { /*
          Player enters a room
          Player makes a weapon and player suggestion
          if other player has card -> show card
          if no players have the card -> player can choose to make accusation or end turn
         */
        bool playerWithCardFound= false;
        for (int i = 0; i < turnController.GetRestOfPlayersInOrder().Count;) {
            if (turnController.GetRestOfPlayersInOrder()[i%turnController.GetRestOfPlayersInOrder().Count].FindCard(sug) != null) {
                Debug.Log(turnController.GetRestOfPlayersInOrder()[i%turnController.GetRestOfPlayersInOrder().Count].FindCard(sug).ToString());
                playerWithCardFound = true;
            }
            else
            {
                i++;
                i = i % turnController.GetRestOfPlayersInOrder().Count;
            }
        }
        if (!playerWithCardFound) {
            print("No Player With Card Found");
            playerWithCardFound = false;
        }

    }

    public void MakeAccusation(List<Card> cards)
    {
        /*Get player 3 chosen cards
         Check the players card against the 3 cards set at the start of the game
        if they match -> player wins
        if they dont match -> player asked to make a second accusation
        if second accusation doesnt not match -> remove player from queue
        */
        if (gameGenerator.IsMatchAnswer(cards))
        {
            //code for wining
            print("PLAYER WIN");
        }
        else
        {
            if (secondAccusationavailable)
            {
                secondAccusationavailable = false;
            }
            else
            {
                playerController.EliminatePlayer();
            }
        }

    }

    public void EndTurn()
    {
        turnController.SetCurrentPlayerToNext();
        canRoll = true;
        secondRollavailable = false;

        //Anson: reset camera
        cameraCloseUp.ClearCloseUp();

        //Anson: start the turn to update the current player
        StartTurn();
    }
    /// <summary>
    /// Method for starting the turn
    /// </summary>
    public void StartTurn()
    {
        playerController = turnController.GetCurrentPlayer();
    }

    /// <summary>
    /// Gets the player controller for the current player
    /// </summary>
    /// <returns>player controller for the current player</returns>
    public PlayerMasterController GetCurrentPlayer()
    {
        return turnController.GetCurrentPlayer();
    }
}
