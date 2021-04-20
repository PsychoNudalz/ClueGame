using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    //private GameManager gM;
    private RoundManager rM;

    [SerializeField] Suggestion suggestion;
    [SerializeField] Accusation accusation;

    public RoundManager RM { get => rM; }
    public Suggestion Suggestion { get => suggestion; }

    private void Awake()
    {
        if (!rM)
        {
            rM = FindObjectOfType<RoundManager>();
        }
        if (suggestion == null)
        {
            suggestion = FindObjectOfType<Suggestion>();
        }
        if (accusation == null)
        {
            accusation = FindObjectOfType<Accusation>();
        }
        //print(FindObjectOfType<Suggestion>());
    
    }

    public void RollDice()
    {
        rM.RollDice();
    }

    //public void MoveCursor() { }

    public void SelectTile(BoardTileScript tile)
    {
        rM.MovePlayer(tile);
    }

    public bool MakeSuggestion()
    {
        List<Card> sug = suggestion.Suggest();
        if (sug == null)
        {
            return false;
        }
        else
        {
            rM.MakeSuggestion(sug);
            return true;
        }
    }

    public void SetCharacter() { }

    public void SetWeapon() { }

    public void SetRoom() { }

    public void PassSelected() { }

    public void MakeAccusation() { }

    public void EndTurn()
    {
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
