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
        playerCursorScript = cursor.GetComponent<PlayerCursorScript>();
        playerSelectionScript = GetComponent<PlayerSelectionScript>();
        playerCursorScript.ConnectedPlayerSelection = playerSelectionScript;
        playerTokenScript = GetComponent<PlayerTokenScript>();
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
}
