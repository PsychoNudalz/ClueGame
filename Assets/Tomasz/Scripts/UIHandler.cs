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
    public CardManager gameGen;
    public List<CardSlot> cardSlots;
    public Animator shownCard;
    private TextMeshProUGUI txt;
    private Image img;
    public UserController userController;
    bool areControlsFrozen = false;
    public GameObject msp;
    private void Start()
    {
        userController = FindObjectOfType<UserController>();
        gameGen = FindObjectOfType<CardManager>();
        gameGen.Initialise();
        cardSlots = new List<CardSlot>(GetComponentsInChildren<CardSlot>());
        cardSlots = cardSlots.OrderBy(p => p.name).ToList();
        msp = GameObject.Find("MakeSuggestionPanel");
        msp.SetActive(false);




        //deck = gameGen.GetSixSetsOfCards()[0];
        //deck = gameGen.GetPlaybleCardsByPlayers(1);

        //Anson: this gets you the deck for the current player, call this when you need to update the UI
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
        if (!areControlsFrozen)
        {
            areControlsFrozen = true;
            userController.RollDice();
            areControlsFrozen = false;
        }

    }



    public void MakeSuggestionButton()
    {
        if (!areControlsFrozen)
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


    public void ShowCard(Card c)
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
    public void DisplayMenuSuggestion()
    {
        if (userController.GetCurrentPlayer().GetCurrentRoom() != null)
        {
            msp.SetActive(true);
            GameObject name = GameObject.FindWithTag("rName");
            string txt = userController.GetCurrentPlayer().GetCurrentRoom().ToString();
            txt = txt.Split('(')[0];
            name.GetComponent<TextMeshProUGUI>().text = txt;
            msp.GetComponent<Suggestion>().SetRoom(GameObject.Find(txt).GetComponent<RoomCard>()); ;
        }
        else
        {
            Debug.Log("not in a room");
        }
    }
    public void DisplayMenuAccusation()
    {

    }

    public void SelectCards_Character()
    {

    }


    public void SelectCards_Weapon()
    {

    }


    public void SelectCards_Room()
    {

    }
    public void SelectCardsToShow(List<Card> toShow)
    {

    }

    public void EndTurn()
    {
        print("Pressed End turn");
        userController.EndTurn();
    }
}







