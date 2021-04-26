using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class to run weapon slot tests in room test scene
/// </summary>
public class WeaponSlotTestScript : MonoBehaviour
{
    WeaponTokenScript[] weapons;
    RoomScript room;
    [SerializeField] Text weaponsInRoom;
    [SerializeField] Button[] daggerButtons;

    /// <summary>
    /// Set required variables
    /// </summary>
    private void Start()
    {
        room = FindObjectOfType<RoomScript>();
        weapons = FindObjectsOfType<WeaponTokenScript>();
    }

    private void Update()
    {
        SetWeaponsInRoomText();
    }
     /// <summary>
     /// Set text to display weapons in room
     /// </summary>
    private void SetWeaponsInRoomText()
    {
        string text = "Weapons in room\n";
        for (int i = 0; i < room.WeaponSlots.Length; i++)
        {
            WeaponTokenScript weapon = room.WeaponSlots[i].GetWeaponInSlot();

            if (weapon != null)
            {
                text += String.Format("Slot {0} : {1}\n", i + 1, weapon.WeaponType);
            }
            else
            {
                text += String.Format("Slot {0} : Empty\n", i + 1);
            }
        }
        weaponsInRoom.text = text;
    }
    /// <summary>
    /// Add a weapon to the room
    /// </summary>
    /// <param name="weapon">Weapon to add to room</param>
    public void AddWeapon(string weapon)
    {
        foreach(WeaponTokenScript weaponToken in weapons)
        {
            if (weaponToken.WeaponType.ToString().Equals(weapon))
            {
                room.AddWeapon(weaponToken);
                break;
            }
        }
    }
    /// <summary>
    /// Remove weapon from the room
    /// </summary>
    /// <param name="weapon">Weapon to remove from the room</param>
    public void RemoveWeapon(string weapon)
    {
        foreach (WeaponTokenScript weaponToken in weapons)
        {
            if (weaponToken.WeaponType.ToString().Equals(weapon))
            {
                room.RemoveWeaponFromRoom(weaponToken);
                weaponToken.MoveToken(new Vector3(-2,0,2));
                break;
            }
        }
    }
}
