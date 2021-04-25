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
    [SerializeField] AIControllerScript aIController;
    bool diceRolled = false;
    bool canRoll = true;
    bool canSug = true;
    bool canAcc = true;
    bool isGameOver = false;
    bool isGameWin = false;
    Coroutine rollCoroutine;
    Coroutine sugCoroutine;

    public bool CanRoll { get => canRoll; set => canRoll = value; }
    public bool CanSug { get => canSug; }
    public bool CanAcc { get => canAcc; }

    private void Awake()
    {
        dice = FindObjectOfType<Dice>();
        turnController = FindObjectOfType<TurnController>();
        boardManager = FindObjectOfType<BoardManager>();
        gameGenerator = FindObjectOfType<CardManager>();
        cameraCloseUp = FindObjectOfType<CameraCloseUp>();
        uIHandler = FindObjectOfType<UIHandler>();
        aIController = FindObjectOfType<AIControllerScript>();
    }


    private void Start()
    {
        //StartTurn();
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
        PlayerMasterController playerMasterController = null;
        try
        {
            playerMasterController = turnController.GetCurrentPlayer();
        }
        catch (ArgumentOutOfRangeException e)
        {
            return;
        }
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
            rollCoroutine = StartCoroutine(DelayRoll(1.5f));
        }
        if (b is FreeSuggestionTileScript)
        {
            sugCoroutine = StartCoroutine(DelaySug(1.5f));
        }
        //uIHandler.DisplayOutputText(b.ToString(), 5f);

    }
    public Card AIShowCard(PlayerMasterController playerMasterController, List<Card> c)
    {
        /*
        AI Chooses a random card
         */
        if (uIHandler == null)
        {
            return null;
        }

        Card selectedCard = c[(UnityEngine.Random.Range(0, c.Count) % c.Count)];
        if (!playerController.isAI)
        {
            uIHandler.ShowCard(playerMasterController, selectedCard);
        }
        return selectedCard;

    }

    /// <summary>
    /// Iterate through the rest of the players searching for if 1 or more of the cards were found 
    /// </summary>
    /// <param name="sug"></param>
    public Tuple<PlayerMasterController, List<Card>> MakeSuggestion(List<Card> sug)
    { 

        uIHandler.DisplayOutputText(String.Concat(playerController.GetCharacter(), " suggested:\n", sug[0], "\n", sug[1], "\n", sug[2]), 5f);

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
                string temp = "";
                foreach (Card c in foundPlayer.Item2)
                {
                    Debug.Log(c.gameObject.name);
                    temp += " " + c.ToString() + ",";
                }

                //uIHandler.DisplayOutputText(foundPlayer.Item1.ToString() + " Has cards:" + temp, 5f);
                playerWithCardFound = true;
            }

        }
        if (!playerWithCardFound)
        {
            print("No Player With Card Found");
            playerWithCardFound = false;
            NotifySuggestion(null);
        }
        else
        {

            //If a real player has those cards let them choose
            Card card = null;
            if (!foundPlayer.Item1.isAI)

            {
                uIHandler.DisplayViewBlocker(true, EnumToString.GetStringFromEnum(foundPlayer.Item1.GetCharacter()) + " \n TO CHOOSE A CARD");
                uIHandler.DisplayChoicePanel(foundPlayer.Item1, foundPlayer.Item2);
            }
            else
            {
                card = AIShowCard(foundPlayer.Item1, foundPlayer.Item2);
                NotifySuggestion(card);
            }
        }
        return foundPlayer;

    }

    public void NotifySuggestion(Card c)
    {
        if (c != null)
        {
            GetCurrentPlayer().RemoveToGessCard(c);
        }
        if (GetCurrentPlayer().isAI)
        {
            aIController.NotifySuggestion();
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
            uIHandler.DisplayWinScreen(true);
            isGameOver = true;
            isGameWin = true;
        }
        else
        {
            print(playerController + " Wrong answer");
            uIHandler.DisplayPlayerEliminated(true, EnumToString.GetStringFromEnum(GetCurrentPlayer().GetCharacter()));
            playerController.EliminatePlayer();
            //EndTurn();
        }

    }
    /// <summary>
    /// Ending the turn.  Returns the next player
    /// </summary>
    /// <returns></returns>
    public PlayerMasterController EndTurn()
    {
        dice.ResetDice(true);
        if (rollCoroutine != null)
        {
            StopCoroutine(rollCoroutine);
        }
        if (sugCoroutine != null)
        {
            StopCoroutine(sugCoroutine);
        }
        if (turnController.SetCurrentPlayerToNext() && !isGameOver)
        {
            canRoll = true;

            //Anson: reset camera
            try
            {
                cameraCloseUp.ClearCloseUp();
            }
            catch (System.NullReferenceException e) { }
            //Anson: start the turn to update the current player

            StartTurn();

            return GetCurrentPlayer();
        }
        else
        {
            isGameOver = true;
            if (!isGameWin)
            {
                uIHandler.DisplayGameOverScreen(true);
            }
            return null;
        }
    }
    /// <summary>
    /// Method for starting the turn
    /// </summary>
    public void StartTurn()
    {
        boardManager.ClearMovable();
        playerController = turnController.GetCurrentPlayer();
        if (playerController.isAI)
        {
            uIHandler.InitialiseTurn(false);
            aIController.SetActive(true, playerController);
        }
        else
        {
            aIController.SetActive(false);
            //Anson: Block View
            uIHandler.DisplayViewBlocker(true, EnumToString.GetStringFromEnum(GetCurrentPlayer().GetCharacter())); ;

            uIHandler.InitialiseTurn(true);
            uIHandler.DisplayDeck(playerController.GetDeck());
        }
        //uIHandler.DisplayShortcutButton(playerController.CanTakeShortcut());
        canRoll = true;
        canSug = true;
        canAcc = true;
    }

    public void voidEndTurn() {
        EndTurn();
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
        if (rollCoroutine != null)
        {
            StopCoroutine(rollCoroutine);
        }
        
        yield return new WaitForSeconds(timeDelay);
        RollDice(true);
    }

    IEnumerator DelaySug(float timeDelay)
    {
        if (sugCoroutine != null)
        {
            StopCoroutine(sugCoroutine);
        }
        yield return new WaitForSeconds(timeDelay);
        if (uIHandler != null && !GetCurrentPlayer().isAI)
        {
            uIHandler.MakeSuggestionButton(true);
        }
    }

    
}
