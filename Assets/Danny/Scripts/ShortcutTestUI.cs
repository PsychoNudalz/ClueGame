using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class to run the shortcut test scene
/// </summary>
public class ShortcutTestUI : MonoBehaviour
{
    [SerializeField] private Transform entryButtonTransform;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button shortcutButton;
    private PlayerMasterController player;
    private BoardManager boardManager;
    private Button[] entryButtons;
    private BoardTileScript exitTile;

    /// <summary>
    /// Assign required variables
    /// </summary>
    void Start()
    {
        player = FindObjectOfType<PlayerMasterController>();
        boardManager = FindObjectOfType<BoardManager>();
        entryButtons = entryButtonTransform.GetComponentsInChildren<Button>();
        exitTile = boardManager.GetTileFromGrid(11, 9);
    }

    /// <summary>
    /// Set button interactables
    /// </summary>
    void Update()
    {
        exitButton.interactable = player.IsInRoom();
        shortcutButton.interactable = player.CanTakeShortcut();
        foreach(Button button in entryButtons)
        {
            button.interactable = !player.IsInRoom();
        }
    }

    /// <summary>
    /// Enter a room
    /// </summary>
    /// <param name="roomToEnter">Room to enter</param>
    public void EnterRoom(string roomToEnter)
    {
        foreach(RoomEntryBoardTileScript entryTile in boardManager.RoomEntries)
        {
            if (entryTile.Room.ToString().Equals(roomToEnter)){
                player.MovePlayer(entryTile);
                break;
            }
        }
    }

    /// <summary>
    /// Trigger take shortcut
    /// </summary>
    public void TakeShortcut()
    {
        player.TakeShortcut();
    }

    /// <summary>
    /// Trigger player to exit room
    /// </summary>
    public void ExitRoom()
    {
        player.GetCurrentRoom().RemovePlayerFromRoom(player, exitTile);
    }
}
