using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMasterController : MonoBehaviour
{


    [Space]
    [Header("Player Components")]
    [SerializeField] PlayerTokenScript playerTokenScript;
    [SerializeField] PlayerStatsScript playerStatsScript;

    [Space]
    [Header("Other Components")]
    [SerializeField] BoardManager boardManager;

    public PlayerTokenScript PlayerTokenScript { get => playerTokenScript; set => playerTokenScript = value; }

    private void Awake()
    {

        playerTokenScript = GetComponent<PlayerTokenScript>();

        playerStatsScript = GetComponent<PlayerStatsScript>();

        boardManager = FindObjectOfType<BoardManager>();


        //test

        PlayerMasterController playerMasterController = FindObjectOfType<PlayerMasterController>();
        print((int )playerMasterController.GetCharacter());

    }

    /// <summary>
    /// Set the player's character
    /// </summary>
    /// <param name="ce">Character Enum</param>
    public void SetCharacter(CharacterEnum ce)
    {
        playerStatsScript.SetCharacter(ce);
    }


    public CharacterEnum GetCharacter()
    {
        return playerStatsScript.Character;
    }

    public Vector2 GetGridPosition()
    {
        return playerTokenScript.GetGridPosition();
    }

    public BoardTileScript GetTile()
    {
        return playerTokenScript.CurrentTile;
    }


    /// <summary>
    /// Add a list of card to the player's deck
    /// </summary>
    /// <param name="cs">list of cards </param>
    /// <returns>if a card from cs is in the deck already</returns>
    public bool AddCard(List<Card> cs)
    {
        return playerStatsScript.AddCard(cs);
    }


    public void StartTurn()
    {
        //DisplayBoardMovableTiles(5);
    }

    public void DisplayBoardMovableTiles(int range)
    {
        if (boardManager == null)
        {
            boardManager = FindObjectOfType<BoardManager>();
        }
        boardManager.ShowMovable(GetTile(),range);

    }

    public bool CanMove(BoardTileScript t)
    {
        return boardManager.CanMove(t);
    }

    public void MovePlayer(BoardTileScript b)
    {
        print("Moving Player");
        playerTokenScript.MoveToken(b);
    }

    public RoomScript GetCurrentRoom()
    {
        return playerTokenScript.CurrentRoom;
    }

    public void MovePlayer(Vector3 v)
    {
        print("Moving Player");
        playerTokenScript.MoveToken(v);
    }
}
