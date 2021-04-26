using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for the Room Weapon slot GameObject
/// </summary>
public class RoomWeaponSlot : MonoBehaviour
{
    private WeaponTokenScript weaponInSlot;

    /// <summary>
    /// Add Weapon to the slot if not occupied
    /// </summary>
    /// <param name="weapon">Weapon token to add</param>
    public void AddWeaponToSlot(WeaponTokenScript weapon)
    {
        if (!SlotOccupied())
        {
            weaponInSlot = weapon;
        }
    }
    /// <summary>
    /// Remove weapon from slot
    /// </summary>
    /// <returns>Weapon removed from slot</returns>
    public WeaponTokenScript RemoveWeaponFromSlot()
    {
        WeaponTokenScript weaponToReturn = weaponInSlot;
        weaponInSlot = null;
        return weaponToReturn;
    }
    /// <summary>
    /// I slot currently occupied by a weapon token
    /// </summary>
    /// <returns>true if occupied, false if not</returns>
    public bool SlotOccupied()
    {
        return (weaponInSlot != null);
    }
    /// <summary>
    /// Return the weapon in the slot
    /// </summary>
    /// <returns></returns>
    public WeaponTokenScript GetWeaponInSlot()
    {
        return weaponInSlot;
    }
}
