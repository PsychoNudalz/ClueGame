using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCard : Card
{
    public CharacterEnum characterEnum;
    private Sprite cardImage;
    /// <summary>
    /// Sets the correct path for cards image
    /// </summary>
    private void Awake()
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
    /// <summary>
    /// Returns the enum of the card
    /// </summary>
    /// <returns>Enum of the card</returns>
    public override Enum GetCardType()
    {
        return characterEnum;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    /// <summary>
    /// Returns cards image 
    /// </summary>
    /// <returns>Cards image</returns>
    public override Sprite GetCardImage()
    {
        return cardImage;
    }
}
