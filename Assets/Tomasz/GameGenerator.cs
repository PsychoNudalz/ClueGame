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




    private void Start()
    {
        Initialise();
    }

    private void Initialise() {
        rooms = new List<RoomCard>(FindObjectsOfType<RoomCard>());
        weapons = new List<WeaponCard>(FindObjectsOfType<WeaponCard>());
        characters = new List<CharacterCard>(FindObjectsOfType<CharacterCard>());

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
    }

    public List<Card> getAnswers() {
        return answers;
    }

    public List<Card> getPlaybleCards() {
        return playableCards;
    }

  
}
