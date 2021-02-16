using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHanderScript : MonoBehaviour
{
    [SerializeField] GameObject cursor;
    [SerializeField] PlayerControlScript playerControlScript;
    [SerializeField] PlayerCursorScript playerCursorScript;
    [SerializeField] PlayerSelectionScript playerSelectionScript;

    private void Awake()
    {
        playerControlScript = GetComponent<PlayerControlScript>();
        playerControlScript.Cursor = cursor;
        playerCursorScript = cursor.GetComponent<PlayerCursorScript>();
        playerSelectionScript = GetComponent<PlayerSelectionScript>();
        playerCursorScript.ConnectedPlayerSelection = playerSelectionScript;
    }
    void MoveWithMouse(InputAction.CallbackContext inputAction)
    {
        playerControlScript.MoveWithMouse(inputAction);
    }
}
