using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTokenScript : MonoBehaviour
{
    [Header("Chracter Token Stuff")]
    [SerializeField] private CharacterEnum character;
    [SerializeField] private Color characterColour;
    [SerializeField] private string characterName;
    private StartTileScript startTile;

    [Header("Movement")]
    [SerializeField] Animator animator;
    [SerializeField] AnimationCurve movementGraph;
    [SerializeField] float timeToMove = 2;
    [SerializeField] float timeToDistance = .25f;
    [SerializeField] float startMoveTime;
    [SerializeField] bool isMove = false;
    [SerializeField] BoardTileScript targetTile;


    [Header("Tile")]
    [SerializeField] BoardTileScript currentTile;


    //Getters and Setters
    public CharacterEnum Character { get => character; }
    public Color CharacterColour { get => characterColour; set => characterColour = value; }
    public string CharacterName { get => characterName; set => characterName = value; }
    public BoardTileScript CurrentTile { get => currentTile; set => currentTile = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            UpdateTokenMovement();
        }
    }

    public void SetCharacter(CharacterEnum setCharacter, StartTileScript tile)
    {
        startTile = tile;
        currentTile = tile;
        switch (setCharacter)
        {
            case CharacterEnum.MissScarlett:
                character = setCharacter;
                characterColour = new Color(255, 0, 0);
                characterName = "Miss Scarlett";
                break;
            case CharacterEnum.ColMustard:
                character = setCharacter;
                characterColour = new Color(255, 255, 0);
                characterName = "Col Mustard";
                break;
            case CharacterEnum.ProfPlum:
                character = setCharacter;
                characterColour = new Color(255, 0, 255);
                characterName = "Prof Plum";
                break;
            case CharacterEnum.RevGreen:
                character = setCharacter;
                characterColour = new Color(0, 255, 0);
                characterName = "Rev Green";
                break;
            case CharacterEnum.MrsPeacock:
                character = setCharacter;
                characterColour = new Color(0, 150, 255);
                characterName = "Mrs Peacock";
                break;
            case CharacterEnum.MrsWhite:
                character = setCharacter;
                characterColour = new Color(255, 255, 255);
                characterName = "Mrs White";
                break;
        }
        AssignToPlayerMaster();

        GetComponentInChildren<Renderer>().material.SetColor("_MainColour",characterColour);
        /*
         * ----To do---- 
         * -token colour from characterColour
         * -startTile.SetTileColour(CharacterColour);
         */
    }

    public Color GetCharacterColour()
    {
        return characterColour;
    }

    public Vector2 GetGridPosition()
    {
        return currentTile.GridPosition;
    }
    /// <summary>
    /// assigns token to the correct player
    /// </summary>
    /// <returns> returns true if it can assign to the player, false if it can't find a matching Player </returns>
    bool AssignToPlayerMaster()
    {
        PlayerMasterController[] allPlayer = FindObjectsOfType<PlayerMasterController>();
        foreach (PlayerMasterController p in allPlayer)
        {
            if (character.Equals(p.GetCharacter()))
            {
                p.PlayerTokenScript = this;
                return true;
            }
        }
        return false;
    }


    public void MoveToken(BoardTileScript newTile)
    {

        targetTile = newTile;
        currentTile.SetToken(null);
        targetTile.SetToken(gameObject);
        isMove = true;
        startMoveTime = Time.time;
        timeToMove = (targetTile.GridPosition - currentTile.GridPosition).magnitude * timeToDistance;
        animator.SetTrigger("Lift");
    }

    void UpdateTokenMovement()
    {
        if (Time.time > (startMoveTime + timeToMove))
        {
            currentTile = targetTile;
            transform.position = currentTile.transform.position;
            isMove = false;
            animator.SetTrigger("Place");

        }
        else
        {
            float currentPoint = movementGraph.Evaluate((Time.time- startMoveTime) / timeToMove);
            print(currentPoint);
            transform.position = (currentPoint *(targetTile.transform.position - currentTile.transform.position))+ currentTile.transform.position;
        }
    }
}
