using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class for the Shortcut board tile Gameobject
/// </summary>
public class ShortcutBoardTileScript : BoardTileScript
{
    private RoomEnum shortcutFrom;
    private RoomEnum shortcutTo;
    private RoomScript roomScript;
    /// <summary>
    /// Room tile is shortcut from
    /// </summary>
    public RoomEnum ShortcutFrom { get => shortcutFrom; }
    /// <summary>
    /// Room tile is shortcut to
    /// </summary>
    public RoomEnum ShortcutTo { get => shortcutTo; }
    /// <summary>
    /// Roomscript from room tile located in.
    /// </summary>
    public RoomScript RoomScript { get => roomScript;}

    /// <summary>
    /// Assign required variables
    /// </summary>
    private void Start()
    {
        GetRoomScript();
    }

    /// <summary>
    /// Set shortcut rooms
    /// </summary>
    /// <param name="from">Set room shortcut is from</param>
    /// <param name="to">Set room shortcut is to</param>
    public void SetShortcutRooms(RoomEnum from, RoomEnum to)
    {
        shortcutFrom = from;
        shortcutTo = to;
    }
    /// <summary>
    /// Set this tiles Roomscript based upon room located in.
    /// </summary>
    private void GetRoomScript()
    {
        foreach (RoomScript tempRoomScript in GameObject.FindObjectsOfType<RoomScript>())
        {
            if (shortcutFrom.Equals(tempRoomScript.Room))
            {
                roomScript = tempRoomScript;
                break;
            }
        }
    }
    
    /// <summary>
    /// Overidden ToString Method
    /// </summary>
    /// <returns>Description of the tile</returns>
    override
    public string ToString()
    {
        return $"{TileType} Tile ({shortcutFrom} ==> {shortcutTo}) located at ({GridPosition.x} : {GridPosition.y})";
    }
}
