using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accusation : MonoBehaviour
{


    public WeaponCard currentWeapon;
    public RoomCard currentRoom;
    public CharacterCard currentCharacter;

    [SerializeField] RoundManager roundManager;
    /// <summary>
    /// Passes a list of selected cards
    /// </summary>
    /// <returns>Returns a list of selcted cards for accusing</returns>
    public List<Card> Accuse()
    {
        if (currentCharacter != null & currentRoom != null & currentWeapon != null)
        {
            //Debug.Log("Accuse " + currentWeapon.name + " "+ currentRoom.name + " " + currentCharacter.name);

            Card[] acc = { currentCharacter, currentWeapon, currentRoom };
            return (new List<Card>(acc));
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// Set the current weapon for accusing
    /// </summary>
    /// <param name="c">Selected weapon card</param>
    public void SetWeapon(WeaponCard c)
    {
        currentWeapon = c;
    }
    /// <summary>
    /// Set the current character for accusing
    /// </summary>
    /// <param name="c">Selected character card</param>
    public void SetCharacter(CharacterCard c)
    {
        currentCharacter = c;
    }
    /// <summary>
    /// Set the current room for accusing
    /// </summary>
    /// <param name="c">Selected room card  </param>
    public void SetRoom(RoomCard c)
    {
        currentRoom = c;
    }
    /// <summary>
    /// Clears selected cards
    /// </summary>
    public void Cancel()
    {

        currentRoom = null;
        currentCharacter = null;
        currentWeapon = null;

    }
}
