using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutBoardTileScript : BoardTileScript
{
    private Room shortcutFrom;
    private Room shortcutTo;
    private RoomScript roomScript;

    public Room ShortcutFrom { get => shortcutFrom; }
    public Room ShortcutTo { get => shortcutTo; }
    public RoomScript RoomScript { get => roomScript;}

    private void Start()
    {
        GetRoomScript();
    }

    public void SetShortcutRooms(Room from, Room to)
    {
        shortcutFrom = from;
        shortcutTo = to;
    }

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
    

    override
    public string ToString()
    {
        return $"{TileType} Tile ({shortcutFrom} ==> {shortcutTo}) located at ({GridPosition.x} : {GridPosition.y})";
    }
}
