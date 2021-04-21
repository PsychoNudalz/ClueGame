using System;
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
    [SerializeField] UIHandler uIHandler;
    bool diceRolled = false;
    bool canRoll = true;
    bool canSug = true;
    bool canAcc = true;

    public bool CanRoll { get => canRoll; }
    public bool CanSug { get => canSug; }
    public bool CanAcc { get => canAcc; }

    private void Awake()
    {
        dice = FindObjectOfType<Dice>();
        turnController = FindObjectOfType<TurnController>();
        playerController = turnController.GetCurrentPlayer();
        boardManager = FindObjectOfType<BoardManager>();
        try
        {

            dice = boardManager.GetComponentInChildren<Dice>();
        }
        catch (System.Exception)
        {
        }
        gameGenerator = FindObjectOfType<CardManager>();
        cameraCloseUp = FindObjectOfType<CameraCloseUp>();
        uIHandler = FindObjectOfType<UIHandler>();
    }

    private void Start()
    {
        StartTurn();
    }

    private void FixedUpdate()
    {
        DiceBehaviour();

    }

    void DiceBehaviour()
    {
        if (dice == null)
        {
            return;
        }
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


    /// <summary>
    /// Rolls dice, will not roll if the player has rolled aready.
    /// Pass true to forcfully roll it
    /// </summary>
    /// <param name="force"> if roll forced</param>
    public void RollDice(bool force = false)
    {
        diceRolled = true;
        if (canRoll || force)
        {
            dice.RollDice();
            canRoll = false;
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
        if (b is FreeRollBoardTileScript)
        {
            StartCoroutine(DelayRoll(1.5f));
        }
        if (b is FreeSuggestionTileScript)
        {
            StartCoroutine(DelaySug(1.5f));
        }

    }
    public void ShowCard(PlayerMasterController playerMasterController, List<Card> c)
    {
        /*
        if getnextPlayer has card show Card
        else if getnextPlayer + 1 has 1 card show card
        else if getnextPlayer + 2 has 1 card show card
        else if getnextPlayer + 3 has 1 card show card
        else if getnextPlayer + 4 has 1 card show card
        else return no card found
         */
        if (uIHandler == null)
        {
            return;
        }
        uIHandler.ShowCard(playerMasterController, c);

    }

    public void MakeSuggestion(List<Card> sug)
    { /*
          Player enters a room
          Player makes a weapon and player suggestion
          if other player has card -> show card
          if no players have the card -> player can choose to make accusation or end turn
         */
        canSug = false;
        bool playerWithCardFound = false;
        List<PlayerMasterController> RestOfPlayers = turnController.GetRestOfPlayersInOrder();
        Tuple<PlayerMasterController, List<Card>> foundPlayer = null;
        for (int i = 0; i < RestOfPlayers.Count && !playerWithCardFound; i++)
        {
            print("Finding: " + RestOfPlayers[i]);
            foundPlayer = RestOfPlayers[i].FindCard(sug);
            if (foundPlayer != null)
            {
                // = RestOfPlayers[i % RestOfPlayers.Count].FindCard(sug);
                Debug.Log(foundPlayer.Item1.ToString() + " Has cards:");
                foreach (Card c in foundPlayer.Item2)
                {
                    Debug.Log(c.gameObject.name);
                }
                playerWithCardFound = true;
            }

        }
        if (!playerWithCardFound)
        {
            print("No Player With Card Found");
            playerWithCardFound = false;
        }
        else
        {
            ShowCard(foundPlayer.Item1, foundPlayer.Item2);
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
        canAcc = false;
        if (gameGenerator.IsMatchAnswer(cards))
        {
            //code for wining
            print("PLAYER WIN");
        }
        else
        {
            print(playerController + " Wrong answer");
            playerController.EliminatePlayer();
            EndTurn();
        }

    }
    /// <summary>
    /// Ending the turn.  Returns the next player
    /// </summary>
    /// <returns></returns>
    public PlayerMasterController EndTurn()
    {
        turnController.SetCurrentPlayerToNext();
        canRoll = true;



        //Anson: reset camera
        cameraCloseUp.ClearCloseUp();

        //Anson: start the turn to update the current player
        StartTurn();

        //Anson: Block View
        uIHandler.DisplayViewBlocker(true, GetCurrentPlayer().GetCharacter().ToString()); ;

        return GetCurrentPlayer();
    }
    /// <summary>
    /// Method for starting the turn
    /// </summary>
    public void StartTurn()
    {
        playerController = turnController.GetCurrentPlayer();
        if (playerController.isAI)
        {
            uIHandler.InitialiseTurn(false);
        }
        else
        {
            uIHandler.InitialiseTurn(true);
            uIHandler.DisplayDeck(playerController.GetDeck());
        }
        uIHandler.DisplayShortcutButton(playerController.CanTakeShortcut());
        canRoll = true;
        canSug = true;
        canAcc = true;
    }

    /// <summary>
    /// Gets the player controller for the current player
    /// </summary>
    /// <returns>player controller for the current player</returns>
    public PlayerMasterController GetCurrentPlayer()
    {
        return turnController.GetCurrentPlayer();
    }


    public List<BoardTileScript> GetBoardMovableTiles()
    {
        return boardManager.MovableTile;
    }

    IEnumerator DelayRoll(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        RollDice(true);
    }

    IEnumerator DelaySug(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        if (uIHandler != null)
        {
            uIHandler.MakeSuggestionButton(true);
        }
    }
}
