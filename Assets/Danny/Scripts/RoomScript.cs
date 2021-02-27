using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Room { Study, Hall, Lounge, Library, Centre, DiningRoom, BilliardRoom, Conservatory, Ballroom, Kitchen };
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
}
