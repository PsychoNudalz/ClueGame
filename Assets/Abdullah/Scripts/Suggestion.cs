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
        if (roundManagerScript == null)
        {
            roundManagerScript = FindObjectOfType<RoundManager>();

        }
    }

    public void Suggest()
    {
        if (sugRoom != null & sugWeapon != null & sugCharacter != null)
        {
            Debug.Log("I suggest that the crime was committed in the " + sugRoom + ", by " + sugCharacter + " with the " + sugWeapon);
            if (!roundManagerScript)
            {
                roundManagerScript = FindObjectOfType<RoundManager>();
            }
            Card[] sug = { sugCharacter, sugWeapon, sugRoom };
            roundManagerScript.MakeSuggestion(new List<Card>(sug));
        }
    }

    public void SetSugWeapon(WeaponCard weaponCard)
    {
        sugWeapon = weaponCard;
        Debug.Log("Weapon Suggested: " + sugWeapon);
    }
    public void SetSugRoom(RoomCard roomCard)
    {
        sugRoom = roomCard;
        Debug.Log("Room Suggested: " + sugRoom);
    }

    public void SetSugCharacter(CharacterCard characterCard)
    {
        sugCharacter = characterCard;
        Debug.Log("Character Suggested: " + sugCharacter);
    }
    public void Cancel()
    {

        sugRoom = null;
        sugCharacter = null;
        sugWeapon = null;

    }
}
