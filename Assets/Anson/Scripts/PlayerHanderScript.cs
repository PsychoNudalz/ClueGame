using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHanderScript : MonoBehaviour
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

    private void Awake()
    {
        playerControlScript = GetComponent<PlayerControlScript>();
        playerControlScript.Cursor = cursor;
        playerCursorScript = cursor.GetComponent<PlayerCursorScript>();
        playerSelectionScript = GetComponent<PlayerSelectionScript>();
        playerCursorScript.ConnectedPlayerSelection = playerSelectionScript;
        playerTokenScript = GetComponent<PlayerTokenScript>();
        playerStatsScript = GetComponent<PlayerStatsScript>();


    }
    void MoveWithMouse(InputAction.CallbackContext inputAction)
    {
        playerControlScript.MoveWithMouse(inputAction);
    }
}
