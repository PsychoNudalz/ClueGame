using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accusation : MonoBehaviour
{

    
    public WeaponCard currentWeapon;
    public RoomCard currentRoom;
    public CharacterCard currentCharacter;

    [SerializeField] RoundManager roundManager;

    public void Accuse() {
        if (currentCharacter != null & currentRoom != null & currentWeapon != null)
        {
            Debug.Log("Accuse " + currentWeapon.name + " "+ currentRoom.name + " " + currentCharacter.name);
            if (!roundManager)
            {
                roundManager = FindObjectOfType<RoundManager>();
            }
            Card[] acc = { currentCharacter, currentWeapon, currentRoom };
            roundManager.MakeAccusation(new List<Card>(acc));
        }
    }
    public void SetWeapon(WeaponCard c) {
        currentWeapon = c;
    }
    public void SetCharacter(CharacterCard c)
    {
        currentCharacter = c;
    }
    public void SetRoom(RoomCard c)
    {
        currentRoom = c;
    }
    public void Cancel()
    {
        
        currentRoom = null;
        currentCharacter = null;
        currentWeapon = null;
       
    }
}
