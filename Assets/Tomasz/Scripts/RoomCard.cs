using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum RoomEnum { Study, Hall, Lounge, Library, DiningRoom, BillardRoom, BallRoom, Kitchen, Conservatory }
public class RoomCard : Card
{
    public Room room;

    public override bool Equals(object obj)
    {
        //print("Comparing room");
        // If the passed object is null
        if (obj == null)
        {
            return false;
        }
        else if (obj is Room)
        {
            //print("Comparing room: "+room.ToString()+","+ ((Room)obj).ToString());

            return room == ((Room)obj);
        }
        if (obj is RoomCard)
        {
            return this.Equals((obj as RoomCard).room);

        }

        return false;

    }

    public override Enum GetCardType()
    {
        return room;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
