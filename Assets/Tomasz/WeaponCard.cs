using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum weaponName { Dagger, Candlestick , Revolver , Rope , LeadPiping , Spanner }
public class WeaponCard : Card
{
    public RoomCard currentRoom = null;
    

    public void setCurrentRoom(RoomCard r) {
        currentRoom = r;
    }

    public RoomCard getCurrentRoom() {
        return currentRoom;
    }

    

}
