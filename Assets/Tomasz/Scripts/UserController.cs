using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    //private GameManager gM;
    private RoundManager rM;
    CardManager cardManager;

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
        if (cardManager == null)
        {
            cardManager = FindObjectOfType<CardManager>();
        }
        //print(FindObjectOfType<Suggestion>());

    }
    /// <summary>
    /// Rolls the dice
    /// </summary>
    public void RollDice()
    {
        rM.RollDice();
    }
    /// <summary>
    /// Takes the shortcut 
    /// </summary>
    public void TakeShortcut()
    {
        GetCurrentPlayer().TakeShortcut();
        rM.CanRoll = false;
    }
    /// <summary>
    /// Selects a tile for player movement
    /// </summary>
    /// <param name="tile"></param>
    public void SelectTile(BoardTileScript tile)
    {
        rM.MovePlayer(tile);
    }
    /// <summary>
    /// Makes suggestion 
    /// </summary>
    /// <returns>True if suggestion is made</returns>
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
    /// <summary>
    /// Sets character for suggestions and accusation
    /// </summary>
    /// <param name="c"></param>
    public void SetCharacter(CharacterEnum c)
    {
        suggestion.SetSugCharacter(cardManager.FindCard(c) as CharacterCard);
        accusation.SetCharacter(cardManager.FindCard(c) as CharacterCard);
    }
    /// <summary>
    /// Sets weapon for suggestions and accusation
    /// </summary>
    /// <param name="c"></param>
    public void SetWeapon(WeaponEnum c)
    {
        suggestion.SetSugWeapon(cardManager.FindCard(c) as WeaponCard);
        accusation.SetWeapon(cardManager.FindCard(c) as WeaponCard);
    }
    /// <summary>
    /// Sets room for suggestions and accusation
    /// </summary>
    /// <param name="c"></param>
    public void SetRoom(Room c)
    {
        suggestion.SetSugRoom(cardManager.FindCard(c) as RoomCard);
        accusation.SetRoom(cardManager.FindCard(c) as RoomCard);
    }

    public void PassSelected() { }
    /// <summary>
    /// Makes accusation
    /// </summary>
    /// <returns>True if accusation is made</returns>
    public bool MakeAccusation()
    {
        List<Card> acc = accusation.Accuse();
        if (acc == null)
        {
            return false;
        }
        else
        {
            rM.MakeAccusation(acc);
            return true;

        }
    }
    /// <summary>
    /// Ends turn
    /// </summary>
    /// <returns></returns>
    public PlayerMasterController EndTurn()
    {
        return rM.EndTurn();
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
