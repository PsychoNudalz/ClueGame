using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    MissScarlett, ProfPlum, ColMustard, MrsPeacock, RevGreen, MrsWhite
}

public class PlayerTokenScript : MonoBehaviour
{
    private Character character;
    private Color characterColour;
    private string characterName;
    private StartTileScript startTile;

    public Character Character { get => character; }
    public Color CharacterColour { get => characterColour; set => characterColour = value; }
    public string CharacterName { get => characterName; set => characterName = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCharacter(Character setCharacter, StartTileScript tile)
    {
        startTile = tile;
        switch (setCharacter)
        {
            case Character.MissScarlett:
                character = setCharacter;
                characterColour = new Color(150, 0, 0);
                characterName = "Miss Scarlett";
                break;
            case Character.ColMustard:
                character = setCharacter;
                characterColour = new Color(150, 150, 0);
                characterName = "Col Mustard";
                break;
            case Character.ProfPlum:
                character = setCharacter;
                characterColour = new Color(150, 0, 150);
                characterName = "Prof Plum";
                break;
            case Character.RevGreen:
                character = setCharacter;
                characterColour = new Color(0, 150, 0);
                characterName = "Rev Green";
                break;
            case Character.MrsPeacock:
                character = setCharacter;
                characterColour = new Color(0, 100, 150);
                characterName = "Mrs Peacock";
                break;
            case Character.MrsWhite:
                character = setCharacter;
                characterColour = new Color(150, 150, 150);
                characterName = "Mrs White";
                break;
        }
        /*
         * ----To do---- 
         * token colour from characterColour
            startTile.SetTileColour(CharacterColour);
         */
    }

    public Color GetCharacterColour()
    {
        return characterColour;
    }
}
