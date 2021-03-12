using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWeaponSlot : MonoBehaviour
{
    WeaponTokenScript weaponInSlot;

    public void AddWeaponToSlot(WeaponTokenScript weapon)
    {
        if (!SlotOccupied())
        {
            weaponInSlot = weapon;
        }
    }

    public WeaponTokenScript RemoveWeaponFromSlot()
    {
        WeaponTokenScript weaponToReturn = weaponInSlot;
        weaponInSlot = null;
        return weaponToReturn;
    }

    public bool SlotOccupied()
    {
        return (weaponInSlot != null);
    }

    public WeaponTokenScript GetWeaponInSlot()
    {
        return weaponInSlot;
    }
}
