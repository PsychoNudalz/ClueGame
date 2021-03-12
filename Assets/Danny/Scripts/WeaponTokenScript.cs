using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponEnum {Dagger, CandleStick, Revolver, Rope, LeadPipe, Spanner}

public class WeaponTokenScript : MonoBehaviour
{
    [SerializeField] private WeaponEnum weaponType;
    private RoomScript currentRoom;

    public WeaponEnum WeaponType { get => weaponType;}
    public RoomScript CurrentRoom { get => currentRoom; set => currentRoom = value; }
}
