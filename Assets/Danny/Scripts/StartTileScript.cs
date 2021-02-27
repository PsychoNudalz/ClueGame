using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTileScript : BoardTileScript
{
    Character character;
    private Text tileText;
    public Character Character { get => character; }
    
    // Start is called before the first frame update
    void Awake()
    {
        tileText = GetComponentInChildren<Text>();
        tileText.text = "START";
    }

    private void SetTileText(string nametext)
    {
        string[] names = nametext.Split(' ');
        tileText.text = "Start\n" + names[0] + "\n" + names[1];
    }

    public void SetCharacter(Character characterSet, string name)
    {
        character = characterSet;
        SetTileText(name);
    }

    public void SetTileColour(Color colourToSet)
    {
        throw new NotImplementedException("Set colour not implemented");
    }

}
