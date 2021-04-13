using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    //private GameManager gM;
    private RoundManager rM;
    public CharacterName SelectedChar;
    public WeaponName SelectedWeapon;
    public RoomEnum SelectedName;

    private void Awake()
    {
        if (!rM)
        {
            rM = FindObjectOfType<RoundManager>();
        }
    }
    public void RollDice()
    {
        rM.RollDice();
    }

    public void MoveCursor() { }

    public void SelectTile() { }

    public void MakeSuggestion() { }

    public void SetCharacter() { }

    public void SetWeapon() { }

    public void SetRoom() { }

    public void PassSelected() { }

    public void MakeAccusation() { }

    public void EndTurn() {
        rM.EndTurn();
    }
}
