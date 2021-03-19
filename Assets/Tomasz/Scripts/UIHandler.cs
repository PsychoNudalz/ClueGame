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
    public GameGenerator gameGen;
    public List<CardSlot> cardSlots;
    public Animator shownCard;
    public TextMeshProUGUI txt;
    public Image img;
    private void Start()
    {
        gameGen = FindObjectOfType<GameGenerator>();
        gameGen.Initialise();
        cardSlots = new List<CardSlot>(GetComponentsInChildren<CardSlot>());
        cardSlots = cardSlots.OrderBy(p => p.name).ToList();
        deck = gameGen.GetSixSetsOfCards()[0];


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

    bool areControlsFrozen = false;
    public void RollDiceButton()
    {
        if (!areControlsFrozen)
        {
            areControlsFrozen = true;
            Debug.Log("Roll Dice Here");
            areControlsFrozen = false;
        }

    }



    public void MakeSuggestionButton()
    {
        if (!areControlsFrozen)
        {
            areControlsFrozen = true;
            Debug.Log("Make Suggestion Here");


            shownCard.SetBool("show", true);

            areControlsFrozen = false;
        }
    }

    private void CancelSuggestion() {
        
        if (!areControlsFrozen)
        {
            areControlsFrozen = true;

            shownCard.SetBool("show", false);

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


    


    public void DisplayMenuMove() { 
    
    }
    public void DisplayMenuSuggestion()
    {

    }
    public void DisplayMenuAccusation()
    {

    }

    public void SelectCards_Character() { 
    
    }


    public void SelectCards_Weapon()
    {

    }


    public void SelectCards_Room()
    {

    }
    public void SelectCardsToShow(List<Card> toShow) { 
    
    }
}







