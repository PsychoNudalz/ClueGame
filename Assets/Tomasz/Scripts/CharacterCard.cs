﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum CharacterName { Col_Mustard, Prof_Plum, Rev_Green, Mrs_Peacock, Miss_Scarlet, Mrs_White }
public class CharacterCard : Card
{
    public CharacterEnum characterEnum;

    public override bool Equals(object obj)
    {
        // If the passed object is null
        if (obj == null)
        {
            return false;
        }
        if (!(obj is Card))
        {
            return false;
        }
        else if (obj is CharacterEnum)
        {
            return characterEnum == ((CharacterEnum)obj);
        }
        if (obj is CharacterCard)
        {
            return this.Equals((obj as CharacterCard).characterEnum);

        }

        return false;

    }

    public override Enum GetCardType()
    {
        return characterEnum;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
