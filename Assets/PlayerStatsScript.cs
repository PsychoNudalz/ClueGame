using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    [SerializeField] CharacterEnum character = CharacterEnum.Initial;
    [SerializeField] List<Card> deck = new List<Card>();
    [SerializeField] bool isEliminated = false;

    public CharacterEnum Character { get => character;}
    public bool IsEliminated { get => isEliminated; set => isEliminated = value; }

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
}
