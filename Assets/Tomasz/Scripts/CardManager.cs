using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    private List<WeaponCard> weapons;
    private List<RoomCard> rooms;
    private List<CharacterCard> characters;

    public List<Card> answers;
    public List<Card> playableCards;

    public List<List<Card>> setOfcards;
    public List<Card> test;
    public PlayerMasterController p;



    /// <summary>
    /// Selects random answer cards and deal cards to players
    /// </summary>
    public void Initialise()
    {
        //Create lists of each type of card
        rooms = new List<RoomCard>(FindObjectsOfType<RoomCard>());
        weapons = new List<WeaponCard>(FindObjectsOfType<WeaponCard>());
        characters = new List<CharacterCard>(FindObjectsOfType<CharacterCard>());

        //Remove one of each type of card and place in a list called answers
        int randomRoom = UnityEngine.Random.Range(0, rooms.Count);
        int randomWeapon = UnityEngine.Random.Range(0, weapons.Count);
        int randomCharacter = UnityEngine.Random.Range(0, characters.Count);

        answers.Add(characters[randomCharacter]);
        characters.RemoveAt(randomCharacter);

        answers.Add(weapons[randomWeapon]);
        weapons.RemoveAt(randomWeapon);

        answers.Add(rooms[randomRoom]);
        rooms.RemoveAt(randomRoom);

        //Put the rest of cards together and randomise 
        playableCards.AddRange(weapons);
        playableCards.AddRange(rooms);
        playableCards.AddRange(characters);

        playableCards = playableCards.OrderBy(a => System.Guid.NewGuid()).ToList();
        DealCardsToPlayers();
    }

    /// <summary>
    /// Returns a list of 3 answer cards in order character, weapon, room;
    /// </summary>
    /// <returns>List of answer cards</returns>
    public List<Card> GetAnswers()
    {
        return answers;
    }




    /// <summary>
    /// Check if the cards passed matches the answer
    /// </summary>
    /// <param name="accusation"></param>
    /// <returns>True if matches else false</returns>
    public bool IsMatchAnswer(List<Card> accusation)
    {
        foreach (Card c in accusation)
        {
            print(c.GetCardType());
        }
        foreach (Card a in answers)
        {
            if (!accusation.Contains(a))
            {
                print("Accusation does not have: " + a);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// shuffles and deal the cards to players
    /// </summary>
    public void DealCardsToPlayers()
    {
        int curPlayer = 0;
        List<Card> cardToAdd = new List<Card>();
        List<Card> remainingCards = new List<Card>(playableCards);
        List<PlayerMasterController> players = FindObjectOfType<TurnController>().CurrentPlayers;
        while (remainingCards.Count > 0)
        {

            cardToAdd.Add(remainingCards[0]);
            players[curPlayer % 6].AddCard(cardToAdd);
            cardToAdd.RemoveAt(0);
            remainingCards.RemoveAt(0);
            curPlayer++;
        }

        foreach (PlayerMasterController player in players)
        {
            player.initializeNotebook();
        }

    }

    /// <summary>
    /// Looks for the specified weapon card
    /// </summary>
    /// <param name="e">Needed weapon card</param>
    /// <returns>Card if exists else null</returns>
    public Card FindCard(WeaponEnum e)
    {
        List<Card> allCards = new List<Card>(playableCards);
        allCards.AddRange(answers);
        foreach (Card c in allCards)
        {
            if (c.Equals(e))
            {
                return c;
            }
        }
        Debug.LogError("Could not Find card Enum e: " + e.ToString());

        return null;
    }
    /// <summary>
    /// Looks for the specified room card
    /// </summary>
    /// <param name="e">Needed room card</param>
    /// <returns>Card if exists else null</returns>
    public Card FindCard(RoomEnum e)
    {
        List<Card> allCards = new List<Card>(playableCards);
        allCards.AddRange(answers);
        //print(allCards.Count);

        foreach (Card c in allCards)
        {
            if (c.Equals(e))
            {
                return c;
            }
        }
        Debug.LogError("Could not Find card Enum e: " + e.ToString());
        return null;
    }
    /// <summary>
    /// Looks for the specified character card
    /// </summary>
    /// <param name="e">Needed character card</param>
    /// <returns>Card if exists else null</returns>
    public Card FindCard(CharacterEnum e)
    {
        List<Card> allCards = new List<Card>(playableCards);
        allCards.AddRange(answers);
        foreach (Card c in allCards)
        {
            if (c.Equals(e))
            {
                return c;
            }
        }
        Debug.LogError("Could not Find card Enum e: " + e.ToString());
        return null;
    }

    /// <summary>
    /// Returns list of all cards
    /// </summary>
    /// <returns>List of all cards</returns>
    public List<Card> GetAllCards()
    {
        List<Card> allCards = new List<Card>(playableCards);
        allCards.AddRange(answers);
        return allCards;
    }

}
