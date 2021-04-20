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
    [SerializeField] TextMeshProUGUI currentPlayerName;


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

        //deck = gameGen.GetSixSetsOfCards()[0];
        //deck = gameGen.GetPlaybleCardsByPlayers(1);

        //Anson: this gets you the deck for the current player, call this when you need to update the UI
    }

    public void DisplayDeck(List<Card> cards)
    {
        deck = userController.GetCurrentPlayer().GetDeck();

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
        SetNamesColours();
    }

    public void SetNamesColours()
    {
        foreach (CardSlot cs in cardSlots)
        {
            img = cs.GetComponent<Image>();
            txt = cs.GetComponentInChildren<TextMeshProUGUI>();
            if (cs.isVisible)
            {
                txt.text = cs.card.name;
                switch (cs.card.GetType().ToString())
                {
                    case "WeaponCard":
                        {
                            img.color = Color.green;
                            break;
                        }
                    case "RoomCard":
                        {
                            img.color = Color.gray;
                            break;
                        }
                    case "CharacterCard":
                        {
                            img.color = Color.blue;
                            break;
                        }
                }
            }
            else
            {
                img.enabled = false;
                txt.text = "";
            }
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
        string nextPlayer =  userController.EndTurn().GetCharacter().ToString();
        currentPlayerName.text ="Turn:"+ nextPlayer;
    }
}







