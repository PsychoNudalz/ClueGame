using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursorScript : MonoBehaviour
{
    [SerializeField] PlayerSelectionScript connectedPlayerSelection;
    [SerializeField] List<string> tagList;

    public PlayerSelectionScript ConnectedPlayerSelection { get => connectedPlayerSelection; set => connectedPlayerSelection = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (tagList.Contains(other.tag))
        {
            if (other.gameObject.TryGetComponent(out BoardTileScript b))
            {
                connectedPlayerSelection.SelectCurrentTile(b);
            }
        }
    }

}
