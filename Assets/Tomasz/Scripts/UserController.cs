using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    //private GameManager gM;
    private RoundManager rM;
    public CharacterEnum SelectedChar;
    public WeaponEnum SelectedWeapon;
    public Room SelectedName;

    public RoundManager RM { get => rM; }

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

    /// <summary>
    /// Gets the player controller for the current player
    /// </summary>
    /// <returns>player controller for the current player</returns>
    public PlayerMasterController GetCurrentPlayer()
    {
        return rM.GetCurrentPlayer();
    }
}
