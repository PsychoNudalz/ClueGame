using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotTestScript : MonoBehaviour
{
    WeaponTokenScript[] weapons;
    RoomScript room;

    [SerializeField] Text weaponsInRoom;
    [SerializeField] Button[] daggerButtons;

    private void Start()
    {
        room = FindObjectOfType<RoomScript>();
        weapons = FindObjectsOfType<WeaponTokenScript>();
    }

    private void Update()
    {
        SetWeaponsInRoomText();
    }

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

    public void RemoveWeapon(string weapon)
    {
        foreach (WeaponTokenScript weaponToken in weapons)
        {
            if (weaponToken.WeaponType.ToString().Equals(weapon))
            {
                room.RemoveWeaponFromRoom(weaponToken);
                weaponToken.transform.position = new Vector3(-1, 0, -1);
                break;
            }
        }
    }
}
