using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    [SerializeField] CharacterEnum character = CharacterEnum.Initial;
    [SerializeField] List<Card> deck = new List<Card>();
    [SerializeField] bool isEliminated = false;
    [SerializeField] List<Card> toGuessList = new List<Card>();
    private Dictionary<System.Enum, bool> notebook;

    public void InitializeNotebook()
    {
        notebook = new Dictionary<System.Enum, bool>();
        foreach(CharacterEnum notebookCharacter in System.Enum.GetValues(typeof(CharacterEnum)))
        {
            if (!notebookCharacter.Equals(CharacterEnum.Initial))
            {
                notebook.Add(notebookCharacter, false);
            }
        }
        foreach (Room notebookRoom in System.Enum.GetValues(typeof(Room)))
        {
            if (!notebookRoom.Equals(Room.None))
            {
                notebook.Add(notebookRoom, false);
            }
        }
        foreach (WeaponEnum notebookWeapon in System.Enum.GetValues(typeof(WeaponEnum)))
        {
                notebook.Add(notebookWeapon, false);
        }
        foreach (Card card in deck)
        {
            notebook[card.GetCardType()] = true;
        }

        foreach(System.Enum key in notebook.Keys)
        {
            print(string.Format("{0} - {1} = {2}", Character, key, notebook[key]));
        }
    }

    public CharacterEnum Character { get => character;}
    public bool IsEliminated { get => isEliminated; set => isEliminated = value; }
    public List<Card> Deck { get => deck; set => deck = value; }
    public List<Card> ToGuessList { get => toGuessList;}

    /// <summary>
    /// Set the player's character
    /// </summary>
    /// <param name="ce">Character Enum</param>
    public void SetCharacter(CharacterEnum ce)
    {
        character = ce;
    }


    /// <summary>
    /// Add a card to the player's deck
    /// </summary>
    /// <param name="c"> card to be added</param>
    /// <returns> if card c is in the deck already </returns>
    public bool AddCard(Card c)
    {
        if (deck.Contains(c))
        {
            print("Found Duplicate Card");
            return true;
        }
        deck.Add(c);
        return false;
    }

    /// <summary>
    /// Add a list of card to the player's deck
    /// </summary>
    /// <param name="cs">list of cards </param>
    /// <returns>if a card from cs is in the deck already</returns>
    public bool AddCard(List<Card> cs)
    {
        bool flag = false;
        foreach (Card c in cs)
        {
            if (AddCard(c))
            {
                flag = true;
            }
        }
        return flag;
    }

    /// <summary>
    /// find if the player has a certain card
    /// return a list of cards found
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public Card FindCard(Card c)
    {
        if (deck.Contains(c))
        {
            Card temp = deck[deck.IndexOf(c)];
            return temp;
        }
        else
        {
            return null;
        }
    }

    public List<Card> FindCard(List<Card> cards)
    {
        int i =0;
        Card returnCard = null;
        List<Card> foundCards = new List<Card>();

        while (i < cards.Count)
        {
            returnCard = FindCard(cards[i]);
            if (returnCard != null)
            {
                foundCards.Add(returnCard);
            }
            i++;
        }

        return foundCards;
    }


    public void InititaliseToGuessList()
    {
        toGuessList = new List<Card>(FindObjectOfType<CardManager>().GetAllCards());
        foreach(Card c in deck)
        {
            toGuessList.Remove(c);
        }
    }

    public bool RemoveToGessCard(Card c)
    {
        if (toGuessList.Contains(c))
        {
            toGuessList.Remove(c);
            return true;
        }
        else
        {
            return false;
        }
    }
}
