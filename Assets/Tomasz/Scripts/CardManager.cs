using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    static Random rnd = new Random();

    private List<WeaponCard> weapons;
    private List<RoomCard> rooms;
    private List<CharacterCard> characters;

    public List<Card> answers;
    public List<Card> playableCards;

    public List<List<Card>> setOfcards;
    public List<Card> test;
    public PlayerMasterController p;


    private void Awake()
    {
        Initialise();
    }

    public void Initialise()
    {
        //Create lists of each type of card
        rooms = new List<RoomCard>(FindObjectsOfType<RoomCard>());
        weapons = new List<WeaponCard>(FindObjectsOfType<WeaponCard>());
        characters = new List<CharacterCard>(FindObjectsOfType<CharacterCard>());

        //Remove one of each type of card and place in a list called answers
        int randomRoom = Random.Range(0, rooms.Count);
        int randomWeapon = Random.Range(0, weapons.Count);
        int randomCharacter = Random.Range(0, characters.Count);

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
    /// check if the cards passed matches the answer
    /// </summary>
    /// <param name="accusation"></param>
    /// <returns></returns>
    public bool IsMatchAnswer(List<Card> accusation)
    {
        foreach (Card a in answers)
        {
            if (!accusation.Contains(a))
            {
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


        /*
        for (int i = 0; i < setOfcards.Count && i < players.Count; i++)
        {
            players[i].AddCard(setOfcards[i]);
        }
        */
    }


}
