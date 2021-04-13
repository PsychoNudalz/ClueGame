using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accusation : MonoBehaviour
{

    
    public WeaponCard currentWeapon;
    public RoomCard currentRoom;
    public CharacterCard currentCharacter;

    public void Accuse() {
        if (currentCharacter != null & currentRoom != null & currentWeapon != null)
        {
            Debug.Log("Accuse " + currentWeapon.name + " "+ currentRoom.name + " " + currentCharacter.name);
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
}
