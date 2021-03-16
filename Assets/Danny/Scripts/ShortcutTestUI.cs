using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutTestUI : MonoBehaviour
{
    [SerializeField] private Transform entryButtonTransform;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button shortcutButton;
    private PlayerMasterController player;
    private BoardManager boardManager;
    private Button[] entryButtons;
    private BoardTileScript exitTile;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMasterController>();
        boardManager = FindObjectOfType<BoardManager>();
        entryButtons = entryButtonTransform.GetComponentsInChildren<Button>();
        exitTile = boardManager.GetTileFromGrid(11, 9);
    }

    // Update is called once per frame
    void Update()
    {
        exitButton.interactable = player.IsInRoom();
        shortcutButton.interactable = player.CanTakeShortcut();
        foreach(Button button in entryButtons)
        {
            button.interactable = !player.IsInRoom();
        }
    }

    public void EnterRoom(string roomToEnter)
    {
        foreach(RoomEntryBoardTileScript entryTile in boardManager.RoomEntries)
        {
            if (entryTile.Room.ToString().Equals(roomToEnter)){
                player.MovePlayer(entryTile);
                //print(entryTile);
                break;
            }
        }
    }

    public void TakeShortcut()
    {
        player.TakeShortcut();
    }

    public void ExitRoom()
    {
        player.GetCurrentRoom().RemovePlayerFromRoom(player, exitTile);
    }
}
