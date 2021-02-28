using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Room {
    Ballroom,
    BilliardRoom,
    Centre,
    Conservatory,
    DiningRoom,
    Hall,
    Kitchen,
    Study,
    Library,
    Lounge
}
    
public class RoomScript : MonoBehaviour
{
    Room room;

    public Room Room { get => room; set => room = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Room GetRoomFromString(string roomString)
    {
        switch (roomString)
        {
            case "Ballroom":
                return Room.Ballroom;
            case "Billiard Room":
                return Room.BilliardRoom;
            case "Centre":
                return Room.Centre;
            case "Conservatory":
                return Room.Conservatory;
            case "Dining Room":
                return Room.DiningRoom;
            case "Hall":
                return Room.Hall;
            case "Kitchen":
                return Room.Kitchen;
            case "Study":
                return Room.Study;
            case "Library":
                return Room.Library;
            case "Lounge":
                return Room.Lounge;
            default:
                return Room.Centre;
        }
    }
}
