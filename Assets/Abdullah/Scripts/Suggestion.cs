using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suggestion : MonoBehaviour
{
    public WeaponCard sugWeapon;
    public RoomCard sugRoom;
    public CharacterCard sugCharacter;
    [SerializeField] RoundManager roundManagerScript;
    [SerializeField] TurnController turnControlerScript;

    public void Suggest() {
        if (sugRoom != null & sugWeapon != null & sugCharacter != null) {
            Debug.Log("I suggest that the crime was committed in the" + sugRoom +", by" + sugCharacter +" with the" + sugWeapon);
            if (!roundManagerScript) {
                roundManagerScript = FindObjectOfType<RoundManager>();
            }
            Card[] sug = { sugCharacter, sugWeapon, sugRoom};
            roundManagerScript.MakeSuggestion(new List<Card>(sug));
        }
    }

    public void SetSugWeapon(WeaponCard weaponCard) {
        sugWeapon = weaponCard;
    }
    public void SetSugRoom(RoomCard roomCard)
    {
       string roomToString = turnControlerScript.GetCurrentPlayer().GetCurrentRoom().ToString();
        sugRoom = roomCard;
    }

    public void SetSugCharacter(CharacterCard characterCard)
    {
        sugCharacter = characterCard;
    }

}
