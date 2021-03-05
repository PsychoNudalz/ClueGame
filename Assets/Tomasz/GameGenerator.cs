using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameGenerator : MonoBehaviour
{
    static Random rnd = new Random();

    public List<WeaponCard> weapons;
    public List<RoomCard> rooms;
    public List<CharacterCard> characters;

    public List<Card> answers;





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
    }





  
}
