using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameGenerator : MonoBehaviour
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
    private void Start()
    {

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
    /// **WIP** Returns a list of lists where each list consists of cards for each player
    /// </summary>
    /// <param name="numberOfPlayers"></param>
    /// <returns></returns>
    public List<Card> GetPlaybleCardsByPlayers(int numberOfPlayers)
    {


        return playableCards;
    }


    /// <summary>
    /// Returns a list of lists where each list consists of 3 random playable cards (for 6 players)
    /// </summary>
    /// <returns></returns>
    public List<List<Card>> GetSixSetsOfCards()
    {
        setOfcards = new List<List<Card>>();

        for (int i = 0; i < 6; i++)
        {
            test = playableCards.GetRange(i * 3, 3).ToList();
            setOfcards.Add(test);
        }
        return setOfcards;
    }
    /// <summary>
    /// chekc if the cards passed matches the answer
    /// </summary>
    /// <param name="accusation"></param>
    /// <returns></returns>
    public bool IsMatchAnswer(List<Card> accusation)
    {
        foreach(Card a in answers)
        {
            if (!accusation.Contains(a))
            {
                return false;
            }
        }
        return true;
    }

}
