﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//public enum WeaponName { Dagger, Candlestick , Revolver , Rope , LeadPiping , Spanner }
public class WeaponCard : Card
{
    public WeaponEnum weaponEnum;

    public override bool Equals(object obj)
    {
        // If the passed object is null
        if (obj == null)
        {
            return false;
        }
        if (obj is WeaponEnum)
        {
            return weaponEnum == ((WeaponEnum)obj);
        }
        if (obj is WeaponCard)
        {
            return this.Equals((obj as WeaponCard).weaponEnum);

        }

        return false;
        
    }

    public override Enum GetCardType()
    {
        return weaponEnum;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
