using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMasterController : MonoBehaviour
{

    [SerializeField] GameObject cursor;

    [Space]
    [Header("Player Components")]
    [SerializeField] PlayerControlScript playerControlScript;
    [SerializeField] PlayerCursorScript playerCursorScript;
    [SerializeField] PlayerSelectionScript playerSelectionScript;
    [SerializeField] PlayerTokenScript playerTokenScript;
    [SerializeField] PlayerStatsScript playerStatsScript;

    [Space]
    [Header("Other Components")]
    [SerializeField] BoardManager boardManager;

    public PlayerSelectionScript PlayerSelectionScript { get => playerSelectionScript; set => playerSelectionScript = value; }
    public PlayerTokenScript PlayerTokenScript { get => playerTokenScript; set => playerTokenScript = value; }

    private void Awake()
    {
        playerControlScript = GetComponent<PlayerControlScript>();
        playerControlScript.Cursor = cursor;
        playerControlScript.PlayerMasterController = this;


        playerTokenScript = GetComponent<PlayerTokenScript>();

        playerSelectionScript = GetComponent<PlayerSelectionScript>();
        playerSelectionScript.PlayerMasterController = this;
        playerSelectionScript.PlayerTokenScript = playerTokenScript;

        playerCursorScript = cursor.GetComponent<PlayerCursorScript>();
        playerCursorScript.ConnectedPlayerSelection = playerSelectionScript;

        playerStatsScript = GetComponent<PlayerStatsScript>();

        boardManager = FindObjectOfType<BoardManager>();

    }
    void MoveWithMouse(InputAction.CallbackContext inputAction)
    {
        playerControlScript.MoveWithMouse(inputAction);
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
        DisplayBoardMovableTiles(5);
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

    public void MovePlayer()
    {
        print("Moving Player");
        playerSelectionScript.MovePlayer();
    }
}
