using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// for controlling the cursor for the player to select which tile to move to
/// </summary>
public class UserCursorScript : MonoBehaviour
{
    [SerializeField] UserSelectionScript connectedUserSelection;
    [SerializeField] List<string> tagList;

    public UserSelectionScript ConnectedPlayerSelection { get => connectedUserSelection; set => connectedUserSelection = value; }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (connectedUserSelection == null)
        {
            Debug.LogError("SDFGHJK");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (tagList.Contains(other.tag))
        {
            if (other.gameObject.TryGetComponent(out BoardTileScript b))
            {
                connectedUserSelection.SelectCurrentTile(b);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tagList.Contains(other.tag))
        {
            connectedUserSelection.ClearCurrentTile();

        }
    }



}
