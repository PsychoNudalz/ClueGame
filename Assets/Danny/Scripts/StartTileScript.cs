using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class for the start tile Gameobject
/// </summary>
public class StartTileScript : BoardTileScript
{
    private CharacterEnum character;
    private Text tileText;
    public CharacterEnum Character { get => character; }
    
    /// <summary>
    /// Set required variables
    /// </summary>
    void Awake()
    {
        tileText = GetComponentInChildren<Text>();
        tileText.text = "START";
    }

    /// <summary>
    /// Format and set the text displayed on the tile
    /// </summary>
    /// <param name="nametext">Name to display on the tile</param>
    private void SetTileText(string nametext)
    {
        string[] names = nametext.Split(' ');
        tileText.text = "Start\n" + names[0] + "\n" + names[1];
    }

    /// <summary>
    /// Set the character for the start tile
    /// </summary>
    /// <param name="characterSet">Character to set on start tile</param>
    /// <param name="name">Name to display on tile</param>
    public void SetCharacter(CharacterEnum characterSet, string name)
    {
        character = characterSet;
        SetTileText(name);
    }

    /// <summary>
    /// Set the tile colour
    /// </summary>
    /// <param name="colourToSet">Colour to set on tile</param>
    public void SetTileColour(Color colourToSet)
    {
        GetComponentInChildren<Renderer>().material.SetColor("Color_2AE3A7FF", colourToSet);
    }

    /// <summary>
    /// Overidden ToString Method
    /// </summary>
    /// <returns>Description of the tile</returns>
    override
    public string ToString()
    {
        return $"{TileType} Tile ({character}) located at ({GridPosition.x} : {GridPosition.y})";
    }
}
