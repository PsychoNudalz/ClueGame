using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameGenerator : MonoBehaviour
{
    static Random rnd = new Random();
    
    private void Start()
    {
        List<List<Card>> test = generateCards();
        foreach(List<Card> subList in test)
{
            print("new");
            foreach (Card item in subList)
            {
                print(item.getName());
            }
        }
    }

    public List<List<Card>> generateCards() {

        List<Card> weapons = new List<Card>();
        List<Card> rooms = new List<Card>();
        List<Card> characters = new List<Card>();
        List<Card> answers = new List<Card>();
        List<Card> playableCards = new List<Card>();


        weapons.Add(new Card(Card.CardType.weapon, "Dagger"));
        weapons.Add(new Card(Card.CardType.weapon, "Candlestick"));
        weapons.Add(new Card(Card.CardType.weapon, "Revolver"));
        weapons.Add(new Card(Card.CardType.weapon, "Rope"));
        weapons.Add(new Card(Card.CardType.weapon, "LeadPiping"));
        weapons.Add(new Card(Card.CardType.weapon, "Spanner"));

        rooms.Add(new Card(Card.CardType.room, "Study"));
        rooms.Add(new Card(Card.CardType.room, "Hall"));
        rooms.Add(new Card(Card.CardType.room, "Lounge"));
        rooms.Add(new Card(Card.CardType.room, "Library"));
        rooms.Add(new Card(Card.CardType.room, "DiningRoom"));
        rooms.Add(new Card(Card.CardType.room, "BillardRoom"));
        rooms.Add(new Card(Card.CardType.room, "BallRoom"));
        rooms.Add(new Card(Card.CardType.room, "Kitchen"));
        rooms.Add(new Card(Card.CardType.room, "Conservatory"));

        characters.Add(new Card(Card.CardType.character, "Col_Mustard"));
        characters.Add(new Card(Card.CardType.character, "Prof_Plum"));
        characters.Add(new Card(Card.CardType.character, "Rev_Green"));
        characters.Add(new Card(Card.CardType.character, "Mrs_Peacock"));
        characters.Add(new Card(Card.CardType.character, "Miss_Scarlet"));
        characters.Add(new Card(Card.CardType.character, "Mrs_White"));


        int randomRoom = Random.Range(0, rooms.Count);
        int randomWeapon = Random.Range(0, weapons.Count);
        int randomCharacter = Random.Range(0, characters.Count);


        answers.Add(characters[randomCharacter]);
        characters.RemoveAt(randomCharacter);

        answers.Add(weapons[randomWeapon]);
        weapons.RemoveAt(randomWeapon);

        answers.Add(rooms[randomRoom]);
        rooms.RemoveAt(randomRoom);

        playableCards.AddRange(weapons);
        playableCards.AddRange(rooms);
        playableCards.AddRange(characters);

        playableCards = playableCards.OrderBy(a => System.Guid.NewGuid()).ToList();

        List<List<Card>> output = new List<List<Card>>();
        output.Add(answers);
        output.Add(playableCards);
        return output;
    }


}
