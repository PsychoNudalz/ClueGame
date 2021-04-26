using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suggestion : MonoBehaviour
{
    public WeaponCard sugWeapon;
    public RoomCard sugRoom;
    public CharacterCard sugCharacter;
    //[SerializeField] RoundManager roundManagerScript;


    private void Awake()
    {
        //Find Round Manager game object

    }

    public List<Card> Suggest()
    {
        // if all elements of the suggestion are made, return a message for the suggestion
        //Cancel();
        
        if (sugRoom != null & sugWeapon != null & sugCharacter != null)
        {
            //Anson: found bug, this is the wrong room, no wonder it is constantly going to louge
            //RoomScript roomScript = FindObjectOfType<RoomScript>();

            //Anson: incoming spageghtti to get all room script to Find the corrisponding room
            RoomScript roomScript = null;

            //Anson: to find the room based on the room card
            RoomScript[] allRooms = FindObjectsOfType<RoomScript>();
            for (int i = 0; i < allRooms.Length && roomScript == null; i++)
            {
                if (allRooms[i].Room.Equals((RoomEnum)sugRoom.GetCardType()))
                {
                    roomScript = allRooms[i];
                }
            }

            if (roomScript != null)
            {
            roomScript.MovePlayerToRoom((CharacterEnum)sugCharacter.GetCardType());
            roomScript.MoveWeaponToRoom((WeaponEnum)sugWeapon.GetCardType());
            }

            Debug.Log("I suggest that the crime was committed in the " + sugRoom + ", by " + sugCharacter + " with the " + sugWeapon);

            //set List to have the sugestions in place
            Card[] sug = { sugCharacter, sugWeapon, sugRoom };
            //call suggestion method from round manager passing in the cards
            return (new List<Card>(sug));
        }
        else
        {
            return null;
        }

        //Cancel();
    }

    public void SetSugWeapon(WeaponCard weaponCard)
    {
        //set suggested weapon to chosen card
        sugWeapon = weaponCard;
        Debug.Log("Weapon Suggested: " + sugWeapon);
    }
    public void SetSugRoom(RoomCard roomCard)
    {
        //set suggested Room to chosen card
        sugRoom = roomCard;
        Debug.Log("Room Suggested: " + sugRoom);
    }

    public void SetSugCharacter(CharacterCard characterCard)
    {
        //set suggested Character to chosen card
        sugCharacter = characterCard;
        Debug.Log("Character Suggested: " + sugCharacter);
    }
    public void Cancel()
    {
        //reset sugestion
        sugRoom = null;
        sugCharacter = null;
        sugWeapon = null;

    }
}
