using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum CharacterName { Col_Mustard, Prof_Plum, Rev_Green, Mrs_Peacock, Miss_Scarlet, Mrs_White }
public class CharacterCard : Card
{
    public CharacterEnum characterEnum;
    private Sprite cardImage;

    public Sprite CardImage { get => cardImage; }

    private void Start()
    {
        String path = "Danny/CardImages/Characters/" + characterEnum.ToString();
        //print("Loading Image - " + path);
        cardImage = Resources.Load<Sprite>(path);
    }

    public override bool Equals(object obj)
    {
        // If the passed object is null
        if (obj == null)
        {
            return false;
        }
       
         if (obj is CharacterEnum)
        {
            //print("Comparing: " + characterEnum + ", " + ((CharacterEnum)obj));
            return characterEnum == ((CharacterEnum)obj);
        }
        else if (obj is CharacterCard)
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
