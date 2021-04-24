using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    public List<Card> deck;
    public CardManager cardManager;
    public List<CardSlot> cardSlots;
    public Animator showCardAnimator;
    private TextMeshProUGUI txt;
    private Image img;
    public UserController userController;
    private PlayerMasterController choosingPlayer;
    bool areControlsFrozen = false;
    [Header("UI elements")]
    [SerializeField] GameObject allButtons;
    [SerializeField] GameObject deckGO;
    [SerializeField] TextMeshProUGUI currentPlayerName;
    [SerializeField] TextMeshProUGUI outputText;
    [SerializeField] GameObject outputTextGO;
    Coroutine outputTextCoroutine;
    [SerializeField] GameObject viewBlocker;
    [SerializeField] TextMeshProUGUI viewBlockerPlayerName;
    [SerializeField] GameObject playerEliminatedScreen;
    [SerializeField] TextMeshProUGUI playerEliminatedText;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject noteBookPanel;
    [SerializeField] Button shortcutButton;
    [SerializeField] Button rollButton;
    [SerializeField] Button accuseButton;
    [SerializeField] Button suggestButton;



    [Header("Suggestion Panels")]
    public GameObject makeSuggestionPanel;
    [SerializeField] Button SuggestionRoomButton;
    [Header("Choose Card Panel")]
    [SerializeField] GameObject choosePanel;
    [SerializeField] List<CardSlot> chooseSlots;
    [SerializeField] TextMeshProUGUI chooseCardText;
    [Header("Show Card Panel")]
    [SerializeField] GameObject showCardGO;
    [SerializeField] TextMeshProUGUI showCardText;
    [SerializeField] CardSlot showCard;
    [Header("Accusation Panels")]
    public GameObject makeAccusationPanel;
    //public GameObject suggestionPanel;
    private RoundManager roundManager;
    private void Start()
    {

        //deck = gameGen.GetSixSetsOfCards()[0];
        //deck = gameGen.GetPlaybleCardsByPlayers(1);

        //Anson: this gets you the deck for the current player, call this when you need to update the UI
    }

    public void StartBehaviour()
    {
        userController = FindObjectOfType<UserController>();
        cardManager = FindObjectOfType<CardManager>();
        //gameGen.Initialise();
        //cardSlots = new List<CardSlot>(GetComponentsInChildren<CardSlot>());
        cardSlots = cardSlots.OrderBy(p => p.name).ToList();
        if (!makeSuggestionPanel)
        {
            makeSuggestionPanel = GameObject.Find("MakeSuggestionPanel");
        }

        makeSuggestionPanel.SetActive(false);
        currentPlayerName.text = EnumToString.GetStringFromEnum(userController.GetCurrentPlayer().GetCharacter()) + "'s turn";

        DisplayViewBlocker(false);
        outputTextGO.SetActive(false);

        roundManager = FindObjectOfType<RoundManager>();
    }

    private void FixedUpdate()
    {
        try
        {

            rollButton.interactable = roundManager.CanRoll;
            shortcutButton.interactable = userController.GetCurrentPlayer().CanTakeShortcut();
            accuseButton.interactable = roundManager.CanAcc;
            suggestButton.interactable = (roundManager.CanSug && userController.GetCurrentPlayer().IsInRoom());
        }
        catch (NullReferenceException e)
        {

        }
    }

    public void DisplayDeck(List<Card> cards)
    {
        deck = cards;

        /*
        int i = 0;
        foreach (CardSlot cs in cardSlots)
        {
            if (i < deck.Count())
            {

                cs.SetCard(deck[i]);
                cs.SetVisible();
                i++;
            }
            else
            {
                cs.SetVisible(false);
            }
        }
        */
        foreach (CardSlot cs in cardSlots)
        {
            cs.SetVisible(false);
        }
        for (int i = 0; i < deck.Count() && i < cardSlots.Count(); i++)
        {
            //print(i + "," + cardSlots[i] + "," + deck[i]);
            cardSlots[i].SetCard(deck[i]);
            cardSlots[i].SetVisible();

        }

    }

    public void RollDiceButton()
    {
        if (!areControlsFrozen && userController.RM.CanRoll)
        {
            areControlsFrozen = true;
            userController.RollDice();
            areControlsFrozen = false;
        }

    }



    public void MakeSuggestionButton(bool forced = false)
    {
        if (!areControlsFrozen && (userController.RM.CanSug || forced))
        {
            areControlsFrozen = true;
            DisplayMenuSuggestion(forced);
            areControlsFrozen = false;
        }
    }

    private void CancelSuggestionButton()
    {

        if (!areControlsFrozen)
        {
            areControlsFrozen = true;
            Debug.Log("Cancel suggestion");
            areControlsFrozen = false;
        }
    }


    public void ShowCard(PlayerMasterController playerMasterController, Card c)
    {
        if (!areControlsFrozen)
        {
            areControlsFrozen = true;
            Debug.Log("Showing a card");
            showCardGO.SetActive(true);
            showCardAnimator.SetBool("show", true);
            showCardText.text = EnumToString.GetStringFromEnum(playerMasterController.GetCharacter()) + "\n has this card";
            showCard.SetCard(c);
            roundManager.NotifySuggestion(c);
            areControlsFrozen = false;
        }

    }
    public void ShowSelectedCard(int cardNum)
    {
        Card c = chooseSlots[cardNum].card;
        choosePanel.SetActive(false);
        if (!roundManager.GetCurrentPlayer().isAI)
        {
            DisplayViewBlocker(true, EnumToString.GetStringFromEnum(roundManager.GetCurrentPlayer().GetCharacter()));
        }
        ShowCard(choosingPlayer, c);
    }

    public void DisplayMenuSuggestion(bool forced = false)
    {
        if (forced)
        {
            makeSuggestionPanel.SetActive(true);
            SuggestionRoomButton.interactable = true;
        }
        else
        {

            if (userController.GetCurrentPlayer().GetCurrentRoom() != null)
            {
                makeSuggestionPanel.SetActive(true);
                SuggestionRoomButton.interactable = false;
                SuggestionRoomButton.GetComponentInChildren<AccusationCardSlot>().SetCard(cardManager.FindCard(userController.GetCurrentPlayer().GetCurrentRoom().Room) as RoomCard);
                //makeSuggestionPanel.GetComponent<Suggestion>().SetSugRoom(cardManager.FindCard(userController.GetCurrentPlayer().GetCurrentRoom().Room)as RoomCard); ;
                //Anson: hmmmmm yes, spaghetti
                userController.Suggestion.SetSugRoom(cardManager.FindCard(userController.GetCurrentPlayer().GetCurrentRoom().Room) as RoomCard); ;
            }
            else
            {
                Debug.Log("not in a room");
            }
        }
    }


    public void ConfirmSuggestion()
    {
        if (userController.MakeSuggestion())
        {
            makeSuggestionPanel.SetActive(false);
        }
    }

    public void MakeAccusationButton()
    {
        if (!areControlsFrozen && userController.RM.CanAcc)
        {
            areControlsFrozen = true;
            DisplayMenuAccusation();
            areControlsFrozen = false;
        }
    }

    public void DisplayMenuAccusation()
    {
        makeAccusationPanel.SetActive(true);
    }

    public void ConfirmAccusation()
    {
        if (userController.MakeAccusation())
        {
            makeAccusationPanel.SetActive(false);

        }
    }

    public void DisplayChoicePanel(PlayerMasterController player, List<Card> toChoose)
    {
        choosingPlayer = player;
        choosePanel.SetActive(true);
        chooseCardText.SetText("SELECT A CARD TO SHOW:\n" + EnumToString.GetStringFromEnum(roundManager.GetCurrentPlayer().GetCharacter()));
        chooseSlots = chooseSlots.OrderBy(p => p.name).ToList();
        foreach (CardSlot cs in chooseSlots)
        {
            cs.SetVisible(false);

        }
        for (int i = 0; i < toChoose.Count() && i < chooseSlots.Count(); i++)
        {

            chooseSlots[i].SetCard(toChoose[i]);
            chooseSlots[i].SetVisible();
        }
    }

    internal void DisplayWinScreen(bool b)
    {
        winScreen.SetActive(b);
    }

    public void ToggleNotepad()
    {
        DisplayNotePad(!noteBookPanel.activeSelf);
    }
    public void DisplayNotePad(bool b)
    {
        
        noteBookPanel.SetActive(b);
    }

    internal void DisplayGameOverScreen(bool b)
    {
        gameOverScreen.SetActive(b);
    }

    public void EndTurn()
    {
        print("Pressed End turn");
        DisplayViewBlocker(false);
        userController.EndTurn();
    }

    public void UpdateCurrentTurnText(string nextPlayer)
    {
        currentPlayerName.text = nextPlayer + "'s turn";
    }

    public void InitialiseTurn(bool displayFullUI)
    {
        showCardGO.SetActive(false);
        DisplayNotePad(false);
        showCardAnimator.SetBool("show", false);
        DisplayPlayerEliminated(false);
        if (displayFullUI)
        {
            allButtons.SetActive(true);
            deckGO.SetActive(true);
        }
        else
        {
            allButtons.SetActive(false);
            deckGO.SetActive(false);
        }
        UpdateCurrentTurnText(EnumToString.GetStringFromEnum(userController.GetCurrentPlayer().GetCharacter()));
    }

    /// <summary>
    /// To display the View Blocker
    /// can include the name of the player that needs it's attention
    /// </summary>
    /// <param name="b">enable or disable the view blocker</param>
    /// <param name="s"> name of the player that needs it's attention</param>
    public void DisplayViewBlocker(bool b, string s = "")
    {
        viewBlocker.SetActive(b);
        
        if (s.Equals(""))
        {
            viewBlockerPlayerName.text = "Next";
        }
        else
        {
            viewBlockerPlayerName.text = s;
        }
    }

    public void DisplayPlayerEliminated(bool b, string s = "")
    {
        playerEliminatedScreen.SetActive(b);
        if (s.Equals(""))
        {
            playerEliminatedText.text = "Current player has made an incorrect accusation and has been eliminated!";
        }
        else
        {
            playerEliminatedText.text = s + " has made an incorrect accusation and has been eliminated!";
        }
    }

    public void TakeShortcutButton()
    {
        userController.TakeShortcut();
    }


    public void DisplayOutputText(string s, float timeUntilDisappear)
    {
        if (outputTextCoroutine != null)
        {
            StopCoroutine(outputTextCoroutine);
        }
        outputTextCoroutine = StartCoroutine(DisplayOutputTextRoutine(s, timeUntilDisappear));
    }
    IEnumerator DisplayOutputTextRoutine(string s, float timeUntilDisappear)
    {
        outputTextGO.gameObject.SetActive(true);
        outputText.text = s;
        yield return new WaitForSeconds(timeUntilDisappear);
        outputTextGO.gameObject.SetActive(false);

    }


    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }



}







