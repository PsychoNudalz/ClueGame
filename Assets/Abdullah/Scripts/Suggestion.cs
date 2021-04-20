using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suggestion : MonoBehaviour
{
    public WeaponCard sugWeapon;
    public RoomCard sugRoom;
    public CharacterCard sugCharacter;
    [SerializeField] RoundManager roundManagerScript;


    private void Awake()
    {
        //Find Round Manager game object
        if (roundManagerScript == null)
        {
            roundManagerScript = FindObjectOfType<RoundManager>();

        }
    }

    public void Suggest()
    {
        // if all elements of the suggestion are made, return a message for the suggestion
        if (sugRoom != null & sugWeapon != null & sugCharacter != null)
        {
            RoomScript roomScript = FindObjectOfType<RoomScript>();

            roomScript.MovePlayerToRoom((CharacterEnum)System.Enum.Parse(typeof(CharacterEnum), sugCharacter.gameObject.name));
            roomScript.MoveWeaponToRoom((WeaponEnum)System.Enum.Parse(typeof(WeaponEnum), sugWeapon.gameObject.name));

            Debug.Log("I suggest that the crime was committed in the " + sugRoom + ", by " + sugCharacter + " with the " + sugWeapon);
            //check for round manager
            if (!roundManagerScript)
            {
                roundManagerScript = FindObjectOfType<RoundManager>();
            }
            //set List to have the sugestions in place
            Card[] sug = { sugCharacter, sugWeapon, sugRoom };
            //call suggestion method from round manager passing in the cards
            roundManagerScript.MakeSuggestion(new List<Card>(sug));
        }
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
