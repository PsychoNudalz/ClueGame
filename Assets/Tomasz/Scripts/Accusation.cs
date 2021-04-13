using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accusation : MonoBehaviour
{

    
    public Card currentWeapon;
    public Card currentRoom;
    public Card currentCharacter;

    public void Accuse() {
        if (currentCharacter != null & currentRoom != null & currentWeapon != null)
        {
            Debug.Log("Accuse " + currentWeapon.name + " "+ currentRoom.name + " " + currentCharacter.name);
        }
    }
    public void SetWeapon(Card c) {
        currentWeapon = c;
    }
    public void SetCharacter(Card c)
    {
        currentCharacter = c;
    }
    public void SetRoom(Card c)
    {
        currentRoom = c;
    }
}
