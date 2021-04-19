using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suggestion : MonoBehaviour
{
    public Card currentWeapon;
    public Card currentRoom;
    public Card currentCharacter;
    [SerializeField] RoundManager roundManager;

    public void Suggest()
    {
        if (currentCharacter != null & currentRoom != null & currentWeapon != null)
        {
            Debug.Log("Accuse " + currentWeapon.name + " " + currentRoom.name + " " + currentCharacter.name);
            if (!roundManager)
            {
                roundManager = FindObjectOfType<RoundManager>();
            }
            Card[] sug = { currentCharacter, currentWeapon, currentRoom };
            //roundManager.MakeSuggestion(new List<Card>(sug));
        }
    }
    public void SetWeapon(Card c)
    {
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
