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
    public Animator shownCard;
    private TextMeshProUGUI txt;
    private Image img;
    public UserController userController;
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
    [SerializeField] GameObject shortcutButton;


    [Header("Suggestion Panels")]
    public GameObject makeSuggestionPanel;
    [SerializeField] TextMeshProUGUI suggestionRoomNameText;
    [Header("Accusation Panels")]
    public GameObject makeAccusationPanel;
    //public GameObject suggestionPanel;
    private void Start()
    {
        userController = FindObjectOfType<UserController>();
        cardManager = FindObjectOfType<CardManager>();
        //gameGen.Initialise();
        cardSlots = new List<CardSlot>(GetComponentsInChildren<CardSlot>());
        cardSlots = cardSlots.OrderBy(p => p.name).ToList();
        if (!makeSuggestionPanel)
        {
            makeSuggestionPanel = GameObject.Find("MakeSuggestionPanel");
        }
        if (suggestionRoomNameText == null)
        {
            suggestionRoomNameText = GameObject.FindGameObjectWithTag("rName").GetComponent<TextMeshProUGUI>();
        }
        makeSuggestionPanel.SetActive(false);
        currentPlayerName.text = "Turn:" + userController.GetCurrentPlayer().GetCharacter().ToString();

        DisplayViewBlocker(false);
        outputTextGO.SetActive(false);

        //deck = gameGen.GetSixSetsOfCards()[0];
        //deck = gameGen.GetPlaybleCardsByPlayers(1);

        //Anson: this gets you the deck for the current player, call this when you need to update the UI
    }

    public void DisplayDeck(List<Card> cards)
    {
        deck = cards;

        int i = 0;
        foreach (CardSlot cs in cardSlots)
        {
            if (i < deck.Count())
            {

                cs.SetCard(deck[i]);
                cs.SetVisible();
            }
            i++;
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
            DisplayMenuSuggestion();
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


    public void ShowCard(PlayerMasterController playerMasterController, List<Card> c)
    {
        if (!areControlsFrozen)
        {
            areControlsFrozen = true;
            Debug.Log("Showing a card");
            shownCard.SetBool("show", true);
            areControlsFrozen = false;
        }

    }





    public void DisplayMenuMove()
    {

    }
    public void DisplayMenuSuggestion(bool forced = false)
    {
        if (forced)
        {
            // if landed on suggestion block, allow selection of room
        }
        else
        {

            if (userController.GetCurrentPlayer().GetCurrentRoom() != null)
            {
                makeSuggestionPanel.SetActive(true);
                string txt = userController.GetCurrentPlayer().GetCurrentRoom().ToString();
                txt = txt.Split('(')[0];
                suggestionRoomNameText.text = txt;
                //makeSuggestionPanel.GetComponent<Suggestion>().SetSugRoom(cardManager.FindCard(userController.GetCurrentPlayer().GetCurrentRoom().Room)as RoomCard); ;
                //Anson: hmmmmm yes, spaghetti
                userController.Suggestion.SetSugRoom(cardManager.FindCard(userController.GetCurrentPlayer().GetCurrentRoom().Room)as RoomCard); ;
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

    public void SelectCardsToShow(List<Card> toShow)
    {

    }

    public void EndTurn()
    {
        print("Pressed End turn");
        DisplayViewBlocker(false);
        userController.EndTurn();
    }

    public void UpdateCurrentTurnText(string nextPlayer)
    {
        currentPlayerName.text = "Turn:" + nextPlayer;
    }

    public void InitialiseTurn(bool displayFullUI)
    {
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
        UpdateCurrentTurnText(userController.GetCurrentPlayer().GetCharacter().ToString());
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



    public void DisplayShortcutButton(bool b)
    {
        shortcutButton.SetActive(b);
    }


    public void TakeShortcutButton()
    {
        userController.TakeShortcut();
    }


    public void DisplayOutputText(string s, float timeUntilDisappear)
    {
        if (outputTextCoroutine!= null)
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
}







