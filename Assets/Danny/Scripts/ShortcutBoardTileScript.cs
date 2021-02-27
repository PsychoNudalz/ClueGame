﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutBoardTileScript : BoardTileScript
{
    Room shortcutFrom;
    Room shortcutTo;

    public Room ShortcutFrom { get => shortcutFrom; }
    public Room ShortcutTo { get => shortcutTo; }

    public void SetShortcutRooms(Room from, Room to)
    {
        shortcutFrom = from;
        shortcutTo = to;
    }
}
